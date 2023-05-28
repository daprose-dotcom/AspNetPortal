Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Owin

Partial Public Class Register
    Inherits Page
    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)

        '// validate the Captcha to check we're not dealing with a bot
        Dim isHuman As Boolean = Session(Application("SESSION_CAPTCHA")) = CaptchaText.Text
        '// clear previous user input
        CaptchaText.Text = ""
        If Not isHuman Then

            '// TODO: Captcha validation failed, show error message  
            ErrorMessage.Text = "<h3>Captcha verification failed!<br>Captcha is case sensitive. Please try again!</h3>"
            ErrorMessage.Visible = True
            Session(Application("SESSION_CAPTCHA")) = ""

        Else

            Dim userName As String = Email.Text.ToLower
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim signInManager = Context.GetOwinContext().Get(Of ApplicationSignInManager)()
            Dim user = New ApplicationUser() With {.UserName = userName, .Email = userName}
            Dim result = manager.Create(user, Password.Text)
            If result.Succeeded Then

                ' For more information on how to enable
                ' account confirmation and password reset
                ' please visit https://go.microsoft.com/fwlink/?LinkID=320771

                Dim code = manager.GenerateEmailConfirmationToken(user.Id)
                Dim callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request)

                manager.SendEmail(user.Id, "Account confirmation required", "<p>Please confirm your account by clicking <a href=""" & callbackUrl & """><b>here</b></a> now!</p>")

                'signInManager.SignIn(user, isPersistent:=False, rememberBrowser:=False)

                ErrorMessage.Text = "<h3><font color='red'>A confirmation and verification e-mail was sent to <b>" & userName & "</b><br>Please check your e-mail inbox for more instructions.</font></h3><br><hr><br>"

                'IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)

            Else

                ErrorMessage.Text = result.Errors.FirstOrDefault()

            End If
        End If

    End Sub
End Class

