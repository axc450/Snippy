Imports System.Runtime.InteropServices

Public Class Snippy
    Public Const MOD_ALT As Integer = &H1
    Public Const WM_HOTKEY As Integer = &H312
    Dim cb As Boolean = True
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
        Dim cb As Boolean = True
        ToolStripMenuItem1.Checked = False
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim cb As Boolean = False
        ToolStripMenuItem2.Checked = False
    End Sub

    Public Sub takeScreenshot(X As Integer, Y As Integer, W As Integer, H As Integer)
        Dim s As New Size(W, H)
        Dim ss As New Bitmap(W, H)
        Dim g As Graphics = Graphics.FromImage(ss)
        g.CopyFromScreen(New Point(X, Y), New Point(0, 0), s)
        Clipboard.SetDataObject(ss, True)
    End Sub
End Class