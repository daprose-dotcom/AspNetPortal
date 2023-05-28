Imports System.Configuration
Imports System.Data.SqlClient
Public Class Topic

    Inherits Page

    Public intCatID As Integer
    Public intForumID As Integer
    Public intTopicID As Integer

    Public strTopicName As String
    Public strTopicContent As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim strSELECT As String = ""
        Dim intRowsAffected As Integer = 0

        intCatID = CInt(Request.QueryString("CatID"))
        intForumID = CInt(Request.QueryString("ForumID"))
        intTopicID = CInt(Request.QueryString("TopicID"))

        If intCatID > 0 And intForumID > 0 And intTopicID > 0 Then

            Dim strCatName As String = GetCategoryName(intCatID)
            'Dim strCatName As String = StaticCache.catsHT(intCategoryID)

            Dim strForumName As String = GetForumName(intForumID)
            'Dim strForumName As String = StaticCache.forumsHT(intForumID)

            Dim strTopicName As String = GetTopicName(intTopicID)

            SqlDataSource1.ConnectionString = constr
            SqlDataSource1.SelectCommand = "SELECT [TOPIC_ID],[T_SUBJECT] FROM [AspNetPortal].[dbo].[FORUM_TOPICS] WHERE [FORUM_ID] = " & intForumID.ToString & " AND [T_STATUS] = 1 ORDER BY [T_SUBJECT]"

            GridView1.HeaderRow.Cells(0).Text = "&nbsp;<i class='fa-solid fa-folder-open'></i>&nbsp;" & strForumName.ToUpper() & "&nbsp;&nbsp;"

            CategoryLink.NavigateUrl = "~/Forums.aspx?CatID=" & intCatID.ToString
            CategoryLink.Text = strCatName

            'ForumLink.NavigateUrl = "~/Forums.aspx?CatID=" & intCatID.ToString
            'ForumLink.Text = strCatName

            ForumName.Text = strForumName

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

    Protected Function GetForumName(intForumID As Integer) As String
        GetForumName = ""
        If intForumID > 0 Then

            Dim connetionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim sqlCnn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSELECT As String

            strSELECT = "SELECT [FORUM_ID],[F_SUBJECT] FROM [AspNetPortal].[dbo].[FORUM_FORUM] WHERE [FORUM_ID] = " & intForumID.ToString & " AND [F_STATUS] = 1"
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

    Protected Function GetTopicName(intTopicID As Integer) As String
        GetTopicName = ""
        If intTopicID > 0 Then

            Dim connetionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim sqlCnn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSELECT As String

            strSELECT = "SELECT [TOPIC_ID],[T_SUBJECT],[T_MESSAGE] FROM [AspNetPortal].[dbo].[FORUM_TOPICS] WHERE [TOPIC_ID] = " & intTopicID.ToString & " AND [T_STATUS] = 1"
            sqlCnn = New SqlConnection(connetionString)
            Try
                sqlCnn.Open()
                sqlCmd = New SqlCommand(strSELECT, sqlCnn)
                Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader()
                If sqlReader.Read() Then

                    strTopicName = sqlReader.Item(1)
                    strTopicContent = sqlReader.Item(2)
                    'strTopicContent = strTopicContent.Replace(vbCrLf, "<br>")
                    GetTopicName = sqlReader.Item(1)

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