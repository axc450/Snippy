Imports System.Runtime.InteropServices

Public Class Snippy
    Public Const MOD_ALT As Integer = &H1
    Public Const WM_HOTKEY As Integer = &H312
    Dim overlay As Snippy_Overlay = New Snippy_Overlay()

    <DllImport("User32.dll")>
    Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    End Function

    Private Sub Snippy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 1, MOD_ALT, Keys.D4)
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
End Class