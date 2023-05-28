<%@ Page Title="Account Confirmation" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.vb" Inherits="AspNetPortal.Confirm" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <hr />
    <div>
        <asp:PlaceHolder ID="successPanel" runat="server" ViewStateMode="Disabled" Visible="true">
            <h2>Your account has been verified successfully</h2>
            <p>
                Thank you for confirming and validating your account. Click <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">here</asp:HyperLink> now to login and start enjoying the use of this application.
            </p>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="errorPanel" runat="server" ViewStateMode="Disabled" Visible="false">
            <h3>An error has occurred.</h3>
            <p class="text-danger">
                The verification code may have expired, if so, please request a new verification code from the Login page.
            </p>
            <p class="text-danger">
                If the problem persists, please notify the Administrator at: <b>Application("SITE_EMAIL") %></b>
            </p>

        </asp:PlaceHolder>
    </div>
</asp:Content>
