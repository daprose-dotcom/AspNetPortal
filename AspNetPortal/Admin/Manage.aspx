<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master" CodeBehind="Manage.aspx.vb" Inherits="AspNetPortal.AdminManage" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="AspNetPortal" %>

<asp:Content ID="ManageAdminContent" ContentPlaceHolderID="MainAdminContent" runat="server">

    <div>
        <asp:PlaceHolder runat="server" ID="SuccessMessagePlaceHolder" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <asp:Button ID="Agents" runat="server" CssClass="redbtn" Text="Agents" OnClientClick="Agents_Clic" />
    <asp:Button ID="Visitors" runat="server" CssClass="redbtn" Text="Visitors" OnClientClick="Visitors_Clic" />
    <br /><br />
    <asp:Panel ID="VisitorsPanel" runat="server">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" />
        <asp:GridView ID="GridView1" runat="server" AutoEventWireup="true" AutoGenerateColumns="true" emptydatatext="No data available." DataKeyNames="ID" Width="100%" DataSourceID="SqlDataSource1" CellPadding="2" ForeColor="Black" GridLines="Both" AllowPaging="true" PageSize="15" ShowFooter="false" AllowSorting="True" CellSpacing="2" SelectedRowStyle-BackColor="#0000CC" SelectedRowStyle-ForeColor="White" EnableSortingAndPagingCallbacks="False" EnablePersistedSelection="False" SelectedRowStyle-HorizontalAlign="Left" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:CommandField ShowEditButton="False" />
            </Columns>

            <RowStyle BackColor="#CCFFFF" ForeColor="Black" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="Maroon" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="AgentsPanel" runat="server">
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" />
        <asp:GridView ID="GridView2" runat="server" AutoEventWireup="true" AutoGenerateColumns="true" emptydatatext="No data available." DataKeyNames="ID" Width="100%" DataSourceID="SqlDataSource2" CellPadding="2" ForeColor="Black" GridLines="Both" AllowPaging="true" PageSize="15" ShowFooter="false" AllowSorting="True" CellSpacing="2" SelectedRowStyle-BackColor="#0000CC" SelectedRowStyle-ForeColor="White" EnableSortingAndPagingCallbacks="False" EnablePersistedSelection="False" SelectedRowStyle-HorizontalAlign="Left" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:CommandField ShowEditButton="False" />
            </Columns>

            <RowStyle BackColor="#CCFFFF" ForeColor="Black" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="Maroon" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
