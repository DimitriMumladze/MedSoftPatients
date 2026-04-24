Public Class FrmMain

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()

        ' რომ არ გალურჯდეს Header-ი როცა row-ი ირჩევა
        dgvPatients.ColumnHeadersDefaultCellStyle.SelectionBackColor =
            dgvPatients.ColumnHeadersDefaultCellStyle.BackColor
        dgvPatients.ColumnHeadersDefaultCellStyle.SelectionForeColor =
            dgvPatients.ColumnHeadersDefaultCellStyle.ForeColor
    End Sub


    Private Sub LoadPatients()
        Try
            Dim patients As List(Of Patient) = PatientRepository.GetAll()

            dgvPatients.DataSource = Nothing
            dgvPatients.DataSource = patients

            ConfigureColumns()

            dgvPatients.ClearSelection()

        Catch ex As Exception
            MessageBox.Show("პაციენტების ჩატვირთვისას მოხდა შეცდომა:" & vbCrLf & vbCrLf & ex.Message,
                            "შეცდომა",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ConfigureColumns()
        If dgvPatients.Columns.Count = 0 Then Return

        dgvPatients.Columns("GenderID").Visible = False

        dgvPatients.Columns("ID").HeaderText = "ID"
        dgvPatients.Columns("FullName").HeaderText = "პაციენტის გვარი სახელი"
        dgvPatients.Columns("Dob").HeaderText = "დაბ. თარიღი"
        dgvPatients.Columns("GenderName").HeaderText = "სქესი"
        dgvPatients.Columns("Phone").HeaderText = "მობ. ნომერი"
        dgvPatients.Columns("Address").HeaderText = "მისამართი"

        dgvPatients.Columns("Dob").DefaultCellStyle.Format = "dd.MM.yyyy"

        dgvPatients.Columns("ID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgvPatients.Columns("ID").Width = 50

        dgvPatients.Columns("ID").DisplayIndex = 0
        dgvPatients.Columns("FullName").DisplayIndex = 1
        dgvPatients.Columns("Dob").DisplayIndex = 2
        dgvPatients.Columns("GenderName").DisplayIndex = 3
        dgvPatients.Columns("Phone").DisplayIndex = 4
        dgvPatients.Columns("Address").DisplayIndex = 5
    End Sub


    Private Sub dgvPatients_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
        Handles dgvPatients.CellFormatting

        If dgvPatients.Columns(e.ColumnIndex).Name = "Phone" AndAlso e.Value IsNot Nothing Then
            Dim phone As String = e.Value.ToString()
            If phone.Length = 9 Then
                e.Value = $"{phone.Substring(0, 3)}-{phone.Substring(3, 3)}-{phone.Substring(6, 3)}"
                e.FormattingApplied = True
            End If
        End If
    End Sub


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Using frm As New FrmPatientEdit()
            If frm.ShowDialog(Me) = DialogResult.OK Then
                LoadPatients()
            End If
        End Using
    End Sub


    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim selected As Patient = GetSelectedPatient()
        If selected Is Nothing Then
            MessageBox.Show("გთხოვთ აირჩიოთ პაციენტი ცხრილიდან.", "ინფორმაცია",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using frm As New FrmPatientEdit(selected)
            If frm.ShowDialog(Me) = DialogResult.OK Then
                LoadPatients()
            End If
        End Using
    End Sub


    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim selected As Patient = GetSelectedPatient()
        If selected Is Nothing Then
            MessageBox.Show("გთხოვთ აირჩიოთ პაციენტი ცხრილიდან.", "ინფორმაცია",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim confirm As DialogResult = MessageBox.Show(
            $"დარწმუნებული ხართ, რომ გსურთ პაციენტის ""{selected.FullName}"" წაშლა?",
            "წაშლის დადასტურება",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2)

        If confirm <> DialogResult.Yes Then Return

        Try
            PatientRepository.Delete(selected.ID)
            LoadPatients()
        Catch ex As Exception
            MessageBox.Show("წაშლისას მოხდა შეცდომა:" & vbCrLf & vbCrLf & ex.Message,
                            "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub dgvPatients_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
        Handles dgvPatients.CellDoubleClick

        If e.RowIndex < 0 Then Return
        btnEdit.PerformClick()
    End Sub


    Private Function GetSelectedPatient() As Patient
        If dgvPatients.SelectedRows.Count = 0 Then Return Nothing
        Return TryCast(dgvPatients.SelectedRows(0).DataBoundItem, Patient)
    End Function

End Class
