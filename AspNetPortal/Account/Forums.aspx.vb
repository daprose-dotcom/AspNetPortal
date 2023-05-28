Imports System
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Public Class AccountForums

    Inherits System.Web.UI.Page

    Protected Property SuccessMessage() As String
        Get
            Return m_SuccessMessage
        End Get
        Private Set(value As String)
            m_SuccessMessage = value
        End Set
    End Property

    Private m_SuccessMessage As String

    Protected Sub Page_Load() Handles Me.Load

        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim appUser = manager.FindById(User.Identity.GetUserId())

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        Dim strSELECT As String = ""
        Dim strForumFromUser As String = ""
        Dim authenticationManager = HttpContext.Current.GetOwinContext().Authentication
        If Not IsPostBack Then

            '## Fill Category DropDownList
            Using con As New SqlConnection(constr)

                con.Open()

                '============================================
                ' lets check the IP
                ' ================================================

                strSELECT = "SELECT [CategoryID], [CategoryName] FROM [AspNetPortal].[dbo].[yaf_Category] WHERE [IsActive] = 1 ORDER BY [CategoryName]"
                Using cmd As New SqlCommand(strSELECT)

                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    ddCategories.DataSource = cmd.ExecuteReader()
                    ddCategories.DataTextField = "CategoryName"
                    ddCategories.DataValueField = "CategoryID"
                    ddCategories.DataBind()

                End Using

                ddCategories.Items.Insert(0, New ListItem("-- Select Category --", "0"))
                con.Close()

            End Using

            SqlDataSource1.ConnectionString = constr
            SqlDataSource1.SelectCommand = "SELECT ForumID, ForumName FROM [AspNetPortal].[dbo].[yaf_Forum] WHERE [ForumID] = 0 AND [IsActive] = 1 ORDER BY [ForumName]"
            GridView1.DataBind()

        Else

            If TextBox1.Text.Trim.Length > 0 And ddCategories.SelectedValue > 0 Then

                '## Add Category
                strForumFromUser = TextBox1.Text.Trim
                AddForum(strForumFromUser)
                TextBox1.Text = ""

            End If

        End If

    End Sub

    Protected Sub ddCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddCategories.SelectedIndexChanged
        DisplayCategories()
    End Sub

    Private Sub DisplayCategories()
        ' Get the selected role
        Dim selectedCategoryID As String = ddCategories.SelectedValue.ToString

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        If selectedCategoryID > 0 Then

            SqlDataSource1.ConnectionString = constr
            SqlDataSource1.SelectCommand = "SELECT ForumID, ForumName FROM [AspNetPortal].[dbo].[yaf_Forum] WHERE [CategoryID] = " & selectedCategoryID & " AND [IsActive] = 1 ORDER BY [ForumName]"
            GridView1.DataBind()

            ddCategories.DataBind()

        End If

    End Sub

    Private Sub AddForum(strForum As String)

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim selectedCategoryID As String = ddCategories.SelectedValue.ToString
        Dim strSELECT As String = ""
        Dim strINSERT As String = ""
        Dim IsOkToAddForum As Boolean = False
        Dim intRowsAffected As Integer = 0

        strForum = strForum.Trim

        If strForum.Length > 0 And selectedCategoryID > 0 Then

            Using con As New SqlConnection(constr)
                con.Open()
                strSELECT = "SELECT [ForumID],[ForumName] FROM [AspNetPortal].[dbo].[Yaf_Forum] WHERE [CategoryID] = " & selectedCategoryID & " AND [ForumName] = '" & strForum & "'"
                Using cmd As New SqlCommand(strSELECT)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    Dim reader As SqlDataReader
                    reader = cmd.ExecuteReader()

                    If reader.HasRows Then

                        ' Category already exists
                        ' Notify user
                        m_SuccessMessage = "Forum [" & strForum & "] Already Exists! If you can not see it in the list, it may temporarily be on hold."
                        SuccessMessagePlaceHolder.Visible = True

                    Else

                        ' Is OK to Add Category Here
                        IsOkToAddForum = True

                    End If

                    reader.Close()

                End Using

                If IsOkToAddForum = True Then

                    strINSERT = "INSERT INTO [AspNetPortal].[dbo].[yaf_Forum] ([CategoryID],[ForumName]) VALUES ('" & selectedCategoryID & "','" & strForum & "')"
                    Using cmd As New SqlCommand(strINSERT)
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        intRowsAffected = cmd.ExecuteNonQuery()
                    End Using

                    m_SuccessMessage = "Forum [" & strForum & "] added successfully!"
                    SuccessMessagePlaceHolder.Visible = True

                    SqlDataSource1.ConnectionString = constr
                    SqlDataSource1.SelectCommand = "SELECT ForumID, ForumName FROM [AspNetPortal].[dbo].[yaf_Forum] WHERE [CategoryID] = " & selectedCategoryID & " AND [IsActive] = 1 ORDER BY [ForumName]"
                    GridView1.DataBind()

                End If

                con.Close()

            End Using

        End If

    End Sub
    Private Sub AddErrors(result As IdentityResult)
        For Each [error] As String In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub

    Private Function GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim intIndex As Integer = GridView1.SelectedIndex
        Dim row As GridViewRow = GridView1.Rows(intIndex)
        Dim intAd As Integer = CInt(row.Cells.Item(2).Text)

        If intAd > 0 Then OpenWindow(intAd)

        Return Nothing
    End Function
    Protected Sub OpenWindow(intAd As Integer)
        If intAd > 0 Then
            Dim url As String = "../ViewAd.aspx?id=" & intAd.ToString
            Dim s As String = "window.open('" & url + "', '_blank', 'menubar=yes,left=100,top=100,resizable=yes,width=800,height=600');"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        End If
    End Sub

End Class