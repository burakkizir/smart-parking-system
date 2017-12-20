' PORT KÜTÜPHANESİ PROJEYE DAHİL EDİLİYOR...
Imports System.IO.Ports

Public Class Form1

    ' GLOBAL DEĞİŞKENLER TANIMLANIYOR
    Dim port As Array
    Delegate Sub SetTextCallBack(ByVal [text] As String)
    Private Delegate Sub UpdateTextboxDelegate(ByVal myText As String)
    Dim aa, bb, cc, dd, ee, ff As Integer
    Dim aaa, bbb, ccc, ddd, eee, fff As Integer

    ' SERIAL PORTTAN VERİ GELDİĞİNDE ÇALIŞACAK KODLAR
    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles ss.DataReceived
        Dim myResponse As String = ss.ReadLine
        UpdateTextbox(myResponse)
    End Sub

    'SERIAL PORTTAN GELEN VERİ TEXTBOX1'E AKTARILIYOR
    Private Sub UpdateTextbox(ByVal myText As String)
        If Me.TextBox1.InvokeRequired Then
            Dim d As New UpdateTextboxDelegate(AddressOf UpdateTextbox)
            Me.TextBox1.Invoke(d, New Object() {myText})
        Else
            TextBox1.Text = myText
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            Me.Close()
        End If

        If e.KeyCode = Keys.F9 Then
            Dim cvp = InputBox("Yeni Başlığı Yazınız", "Başlık Değiştirme", Me.Text)
            If cvp.Length > 2 Then
                Me.Text = cvp
                My.Settings.baslik = cvp
                My.Settings.Save()
            End If
        End If

        If e.KeyCode = Keys.F8 Then
            Dim cvp = InputBox("Yeni Sloganı Yazınız", "Slogan Değiştirme", Label12.Text)
            If cvp.Length > 2 Then
                Label12.Text = cvp
                My.Settings.banner = cvp
                My.Settings.Save()
            End If
        End If

        If e.KeyCode = Keys.F7 Then
            Form3.Show()
        End If
    End Sub

    'PROGRAM BAŞLARKEN ÇALIŞACAK KODLAR
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = My.Settings.baslik
        'TODO: This line of code loads data into the 'OtoDataSet.otolar' table. You can move, or remove it, as needed.
        Me.OtolarTableAdapter.Fill(Me.OtoDataSet.otolar)
        ss.PortName = My.Settings.port
        Try
            ss.Open()
        Catch ex As Exception

        End Try
        aaa = 0
        bbb = 0
        ccc = 0
        ddd = 0
        eee = 0
        fff = 0
        Label12.Text = My.Settings.banner

    End Sub

    ' TEXTBOX İÇERİĞİ DEĞİŞTİĞİNDE ÇALIŞACAK KODLAR
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Not TextBox1.Text = "" Then
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox4.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox5.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox6.SizeMode = PictureBoxSizeMode.StretchImage

            Dim gelen As String = TextBox1.Text
            gelen.Trim()
            Dim gelenler() As String = gelen.Split(":")

            If gelenler(0) = 1 Then
                PictureBox1.Image = My.Resources.var
                aa = 1
                If aaa = 0 Then
                    ekle()
                    aaa = 1
                End If
            Else
                PictureBox1.Image = My.Resources.yok
                aa = 0
                aaa = 0
            End If

            If gelenler(1) = 1 Then
                PictureBox2.Image = My.Resources.var
                bb = 1
                If bbb = 0 Then
                    ekle()
                    bbb = 1
                End If
            Else
                PictureBox2.Image = My.Resources.yok
                bb = 0
                bbb = 0
            End If

            If gelenler(2) = 1 Then
                PictureBox3.Image = My.Resources.var
                cc = 1
                If ccc = 0 Then
                    ekle()
                    ccc = 1
                End If
            Else
                PictureBox3.Image = My.Resources.yok
                cc = 0
                ccc = 0
            End If

            If gelenler(3) = 1 Then
                PictureBox4.Image = My.Resources.var
                dd = 1
                If ddd = 0 Then
                    ekle()
                    ddd = 1
                End If
            Else
                PictureBox4.Image = My.Resources.yok
                dd = 0
                ddd = 0
            End If

            If gelenler(4) = 1 Then
                PictureBox5.Image = My.Resources.var
                ee = 1
                If eee = 0 Then
                    ekle()
                    eee = 1
                End If
            Else
                PictureBox5.Image = My.Resources.yok
                ee = 0
                eee = 0
            End If

            If gelenler(5) = 1 Then
                PictureBox6.Image = My.Resources.var
                ff = 1
                If fff = 0 Then
                    ekle()
                    fff = 1
                End If
            Else
                PictureBox6.Image = My.Resources.yok
                ff = 0
                fff = 0
            End If

            Label10.Text = 6 - (aa + bb + cc + dd + ee + ff)
            Label11.Text = (aa + bb + cc + dd + ee + ff)

            If Label10.Text = "0" Then
                PictureBox7.Visible = True
            Else
                PictureBox7.Visible = False
            End If

        End If
    End Sub

    ' ANASAYFADAKİ LOGOYA ÇİFT TIKLAYINCA ÇALIŞACAK KODLAR
    Private Sub Label12_DoubleClick(sender As Object, e As EventArgs) Handles Label12.DoubleClick
        If ss.IsOpen = True Then
            ss.Close()
        End If
        Me.Close()
    End Sub

    Private Sub kaydet()
        Me.Validate()
        Me.OtolarBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.OtoDataSet)
        Me.OtolarTableAdapter.Fill(Me.OtoDataSet.otolar)
    End Sub

    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        Dim dt As String = MonthCalendar1.SelectionStart.ToShortDateString()
        OtolarBindingSource.Filter = "tarih = '" & dt & "'"
        say()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OtolarBindingSource.RemoveFilter()
        say()
    End Sub

    Private Sub ekle()
        OtolarBindingSource.AddNew()
        OtolarDataGridView.CurrentRow.Cells(0).Value = Now.ToShortDateString
        OtolarDataGridView.CurrentRow.Cells(1).Value = Now.ToShortTimeString
        kaydet()
        say()
    End Sub

    Private Sub say()
        Dim kac As Integer = OtolarDataGridView.RowCount
        Label14.Text = kac.ToString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ss.Write("acil")
    End Sub
End Class
