Public Class UserControl1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.RichTextBox1.Text = "Hello to everyone, this is my first program in VB.net to understand how this language works"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.RichTextBox1.ForeColor = Color.Red

    End Sub

    Private Sub Button3_MouseHover(sender As Object, e As EventArgs) Handles Button3.MouseHover
        Me.RichTextBox1.BackColor = Color.Yellow
    End Sub
    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles Button3.MouseLeave
        Me.RichTextBox1.BackColor = Color.White
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked Then Me.RichTextBox1.Text = "Congratulation, you've learned about handlers in VB.net" Else Me.RichTextBox1.Text = "You unchecked the checkbox, so you are not so confident with handlers. Try the button above to understand better"
    End Sub
End Class
