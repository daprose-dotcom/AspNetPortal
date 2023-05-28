<%@ Page Title="Import a WordPress XML file" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/Admin.Master" CodeBehind="ImportWPXML.aspx.vb" Inherits="AspNetPortal.ImportWPXML" %>

<asp:Content ID="ImportWPXMLContent" ContentPlaceHolderID="MainAdminContent" runat="server">
    <h2><%: Title %></h2>
    <hr />
    <p>You can import any WordPress content from a WordPress Extended XML file.</p>

    STEP #1: Select any WordPress XML file to import:
    <br /><br /><asp:FileUpload ID="FileUpload1" runat="server" ValidateRequestMode="Enabled" />

    <br /><br />STEP #2: Import the selected WordPress XML file:
    <br /><br /><asp:Button ID="Import" runat="server" Text="Import XML File Now!" CssClass="redbtn" />

    <br /><br /><asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="Larger" ForeColor="#660066"></asp:Label>

    <br /><br />
    <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>

</asp:Content>
