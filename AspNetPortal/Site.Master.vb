Imports Microsoft.AspNet.Identity
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.Page
Imports System.Collections
Imports System.Reflection.Emit
Imports System.Runtime.Serialization
Imports System.Web.UI.Timer
Imports System.Web.Security
Public Class SiteMaster

    Inherits MasterPage

    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String
    Public strPage As String

    Public constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Public APP_Categories As DropDownList = New DropDownList
    Public APP_Forums As DropDownList = New DropDownList
    Protected Sub Page_Init(sender As Object, e As EventArgs)

        ' The code below helps to protect against XSRF attacks
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                 .HttpOnly = True,
                 .Value = _antiXsrfTokenValue
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad

    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)
        Else
            ' Validate the Anti-XSRF token
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        On Error Resume Next

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        Dim strUserIP = Request.ServerVariables("REMOTE_ADDR")
        Dim strUserAgent = Replace(Server.HtmlEncode(Request.ServerVariables("HTTP_USER_AGENT")), "'", "''")
        Dim strPage = Replace(Server.HtmlEncode(Request.ServerVariables("SCRIPT_NAME")), "'", "''")
        Dim strQuery = Replace(Server.HtmlEncode(Request.ServerVariables("QUERY_STRING")), "'", "''")

        Dim intRowsAffected As Integer

        Dim strINSERT As String = ""
        Dim strDELETE As String = ""
        Dim strUPDATE As String = ""
        Dim strSELECT As String = ""
        Dim strCatName As String = ""
        Dim strForumName As String = ""
        Dim Ip4 As New DropDownList

        If Not IsPostBack Then

            ' On Error Resume Next

            '## FIRST, WE DO NOT ALLOW HIDEN IPs OR NO USER AGENT
            If Trim(strUserIP) = "" Or Trim(strUserAgent) = "" Then

                Response.Write("<h2>403.8 - Site access denied due to invalid credentials.</h2>")
                Response.Write("<hr>")
                Response.Write("<p>You are not authorized to access this directory or view this web page while using your current credentials.</p>")
                Response.Write("<p>Please <b>turn on IP discovery</b> and <b>enable POPUP WINDOWS, JAVA and COOKIES</b> to access this website.</p>")
                Response.Write("<p>Ref: 2. " & Now.ToString & "</p>")

                Session.Timeout = 1
                Response.End()

            End If

            'SECOND, LOG VISITOR
            'AND BAN ALL SUCKERS
            If InStr(strUserIP, "104.11.112.") = 0 And InStr(strUserIP, "127.0.0.") = 0 And InStr(strUserIP, "192.168.1.") = 0 And InStr(strUserIP, "::1") = 0 Then
                Using con As New SqlConnection(constr)
                    con.Open()

                    'Log visitor regardless
                    If Trim(strQuery) = "" Then
                        strINSERT = "INSERT INTO [AspNetPortal].[dbo].[UsersLog] ([UserIP],[UserScript]) VALUES ('" & strUserIP & "','" & strPage & "')"
                    Else
                        strINSERT = "INSERT INTO [AspNetPortal].[dbo].[UsersLog] ([UserIP],[UserScript]) VALUES ('" & strUserIP & "','" & strPage & "?" & strQuery & "')"
                    End If
                    Using cmd As New SqlCommand(strINSERT)
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        intRowsAffected = cmd.ExecuteNonQuery()
                    End Using

                    strINSERT = "INSERT INTO [AspNetPortal].[dbo].[UsersAgent] ([UserIP],[UserAgent]) VALUES ('" & strUserIP & "','" & strUserAgent & "')"
                    Using cmd As New SqlCommand(strINSERT)
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        intRowsAffected = cmd.ExecuteNonQuery()
                    End Using

                    '## ANTI-FLOOD, ANTI-HACKER, ANTI-IRRATIONAL BOT CODE
                    Session("Visits") = Session("Visits") + 1
                    Dim intInterval = DateDiff("s", Session("Start"), Now())
                    '## BAN ALL IRRATIONAL/STUPID BOTS
                    If (Session("Visits") > 10 And Session("Visits") > intInterval) _
                        Or Session("Visits") > 200 _
                        Or Session("LoginFails") > 20 _
                        Or strQuery.Contains("wp-login") _
                        Or strQuery.Contains("wp-includes") _
                        Or strQuery.Contains("wp-admin") _
                        Then
                        Ip4.Items.Clear()
                        strSELECT = "SELECT [ID],[IP] FROM [AspNetPortal].[dbo].[BannedIP] WHERE [IP] = '" & strUserIP & "'"
                        Using cmd As New SqlCommand(strSELECT)
                            cmd.CommandType = CommandType.Text
                            cmd.Connection = con
                            Ip4.DataSource = cmd.ExecuteReader()
                            Ip4.DataTextField = "IP"
                            Ip4.DataValueField = "ID"
                            Ip4.DataBind()
                        End Using

                        If Ip4.Items.Count > 0 Then
                            strUPDATE = "UPDATE [AspNetPortal].[dbo].[BannedIP] SET [Tries] = ([Tries] + 1) WHERE [ID] = " & Ip4.Items(0).Text
                            Using cmd As New SqlCommand(strUPDATE)
                                cmd.CommandType = CommandType.Text
                                cmd.Connection = con
                                intRowsAffected = cmd.ExecuteNonQuery()
                            End Using
                        Else
                            strINSERT = "INSERT INTO [AspNetPortal].[dbo].[BannedIP] ([IP]) VALUES ('" & strUserIP & "')"
                            Using cmd As New SqlCommand(strINSERT)
                                cmd.CommandType = CommandType.Text
                                cmd.Connection = con
                                intRowsAffected = cmd.ExecuteNonQuery()
                            End Using
                            Session("IPOK") = False
                        End If

                        If Ip4.Items.Count > 0 Then
                            Ip4.Items.Clear()
                            Ip4 = Nothing
                            Session("IPOK") = False
                        End If
                    End If

                    con.Close()

                End Using

            End If

            If Session("IPOK") <> True Then

                If IsUserIPBanned(strUserIP) = True Then

                    Response.Write("<h2>403.8 - Site access denied due to invalid credentials.</h2>")
                    Response.Write("<hr>")
                    Response.Write("<p>You are not authorized to access this directory or view this web page while using your current credentials.</p>")
                    Response.Write("<p>Please <b>turn on IP discovery</b> and <b>enable POPUP WINDOWS, JAVA and COOKIES</b> to access this website.</p>")
                    Response.Write("<p>Ref: 3. " & Now.ToString & "</p>")
                    Session.Timeout = 1
                    Response.End()

                End If

            End If

            '## Anti-ForumPoster, crawlers, bots and stupid people code
            Dim isSpambot : isSpambot = 0

            If InStr(strUserAgent, "forum poster") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "fp.icontool.com") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "icontool") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Wappalyzer") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "ThinkChaos") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Expanse") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "mojeek") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "MojeekBot") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Klondike") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Python") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "aiohttp") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Sogou") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Spider") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "ALittle") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Mail.RU") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "crawler") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Baiduspider") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "YandexBot") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Konqueror") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Exabot") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "ia_archiver") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Slurp") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Python-urllib") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "AhrefsBot") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "MJ12bot") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "GRequests") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "python") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "Adsbot") > 0 Then isSpambot = 1
            If InStr(strUserAgent, "DataForSeoBot") > 0 Then isSpambot = 1

            If isSpambot = 1 Then

                Response.Write("<h2>403.8 - Site access denied due to invalid credentials.</h2>")
                Response.Write("<hr>")
                Response.Write("<p>You are not authorized to access this directory or view this web page while using your current credentials.</p>")
                Response.Write("<p>Please <b>turn on IP discovery</b> and <b>enable POPUP WINDOWS, JAVA and COOKIES</b> to access this website.</p>")
                Response.Write("<p>Ref: 4. " & Now.ToString & "</p>")
                Session.Timeout = 1
                Response.End()

            End If
            'End Anti-ForumPoster Code

        Else

            '## SECOND, WE RECORD ALL VISITORS AND BAN ALL SUCKERS
            If InStr(strUserIP, "104.11.112.") = 0 And InStr(strUserIP, "127.0.0.") = 0 And InStr(strUserIP, "192.168.1.") = 0 And InStr(strUserIP, "::1") = 0 Then

                '## We display the main categories
                Using con As New SqlConnection(constr)

                    con.Open()

                    '## We record all visitors regardless
                    If Trim(strQuery) = "" Then
                        strINSERT = "INSERT INTO [AspNetPortal].[dbo].[UsersLog] ([UserIP],[UserScript]) VALUES ('" & strUserIP & "','" & strPage & "')"
                    Else
                        strINSERT = "INSERT INTO [AspNetPortal].[dbo].[UsersLog] ([UserIP],[UserScript]) VALUES ('" & strUserIP & "','" & strPage & "?" & strQuery & "')"
                    End If
                    Using cmd As New SqlCommand(strINSERT)
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        intRowsAffected = cmd.ExecuteNonQuery()
                    End Using
                    con.Close()
                End Using
            End If
        End If

        If Session("IPOK") <> True Then

            If IsUserIPBanned(strUserIP) = True Or IsUserAgentBanned(strUserAgent) = True Then

                Response.Write("<h2>403.8 - Site access denied due to invalid credentials.</h2>")
                Response.Write("<hr>")
                Response.Write("<p>You are not authorized to access this directory or view this web page while using your current credentials.</p>")
                Response.Write("<p>Please <b>turn on IP discovery</b> and <b>enable POPUP WINDOWS, JAVA and COOKIES</b> to access this website.</p>")
                Response.Write("<p>Ref: 5. " & Now.ToString & "</p>")
                Session.Timeout = 1
                Response.End()

            End If
        End If

    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
    End Sub

    Protected Function IsUserAgentBanned(strUserAgent As String) As Boolean

        On Error Resume Next
        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim strSELECT As String
        Dim strUPDATE As String
        Dim strINSERT As String
        Dim intRowsAffected As Integer

        IsUserAgentBanned = False

        Dim Ip4 As New DropDownList
        Ip4.Items.Clear()

        Using con As New SqlConnection(constr)
            con.Open()

            If strUserAgent.Length > 0 And strUserAgent.Length < 255 Then

                ' Make sure Agent exists
                ' If not?  INSERT

                Ip4.Items.Clear()
                strSELECT = "SELECT [ID], [BrowserName] FROM [AspNetPortal].[dbo].[BannedAgent] WHERE [BrowserName] = '" & strUserAgent & "'"
                Using cmd As New SqlCommand(strSELECT)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    Ip4.DataSource = cmd.ExecuteReader()
                    Ip4.DataTextField = "BrowserName"
                    Ip4.DataValueField = "ID"
                    Ip4.DataBind()
                    cmd.Dispose()
                End Using

                If Ip4.Items.Count = 0 Then
                    strINSERT = "INSERT INTO [AspNetPortal].[dbo].[BannedAgent] ([BrowserName]) VALUES ('" & strUserAgent & "')"
                    Using cmd As New SqlCommand(strINSERT)
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        intRowsAffected = cmd.ExecuteNonQuery()
                        cmd.Dispose()
                    End Using
                End If

                ' ============================================
                ' check if Banned
                ' ============================================
                Ip4.Items.Clear()
                strSELECT = "SELECT [ID], [BrowserName] FROM [AspNetPortal].[dbo].[BannedAgent] WHERE [BrowserName] = '" & strUserAgent & "' AND [IsBanned] = 1"
                Using cmd As New SqlCommand(strSELECT)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    Ip4.DataSource = cmd.ExecuteReader()
                    Ip4.DataTextField = "BrowserName"
                    Ip4.DataValueField = "ID"
                    Ip4.DataBind()
                    cmd.Dispose()
                End Using

                If Ip4.Items.Count > 0 Then
                    strUPDATE = "UPDATE [AspNetPortal].[dbo].[BannedAgent] SET [Tries] = ([Tries] + 1) WHERE [ID] = " & Ip4.Items(0).Text
                    Using cmd As New SqlCommand(strUPDATE)
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        intRowsAffected = cmd.ExecuteNonQuery()
                        cmd.Dispose()
                    End Using

                    Ip4.Items.Clear()
                    con.Close()
                    Return True

                End If

            Else

                Ip4.Items.Clear()
                con.Close()
                Return True

            End If

            con.Close()

        End Using

        IsUserAgentBanned = False

    End Function

    Protected Function IsUserIPBanned(strUserIP As String) As Boolean

        On Error Resume Next

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim strUserAgent = Request.ServerVariables("HTTP_USER_AGENT")

        Dim strSELECT As String
        Dim strUPDATE As String
        Dim strINSERT As String

        Dim intRowsAffected As Integer

        IsUserIPBanned = True

        ' make sure we have a valid IP address and not just a local IP ::1
        If InStr(strUserIP, "104.11.112.") = 0 And InStr(strUserIP, "127.0.0.1") = 0 And InStr(strUserIP, "192.168.1.") = 0 And InStr(strUserIP, "::1") = 0 Then

            Dim Ip4 As New DropDownList
            Ip4.Items.Clear()

            Using con As New SqlConnection(constr)

                con.Open()

                '============================================
                ' lets check the IP
                ' ================================================
                Ip4.Items.Clear()
                strSELECT = "SELECT [ID], [IP] FROM [AspNetPortal].[dbo].[BannedIP] WHERE [IP] = '" & strUserIP & "'"
                Using cmd As New SqlCommand(strSELECT)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    Ip4.DataSource = cmd.ExecuteReader()
                    Ip4.DataTextField = "IP"
                    Ip4.DataValueField = "ID"
                    Ip4.DataBind()
                End Using

                If Ip4.Items.Count > 0 Then
                    strUPDATE = "UPDATE [AspNetPortal].[dbo].[BannedIP] SET [Tries] = ([Tries] + 1) WHERE [ID] = " & Ip4.Items(0).Text
                    Using cmd As New SqlCommand(strUPDATE)
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        intRowsAffected = cmd.ExecuteNonQuery()
                    End Using
                End If

                If Ip4.Items.Count > 0 Then
                    Ip4.Items.Clear()
                    con.Close()
                    Return True
                End If

                '============================================
                ' passed IP ban check, lets check the countries
                ' CHECK NETWORK BAN
                ' ================================================

                ' We borrow the IP temporarily,
                ' We do not touch the contents of strUserIP
                Ip4.Items.Clear()
                Dim strNetban = strUserIP
                Do While Right(strNetban, 1) <> "." And strNetban <> ""
                    Do While Right(strNetban, 1) <> "." And strNetban <> ""
                        strNetban = Left(strNetban, Len(strNetban) - 1)
                    Loop

                    If strNetban <> "" Then

                        Ip4.Items.Clear()
                        strSELECT = "SELECT [ID],[IP] FROM [AspNetPortal].[dbo].[BannedIP] WHERE [IP] = '" & strNetban & "'"
                        Using cmd As New SqlCommand(strSELECT)
                            cmd.CommandType = CommandType.Text
                            cmd.Connection = con
                            Ip4.DataSource = cmd.ExecuteReader()
                            Ip4.DataTextField = "IP"
                            Ip4.DataValueField = "ID"
                            Ip4.DataBind()
                        End Using

                        If Ip4.Items.Count > 0 Then
                            strUPDATE = "UPDATE [AspNetPortal].[dbo].[BannedIP] SET [Tries] = ([Tries] + 1) WHERE [ID] = " & Ip4.Items(0).Text
                            Using cmd As New SqlCommand(strUPDATE)
                                cmd.CommandType = CommandType.Text
                                cmd.Connection = con
                                intRowsAffected = cmd.ExecuteNonQuery()
                            End Using
                        End If

                        If Ip4.Items.Count > 0 Then
                            Ip4.Items.Clear()
                            Ip4 = Nothing
                            con.Close()
                            Return True
                        End If

                    End If

                    If strNetban <> "" Then
                        strNetban = Left(strNetban, Len(strNetban) - 1)
                    End If

                Loop

                '============================================
                ' passed NETWORK ban check, lets check the countries
                '============================================

                Dim g_ipno As Long = Dot2LongIP(strUserIP)
                If g_ipno > 0 Then

                    ' ================================================
                    ' open database #2 (** EDIT THIS IF NECESSARY ** )
                    ' ================================================

                    Ip4.Items.Clear()
                    strSELECT = "SELECT [ID],[country3] FROM [AspNetPortal].[dbo].[IpToCountry] WHERE " & g_ipno & " BETWEEN [begin_num] AND [end_num]"
                    Using cmd As New SqlCommand(strSELECT)
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        Ip4.DataSource = cmd.ExecuteReader()
                        Ip4.DataTextField = "country3"
                        Ip4.DataValueField = "ID"
                        Ip4.DataBind()
                    End Using

                    If Ip4.Items.Count > 0 Then

                        ' is it a banned g_country?
                        strSELECT = "SELECT [id],[Country3] FROM [AspNetPortal].[dbo].[Countries] WHERE [country3] = '" & Ip4.Items(0).Value & "' And [IsBanned] = 1"
                        Ip4.Items.Clear()
                        Using cmd As New SqlCommand(strSELECT)
                            cmd.CommandType = CommandType.Text
                            cmd.Connection = con
                            Ip4.DataSource = cmd.ExecuteReader()
                            Ip4.DataTextField = "country3"
                            Ip4.DataValueField = "ID"
                            Ip4.DataBind()
                        End Using

                        If Ip4.Items.Count > 0 Then
                            Ip4.Items.Clear()
                            Ip4 = Nothing
                            con.Close()
                            Return True
                        End If

                    End If

                End If

                If Ip4.Items.Count > 0 Then
                    Ip4.Items.Clear()
                    Ip4 = Nothing
                    con.Close()
                    Return True
                End If

                con.Close()

            End Using

        End If

        ' Success:
        ' Not Banned, IP3 Not Banned, Country NOT BANNED
        ' We now continue with a regular session.

        'Uncomment the next two lines for testing.
        'Response.Write ("passed")
        'responde.end

        Session("IPOK") = True
        IsUserIPBanned = False

    End Function

    Protected Function Dot2LongIP(DottedIP As String) As Long

        Dim i7, pos As Integer
        Dim PrevPos, num As Integer

        Dot2LongIP = 0
        If DottedIP = "" Or InStr(DottedIP, ":") > 0 Then
            Dot2LongIP = 0
        Else
            For i7 = 1 To 4
                pos = InStr(PrevPos + 1, DottedIP, ".", 1)
                If i7 = 4 Then
                    pos = Len(DottedIP) + 1
                End If
                num = Int(Mid(DottedIP, PrevPos + 1, pos - PrevPos - 1))
                PrevPos = pos
                Dot2LongIP = ((num Mod 256) * (256 ^ (4 - i7))) + Dot2LongIP
            Next
        End If

    End Function

    '## FUNCTIONS USED BY THIS APPLICATION *******************
    Function IIf(condition, value1, value2)
        If condition Then IIf = value1 Else IIf = value2
    End Function

    '**************************************
    ' Description:Collection of functions to deal with strings, 
    ' including to capitalize, format, compare, parse links, check arrays and others. 
    ' This is an added value to any existing string library. 
    ' If you have comments or suggestions please do. 
    ' Let me know this has been useful to you by voting for me or linking back to my website.
    '**************************************

    ' short replacement for
    ' Response.Write just use:
    ' w("Hello World!")

    Public Sub w(str As String)
        Response.Write(str)
    End Sub

    Public Sub WriteLn(str As String)
        Response.Write(str & vbCrLf)
    End Sub

    ' uppercase first words of a string

    Public Function Capitalize(str As String) As String

        Dim arrTemp() As String
        Dim strTemp As String = ""
        Dim i As Integer

        arrTemp = Split(str, " ")
        For i = 0 To UBound(arrTemp)
            strTemp = strTemp & " " & UCase(Left(arrTemp(i), 1)) & LCase(Mid(arrTemp(i), 2))
        Next
        Capitalize = strTemp
    End Function
    '
    ' uppercase first letter in all words in a string
    Public Function CapitalizeAllWords(ByVal str)
        Dim arrTemp() As String
        Dim strTemp As String = ""
        Dim i As Integer
        arrTemp = Split(str, " ")
        For i = 0 To UBound(arrTemp)
            strTemp = strTemp & " " & UCase(Left(arrTemp(i), 1)) & LCase(Mid(arrTemp(i), 2))
        Next
        CapitalizeAllWords = strTemp
    End Function

    ' uppercase first letter of a string
    Public Function CapFirstWord(str)
        CapFirstWord = UCase(Left(str, 1)) & LCase(Mid(str, 2))
    End Function
    '
    ' returns true if the variable
    ' has any part of the string. i.e.: 
    ' IsExpr("in god name", "in name")
    ' returns True
    Public Function IsExpr(patrn As String, strng As String) As Boolean
        Dim myregEx, retVal               ' Create variable.
        myregEx = New Regex(patrn)            ' Create regular expression.
        retVal = myregEx.isMatch(strng)      ' Execute the search test.
        If retVal Then IsExpr = True Else IsExpr = False
    End Function
    '
    ' it bolds any strings[strToBold]
    ' found on another string[strText].
    ' Useful when implementing a
    ' search engine

    Public Function BoldFoundString(strText As String, strToBold As String)
        Dim strTemp
        strTemp = Replace(strText, strToBold, "<b>" & strToBold & "</b>")
        BoldFoundString = strTemp
    End Function
    '
    ' clear quotes from strings

    Public Function ClearQuotes(str As String)
        If Not IsNothing(str) And str <> "" Then
            ClearQuotes = Replace(str, "'", " ")
        Else
            ClearQuotes = str
        End If
    End Function

    Public Function isThisEmailValid(email As String) As Boolean
        Dim myRegEx
        Dim pattern As String = "^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w{2,}$"
        myRegEx = New Regex(pattern)
        isThisEmailValid = myRegEx.isMatch(Trim(email))
    End Function

    Public Function MyDate(strDate As String) As String

        Dim strMyDate = CDate(strDate)
        Dim strYear = Right("0000" & LTrim(Year(strMyDate)), 4)
        Dim strMonth = Right("0" & LTrim(Month(strMyDate)), 2)
        Dim strDay = Right("0" & LTrim(Day(strMyDate)), 2)
        Dim strNewDate = strYear & strMonth & strDay

        Dim strHour = Right("0" & LTrim(Hour(strMyDate)), 2)
        Dim strMin = Right("0" & LTrim(Minute(strMyDate)), 2)
        Dim strSec = Right("0" & LTrim(Second(strMyDate)), 2)
        Dim strTime = strHour & ":" & strMin & ":" & strSec
        MyDate = strNewDate & " " & strTime

    End Function

    Public Function PostDate(strDate As String) As String

        Dim strMyDate = CDate(strDate)
        Dim strYear = Right("0000" & LTrim(Year(strMyDate)), 4)
        Dim strMonth = Right("0" & LTrim(Month(strMyDate)), 2)
        Dim strDay = Right("0" & LTrim(Day(strMyDate)), 2)
        Dim strNewDate = strYear & "-" & strMonth & "-" & strDay

        Dim strHour = Right("0" & LTrim(Hour(strMyDate)), 2)
        Dim strMin = Right("0" & LTrim(Minute(strMyDate)), 2)
        Dim strSec = Right("0" & LTrim(Second(strMyDate)), 2)
        Dim strTime = strHour & ":" & strMin & ":" & strSec
        PostDate = strNewDate & " at " & strTime

    End Function

    Public Function HTMLDecode(sText As String) As String

        Dim MyRegEx
        Dim matches
        Dim match
        Dim strPattern = "&#(\d+);"
        sText = Replace(sText, "&quot;", Chr(34))
        sText = Replace(sText, "&lt;", Chr(60))
        sText = Replace(sText, "&gt;", Chr(62))
        sText = Replace(sText, "&amp;", Chr(38))
        sText = Replace(sText, "&nbsp;", Chr(32))

        MyRegEx = New Regex(strPattern)
        'MyRegEx.Global = True
        matches = MyRegEx.Execute(sText)

        'Iterate over matches
        For Each match In matches
            'For each unicode match, replace the whole match, with the ChrW of the digits.
            sText = Replace(sText, match.Value, ChrW(match.SubMatches(0)))
        Next

        HTMLDecode = sText
    End Function
    ' *************

    Public Function FolderExists(fldr As String) As Boolean
        FolderExists = False
        Dim fso
        fso = CreateObject("Scripting.FileSystemObject")
        If (fso.FolderExists(fldr)) Then
            FolderExists = True
        End If
        fso = Nothing
    End Function

    Public Sub CreateDirectory(fldr As String)
        Dim fso, f
        fso = CreateObject("Scripting.FileSystemObject")
        If Not fso.FolderExists(fldr) Then
            f = fso.CreateFolder(fldr)
        End If
    End Sub

    Public Function RemoveHTML(strText As String) As String
        Dim myRegEx
        Dim Pattern = "<[^>]*>"
        myRegEx = New Regex(Pattern)
        RemoveHTML = myRegEx.Replace(strText, "")
    End Function

    ' repeats a string(strC) a number
    ' of times(intT). e.i:
    ' RepeatString("a", 4) returns aaaa

    Public Function RepeatString(strC As Char, intT As Integer) As String
        Dim i, strTemp
        strTemp = strC
        If intT > 1 Then
            For i = 1 To intT + 1
                strTemp = strTemp & strC
            Next
        End If
        RepeatString = strTemp
    End Function

    Public Function GetRandomString(myLength) As String

        Dim strPW As String = ""
        Const minLength = 10
        Const maxLength = 20
        Dim X, Y

        Randomize()

        If myLength = 0 Then
            myLength = Int((maxLength * Rnd()) + minLength)
        End If
        For X = 1 To myLength
            'Randomize the type of this character
            Y = Int((3 * VBMath.Rnd()) + 1)

            ' (1) Numeric, (2) Uppercase, (3) Lowercase
            Select Case Y
                Case 1
                    'Randomize
                    strPW = strPW & Chr(Int((9 * VBMath.Rnd()) + 48))
                Case 2
                    'Randomize
                    strPW = strPW & Chr(Int((25 * VBMath.Rnd()) + 65))
                Case 3
                    'Randomize
                    strPW = strPW & Chr(Int((25 * VBMath.Rnd()) + 97))
            End Select
        Next
        GetRandomString = strPW

    End Function

    Public Function Is_Mobile() As Boolean
        Dim strUserAgent As String = Request.ServerVariables("HTTP_USER_AGENT")
        Dim Patterns() As String = Split("up.browser|up.link|mmp|symbian|smartphone|phone|tablet|android|iphone|midp|wap|phone|windows ce|pda|mobile|mini|palm|ipad", "|")
        Dim Match As Boolean = False
        Dim i1 As Integer
        For i1 = 0 To UBound(Patterns)
            If InStr(strUserAgent, Patterns(i1)) > 0 Then
                Match = True
                Exit For
            End If
        Next
        Is_Mobile = Match
    End Function

    Public Function RandomPW(myLength As Integer) As String

        Randomize()
        Dim upperbound As Integer
        Dim lowerbound As Integer
        Dim X, Y1
        Dim strPW As String = ""

        If myLength = 0 Then
            Const minLength = 16
            Const maxLength = 32
            myLength = Int((maxLength * VBMath.Rnd()) + minLength)
        End If
        For X = 1 To myLength

            'Randomize the type of character: 1=Numeric, 2=Uppercase, 3=Lowercase
            Y1 = Int((3 * VBMath.Rnd()) + 1)

            Select Case Y1
                Case 1
                    upperbound = 57
                    lowerbound = 48
                Case 2
                    upperbound = 90
                    lowerbound = 65
                Case 3
                    upperbound = 122
                    lowerbound = 97
            End Select
            strPW = strPW & Chr(Int((upperbound - lowerbound + 1) * VBMath.Rnd() + lowerbound))

        Next
        RandomPW = strPW

    End Function

    Public Function IsEmailValid(eaddr As String) As Boolean
        Dim MyRegEx
        Dim Pattern = "^[-+.\w]{1,64}@[-.\w]{1,64}\.[-.\w]{2,6}$"
        MyRegEx = New Regex(Pattern)
        IsEmailValid = MyRegEx.isMatch(eaddr)
    End Function

    Public Function SQLInject(strWords As String) As String
        Dim newChars, i
        Dim badChars() As String = {"select", "drop", ";", "--", "insert", "delete", "update", "xp_"}

        newChars = strWords
        For i = 0 To UBound(badChars)
            newChars = Replace(newChars, badChars(i), "")
        Next
        newChars = newChars
        newChars = Replace(newChars, "'", "''")
        newChars = Replace(newChars, "'", "|")
        newChars = Replace(newChars, "|", "''")
        newChars = Replace(newChars, "\""", "|")
        newChars = Replace(newChars, "|", "''")
        SQLInject = newChars
    End Function

    Public Function SQLInject2(strWords As String) As String
        Dim newChars, i
        Dim badChars() As String = {
        "select(.*)(from|with|by)1", "insert(.*)(into|values)1",
        "update(.*)set", "delete(.*)(from|with)1",
        "drop(.*)(from|aggre|role|assem|key|cert|cont|credential|data|endpoint|event|fulltext|function|index|login|type|schema|procedure|que|remote|role|route|sign| stat|syno|table|trigger|user|view|xml)1",
        "alter(.*)(application|assem|key|author|cert|credential|data|endpoint|fulltext |function|index|login|type|schema|procedure|que|remote|role|route|serv|table|u ser|view|xml)1",
        "xp_", "sp_", "restore\s", "grant\s", "revoke\s",
        "dbcc", "dump", "use\s", "set\s", "truncate\s",
        "backup\s", "load\s", "save\s", "shutdown",
        "cast(.*)\(", "convert(.*)\(", "execute\s",
        "updatetext", "writetext", "reconfigure",
        "/\*", "\*/", ";", "\-\-", "\[", "\]", "char(.*)\(",
        "nchar(.*)\("
        }

        newChars = strWords
        Dim Pattern = badChars
        For i = 0 To UBound(badChars)
            'MyRegEx = New Regex(Pattern)
            'newChars = MyRegEx.Replace(newChars, "**" & newChars & "**")
            'MyRegEx = Nothing
        Next
        newChars = Replace(newChars, "'", "''")
        SQLInject2 = newChars
    End Function

    Public Function GetUserName(strOwnerID) As String
        GetUserName = ""
        Dim strSELECT = "SELECT UserName, DisplayName From Users WHERE ID = " & strOwnerID
        'Set rsName = Conn.Execute(strSELECT)
        'If Not rsName.EOF Then
        'If Trim(rsName("DisplayName")) <> "" Then
        'GetUserName = rsName("DisplayName")
        'Else
        'GetUserName = rsName("UserName")
        'End If
        'End If
        'rsName.Close
        'Set rsName = Nothing

    End Function

    Protected Function GetCategoryName(intCat As Integer) As String
        GetCategoryName = ""
        If intCat > 0 Then

            Dim connetionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim sqlCnn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSELECT As String

            strSELECT = "SELECT [CategoryID],[CategoryName] FROM [AspNetPortal].[dbo].[Yaf_Category] WHERE [CategoryID] = " & intCat.ToString & " AND [IsActive] = 1"
            sqlCnn = New SqlConnection(connetionString)
            Try
                sqlCnn.Open()
                sqlCmd = New SqlCommand(strSELECT, sqlCnn)
                Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader()
                If sqlReader.Read() Then
                    GetCategoryName = sqlReader.Item(1)
                End If
                sqlReader.Close()
                sqlCmd.Dispose()
                sqlCnn.Close()
            Catch ex As Exception
                ' MsgBox("Can not open connection ! ")
            End Try
        End If
    End Function

    Protected Function GetForumName(intForumID As Integer) As String
        GetForumName = ""
        If intForumID > 0 Then

            Dim connetionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim sqlCnn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSELECT As String

            strSELECT = "SELECT [ForumID],[ForumName] FROM [AspNetPortal].[dbo].[Yaf_Forum] WHERE [ForumID] = " & intForumID.ToString & " AND [IsActive] = 1"
            sqlCnn = New SqlConnection(connetionString)
            Try
                sqlCnn.Open()
                sqlCmd = New SqlCommand(strSELECT, sqlCnn)
                Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader()
                If sqlReader.Read() Then
                    GetForumName = sqlReader.Item(1)
                End If
                sqlReader.Close()
                sqlCmd.Dispose()
                sqlCnn.Close()
            Catch ex As Exception
                ' MsgBox("Can not open connection ! ")
            End Try
        End If
    End Function

    Private Sub navbarTogglerDemo01()
        'If navbarTogglerDemo01.datatoggle = "expand" Then
        'navbarTogglerDemo01.datatoggle = "collapse"
        'Else
        'navbarTogglerDemo01.datatoggle = "expand"
        'End If
    End Sub


End Class