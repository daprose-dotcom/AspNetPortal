Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.Web.Security
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Public Class UsersAndRoles
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            ' Bind the users and roles
            BindUsersToUserList()
            BindRolesToList()

            ' Check the selected user's roles
            CheckRolesForSelectedUser()

            'Display those users belonging to the currently selected role
            DisplayUsersBelongingToRole()
        End If
    End Sub

    Private Sub BindRolesToList()
        ' Get all of the roles
        Dim roleNames() As String = Roles.GetAllRoles()
        UsersRoleList.DataSource = roleNames
        UsersRoleList.DataBind()

        RoleList.DataSource = roleNames
        RoleList.DataBind()
    End Sub

#Region "'By User' Interface-Specific Methods"
    Private Sub BindUsersToUserList()

        ' Get all of the user accounts
        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        SqlDataSource1.ConnectionString = constr
        SqlDataSource1.SelectCommand = "SELECT [UserName], [UserName] as [User] FROM [AspNetPortal].[dbo].[AspNetUsers] ORDER BY [USER]"

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("SELECT [UserName], [UserName] as [User] FROM [AspNetPortal].[dbo].[AspNetUsers] ORDER BY [USER]")
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()

                UserList.DataSource = cmd.ExecuteReader()
                UserList.DataTextField = "User"
                UserList.DataValueField = "UserName"
                UserList.DataBind()

                con.Close()

            End Using
        End Using
        UserList.Items.Insert(0, New ListItem("--Select User --", ""))

    End Sub

    Protected Sub UserList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UserList.SelectedIndexChanged
        'Protected Sub UserList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckRolesForSelectedUser()
    End Sub

    Private Sub CheckRolesForSelectedUser()
        ' Determine what roles the selected user belongs to
        Dim selectedUserName As String = UserList.SelectedValue.ToString
        Dim selectedUsersRoles() As String = Roles.GetRolesForUser(selectedUserName)

        ' Loop through the Repeater's Items and check or uncheck the checkbox as needed
        For Each ri As RepeaterItem In UsersRoleList.Items
            ' Programmatically reference the CheckBox
            Dim RoleCheckBox As CheckBox = CType(ri.FindControl("RoleCheckBox"), CheckBox)

            ' See if RoleCheckBox.Text is in selectedUsersRoles
            If Linq.Enumerable.Contains(Of String)(selectedUsersRoles, RoleCheckBox.Text) Then
                RoleCheckBox.Checked = True
            Else
                RoleCheckBox.Checked = False
            End If
        Next
    End Sub

    Protected Sub RoleCheckBox_CheckChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Reference the CheckBox that raised this event
        Dim RoleCheckBox As CheckBox = CType(sender, CheckBox)

        ' Get the currently selected user and role
        Dim selectedUserName As String = UserList.SelectedValue.ToString
        Dim roleName As String = RoleCheckBox.Text

        ' Determine if we need to add or remove the user from this role
        If RoleCheckBox.Checked Then
            ' Add the user to the role
            Roles.AddUserToRole(selectedUserName, roleName)
            ' Display a status message
            ActionStatus.Text = String.Format("User <b>{0}</b> was <b>added</b> to role <b>{1}</b>.", selectedUserName, roleName)
        Else
            ' Remove the user from the role
            Roles.RemoveUserFromRole(selectedUserName, roleName)

            ' Display a status message
            ActionStatus.Text = String.Format("User <b>{0}</b> was <b>removed</b> from role <b>{1}</b>.", selectedUserName, roleName)
        End If

        ' Refresh the "by role" interface
        DisplayUsersBelongingToRole()

    End Sub
#End Region

#Region "'By Role' Interface-Specific Methods"
    Protected Sub RoleList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RoleList.SelectedIndexChanged
        DisplayUsersBelongingToRole()
    End Sub

    Private Sub DisplayUsersBelongingToRole()
        ' Get the selected role
        Dim selectedRoleName As String = RoleList.SelectedValue.ToString

        ' Get the list of usernames that belong to the role
        Dim usersBelongingToRole() As String = Roles.GetUsersInRole(selectedRoleName)

        ' Bind the list of users to the GridView
        'RolesUserList_RowDeleting.DataSource = usersBelongingToRole
        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        SqlDataSource1.ConnectionString = constr
        SqlDataSource1.SelectCommand = "SELECT [UserName], [UserName] as [User] FROM [AspNetPortal].[dbo].[AspNetUsers] ORDER BY [USER]"
        RolesUserList.DataSource = usersBelongingToRole
        RolesUserList.DataBind()


    End Sub

    Protected Sub RolesUserList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles RolesUserList.RowDeleting

        ' Get the selected role
        Dim selectedRoleName As String = RoleList.SelectedValue

        ' Reference the UserNameLabel
        Dim UserNameLabel As Label = CType(RolesUserList.Rows(e.RowIndex).FindControl("UserNameLabel"), Label)

        ' Remove the user from the role
        Roles.RemoveUserFromRole(UserNameLabel.Text, selectedRoleName)

        ' Refresh the GridView
        DisplayUsersBelongingToRole()

        ' Display a status message
        ActionStatus.Text = String.Format("User <b>{0}</b> was <b>removed</b> from role <b>{1}</b>.", UserNameLabel.Text, selectedRoleName)

        ' Refresh the "by user" interface
        CheckRolesForSelectedUser()
    End Sub

    Protected Sub AddUserToRoleButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddUserToRoleButton.Click

        ' Get the selected role and username
        Dim selectedRoleName As String = RoleList.SelectedValue.ToString()
        Dim userToAddToRole As String = UserNameToAddToRole.Text().Trim().ToLower()

        ' Make sure that a value was entered
        If userToAddToRole.Length() = 0 Then
            ActionStatus.Text = "You must enter a valid user e-mail in the textbox."
            Exit Sub
        End If

        ' Make sure that the user exists in the system
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim userInfo = manager.FindByEmail(userToAddToRole)
        If userInfo Is Nothing Then
            ActionStatus.Text = String.Format("The user <b>{0}</b> does not exist in the system.", userToAddToRole)
            Exit Sub
        End If

        ' Make sure that the user doesn't already belong to this role
        If Roles.IsUserInRole(userToAddToRole, selectedRoleName) Then
            ActionStatus.Text = String.Format("User <b>{0}</b> is already a member of role <b>{1}</b>.", UserNameToAddToRole, selectedRoleName)
            Exit Sub
        End If

        ' If we reach here, we need to add the user to the role
        Roles.AddUserToRole(userToAddToRole, selectedRoleName)

        ' Clear out the TextBox
        UserNameToAddToRole.Text = String.Empty

        ' Refresh the GridView
        DisplayUsersBelongingToRole()

        ' Display a status message
        ActionStatus.Text = String.Format("User <b>{0}</b> was added to role <b>{1}</b>.", userToAddToRole, selectedRoleName)

        ' Refresh the "by user" interface
        CheckRolesForSelectedUser()

    End Sub
#End Region

End Class