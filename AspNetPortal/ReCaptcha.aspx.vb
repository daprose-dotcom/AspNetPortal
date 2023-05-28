Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Partial Class ReCaptcha

    Inherits System.Web.UI.Page

    Dim DrawingFont As New Font("Arial", 30)
    Dim CaptchaImage As New Bitmap(280, 50)
    Dim CaptchaGraf As Graphics = Graphics.FromImage(CaptchaImage)
    Dim Alphabet As String = "AaBbCcDdEeFfGgHhJjKkLMmNnPpQqRrSsTtUuVvWwXxYyZz"
    Dim CaptchaString, TickRandom As String
    Dim ProcessNumber As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strReferer = Request.ServerVariables("HTTP_REFERER")
        Dim strHost = Request.ServerVariables("HTTP_REFERER")

        If InStr(strReferer, strHost) > 0 Then

            '## generate a 6 digit random string  
            Dim RandomStr As String = GenerateCaptchaWord()
            If RandomStr.Length > 0 Then

                'create object of Bitmap Class and set its width and height.
                Dim objBMP As New Bitmap(280, 50)
                RenderLoop(objBMP)
                Dim texture As New TextureBrush(objBMP)
                texture.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile

                'Create Graphics object and assign bitmap object to graphics' object.  
                Dim objGraphics As Graphics = Graphics.FromImage(objBMP)
                'objGraphics.Clear(Color.OrangeRed)
                'objGraphics.FillEllipse(texture, New Rectangle(10.0F, 10.0F, 20, 10))
                'objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias

                Dim objFont As New Font("verdana", 28, FontStyle.Bold)

                '## Set this RandomStr in a session variable
                Session(Application("SESSION_CAPTCHA")) = RandomStr

                Dim drawFormat As New StringFormat()
                drawFormat.FormatFlags = StringFormatFlags.NoWrap

                objGraphics.DrawString("   " & RandomStr, objFont, Brushes.White, 2, 2, drawFormat)

                '   The following commented out code can be used for testing
                '   It writes the image to a page by itself

                Response.ContentType = "image/JPEG"
                Response.AppendHeader("Content-Disposition", "inline; filename=captcha.jpeg")
                Response.CacheControl = "no-cache"
                Response.AppendHeader("Pragma", "no-cache")
                Response.Expires = -1

                objBMP.Save(Response.OutputStream, Imaging.ImageFormat.Jpeg)
                Response.Flush()

                objFont.Dispose()
                objGraphics.Dispose()
                objBMP.Dispose()

            End If

        End If

    End Sub
    Private Function GenerateCaptchaWord() As String
        ' Below code describes how to create random numbers.some of the digits and letters  
        ' are ommited because they look same like "i","o","1","0","I","O".  
        Dim allowedChars As String
        GenerateCaptchaWord = ""
        allowedChars = "a,b,c,d,h,k,m,n,p,q,r,s,u,v,w,x,y,z,"
        allowedChars += "A,B,C,D,E,F,G,K,L,M,N,P,R,S,U,V,W,X,Y,Z,"
        allowedChars += "2,3,4,5,6,7,8,9"
        'Dim sep() As Char = {","c}
        Dim arr() As String = allowedChars.Split(",")
        Dim passwordString As String = ""
        Dim temp As String
        Dim rand As New Random()
        Dim i As Integer
        For i = 0 To 6 - 1 Step i + 1
            temp = arr(rand.Next(0, arr.Length))
            passwordString += temp
        Next
        GenerateCaptchaWord = passwordString
    End Function

    Public Sub RefreshCaptcha(sender As Object, e As System.EventArgs)
        'create object of Bitmap Class and set its width and height.  
        Dim objBMP As New Bitmap(180, 51)
        'Create Graphics object and assign bitmap object to graphics' object.  
        Dim objGraphics As Graphics = Graphics.FromImage(objBMP)
        objGraphics.Clear(Color.OrangeRed)
        objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias
        Dim objFont As New Font("arial", 30, FontStyle.Regular)
        'genetating random 6 digit random number  
        Dim RandomStr As String = GenerateCaptchaWord()
        'set this random number in session  
        Session.Add("RandomStr", RandomStr)
        objGraphics.DrawString(RandomStr, objFont, Brushes.White, 2, 2)
        Response.ContentType = "image/GIF"
        objBMP.Save(Response.OutputStream, ImageFormat.Gif)
        objFont.Dispose()
        objGraphics.Dispose()
        objBMP.Dispose()
    End Sub

    Public Function IsThisCaptchaValid(strSessionName As String, strCaptcha As String) As Boolean
        IsThisCaptchaValid = False
        If strCaptcha.Length > 0 And strSessionName.Length > 0 Then
            Dim strSession = Session(strSessionName)
            If strSession = strCaptcha Then
                Session(strSessionName) = ""
                IsThisCaptchaValid = True
            End If
        End If
    End Function

    Private Sub GenerateCaptcha()
        ProcessNumber = My.Computer.Clock.LocalTime.Millisecond
        If ProcessNumber < 521 Then
            ProcessNumber = ProcessNumber \ 10
            CaptchaString = Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString = CStr(My.Computer.Clock.LocalTime.Second \ 6)
        End If
        ProcessNumber = My.Computer.Clock.LocalTime.Second
        If ProcessNumber < 30 Then
            ProcessNumber = Math.Abs(ProcessNumber - 8)
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(My.Computer.Clock.LocalTime.Minute \ 6)
        End If
        ProcessNumber = My.Computer.Clock.LocalTime.DayOfYear
        If ProcessNumber Mod 2 = 0 Then
            ProcessNumber = ProcessNumber \ 8
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(ProcessNumber \ 37)
        End If
        TickRandom = My.Computer.Clock.TickCount.ToString
        ProcessNumber = Val(TickRandom.Substring(TickRandom.Length - 1, 1))
        If ProcessNumber Mod 2 = 0 Then
            CaptchaString += CStr(ProcessNumber)
        Else
            ProcessNumber = Math.Abs(Int(Math.Cos(Val(TickRandom)) * 51))
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        End If
        ProcessNumber = My.Computer.Clock.LocalTime.Hour
        If ProcessNumber Mod 2 = 0 Then
            ProcessNumber = Math.Abs(Int(Math.Sin(Val(My.Computer.Clock.LocalTime.Year)) * 51))
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(ProcessNumber \ 3)
        End If
        ProcessNumber = My.Computer.Clock.LocalTime.Millisecond
        If ProcessNumber > 521 Then
            ProcessNumber = Math.Abs((ProcessNumber \ 10) - 52)
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(My.Computer.Clock.LocalTime.Second \ 6)
        End If
        CaptchaGraf.Clear(Color.White)

        For hasher As Integer = 0 To 5
            CaptchaGraf.DrawString(CaptchaString.Substring(hasher, 1), DrawingFont, Brushes.Black, hasher * 20 + hasher + ProcessNumber \ 200, (hasher Mod 3) * (ProcessNumber \ 200))
        Next
        'PictureBox1.Image = CaptchaImage
    End Sub

    Private Sub RenderLoop(ByRef b As Bitmap)

        Const cfPadding As Single = 5.0F

        'Dim b As New Bitmap(ClientSize.Width, ClientSize.Width, PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim r As New Random(Now.Millisecond)
        Dim oBMPData As BitmapData = Nothing
        Dim oPixels() As Integer = Nothing
        Dim oBlackWhite() As Integer = {Color.White.ToArgb, Color.Black.ToArgb}
        Dim oStopwatch As New Stopwatch
        Dim fElapsed As Single = 0.0F
        Dim iLoops As Integer = 0
        Dim iMyLoops As Integer = 0
        Dim sFPS As String = ""
        Dim objFont As New Font("arial", 30, FontStyle.Regular)
        Dim oFPSSize As SizeF = g.MeasureString(sFPS, objFont)
        Dim oFPSBG As RectangleF = New RectangleF(280 - cfPadding - oFPSSize.Width, cfPadding, oFPSSize.Width, oFPSSize.Height)

        ' Get ourselves a nice, clean, black canvas to work with.
        g.Clear(Color.Black)

        ' Prep our bitmap for a read.
        oBMPData = b.LockBits(New Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb)

        ' Allocate sufficient space for the pixel data and 
        ' flash copy it to our array.
        ' We want an integer to hold the color for each pixel in the canvas.
        Array.Resize(oPixels, b.Width * b.Height)
        Runtime.InteropServices.Marshal.Copy(oBMPData.Scan0, oPixels, 0, oPixels.Length)
        b.UnlockBits(oBMPData)
        ' Start looping.

        Do

            ' Find our frame time and add it to the total amount of time 
            ' elapsed since our last FPS update (once per second).
            fElapsed += oStopwatch.ElapsedMilliseconds / 1000.0F
            oStopwatch.Reset() : oStopwatch.Start()
            ' Adjust the number of loops since the last whole second has elapsed
            iLoops += 1
            iMyLoops += 1

            If fElapsed >= 1.0F Then
                ' Since we've now had a whole second elapse
                ' figure the Frames Per Second, 
                ' measure our string,
                ' setup our backing rectangle for the FPS string
                '        (so it's clearly visible over the snow)
                ' reset our loop counter 
                ' and our elapsed counter.
                sFPS = (iLoops / fElapsed).ToString("") & ""
                oFPSSize = g.MeasureString(sFPS, objFont)
                oFPSBG = New RectangleF(280 - cfPadding - oFPSSize.Width, cfPadding, oFPSSize.Width, oFPSSize.Height)
                ' We don't set this to 0 in case our frame time has gone
                ' a bit over 1 second since last update.
                fElapsed -= 1.0F
                iLoops = 0
            End If

            ' Generate our snow.
            For i As Integer = 0 To oPixels.GetUpperBound(0)
                oPixels(i) = oBlackWhite(r.Next(oBlackWhite.Length))
            Next

            ' Prep the bitmap for an update.
            oBMPData = b.LockBits(New Rectangle(0, 0, b.Width, b.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb)
            ' Flash copy the new data into our bitmap.
            Runtime.InteropServices.Marshal.Copy(oPixels, 0, oBMPData.Scan0, oPixels.Length)
            b.UnlockBits(oBMPData)

            ' Draw the backing for our FPS display.
            g.FillRectangle(Brushes.Black, oFPSBG)
            ' Draw our FPS.

            g.DrawString(sFPS, objFont, Brushes.Yellow, oFPSBG.Left, oFPSBG.Top)

            ' Update the form's background and draw.
            ' Let windows handle some queued events.
            'Application.DoEvents()

        Loop While iMyLoops < 10

    End Sub

End Class