<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master" CodeBehind="ManageRoles.aspx.vb" Inherits="AspNetPortal.ManageRoles" %>
<asp:Content ID="ManageRolesContent" ContentPlaceHolderID="MainAdminContent" runat="server">

    <h2>Manage Roles</h2>
    <p>
        <b>Create a New Role: </b>
        <asp:TextBox ID="RoleName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RoleNameReqField" runat="server" 
            ControlToValidate="RoleName" Display="Dynamic" 
            ErrorMessage="You must enter a role name.">
        </asp:RequiredFieldValidator>
        <asp:Button ID="CreateRoleButton" runat="server" Text="Create Role" CssClass="redbtn" />
    </p>

    <p>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server"/>
        <asp:GridView ID="RoleList" runat="server"  AutoGenerateColumns="False" Enabled="True"  Font-Bold="True" Font-Names="Verdana" Font-Size="X-Large" EmptyDataText="No Data Available." CellPadding="3" AllowSorting="True" AllowPaging="True" AlternatingRowStyle-BackColor="#FFFFCC" EmptyDataRowStyle-Wrap="False" HeaderStyle-Wrap="False" HeaderStyle-BackColor="#660066" HeaderStyle-ForeColor="White" ShowHeaderWhenEmpty="True" SelectedRowStyle-Wrap="False">
            <Columns>
                <asp:CommandField DeleteText=" Delete Role: " ShowDeleteButton="True" />
                <asp:TemplateField HeaderText="Role">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="RoleNameLabel" Text='<%# Container.DataItem %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </p>

</asp:Content>
