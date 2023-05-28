<%@ Page Title="Password reset process" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.vb" Inherits="AspNetPortal.ForgotPassword" Async="true" %>

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
    <div class="row">
        <div class="col-md-8">
            <asp:PlaceHolder id="loginForm" runat="server">
                <div class="form-horizontal">
                    <h4>Forgot your password?</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" ID="CaptchaLabel" CssClass="col-md-2 control-label">Captcha</asp:Label>
                        <div class="col-md-10">
                            <img id="imgCaptcha" src="/Recaptcha.aspx" /><br />
				            <a href="javascript:void(0)" onclick="RefreshImage('imgCaptcha')"><font color="red" face="Verdana" size="2"><b>Change Captcha</b></font></a><br />
                            <asp:Label runat="server" ID="Label1" CssClass="col-md-2 control-label">Type the captcha you see above</asp:Label>
                            <asp:TextBox runat="server" ID="CaptchaText" TextMode="SingleLine" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="CaptchaText"
                                CssClass="text-danger" ErrorMessage="The captcha field is required." />
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="Forgot" Text="Email Link" CssClass="redbtn btn-default" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">
                <p>
                    Your request to reset your password has been accepted.<br />
                    Please check your email inbox for instructions on how to reset your password.<br />
                    If you do not receive our email, (any time in the next few minutes), please check your SPAM folder and mark all our email as NOT A SPAM.<br /><hr />
                    If after several tries you still do not receive our emails, then there may be a problem with your email service provider. Please check with their support system to find out why you are not receiving our emails.
                </p>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
