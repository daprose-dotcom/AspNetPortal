Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Partial Public Class Login
    Inherits Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        RegisterHyperLink.NavigateUrl = "Register"
        ForgotPasswordHyperLink.NavigateUrl = "Forgot"
        VerifyAccountHyperLink.NavigateUrl = "Verify"
        OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
        Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        If Not [String].IsNullOrEmpty(returnUrl) Then
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
        End If

    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        If IsValid Then

            ' This doen't count login failures towards account lockout
            ' To enable password failures to trigger lockout, change to shouldLockout := True

            Dim userName As String = Email.Text
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim signInManager = Context.GetOwinContext().Get(Of ApplicationSignInManager)()

            Dim User = manager.FindByEmail(userName)
            If User IsNot Nothing Then

                Dim result = manager.IsEmailConfirmed(User.Id)
                If result = True Then

                    'Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()
                    Dim myresult = signInManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout:=True)

                    Select Case myresult
                        Case SignInStatus.Success
                            If userName.ToLower = "support@aspnetportal.xyz" Then
                                Session("IsAdmin") = True
                            Else
                                Session("IsAdmin") = False
                            End If
                            IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                            Exit Select
                        Case SignInStatus.LockedOut
                            Response.Redirect("/Account/Lockout")
                            Exit Select
                        Case SignInStatus.RequiresVerification
                            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                            Request.QueryString("ReturnUrl"),
                            RememberMe.Checked),
                            True)
                            Exit Select
                        Case Else

                            Session("LoginFails") = Session("LoginFails") + 1
                            FailureText.Text = "Invalid login attempt"
                            ErrorMessage.Visible = True
                            Exit Select

                    End Select

                Else

                    FailureText.Text = "<h3>Your account has not been verified as yet!<br>Please complete the verification process to be able to login!</h3><br><hr><br>"
                    ErrorMessage.Visible = True

                End If

            Else

                Session("LoginFails") = Session("LoginFails") + 1
                FailureText.Text = "<h3>We do not have an account registered with the e-mail provided! Please register as a new user first to be able to login!</h3><br><hr><br>"
                ErrorMessage.Visible = True

            End If

        End If
    End Sub
End Class
