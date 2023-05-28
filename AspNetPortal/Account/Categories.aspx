<%@ Page Title="Categories" Language="vb" AutoEventWireup="false" MasterPageFile="~/Account/Account.Master" CodeBehind="Categories.aspx.vb" Inherits="AspNetPortal.Categories" %>
<%@ Import Namespace="AspNetPortal" %>
<%@ Import Namespace="Microsoft.AspNet.Identity" %>

<asp:Content ID="AccountCategories" ContentPlaceHolderID="MainMemberContent" runat="server">

    <div>
        <asp:PlaceHolder runat="server" ID="SuccessMessagePlaceHolder" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Add Category: "></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" CssClass="redbtn" Text="Add category" />
    <br /><br />

    <h4>Categories Available</h4>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" />
    
    <asp:GridView ID="GridView1" runat="server" AutoEventWireup="false" AutoGenerateColumns="true" emptydatatext="No data available." DataKeyNames="CategoryID" Width="100%" DataSourceID="SqlDataSource1" CellPadding="2" ForeColor="Black" GridLines="Both" AllowPaging="true" PageSize="100" ShowFooter="false" AllowSorting="True" CellSpacing="2" SelectedRowStyle-BackColor="#0000CC" SelectedRowStyle-ForeColor="White" EnableSortingAndPagingCallbacks="False" EnablePersistedSelection="True" SelectedRowStyle-HorizontalAlign="Left" AutoGenerateSelectButton="True" ShowHeaderWhenEmpty="True">
        <Columns>
        </Columns>
        <RowStyle BackColor="#CCFFFF" ForeColor="Black" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <HeaderStyle BackColor="Maroon" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>

</asp:Content>
