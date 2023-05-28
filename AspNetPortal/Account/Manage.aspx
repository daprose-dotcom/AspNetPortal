<%@ Page Title="Manage Account" Language="vb" AutoEventWireup="false" MasterPageFile="~/Account/Account.Master" CodeBehind="Manage.aspx.vb" Inherits="AspNetPortal.Manage" %>

<%@ Import Namespace="AspNetPortal" %>
<%@ Import Namespace="Microsoft.AspNet.Identity" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainMemberContent" runat="server">

    <div>
        <asp:PlaceHolder runat="server" ID="SuccessMessagePlaceHolder" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <h4>Your Latest Classifieds</h4>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString=""     
        SelectCommand="">
    </asp:SqlDataSource> 
    
    <asp:GridView ID="GridView1" runat="server" AutoEventWireup="true" AutoGenerateColumns="false" emptydatatext="No data available." DataKeyNames="ID" Width="100%" DataSourceID="SqlDataSource1" CellPadding="2" ForeColor="Black" GridLines="Both" AllowPaging="true" PageSize="10" ShowFooter="true" AllowSorting="False" CellSpacing="2" SelectedRowStyle-BackColor="#0000CC" SelectedRowStyle-ForeColor="White" EnableSortingAndPagingCallbacks="False" EnablePersistedSelection="False" SelectedRowStyle-HorizontalAlign="Left" AutoGenerateSelectButton="True" ShowHeaderWhenEmpty="True">

        <Columns>
            <asp:CommandField ShowEditButton="False" />
            <asp:BoundField DataField="ID" HeaderText="ID" 
                InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Country" HeaderText="COUNTRY" 
                SortExpression="Country" />
            <asp:BoundField DataField="State" HeaderText="STATE" 
                SortExpression="State" />
            <asp:BoundField DataField="City" HeaderText="CITY" 
                SortExpression="City" />
            <asp:BoundField DataField="Category" HeaderText="CATEGORY" 
                SortExpression="Category" />
            <asp:BoundField DataField="Title" HeaderText="TITLE" 
                SortExpression="Title" />
        </Columns>

        <RowStyle BackColor="#CCFFFF" ForeColor="Black" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <HeaderStyle BackColor="Maroon" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>


</asp:Content>