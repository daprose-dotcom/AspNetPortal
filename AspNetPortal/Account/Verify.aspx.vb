Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Owin

Partial Public Class Verify
    Inherits Page

    Protected Property StatusMessage() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Intro.Text = "<h4><b>Need to verify your account?</b></h4>"
        Intro.Text = Intro.Text & "<p>An e-mail to verify your account was already sent to you during your registration process.</p>"
        Intro.Text = Intro.Text & "<p>Before you start a new verification process, please check your e-mail inbox and click on the URL provided in that e-mail.</p>"
        Intro.Text = Intro.Text & "<p>If you did not received that e-mail, then type your e-mail address in the box provided below and click on the <b>Email Link</b> button. We will send you (via e-mail) a link with a new verification code to verify your account immediatelly!</p>"
        Intro.Text = Intro.Text & "<p>Please do not start this process repeatedly as a new request will invalidate the previous verification code. If you can't find the e-mail in your inbox, please check your SPAM folder and mark our e-mails as NOT A SPAM!</p>"
        Intro.Text = Intro.Text & "<hr />"
        IntroMessage.Visible = True

    End Sub

    Protected Sub VerifyAccount(sender As Object, e As EventArgs)

        ' This doen't count login failures towards account lockout
        ' To enable password failures to trigger lockout, change to shouldLockout := True

        '// validate the Captcha to check we're not dealing with a bot
        Dim isHuman As Boolean = Session(Application("SESSION_CAPTCHA")) = CaptchaText.Text
        '// clear previous user input
        CaptchaText.Text = ""
        If Not isHuman Then

            '// TODO: Captcha validation failed, show error message  
            FailureText.Text = "<h3>Captcha verification failed!<br>Captcha is case sensitive. Please try again!</h3>"
            ErrorMessage.Visible = True
            Session(Application("SESSION_CAPTCHA")) = ""

        Else
            If IsValid Then

                IntroMessage.Visible = False
                loginForm.Visible = False

                Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
                Dim user As ApplicationUser = manager.FindByName(Email.Text)
                If user Is Nothing Then
                    FailureText.Text = "<h3>The email account provided does not exist in our records!</h3>"
                    ErrorMessage.Visible = True
                    Return
                End If

                ' For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                Dim code = manager.GenerateEmailConfirmationToken(user.Id)
                Dim callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request)

                manager.SendEmail(user.Id, "Account confirmation required", "<p>Please confirm your account by clicking <a href=""" & callbackUrl & """><b>here</b></a> now!</p>")
                'manager.SendEmail(user.Id, "Account confirmation required", "<p>Please confirm your account by clicking <a href=""" & callbackUrl & """><b>here</b></a> now!</p>")

                FailureText.Text = "<h3><font color='red'>A confirmation e-mail was sent to <b>" & Email.Text & "</b><br>Please check your e-mail inbox for more instructions.</font></h3><hr>"
                ErrorMessage.Visible = True
                DisplayEmail.Visible = True

                'IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)

            End If
        End If

    End Sub
End Class