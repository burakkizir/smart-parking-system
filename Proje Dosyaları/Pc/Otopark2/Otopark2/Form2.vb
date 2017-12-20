Imports System.IO.Ports

Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        For Each pp In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(pp)
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Kullanıcı Adı Boş Olamaz", MsgBoxStyle.Critical, "UYARI")
            Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox("Parola Yazmadınız!", MsgBoxStyle.Critical, "UYARI")
            Exit Sub
        End If
        If ComboBox1.Text = "" Then
            MsgBox("Port Adı Boş Olamaz", MsgBoxStyle.Critical, "UYARI")
            Exit Sub
        End If

        If TextBox1.Text = My.Settings.user And TextBox2.Text = My.Settings.pass Then
            My.Settings.port = ComboBox1.Text
            My.Settings.Save()
            Form1.Show()
            Me.Close()
        Else
            MsgBox("Kullanıcı Adı yada Parola Hatalı", MsgBoxStyle.Critical, "HATA")
        End If
    End Sub
End Class