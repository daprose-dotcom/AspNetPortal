Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient

Public Class StaticCache

    Public Shared catsHT As New Hashtable
    Public Shared forumsHT As New Hashtable

    Public Shared Sub LoadStaticCache()
        GetAllCategories()
        GetAllForums()
    End Sub

    Public Shared Sub GetAllCategories()

        ' Get categories in cache
        Dim strSELECT As String = ""
        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        catsHT.Clear()

        Using con As New SqlConnection(constr)
            con.Open()
            strSELECT = "SELECT [CAT_ID],[CAT_NAME] FROM [AspNetPortal].[dbo].[FORUM_CATEGORY] WHERE [CAT_STATUS] = 1"
            Using cmd As New SqlCommand(strSELECT)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                Dim reader As SqlDataReader
                reader = cmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read()
                        catsHT.Add(CInt(reader.Item(0)), reader.Item(1))
                    End While
                End If
                reader.Close()
                cmd.Dispose()
            End Using
            con.Close()
        End Using
    End Sub

    Public Shared Sub GetAllForums()
        ' Get forums in cache
        Dim strSELECT As String = ""
        Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        forumsHT.Clear()

        Using con As New SqlConnection(constr)
            con.Open()
            strSELECT = "SELECT [FORUM_ID],[F_SUBJECT] FROM [AspNetPortal].[dbo].[FORUM_FORUM] WHERE [F_STATUS] = 1 ORDER BY [FORUM_ID]"
            Using cmd As New SqlCommand(strSELECT)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                Dim reader As SqlDataReader
                reader = cmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read()
                        forumsHT.Add(CInt(reader.Item(0)), reader.Item(1))
                    End While
                End If
                reader.Close()
                cmd.Dispose()
            End Using
            con.Close()
        End Using
    End Sub

End Class
