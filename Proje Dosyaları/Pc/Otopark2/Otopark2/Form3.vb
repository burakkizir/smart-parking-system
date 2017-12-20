Public Class Form3

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.Clear()
        For Each u In My.Settings.users
            ListBox1.Items.Add(u)
        Next

        ListBox2.Items.Clear()
        For Each pw In My.Settings.pasw
            ListBox2.Items.Add(pw)
        Next
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        ListBox2.SelectedIndex = ListBox1.SelectedIndex
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        ListBox1.SelectedIndex = ListBox2.SelectedIndex
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cvp = MsgBox("Seçili kayıt silinecek", MsgBoxStyle.YesNo, "DİKKAT")
        If cvp = MsgBoxResult.Yes Then
            Dim xc As Integer = ListBox1.SelectedIndex
            My.Settings.users.RemoveAt(xc)
            My.Settings.pasw.RemoveAt(xc)
            My.Settings.Save()
            ListBox1.Items.Clear()
            For Each u In My.Settings.users
                ListBox1.Items.Add(u)
            Next

            ListBox2.Items.Clear()
            For Each pw In My.Settings.pasw
                ListBox2.Items.Add(pw)
            Next
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim us As String = InputBox("Yeni Kullanıcı Adını Yazınız", "Kullanıcı Ekleme")
        If us.Length > 2 Then
            My.Settings.users.Add(us)
            Dim ps As String = InputBox("Kullanıcı Parolasını Yazınız", "Kullanıcı Ekleme")
            If ps.Length > 2 Then
                My.Settings.pasw.Add(ps)
                My.Settings.Save()
                ListBox1.Items.Clear()
                For Each u In My.Settings.users
                    ListBox1.Items.Add(u)
                Next

                ListBox2.Items.Clear()
                For Each pw In My.Settings.pasw
                    ListBox2.Items.Add(pw)
                Next

            End If
        End If
    End Sub
End Class