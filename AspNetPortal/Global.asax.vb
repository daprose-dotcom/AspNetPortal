Imports System.Web.Mvc
Imports System.Web.Optimization

Public Class Global_asax

    Inherits HttpApplication

    Public Shared constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Public Shared APP_Categories As New Hashtable
    Public Shared APP_Forums As New Hashtable

    Sub Application_Start(sender As Object, e As EventArgs)

        Application.Lock()

        '## YOU CAN NOT AND SHOULD NOT MODIFY THE FOLLOWING LINES 
        '## DO NOT CHANGE FROM HERE ****************************************

        Application.Add("LANGUAGE", "en")              '## Do Not Edit This 
        Application.Add("SESSION_LCID", 1033)              '## Do Not Edit This
        Application.Add("SESSION_TIMEOUT", 30)                '## Do Not Edit This
        Application.Add("SESSION_RESPONSE_BUFFER", True)              '## Do Not Edit This
        Application.Add("SESSION_CODEPAGE", "ISO-8859-1")              '## Do Not Edit This
        Application.Add("SESSION_CHARSET", "utf-8")    '## Do Not Edit This

        Application.Add("APP_VERSION", "1.1.1")
        Application.Add("APP_VISITORS", 0)

        Application.Add("APP_ROOT", Server.MapPath("/"))
        Application.Add("APP_UPLOADS", Server.MapPath("uploads/"))

        '## DO NOT CHANGE TO HERE ****************************************

        '## YOU CAN CERTAINLY AND SHOULD MODIFY THE FOLLOWING LINES 
        '## CHANGE FROM HERE ****************************************

        Application.Add("SITE_NAME", "AspNetPortal.XYZ")
        Application.Add("SITE_DESCRIPTION", "Asp.NET Portal. Fast and complete ASP.NET website startup kit with (CMS)")
        Application.Add("SITE_KEYWORDS", "asp.net,vb,template,startup kit,website template,source code")
        Application.Add("SITE_EMAIL", "support@AspNetPortal.XYZ")
        Application.Add("SITE_LOGO", "images/AspNetPortal-Logo.png")
        Application.Add("SITE_LOGO_NAME", "images/AspNetPortal-Logo-Name.png")

        Application.Add("SITE_POWERED_LOGO", "images/powered_by_AspNetPortal.png")

        Application.Add("SHOW_ADDTHIS", True)
        Application.Add("SITE_ADDTHIS_RA", "ra-622ef23aca69ad06")

        Application.Add("SHOW_FOOTER_WIDGET", True)

        '#################################################################################
        '## VERIFY NEW USER REGISTRATIONS BY EMAIL?  1 = YES, 0 = NO 
        '#################################################################################

        Application.Add("VERIFY_BY_EMAIL", True)

        '#################################################################################
        '## SELECT your DATABASE TYPE 
        '#################################################################################
        Application.Add("APP_DB_TYPE", "sqlserver")
        'Application("APP_DB_TYPE") = "access"
        'Application("APP_DB_TYPE") = "mysql"

        '#################################################################################
        '## SELECT your DATABASE CONNECTION STRING (access, sqlserver or mysql)
        '#################################################################################
        '## Make sure to uncomment one of the Application("CSTRING") lines and edit it so that it points to where your database is!

        'Application("CSTRING") = "Provider=SQLOLEDB;Data Source=SERVER_NAME;database=DB_NAME;uid=UID;pwd=PWD;"                           '## MS SQL Server 6.x/7.x/2000 (OLEDB connection)
        'Application("CSTRING") = "Driver={SQL Server};server=SERVER_NAME_OR_IP;uid=USER_NAME;pwd=USER_PASSWORD;database=DATABASE_NAME"                               '## MS SQL Server 6.x/7.x/2000 (ODBC connection)
        'Application("CSTRING") = "Provider=SQLNCLI;server=SERVER_NAME;database=DB_NAME;uid=UID;pwd=PWD;"                                 '## MS SQL Server 2005 w/ SQL Native Client
        'Application("CSTRING") = "DSN_NAME" 

        '#################################################################################
        '## USE A TABLE PREFIX to create the table names. 
        '## If you are going to run multiple portals from the same database, 
        '## change the table prefixes accordingly so there is no conflict between portals.
        '#################################################################################

        Application.Add("APP_TABLE_PREFIX", "MC1_")     'used in case multiple sites on same database
        Application.Add("APP_MEMBER_TABLE_PREFIX", "MC1_")  'used in case multiple sites on same database
        Application.Add("APP_FILTER_TABLE_PREFIX", "MC1_")  'used for BADWORDS and NAMEFILTER tables

        '#################################################################################
        '## Application("SESSION_COOKIE_DURATION") is the amount of days before the cookie expires
        '## You can set it to a higher value
        '## For example for one year you can set it to 365
        '## (default is 30 days)
        '#################################################################################

        Application.Add("SESSION_COOKIE_DURATION", 30)

        '#################################################################################
        '## Application("SESSION_UNIQUE_ID") is used in creating cookies and other related tasks. It should be
        '## a unique identifier per portal. More often than not, you won't need to change
        '## this unless you are trying to run more than one portal or subdomains on the same domain.
        '#################################################################################

        Application.Add("SESSION_UNIQUE_ID", "AspNetPortal00")

        '#################################################################################
        '## Application("APP_IP_LOOKUP") is the address of the service you want to use for looking up IP
        '## addresses. It assumes the IP address is going to be appended on the end of the
        '## URL. If this isn't the case then you will need to make some changes in 
        '## pop_viewip.asp, inc_profile.asp (x2), admin_accounts_pending.asp, and 
        '## admin_member_search.asp (x2)
        '#################################################################################

        Application.Add("APP_IP_LOOKUP", "https://www.whatismyip.com/ip-address-lookup?s=")
        Application.Add("SESSION_CAPTCHA", "CAPTCHA")

        '## TO HERE ****************************************

        '## Create two HashTables

        Application.UnLock()

        ' Fires when the application is started
        AreaRegistration.RegisterAllAreas()
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        StaticCache.LoadStaticCache()

    End Sub

    Sub Session_Start(sender As Object, e As EventArgs)

        On Error Resume Next

        Dim strUserIP As String
        Dim strUserAgent As String

        'Response.Cookies(Application("SESSION_UNIQUE_ID"))("test") = "test"

        strUserIP = Request.ServerVariables("REMOTE_ADDR")
        strUserAgent = Request.ServerVariables("HTTP_USER_AGENT")

        If strUserIP.Length = 0 Or Trim(strUserAgent) = "" Then
            Response.Write("<h2>403.8 - Site access denied due to invalid credentials.</h2>")
            Response.Write("<hr>")
            Response.Write("<p>You are not authorized to access this directory or view this web page while using your current credentials.</p>")
            Response.Write("<p>Please <b>turn on IP discovery</b>, enable <b>Popup Windows</b>, <b>JavaScripts</b> and <b>Cookies</b> to access this website.</p>")
            Response.Write("<p>Ref: 1. " & Now.ToString & "</p>")
            Session.Timeout = 1
            Response.End()
        End If

        '## THESE SESSION VARIABLES ARE RESET IF SESSION EXPIRES
        Session.Add("UserIP", strUserIP)
        Session.Add("LoginFails", 0)
        Session.Add("Visits", 0)
        Session.Add("IPOK", False)
        Session.Add("UserID", 0)
        Session.Add("UserName", "")
        Session.Add("IsAdmin", False)
        Session.Add("Start", Now())

        Application.Lock()
        Application("APP_VISITORS") = Application("APP_VISITORS") + 1
        Application.UnLock()

        Response.Cookies(Application("SESSION_UNIQUE_ID"))("test") = "test"

    End Sub
End Class