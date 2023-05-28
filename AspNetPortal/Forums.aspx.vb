Imports System.Configuration
Imports System.Data.SqlClient
Public Class Forums

    Inherits Page
    Public IntCatID As Integer

    Public Shared catsHT As Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim strSELECT As String = ""
        Dim intRowsAffected As Integer = 0

        IntCatID = CInt(Request.QueryString("CatID"))

        If intCatID > 0 Then

            Dim strCatName As String = GetCategoryName(intCatID)
            SqlDataSource1.ConnectionString = constr
            SqlDataSource1.SelectCommand = "SELECT [FORUM_ID],[F_SUBJECT] FROM [AspNetPortal].[dbo].[FORUM_FORUM] WHERE [CAT_ID] = " & IntCatID.ToString & " AND [F_STATUS] = 1 ORDER BY [F_SUBJECT]"
            GridView1.HeaderRow.Cells(0).Text = "&nbsp;<i class='fa-solid fa-folder-open'></i>&nbsp;" & strCatName.ToUpper() & "&nbsp;&nbsp;"
            CategoryName.Text = strCatName

            If StaticCache.catsHT.Count > 0 Then
                CategoryName.Text = strCatName
            End If

        Else

                Response.Redirect("~/")

        End If
    End Sub

    Protected Function GetCategoryName(intCatID As Integer) As String

        GetCategoryName = ""
        If intCatID > 0 Then

            Dim connetionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim sqlCnn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSELECT As String

            strSELECT = "SELECT [CAT_ID],[CAT_NAME] FROM [AspNetPortal].[dbo].[FORUM_CATEGORY] WHERE [CAT_ID] = " & intCatID.ToString & " AND [CAT_STATUS] = 1"
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

    Protected Sub OpenWindow(intAd As Integer)
        If intAd > 0 Then
            Dim url As String = "ViewAd.aspx?id=" & intAd.ToString
            Dim s As String = "window.open('" & url + "', '_blank', 'menubar=yes,left=100,top=100,resizable=yes,width=950,height=600');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        End If
    End Sub

End Class