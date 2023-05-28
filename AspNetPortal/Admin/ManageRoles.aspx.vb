Public Class ManageRoles
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        If Not Page.IsPostBack Then

            SqlDataSource1.ConnectionString = constr
            SqlDataSource1.SelectCommand = "SELECT * FROM [AspNetPortal].[dbo].[aspnet_Roles]"

            If RoleList.Enabled Then
                RoleList.DataSource = SqlDataSource1
                RoleList.DataSource = Roles.GetAllRoles()
                RoleList.DataBind()
            End If
        End If

    End Sub

    Protected Sub DisplayRolesInGrid()

        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        SqlDataSource1.ConnectionString = constr
        SqlDataSource1.SelectCommand = "SELECT * FROM [AspNetPortal].[dbo].[aspnet_Roles]"

        If RoleList.Enabled Then
            RoleList.DataSource = SqlDataSource1
            RoleList.DataSource = Roles.GetAllRoles()
            RoleList.DataBind()
        End If

    End Sub

    Protected Sub CreateRoleButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateRoleButton.Click
        'Protected Sub CreateRoleButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim newRoleName As String = RoleName.Text.Trim()

        If Not Roles.RoleExists(newRoleName) Then
            ' Create the role
            Roles.CreateRole(newRoleName)
            ' Refresh the RoleList Grid
            DisplayRolesInGrid()
        End If

        RoleName.Text = String.Empty

    End Sub

    Protected Sub RoleList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles RoleList.RowDeleting
        'Protected Sub RoleList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        ' Get the RoleNameLabel
        Dim RoleNameLabel As Label = CType(RoleList.Rows(e.RowIndex).FindControl("RoleNameLabel"), Label)

        ' Delete the role
        Roles.DeleteRole(RoleNameLabel.Text, False)

        ' Rebind the data to the RoleList grid
        DisplayRolesInGrid()
    End Sub

End Class