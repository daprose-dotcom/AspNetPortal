﻿Imports System
Imports System.Web
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Owin

Partial Public Class ForgotPassword
    Inherits System.Web.UI.Page

    Protected Property StatusMessage() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub Forgot(sender As Object, e As EventArgs)

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
                ' Validate the user's email address
                Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
                Dim user As ApplicationUser = manager.FindByName(Email.Text)
                If user Is Nothing OrElse Not manager.IsEmailConfirmed(user.Id) Then
                    FailureText.Text = "The user either does not exist or is not confirmed."
                    ErrorMessage.Visible = True
                    Return
                End If
                ' For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                ' Send email with the code and the redirect to reset password page
                Dim code = manager.GeneratePasswordResetToken(user.Id)
                Dim callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request)
                manager.SendEmail(user.Id, "Reset Password", "<p>Please reset your password by clicking <a href=""" & callbackUrl & """><b>here</b></a>.</p>")
                loginForm.Visible = False
                DisplayEmail.Visible = True
            End If

        End If

    End Sub
End Class