Public Class Snippy_Overlay

    Dim beginX As Integer = 0
    Dim beginY As Integer = 0
    Dim endX As Integer = 0
    Dim endY As Integer = 0

    Private Sub Snippy_Overlay_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Sinppy_Overaly_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        beginX = e.X
        beginY = e.Y
    End Sub

    Private Sub Sinppy_Overaly_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        endX = e.X
        endY = e.Y
        MsgBox(beginX & " " & beginY & " " & endX & " " & endY)
        Me.Close()
    End Sub

End Class