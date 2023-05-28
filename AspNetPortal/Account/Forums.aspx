<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Account/Account.Master" CodeBehind="Forums.aspx.vb" Inherits="AspNetPortal.AccountForums" %>
<%@ Import Namespace="AspNetPortal" %>
<%@ Import Namespace="Microsoft.AspNet.Identity" %>

<asp:Content ID="ForumsContent" ContentPlaceHolderID="MainMemberContent" runat="server">

    <div>
        <asp:PlaceHolder runat="server" ID="SuccessMessagePlaceHolder" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <asp:DropDownList ID="ddCategories" runat="server" OnSelectedIndexChanged="ddCategories_SelectedIndexChanged" Width="350px" Font-Bold="True" Font-Names="verdana" Font-Size="Large" AutoPostBack="True" />
    <asp:Label ID="Label1" runat="server" Text="Add Forum: "></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" Width="300px" Wrap="False"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" CssClass="redbtn" Text="Add forum" />
    <br /><br />

    <h4>Forums Available</h4>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" />
    
    <asp:GridView ID="GridView1" runat="server" AutoEventWireup="false" AutoGenerateColumns="true" emptydatatext="No data available. Select another category." DataKeyNames="ForumID" Width="100%" DataSourceID="SqlDataSource1" CellPadding="2" ForeColor="Black" GridLines="Both" AllowPaging="false" ShowFooter="false" AllowSorting="True" CellSpacing="2" SelectedRowStyle-BackColor="#0000CC" SelectedRowStyle-ForeColor="White" EnableSortingAndPagingCallbacks="False" EnablePersistedSelection="True" SelectedRowStyle-HorizontalAlign="Left" AutoGenerateSelectButton="True" ShowHeaderWhenEmpty="True" RowStyle-Wrap="True" HeaderStyle-ForeColor="#FFFFCC" HeaderStyle-BackColor="#660066">
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

