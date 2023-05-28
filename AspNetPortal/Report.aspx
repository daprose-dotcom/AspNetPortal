<%@ Page Title="Report This Ad" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Report.aspx.vb" Inherits="AspNetPortal.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript">
<!--
    function RefreshImage(valImageId) {
        var objImage = document.images[valImageId];
        if (objImage == undefined) {
            return;
        }
        var now = new Date();
        //objImage.src = objImage.src.split('?')[0] + '?x=' + now.toUTCString();
        objImage.src = objImage.src.split('?')[0] + '?x=' + now.toUTCString() + '&s=' + "<%=Session.SessionID%>";;
    }
// -->
</script>
    <h2><%: Title %></h2>
    <hr />
    <p>This area is now under contruction!</p>

    <div class="form-group">
        <asp:Label runat="server" ID="CaptchaLabel" CssClass="col-md-2 control-label">Captcha Image</asp:Label>
        <div class="col-md-10">
            <img id="imgCaptcha" src="http://AspNetPortal.XYZ/captcha/captcha.asp?s=<%= Session.SessionID %>" /><br />
			<a runat="server" href="javascript:void(0)" onclick="RefreshImage('imgCaptcha')"><font color="red" face="Verdana" size="2"><b>Change Captcha</b></font></a><br />
            <asp:Label runat="server" ID="Label1" CssClass="col-md-2 control-label">Type the captcha you see above</asp:Label>
            <asp:TextBox runat="server" ID="CaptchaText" TextMode="SingleLine" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="CaptchaText"
                CssClass="text-danger" ErrorMessage="The captcha field is required." />
        </div>
    </div>

</asp:Content>
