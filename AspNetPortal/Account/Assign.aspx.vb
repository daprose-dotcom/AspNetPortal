Imports System.Configuration
Imports System.Data.SqlClient
Public Class Assign
    Inherits System.Web.UI.Page
    Protected intAd As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim strPage = Request.ServerVariables("SCRIPT_NAME")
        Dim intRowsAffected As Integer

        intAd = CInt(Request.QueryString("id"))
        If intAd > 0 Then
            SqlDataSource1.ConnectionString = constr
            SqlDataSource1.SelectCommand = "SELECT TOP 10 a.ID,c.Country,b.State,b.City,d.Category,a.Title,a.Description FROM [AspNetPortal].[dbo].[Ads] a, [AspNetPortal].[dbo].[states] b, [AspNetPortal].[dbo].[Countries] c, [AspNetPortal].[dbo].[Categories] d WHERE a.CityID = b.CiudadID AND a.CategoryID=d.Category_Id AND b.country3 = c.country3 AND a.ID = " & Request.QueryString("id") & ""
            Dim strUPDATE As String
            strUPDATE = "UPDATE [AspNetPortal].[dbo].[Ads] SET [Hits] = [Hits] + 1 WHERE [ID] = " & intAd.ToString
            Using con As New SqlConnection(constr)

                con.Open()

                Using cmd As New SqlCommand(strUPDATE)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    intRowsAffected = cmd.ExecuteNonQuery()
                End Using

                con.Close()

            End Using
        End If
    End Sub
End Class