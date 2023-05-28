Imports System
Imports System.Data.SqlClient
Imports System.Web
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Owin

Public Class PostClassified
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack Then

            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim appUser = manager.FindById(User.Identity.GetUserId())
            Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

            Dim strINSERT As String = ""
            Dim strSELECT As String = ""
            Dim intRowsAffected As Integer = 0
            Dim bSuccess As Boolean = False
            Dim strAdID As Integer = 0

            Dim strOwnerID As String = appUser.Id
            Dim strOwnerIP As String = Request.ServerVariables("REMOTE_ADDR")
            Dim strTitle As String = Request.Form("pTitle")
            Dim strContent As String = Request.Form("pContent")
            Dim strSEODesc As String = Request.Form("pSeoDesc")
            Dim strSEOAbs As String = Request.Form("pSeoAbs")
            Dim strContact As String = Request.Form("pContact")
            Dim strEmail As String = Request.Form("pEmail")
            Dim strPhone As String = Request.Form("pPhone")
            Dim strAddress As String = Request.Form("pAddress")
            Dim strQuantity As String = Request.Form("pQuantity")
            Dim strPrice As String = Request.Form("pPrice")
            Dim strLink As String = Request.Form("pLink")
            Dim strIsPublised As String = Request.Form("pIsPublished")
            Dim strIsTwittered As String = Request.Form("pIsTwittered")
            Dim strIsCommented As String = Request.Form("pIsCommented")
            Dim strIsNotified As String = Request.Form("pIsNotified")
            Dim strIsSigned As String = Request.Form("pIsSigned")
            Dim strTracker As String = GetRandomString(32)

            strINSERT = "INSERT INTO [AspNetPortal].[dbo].[Ads] ("
            strINSERT = strINSERT & " [OwnerID]"
            strINSERT = strINSERT & ",[OwnerIP]"
            strINSERT = strINSERT & ",[Title]"
            strINSERT = strINSERT & ",[Description]"
            strINSERT = strINSERT & ",[SEODescription]"
            strINSERT = strINSERT & ",[SEOAbstrat]"
            strINSERT = strINSERT & ",[Contact]"
            strINSERT = strINSERT & ",[Email]"
            strINSERT = strINSERT & ",[Phone]"
            strINSERT = strINSERT & ",[Address]"
            strINSERT = strINSERT & ",[Quantity]"
            strINSERT = strINSERT & ",[Price]"
            strINSERT = strINSERT & ",[Link]"
            strINSERT = strINSERT & ",[IsPublished]"
            strINSERT = strINSERT & ",[IsTwittered]"
            strINSERT = strINSERT & ",[IsCommented]"
            strINSERT = strINSERT & ",[IsNotified]"
            strINSERT = strINSERT & ",[IsSigned]"
            strINSERT = strINSERT & ",[Tracker]"
            strINSERT = strINSERT & ") VALUES ("
            strINSERT = strINSERT & "'" & strOwnerID & "'"
            strINSERT = strINSERT & ",'" & strOwnerIP & "'"
            strINSERT = strINSERT & ",'" & strTitle & "'"
            strINSERT = strINSERT & ",'" & strContent & "'"
            strINSERT = strINSERT & ",'" & strSEODesc & "'"
            strINSERT = strINSERT & ",'" & strSEOAbs & "'"
            strINSERT = strINSERT & ",'" & strContact & "'"
            strINSERT = strINSERT & ",'" & strEmail & "'"
            strINSERT = strINSERT & ",'" & strPhone & "'"
            strINSERT = strINSERT & ",'" & strAddress & "'"
            strINSERT = strINSERT & ",'" & strQuantity & "'"
            strINSERT = strINSERT & ",'" & strPrice & "'"
            strINSERT = strINSERT & ",'" & strLink & "'"
            strINSERT = strINSERT & "," & strIsPublised & ""
            strINSERT = strINSERT & "," & strIsTwittered & ""
            strINSERT = strINSERT & "," & strIsCommented & ""
            strINSERT = strINSERT & "," & strIsNotified & ""
            strINSERT = strINSERT & "," & strIsSigned & ""
            strINSERT = strINSERT & ",'" & strTracker & "'"
            strINSERT = strINSERT & ")"

            FailureText.Text = "<h3>" & strINSERT & "</h3>"
            ErrorMessage.Visible = True

            Using con As New SqlConnection(constr)
                con.Open()

                Using cmd As New SqlCommand(strINSERT)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    intRowsAffected = cmd.ExecuteNonQuery()
                End Using

                FailureText.Text = FailureText.Text & "<h3>Classified successfully added to database.</h3>"
                ErrorMessage.Visible = True
                bSuccess = True

                '## Find Post
                strSELECT = "SELECT [ID] FROM [AspNetPortal].[dbo].[Ads] WHERE [Tracker] = '" & strTracker & "'"
                Using cmd As New SqlCommand(strSELECT)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    Dim reader As SqlDataReader
                    reader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        While reader.Read()
                            strAdID = CInt(reader.Item(0))
                        End While
                    End If
                    reader.Close()
                End Using

                con.Close()

            End Using
            If strAdID > 0 Then
                If bSuccess Then Response.Redirect("~/Account/Assign.aspx?id=" & strAdID)
            Else
                FailureText.Text = FailureText.Text & "<h3>CAN'T FIND THE POST ID<br>PLEASE NOTIFY THE ADMINISTRATOR</h3>"
                ErrorMessage.Visible = True
            End If

            FailureText.Text = FailureText.Text & "<h3>ERROR ATTEMPTING TO REDIRECT<br>PLEASE NOTIFY THE ADMINISTRATOR</h3>"
            ErrorMessage.Visible = True

        End If
    End Sub

    Protected Sub AddClassified_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim appUser = manager.FindById(User.Identity.GetUserId())
        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        Dim strINSERT As String = ""
        Dim strSELECT As String = ""
        Dim intRowsAffected As Integer = 0
        Dim bSuccess As Boolean = False
        Dim strAdID As Integer = 0

        Dim strOwnerID As String = appUser.Id
        Dim strOwnerIP As String = Request.ServerVariables("REMOTE_ADDR")
        Dim strTitle As String = Request.Form("pTitle")
        Dim strDesc As String = Request.Form("pContent")
        Dim strSEODesc As String = Request.Form("pSeoDesc")
        Dim strSEOAbs As String = Request.Form("pSeoAbs")
        Dim strContact As String = Request.Form("pContact")
        Dim strEmail As String = Request.Form("pEmail")
        Dim strPhone As String = Request.Form("pPhone")
        Dim strAddress As String = Request.Form("pAddress")
        Dim strQuantity As String = Request.Form("pQuantity")
        Dim strPrice As String = Request.Form("pPrice")
        Dim strLink As String = Request.Form("pLink")
        Dim strIsPublised As String = Request.Form("pIsPublished")
        Dim strIsTwittered As String = Request.Form("pIsTwittered")
        Dim strIsCommented As String = Request.Form("pIsCommented")
        Dim strIsNotified As String = Request.Form("pIsNotified")
        Dim strIsSigned As String = Request.Form("pIsSigned")
        Dim strTracker As String = GetRandomString(32)

        strINSERT = "INSERT INTO [AspNetPortal].[dbo].[Ads] ("
        strINSERT = strINSERT & " [OwnerID]"
        strINSERT = strINSERT & ",[OwnerIP]"
        strINSERT = strINSERT & ",[Title]"
        strINSERT = strINSERT & ",[Description]"
        strINSERT = strINSERT & ",[SEODescription]"
        strINSERT = strINSERT & ",[SEOAbstrat]"
        strINSERT = strINSERT & ",[Contact]"
        strINSERT = strINSERT & ",[Email]"
        strINSERT = strINSERT & ",[Phone]"
        strINSERT = strINSERT & ",[Address]"
        strINSERT = strINSERT & ",[Quantity]"
        strINSERT = strINSERT & ",[Price]"
        strINSERT = strINSERT & ",[Link]"
        strINSERT = strINSERT & ",[IsPublished]"
        strINSERT = strINSERT & ",[IsTwittered]"
        strINSERT = strINSERT & ",[IsCommented]"
        strINSERT = strINSERT & ",[IsNotified]"
        strINSERT = strINSERT & ",[IsSigned]"
        strINSERT = strINSERT & ",[Tracker]"
        strINSERT = strINSERT & ") VALUES ("
        strINSERT = strINSERT & "'" & strOwnerID & "'"
        strINSERT = strINSERT & ",'" & strOwnerIP & "'"
        strINSERT = strINSERT & ",'" & strTitle & "'"
        strINSERT = strINSERT & ",'" & strDesc & "'"
        strINSERT = strINSERT & ",'" & strSEODesc & "'"
        strINSERT = strINSERT & ",'" & strSEOAbs & "'"
        strINSERT = strINSERT & ",'" & strContact & "'"
        strINSERT = strINSERT & ",'" & strEmail & "'"
        strINSERT = strINSERT & ",'" & strPhone & "'"
        strINSERT = strINSERT & ",'" & strAddress & "'"
        strINSERT = strINSERT & ",'" & strQuantity & "'"
        strINSERT = strINSERT & ",'" & strPrice & "'"
        strINSERT = strINSERT & ",'" & strLink & "'"
        strINSERT = strINSERT & "," & strIsPublised & ""
        strINSERT = strINSERT & "," & strIsTwittered & ""
        strINSERT = strINSERT & "," & strIsCommented & ""
        strINSERT = strINSERT & "," & strIsNotified & ""
        strINSERT = strINSERT & "," & strIsSigned & ""
        strINSERT = strINSERT & ",'" & strTracker & "'"
        strINSERT = strINSERT & ")"

        FailureText.Text = "<h3>" & strINSERT & "</h3>"
        ErrorMessage.Visible = True

        Using con As New SqlConnection(constr)

            con.Open()
            Try

                Using cmd As New SqlCommand(strINSERT)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    intRowsAffected = cmd.ExecuteNonQuery()
                End Using

                FailureText.Text = FailureText.Text & "<h3>Add successfully added<br>You will now be transfered to the EDIT process!</h3>"
                ErrorMessage.Visible = True

                bSuccess = True

            Catch ex As Exception

                FailureText.Text = FailureText.Text & "<h3>ERROR ATTEMPTING TO ADD THIS CLASSIFIED<br>PLEASE NOTIFY THE ADMINISTRATOR<br>" & ex.Message & "</h3>"
                ErrorMessage.Visible = True

            End Try

            '## Find Post
            strSELECT = "SELECT [ID] FROM [AspNetPortal].[dbo].[Ads] WHERE [Tracker] = '" & strTracker & "'"
            Using cmd As New SqlCommand(strSELECT)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                Dim reader As SqlDataReader
                reader = cmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read()
                        strAdID = CInt(reader.Item(0))
                    End While
                End If
                reader.Close()
            End Using

            con.Close()

        End Using
        If strAdID > 0 Then
            If bSuccess Then Response.Redirect("Assign.aspx?id=" & strAdID)
        Else
            FailureText.Text = FailureText.Text & "<h3>CAN'T FIND THE POST ID<br>PLEASE NOTIFY THE ADMINISTRATOR</h3>"
            ErrorMessage.Visible = True
        End If

        FailureText.Text = FailureText.Text & "<h3>ERROR ATTEMPTING TO REDIRECT<br>PLEASE NOTIFY THE ADMINISTRATOR</h3>"
        ErrorMessage.Visible = True

    End Sub

    Protected Function GetRandomString(myLength As Integer) As String

        Dim strPW As String = ""
        Const minLength = 24
        Const maxLength = 32
        Dim X, Y

        Randomize()

        If myLength = 0 Then
            myLength = Int((maxLength * Rnd()) + minLength)
        End If
        For X = 1 To myLength
            'Randomize the type of this character
            Y = Int((3 * VBMath.Rnd()) + 1)

            ' (1) Numeric, (2) Uppercase, (3) Lowercase
            Select Case Y
                Case 1
                    'Randomize
                    strPW = strPW & Chr(Int((9 * VBMath.Rnd()) + 48))
                Case 2
                    'Randomize
                    strPW = strPW & Chr(Int((25 * VBMath.Rnd()) + 65))
                Case 3
                    'Randomize
                    strPW = strPW & Chr(Int((25 * VBMath.Rnd()) + 97))
            End Select
        Next
        GetRandomString = strPW

    End Function

End Class