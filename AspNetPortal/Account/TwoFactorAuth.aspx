<%@ Page Title="Two Factor Authentication" Language="vb" AutoEventWireup="false" MasterPageFile="~/Account/Account.Master" CodeBehind="TwoFactorAuth.aspx.vb" Inherits="AspNetPortal.TwoFactorAuth" %>
<asp:Content ID="TwoFactorContent" ContentPlaceHolderID="MainMemberContent" runat="server">
    <dt><h4>Two-Factor Authentication:</h4></dt>
        <dd>
            <p>
                There are no two-factor authentication providers configured. See <a href="https://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                for details on setting up this ASP.NET application to support two-factor authentication.
            </p>                        
            <% If TwoFactorEnabled Then %>
            Enabled
            <asp:LinkButton Text="[Disable]" runat="server" CommandArgument="false" OnClick="TwoFactorDisable_Click" />
            <% Else %>
            Disabled
            <asp:LinkButton Text="[Enable]" CommandArgument="true" OnClick="TwoFactorEnable_Click" runat="server" />
            <% End If %>
        </dd>
    </dt>

</asp:Content>
