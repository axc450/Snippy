Imports System.Runtime.InteropServices
Imports System.Net

Public Class Snippy
    Public Const MOD_ALT As Integer = &H1
    Public Const WM_HOTKEY As Integer = &H312
    Dim cb As Boolean = True
    Dim localSave As Boolean = False
    Dim overlay As Snippy_Overlay = New Snippy_Overlay()

    <DllImport("User32.dll")>
    Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    End Function

    Private Sub Snippy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 1, MOD_ALT, Keys.D4)

        Dim header As ToolStripLabel = New ToolStripLabel("Snippy")
        header.Enabled = False
        ContextMenuStrip1.Items.Insert(0, header)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            If (overlay.IsDisposed) Then
                overlay = New Snippy_Overlay()
            End If
            overlay.Show()
                overlay.Focus()
                overlay.BringToFront()
            End If

            MyBase.WndProc(m)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        cb = True
        ToolStripMenuItem1.Checked = False
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        cb = False
        ToolStripMenuItem2.Checked = False
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        localSave = Not localSave
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Try
            IO.File.Copy(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "/Snippy.exe", True)
            MsgBox("Snippy will automatically start when you log on!", vbInformation)
        Catch ex As Exception
            MsgBox("Could not add Snippy to the startup programs!", vbCritical)
        End Try
    End Sub

    Public Sub takeScreenshot(X As Integer, Y As Integer, W As Integer, H As Integer)
        Dim s As New Size(W, H)
        Dim ss As New Bitmap(W, H)
        Dim g As Graphics = Graphics.FromImage(ss)
        g.CopyFromScreen(New Point(X, Y), New Point(0, 0), s)

        If cb Then
            Clipboard.SetDataObject(ss, True)
        Else
            upload(ss)
        End If

    End Sub

    Private Sub upload(ss As Bitmap)
        Dim MS As System.IO.MemoryStream = New System.IO.MemoryStream()
        ss.Save(MS, System.Drawing.Imaging.ImageFormat.Png)
        Dim byteImage As Byte() = MS.ToArray()
        Dim base64img As String = Convert.ToBase64String(byteImage)

        Dim uploadRequestString As String = System.Web.HttpUtility.UrlEncode("image", System.Text.Encoding.UTF8) + "=" + base64img
        Console.Out.WriteLine(uploadRequestString)

        Dim httpReq As HttpWebRequest = WebRequest.Create("https://api.imgur.com/3/upload")
        httpReq.Headers.Add("Authorization", "Client-ID 4f2b6d0841fd112")
        httpReq.Method = "POST"
        httpReq.ContentType = "application/x-www-form-urlencoded"
        httpReq.ContentLength = base64img.Length
        httpReq.ServicePoint.Expect100Continue = False


        Dim streamWriter As System.IO.StreamWriter = New System.IO.StreamWriter(httpReq.GetRequestStream())
        streamWriter.Write(base64img, 0, base64img.Length)
        streamWriter.Close()


        Dim response As WebResponse = httpReq.GetResponse()
        Dim responseStream As System.IO.Stream = response.GetResponseStream()
        Dim responseReader As System.IO.StreamReader = New System.IO.StreamReader(responseStream)
        Dim responseString As String = responseReader.ReadToEnd()

        Dim img_url As String = responseString.Remove(0, responseString.IndexOf("link") + 7)
        img_url = img_url.Substring(0, img_url.IndexOf("success") - 4)
        img_url = img_url.Replace("\/", "/")
        responseStream.Close()

        Clipboard.SetText(img_url)

    End Sub

End Class