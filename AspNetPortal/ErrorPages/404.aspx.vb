Imports System.Data.SqlClient
Public Class Error404
    Inherits System.Web.UI.Page

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

        If strUserIP.Length = 0 Then

            Response.Write("<h2>Enable IP discovery to access this website</h2>")
            Response.Write("<hr>")
            Response.Write("<p>This website uses IP discovery, cookies, popup windows and JavaScripts.</p>")
            Response.Write("<p>Please enable all these features to access this website. </p>")

            Session.Timeout = 1
            Response.End()

        End If

        If Not strUserIP.Contains("192.168.1") And Not strUserIP.Contains("104.11.112") And Not strUserIP.Contains("::1") Then

            If strQuery.Contains("wlwmanifest.xml") Or strQuery.Contains("/wp_") Then

                Using con As New SqlConnection(constr)

                    con.Open()
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
                        strINSERT = "INSERT INTO [AspNetPortal].[dbo].[BannedIP] ([IP],[Tries],[Redirect]) VALUES ('" & strUserIP & "'," & Session("Visits") & ",'/guardian/accessdenied.aspx')"
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

                    con.Close()

                End Using

            End If

        End If

        'Response.Write("<h2>Enable IP discovery to access this website</h2>")
        'Response.Write("<hr>")
        'Response.Write("<p>This website uses IP discovery, cookies, popup windows and JavaScripts.</p>")
        'Response.Write("<p>Please enable all these features to access this website. </p>")
        'Session.Timeout = 1
        'Response.End()

    End Sub

End Class