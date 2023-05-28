<%@ Page Title="Assign Classified to .." Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Assign.aspx.vb" Inherits="AspNetPortal.Assign" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:LoginView runat="server" ViewStateMode="Disabled">
        <AnonymousTemplate>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <table width="100%" bgcolor="darkbrown">
                <tr>
                    <td width="1%" nowrap="true"><a class="redbtn" runat="server" href="#">ADD IMAGE</a></td>
                    <td align="center"><a class="redbtn" runat="server" href="#">COPY</a></td>
                    <td align="center"><a class="redbtn" runat="server" href="#">DELETE</a></td>
                    <td align="center"><a class="redbtn" runat="server" href="#">EDIT</a></td>
                    <td align="center"><a class="redbtn" runat="server" href="#">RENEW</a></td>
                    <td width="1%" align="right"><a class="redbtn" runat="server" href="#">UNPUBLISH</a></td>
                </tr>
            </table>
        </LoggedInTemplate>
    </asp:LoginView>
    <asp:SqlDataSource id="SqlDataSource1" runat="server">
    </asp:SqlDataSource>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" Width="100%" DataSourceID="SqlDataSource1" CellPadding="20" ForeColor="#333333" GridLines="Both" AllowPaging="false" PageSize="1" ShowFooter="False" AllowSorting="false" ShowHeader="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                <h2><b><%# Eval("Title") %></b></h2>
                <b>Ad#: <%# Eval("ID") %>, Published at: <%# Eval("Country") %> - <%# Eval("State") %> - <%# Eval("City") %></b>
                <hr />
                <%# Eval("Description") %><br />
                <i><b>@<%# Eval("Category") %></b></i><br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns> 
        
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />

    </asp:GridView>

    <p align="center">
        <!-- Go to www.addthis.com/dashboard to customize your tools -->
        <div class="addthis_inline_share_toolbox"></div>
    </p>
</asp:Content>
