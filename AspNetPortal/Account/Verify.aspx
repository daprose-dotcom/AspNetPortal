<%@ Page Title="Account verification process" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Verify.aspx.vb" Inherits="AspNetPortal.Verify" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<script language="javascript">
<!--
    function RefreshImage(valImageId) {
        var objImage = document.images[valImageId];
        if (objImage == undefined) {
            return;
        }
        var now = new Date();
        objImage.src = objImage.src.split('?')[0] + '?x=' + now.toUTCString();
        //objImage.src = objImage.src.split('?')[0] + '?x=' + now.toUTCString() + '&s=' + "<%=Session.SessionID%>";;
    }
// -->
</script>

    <h2><%: Title %></h2>
    <hr />

    <asp:PlaceHolder runat="server" ID="IntroMessage" Visible="false">
        <p class="text-danger">
            <asp:Literal runat="server" ID="Intro" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
        <p class="text-danger">
            <asp:Literal runat="server" ID="FailureText" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">
        <p class="text-info">Please check your e-mail inbox to verify your account.</p>
    </asp:PlaceHolder>

    <div class="row">
        <div class="col-md-8">
            <asp:PlaceHolder id="loginForm" runat="server" Visible="true">

                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="CaptchaLabel" CssClass="col-md-2 control-label">Captcha Image</asp:Label>
                        <div class="col-md-10">
                            <img id="imgCaptcha" src="/Recaptcha.aspx" width="280" /><br />
                            <a href="javascript:void(0)" onclick="RefreshImage('imgCaptcha')"><font color="red" face="Verdana" size="2"><b>Change Captcha</b></font></a><br />
                            <asp:Label runat="server" ID="Label1" CssClass="col-md-2 control-label">Type the captcha you see above</asp:Label>
                            <asp:TextBox runat="server" ID="CaptchaText" TextMode="SingleLine" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="CaptchaText"
                                CssClass="text-danger" ErrorMessage="The captcha field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="VerifyAccount" Text="Email Link" CssClass="redbtn btn-default" />
                        </div>
                    </div>
                </div>

            </asp:PlaceHolder>
        </div>
    </div>

</asp:Content>
