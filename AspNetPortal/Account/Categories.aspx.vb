Imports System
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Public Class Categories
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

        SqlDataSource1.ConnectionString = constr
        SqlDataSource1.SelectCommand = "SELECT CategoryID, CategoryName FROM [AspNetPortal].[dbo].[yaf_Category] WHERE [IsActive] = 1 ORDER BY [CategoryName]"

        Dim authenticationManager = HttpContext.Current.GetOwinContext().Authentication
        If Not IsPostBack Then
        Else
            If TextBox1.Text.Trim.Length > 0 Then

                '## Add Category
                AddCategory(TextBox1.Text.Trim)
                TextBox1.Text = ""

            End If
        End If
    End Sub

    Private Sub AddCategory(strCategory As String)

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        strCategory = strCategory.Trim

        Dim strSELECT As String = ""
        Dim strINSERT As String = ""
        Dim IsOkToAddCategory As Boolean = False
        Dim intRowsAffected As Integer = 0

        Using con As New SqlConnection(constr)
            con.Open()
            strSELECT = "SELECT [CategoryID],[CategoryName] FROM [AspNetPortal].[dbo].[Yaf_Category] WHERE [CategoryName] = '" & strCategory & "'"
            Using cmd As New SqlCommand(strSELECT)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                Dim reader As SqlDataReader
                reader = cmd.ExecuteReader()
                If reader.HasRows Then

                    ' Category already exists
                    ' Notify user
                    m_SuccessMessage = "Category [" & strCategory & "] Already Exist! If you can not see it in the list, it may temporarily be on hold."
                    SuccessMessagePlaceHolder.Visible = True

                Else

                    ' Is OK to Add Category Here
                    IsOkToAddCategory = True

                End If
                reader.Close()

            End Using

            If IsOkToAddCategory = True Then

                strINSERT = "INSERT INTO [AspNetPortal].[dbo].[yaf_Category] ([CategoryName]) VALUES ('" & strCategory & "')"
                Using cmd As New SqlCommand(strINSERT)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    intRowsAffected = cmd.ExecuteNonQuery()
                End Using

                m_SuccessMessage = "Category [" & strCategory & "] Added Successfully!"
                SuccessMessagePlaceHolder.Visible = True

                GridView1.DataBind()

            End If

            con.Close()

        End Using

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