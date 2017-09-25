Public Class Snippy_Overlay

    Dim isDrawing As Boolean
    Dim beginX As Integer = 0
    Dim beginY As Integer = 0
    Dim endX As Integer = 0
    Dim endY As Integer = 0
    Dim b As SolidBrush = New SolidBrush(Color.Yellow)
    Dim p As Pen = New Pen(Color.Red, 3)


    Private Sub Snippy_Overlay_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Sinppy_Overaly_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        beginX = e.X
        beginY = e.Y
        isDrawing = True
    End Sub

    Private Sub Sinppy_Overaly_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If (isDrawing) Then
            endX = e.X
            endY = e.Y
            Me.Invalidate()
        End If
    End Sub

    Private Sub Sinppy_Overaly_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        isDrawing = False
        Me.Close()

        Dim X As Integer = beginX
        Dim Y As Integer = beginY
        Dim W As Integer = endX - beginX
        Dim H As Integer = endY - beginY

        If (endX < beginX) Then
            X = endX
            W = beginX - endX
        End If

        If (endY < beginY) Then
            Y = endY
            H = beginY - endY
        End If

        Snippy.takeScreenshot(X, Y, W, H)
    End Sub

    Private Sub Sinppy_Overaly_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        Dim X As Integer = beginX
        Dim Y As Integer = beginY
        Dim W As Integer = endX - beginX
        Dim H As Integer = endY - beginY

        If (endX < beginX) Then
            X = endX
            W = beginX - endX
        End If

        If (endY < beginY) Then
            Y = endY
            H = beginY - endY
        End If

        e.Graphics.FillRectangle(b, X, Y, W, H)
        e.Graphics.DrawRectangle(p, X, Y, W, H)
    End Sub
End Class