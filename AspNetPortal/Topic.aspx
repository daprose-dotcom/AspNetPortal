<%@ Page Title="Forums" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Topic.aspx.vb" Inherits="AspNetPortal.Topic" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a runat="server" href="~/">All Categories</a></li>
        <li class="breadcrumb-item"><asp:HyperLink ID="CategoryLink" runat="server" NavigateUrl="" Text="" /></li>
        <li class="breadcrumb-item" aria-current="page"><a runat="server" href="#"><asp:label ID="ForumName" runat="server" Text=""></asp:label></a></li>
      </ol>
    </nav>

    <div class="row">

        <div class="col-md-8">
            <div class="alert alert-primary">
                <h2><strong><%= strTopicName %></strong></h2>
                <hr />
                <p class="black">
                    <%= strTopicContent %>
                </p>
                <hr />
                <p>
                    <a  runat="server" class="greenbtn btn-default" href="~/#">Reply</a>
                </p>
            </div>
        </div>

        <div class="col-md-4">
            <asp:SqlDataSource id="SqlDataSource1" runat="server">
            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="TOPIC_ID" Width="100%" DataSourceID="SqlDataSource1" CellPadding="2" ForeColor="#333333" GridLines="Both" AllowPaging="false" PageSize="1" ShowFooter="False" AllowSorting="True" ShowHeader="True" UseAccessibleHeader="True" RowStyle-Wrap="True" SelectedRowStyle-Wrap="True" TabIndex="1" HeaderStyle-Wrap="True" HeaderStyle-CssClass="CategoryBox" ShowHeaderWhenEmpty="True" EmptyDataText="<b>No Data Available. Try Later.</b>">
                <Columns>
                    <asp:TemplateField HeaderText="" ShowHeader="True"  HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#660066" HeaderStyle-ForeColor="#FFFFCC" ControlStyle-Font-Size="Medium" ControlStyle-Font-Names="Verdana,Arial" ControlStyle-Font-Bold="false">
                        <ItemTemplate>
                        &nbsp;<i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;<a href="/Topic.aspx?TopicID=<%# Eval("TOPIC_ID") %>&ForumID=<%=intForumID %>&CatID=<%= IntCatID %>"><b><%# Eval("T_SUBJECT") %></b></a>&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns> 
        
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="True" Font-Size="Large" Font-Names="Verdana" Font-Bold="False" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="False" ForeColor="Navy" />
                <AlternatingRowStyle BackColor="#FFCCFF"  Wrap="True" Font-Size="Large" Font-Names="Verdana" Font-Bold="False" ForeColor="Black" />
            </asp:GridView>
        </div>

        <div class="row">
        </div> 
    </div>

    <br />

</asp:Content>
