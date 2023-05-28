<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master" CodeBehind="UsersAndRoles.aspx.vb" Inherits="AspNetPortal.UsersAndRoles" %>
<asp:Content ID="UsersAndRolesContent" ContentPlaceHolderID="MainAdminContent" runat="server">

    <h2>User Role Management</h2>
    <p align="center">
        <asp:Label ID="ActionStatus" runat="server" CssClass="Important"></asp:Label>
    </p>
    <h3>Manage Roles By User</h3>
    <p>
        <b>Select a User:</b><br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"/>
        <asp:DropDownList ID="UserList" runat="server" AutoPostBack="true" Width="300px">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Repeater ID="UsersRoleList" runat="server">
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="true" Text='<%# Container.DataItem %>' OnCheckedChanged="RoleCheckBox_CheckChanged" />
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </p>
    
    <h3>Manage Users By Role</h3>
    <p>
        <b>Select a Role:</b>
        <asp:DropDownList ID="RoleList" runat="server" AutoPostBack="true">
        </asp:DropDownList>
    </p>
    <p>
        <asp:GridView ID="RolesUserList" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="No users belong to this role." CellPadding="2" Font-Bold="True" Font-Size="Large" HeaderStyle-BackColor="#660066" HeaderStyle-ForeColor="White" HeaderStyle-Wrap="False" RowStyle-Wrap="False" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                <asp:TemplateField HeaderText="Users">
                    <ItemTemplate>
                        <asp:Label runat="server" id="UserNameLabel" Text='<%# Container.DataItem %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <b>UserName:</b> <asp:TextBox ID="UserNameToAddToRole" runat="server"></asp:TextBox> <asp:Button ID="AddUserToRoleButton" runat="server" Text="Add User to Role" CssClass="redbtn" />
    </p>

</asp:Content>
