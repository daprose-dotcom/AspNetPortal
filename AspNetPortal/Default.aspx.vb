Imports System.Configuration
Imports System.Data.SqlClient
Public Class _Default
    Inherits Page

    Shared constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            '## we do this only when we load the page
            SqlDataSource1.ConnectionString = constr
            SqlDataSource1.SelectCommand = "SELECT [CAT_ID],[CAT_NAME] FROM [AspNetPortal].[dbo].[FORUM_CATEGORY] WHERE [CAT_STATUS] = 1 ORDER BY [CAT_NAME]"

        Else

            ' We can update anything here

        End If

    End Sub

    Protected Sub OpenWindow(intAd As Integer)
        If intAd > 0 Then
            Dim url As String = "ViewAd.aspx?id=" & intAd.ToString
            Dim s As String = "window.open('" & url + "', '_blank', 'menubar=yes,left=100,top=100,resizable=yes,width=950,height=600');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        End If
    End Sub

End Class