Public Class Form1
    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim myForm As New Form2
        myForm.Size = New Size(1000, 950)
        myForm.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class