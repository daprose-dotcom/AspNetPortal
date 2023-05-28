Imports System
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin
Partial Public Class AdminManage
    Inherits System.Web.UI.Page

    Shared constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Protected Property SuccessMessage() As String
        Get
            Return m_SuccessMessage
        End Get
        Private Set(value As String)
            m_SuccessMessage = value
        End Set
    End Property
    Private m_SuccessMessage As String

    Private Function HasPassword(manager As ApplicationUserManager) As Boolean
        Dim appUser = manager.FindById(User.Identity.GetUserId())
        Return (appUser IsNot Nothing AndAlso appUser.PasswordHash IsNot Nothing)
    End Function

    Protected Property HasPhoneNumber() As Boolean
        Get
            Return m_HasPhoneNumber
        End Get
        Private Set(value As Boolean)
            m_HasPhoneNumber = value
        End Set
    End Property
    Private m_HasPhoneNumber As Boolean

    Protected Property TwoFactorEnabled() As Boolean
        Get
            Return m_TwoFactorEnabled
        End Get
        Private Set(value As Boolean)
            m_TwoFactorEnabled = value
        End Set
    End Property
    Private m_TwoFactorEnabled As Boolean

    Protected Property TwoFactorBrowserRemembered() As String
        Get
            Return m_TwoFactorBrowserRemembered
        End Get
        Private Set(value As String)
            m_TwoFactorBrowserRemembered = value
        End Set
    End Property

    Private m_TwoFactorBrowserRemembered As String

    Public Property LoginsCount As Integer

    Protected Sub Page_Load() Handles Me.Load

        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim appUser = manager.FindById(User.Identity.GetUserId())

        If Not IsPostBack Then

            Visitors.Visible = True
            VisitorsPanel.Visible = False

            Agents.Visible = False
            AgentsPanel.Visible = True

            SqlDataSource2.ConnectionString = constr
            SqlDataSource2.SelectCommand = "SELECT [ID],[UserDate],[UserIP],[UserAgent] FROM [AspNetPortal].[dbo].[UsersAgent] WHERE [UserDate] > (GETDATE() -2 ) AND CHARINDEX('192.168.1.',[UserIP]) = 0 AND CHARINDEX('104.11.112.',[UserIP]) = 0 ORDER BY [UserDate] DESC"

            SqlDataSource1.ConnectionString = constr
            SqlDataSource1.SelectCommand = "SELECT [ID],[UserDate],[UserIP],[UserScript] FROM [AspNetPortal].[dbo].[UsersLog] WHERE [UserDate] > (GETDATE() -2 ) AND CHARINDEX('192.168.1.',[UserIP]) = 0 AND CHARINDEX('104.11.112.',[UserIP]) = 0 ORDER BY [UserDate] DESC"

        End If


        If Not IsPostBack Then

        End If
    End Sub

    Private Sub Agents_Clic() Handles Agents.Click

        SqlDataSource2.ConnectionString = constr
        SqlDataSource2.SelectCommand = "SELECT [ID],[UserDate],[UserIP],[UserAgent] FROM [AspNetPortal].[dbo].[UsersAgent] WHERE [UserDate] > (GETDATE() -2 ) AND CHARINDEX('192.168.1.',[UserIP]) = 0 AND CHARINDEX('104.11.112.',[UserIP]) = 0 ORDER BY [UserDate] DESC"

        VisitorsPanel.Visible = False
        AgentsPanel.Visible = True

        Agents.Visible = False
        Visitors.Visible = True

    End Sub

    Private Sub Visitors_Clic() Handles Visitors.Click

        SqlDataSource1.ConnectionString = constr
        SqlDataSource1.SelectCommand = "SELECT [ID],[UserDate],[UserIP],[UserScript] FROM [AspNetPortal].[dbo].[UsersLog] WHERE [UserDate] > (GETDATE() -2 ) AND CHARINDEX('192.168.1.',[UserIP]) = 0 AND CHARINDEX('104.11.112.',[UserIP]) = 0 ORDER BY [UserDate] DESC"

        AgentsPanel.Visible = False
        VisitorsPanel.Visible = True

        Agents.Visible = True
        Visitors.Visible = False

    End Sub

    Private Sub AddErrors(result As IdentityResult)
        For Each [error] As String In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub

    ' Remove phonenumber from user
    Protected Sub RemovePhone_Click(sender As Object, e As EventArgs)
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim signInManager = Context.GetOwinContext().Get(Of ApplicationSignInManager)()
        Dim result = manager.SetPhoneNumber(User.Identity.GetUserId(), Nothing)
        If Not result.Succeeded Then
            Return
        End If
        Dim userInfo = manager.FindById(User.Identity.GetUserId())
        If userInfo IsNot Nothing Then
            signInManager.SignIn(userInfo, isPersistent:=False, rememberBrowser:=False)
            Response.Redirect("/Account/Manage?m=RemovePhoneNumberSuccess")
        End If
    End Sub

    ' DisableTwoFactorAuthentication
    Protected Sub TwoFactorDisable_Click(sender As Object, e As EventArgs)

        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        manager.SetTwoFactorEnabled(User.Identity.GetUserId(), False)

        Response.Redirect("/Account/Manage")

    End Sub

    'EnableTwoFactorAuthentication 
    Protected Sub TwoFactorEnable_Click(sender As Object, e As EventArgs)
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        manager.SetTwoFactorEnabled(User.Identity.GetUserId(), True)

        Response.Redirect("/Account/Manage")
    End Sub

    Private Function GridView1_GetData(maximumRows As Integer, startRowIndex As Integer, ByRef totalRowCount As Integer, sortByExpression As String) As IQueryable
        Return Nothing
    End Function

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