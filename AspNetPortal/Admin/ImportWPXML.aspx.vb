Imports System.IO
Imports System.Xml

Public Class ImportWPXML

    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Not Directory.Exists(Server.MapPath("~/") & "Uploads") Then
                Try
                    Directory.CreateDirectory(Server.MapPath("~/") & "Uploads")
                Catch ex As Exception
                    Label1.Text = "Can not create the Uploads folder!<br>" & ex.Message & "<br>Make sure the Uploads folder exists and it has full write priviledges!"
                    Return
                End Try
            End If

            If Not Directory.Exists(Server.MapPath("~/") & "Uploads/Xml") Then
                Try
                    Directory.CreateDirectory(Server.MapPath("~/") & "Uploads/Xml")
                Catch ex As Exception
                    Label1.Text = "Can not create the Uploads/Xml folder!<br>" & ex.Message & "<br>Make sure the Uploads/Xml folder exists and it has full write priviledges!"
                    Return
                End Try
            End If

        Else

            Dim i As Integer
            Dim fileOK As Boolean = False
            Dim path As String = Server.MapPath("~/Uploads/Xml/")
            Dim strFileToSave As String

            If (FileUpload1.HasFile) Then

                Dim fileExtension As String = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower()
                Dim allowedExtensions() As String = {".xml", ".gif", ".png", ".jpeg", ".jpg"}

                For i = 0 To allowedExtensions.Length - 1
                    If fileExtension = allowedExtensions(i) Then
                        fileOK = True
                        Exit For
                    End If
                Next

                If (fileOK) Then

                    strFileToSave = path & FileUpload1.FileName

                    Try

                        If File.Exists(strFileToSave) Then
                            File.Delete(strFileToSave)
                        End If

                        FileUpload1.PostedFile.SaveAs(strFileToSave)
                        Label1.Text = FileUpload1.FileName & "<br>File uploaded successfully!"

                    Catch ex As Exception
                        Label1.Text = FileUpload1.FileName & "<br>File could not be uploaded.<br>" & ex.Message
                    End Try

                    Try
                        ImportXML(strFileToSave)
                    Catch ex As Exception
                        Label1.Text = "ERROR WHILE IMPORTING XML FILE.<br>" & ex.Message
                    End Try
                Else
                    Label1.Text = "Cannot accept files of this type (<b>" & fileExtension & "</b>)."
                End If
            End If
        End If

    End Sub

    Function GetOwnerID(strPostOwner) As String

        Dim strOwnerID As String = ""
        Dim strSELECT As String = "SELECT [UserID] FROM [aspUsers] WHERE LOWER([UserName]) = '" & LCase(strPostOwner) & "'"

        'Set rsOwner = Conn.Execute(strSELECT)
        'If Not rsOwner.EOF Then
        'strOwnerID = rsOwner("UserID")
        'Else
        'strUserEmail = LCase(strPostOwner) & "@" & LCase(strSiteName)
        'strINSERT = "INSERT INTO [Users] ([UserName],[UserEmail]) VALUES (strPostOwner,strUserEmail)"
        'Conn.Execute(strINSERT)
        'strOwnerID = GetOwnerID(strPostOwner)
        'End If
        'rsOwner.Close
        'rsOwner = Nothing

        GetOwnerID = strOwnerID

    End Function

    Function InsertPost(strOwner, strType, strTitle, strDesc)

        Dim strDBTitle
        Dim strDBDesc
        Dim IPostID
        Dim rsPosts
        Dim iOwnerID
        Dim strSELECT
        Dim strINSERT

        strDBTitle = Replace(strTitle, "'", "''")
        strDBDesc = Replace(strDesc, "'", "''")

        IPostID = CInt(0)
        iOwnerID = CInt(0)

        '## Is Post already inserted? If So, Return Post ID Only
        If strType = "post" Then
            strSELECT = "SELECT [ID] FROM [Posts] WHERE (LOWER(Post_Name) = '" & LCase(strDBTitle) & "')"
        Else
            strSELECT = "SELECT [ID] FROM [Pages] WHERE (LOWER(Page_Name) = '" & LCase(strDBTitle) & "')"
        End If

        'Response.Write "<br>" & strSELECT
        'Set rsPosts = Conn.Execute(strSELECT)
        'If Err.Number > 0 Then
        'Response.Write "<br>" & Err.Description
        'Response.End
        '    Err.Clear()
        'End If

        'Response.Write "<br>I AM HERE"

        'If Not rsPosts.EOF Then
        'IPostID = CInt(rsPosts("ID"))
        ''Response.Write "<br>1.- iPostID = " & cStr(iPostID)
        'Else

        'Response.Write "<br>2.- I AM HERE"
        '## Find the Post Owner ID
        iOwnerID = GetOwnerID(strOwner)
        'Response.Write "<br>iOwnerID = " & cStr(iOwnerID)

        If iOwnerID > 0 Then
                If strType = "post" Then

                    strINSERT = "INSERT INTO [Posts] ([Post_Owner],[Post_Name],[Post_Content]) "
                    strINSERT = strINSERT & " VALUES "
                    strINSERT = strINSERT & " (" & CStr(iOwnerID) & ",'" & strDBTitle & "','" & strDBDesc & "');"

                Else

                    strINSERT = "INSERT INTO [Pages] ([Page_Owner],[Page_Name],[Page_Content]) "
                    strINSERT = strINSERT & " VALUES "
                    strINSERT = strINSERT & " (" & CStr(iOwnerID) & ",'" & strDBTitle & "','" & strDBDesc & "');"

                End If

            'Response.Write "<br>" & strINSERT
            'Conn.Execute(strINSERT)
            IPostID = InsertPost(strOwner, strType, strTitle, strDesc)

            'End If
        End If
        'rsPosts.Close
        'Set rsPosts = Nothing

        InsertPost = IPostID

    End Function

    Sub InsertCategory(iPostID, strCat)

        Dim strSELECT
        Dim strINSERT
        Dim strDBCat
        Dim intCatID
        Dim rsCategories
        Dim rsXref

        'Response.Write "<br>" & iPostID & " .. " & strCat
        strDBCat = Left(Replace(strCat, "'", "''"), 50)
        strSELECT = "SELECT * FROM [AspNetPortal].[dbo].[Categories] WHERE (LOWER([Category_Name]) = '" & LCase(strDBCat) & "')"

        'Set rsCategories = Conn.Execute(strSELECT)
        'If Not rsCategories.EOF Then
        'Response.Write "<br>INSERTING XREF = " & iPostID
        'intCatID = rsCategories("Category_ID")
        strSELECT = "SELECT * FROM [xRef] WHERE ([Category_ID] = " & intCatID & " AND [Post_ID] = " & iPostID & ")"
        'Response.Write "<br>" & strSELECT
        'Set rsXref = Conn.Execute(strSELECT)

        'If rsXref.EOF Then
        strINSERT = "INSERT INTO [xRef] ([Category_ID],[Post_ID]) Values (" & CStr(intCatID) & "," & CStr(iPostID) & ")"
        'Response.Write "<br>" & strINSERT
        '        Conn.Execute(strINSERT)
        'End If
        'rsXref.Close
        '    Set rsXref = Nothing   

        'Else
        'Response.Write "<br>INSERTING = " & strDBCat
        'Response.Flush()
        strINSERT = "INSERT INTO [MyAspPortal].[dbo].[Categories] ([Category_Parent],[Category_Name]) VALUES (0,'" & strDBCat & "')"
        '    Conn.Execute(strINSERT)
        Call InsertCategory(iPostID, strCat)
        'End If

        'rsCategories.Close
        rsCategories = Nothing

    End Sub

    'Protected Sub ImportXML_Click(sender As Object, e As EventArgs) Handles ImportXML.Click
    Private Sub ImportXML(strFileName As String)

        If File.Exists(strFileName) Then

            Dim AllText As String = File.ReadAllText(strFileName)
            If File.Exists(strFileName) Then
                File.Delete(strFileName)
            End If

            AllText = AllText.Replace(Chr(10), vbCrLf)

            Do While AllText.Contains(Chr(8))
                AllText = AllText.Replace(Chr(8), "")
            Loop

            Do While AllText.Contains(Chr(0))
                AllText = AllText.Replace(Chr(0), "")
            Loop

            File.WriteAllText(strFileName, AllText, ASCIIEncoding.ASCII)

        End If

        Dim xmldoc As New XmlDocument()
        Dim xmlnode As XmlNodeList

        xmldoc.Load(strFileName)
        xmlnode = xmldoc.GetElementsByTagName("channel")

        For Each elem As XmlNode In xmlnode

            ListBox1.Items.Add(elem.Name.ToString)

            Dim child As XmlNodeList = elem.ChildNodes
            For Each node As XmlNode In child

                ListBox1.Items.Add("--" & node.Name.ToString)

                Dim objXML As New XmlDocument()
                Dim objLst As XmlNodeList

                'Dim SizeofObject As Integer = objLst.Count
                Dim iCounter As Integer = 0

                Dim strPostType As String = ""
                Dim strPostTitle As String = ""
                Dim strPostDate As String = ""
                Dim strPostOwner As String = ""
                Dim strPostDesc As String = ""
                Dim strPostCat As String = ""
                Dim strDescription As String = ""
                Dim bAlreadyPosted As Boolean

                Dim ArrContent As String()
                Dim iPostID As Integer

                objLst = objXML.GetElementsByTagName("item")
                For Each element As XmlNode In objLst

                    strPostType = ""
                    strPostTitle = ""
                    strPostDate = ""
                    strPostOwner = ""
                    strPostDesc = ""
                    strPostCat = ""
                    strDescription = ""

                    bAlreadyPosted = False

                    '    iPostID = CInt(0)

                    iCounter = iCounter + 1

                    Dim childNodes As XmlNodeList = element.ChildNodes
                    For Each ItemNode As XmlNode In childNodes

                        Select Case ItemNode.Name.ToLower
                            Case "title"
                                'Response.Write "<h2>" & node.text & "</h2>" 
                                strPostTitle = Trim(ItemNode.Value)
                            Case "pubdate"
                                'Response.Write "<br>Created on: " & node.text 
                                strPostDate = ItemNode.Value
                            Case "dc:creator"
                                'Response.Write "<br>By: " & node.text 
                                strPostOwner = ItemNode.Value
                            Case "content:encoded"
                                strDescription = ItemNode.Value
                                strDescription = strDescription
                                strPostDesc = ""
                                ArrContent = Split(strDescription, Chr(10))
                                For i1 = 0 To UBound(ArrContent)
                                    If LCase(Trim(ArrContent(i1))) <> LCase(Trim(strPostTitle)) Then
                                        'ArrContent(i1) = Trim(Replace(ArrContent(i1), "&nbsp;", " "))
                                        If ArrContent(i1) <> "" Then
                                            strPostDesc = strPostDesc & "<p>" & Trim(ArrContent(i1)) & "</p>"
                                        End If
                                    End If
                                Next
                                'strDescription = Replace(strDescription,chr(10),"<br><br>")
                                'Response.Write "<br><br>" & strDescription & "<br><br>"
                                strPostDesc = Replace(strPostDesc, "<p></p>", "")

                            Case "wp:post_type" : strPostType = ItemNode.Value
                            Case "category"
                                'Response.Write "<br>Category: " & node.text 
                                strPostCat = ItemNode.Value
                                strPostCat = UCase(Mid(strPostCat, 1, 1)) & Mid(strPostCat, 2)
                                If bAlreadyPosted = True And iPostID > 0 Then
                                    ' Insert Category here
                                    Response.Write("<br>Inserting Category: " & strPostCat)
                                    Response.Flush()
                                    Call InsertCategory(iPostID, strPostCat)
                                End If

                        End Select

                        If strPostTitle <> "" And strPostDate <> "" And strPostOwner <> "" And strPostDesc <> "" And strPostType <> "" And bAlreadyPosted = False Then
                            iPostID = InsertPost(strPostOwner, strPostType, strPostTitle, strPostDesc)
                            If iPostID > 0 Then
                                Response.Write("<br>POST already inserted: " & strPostTitle)
                            Else
                                Response.Write("<br>ERROR Inserting Post: " & strPostTitle)
                            End If
                            bAlreadyPosted = True
                        End If
                    Next
                Next
            Next
        Next

    End Sub

End Class