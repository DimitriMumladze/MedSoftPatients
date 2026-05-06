Public Class FrmPatientEdit

    Private ReadOnly _editPatient As Patient
    Private ReadOnly _isEditMode As Boolean

    ' Add რეჟიმი: ახალი პაციენტი
    Public Sub New()
        InitializeComponent()
        _editPatient = Nothing
        _isEditMode = False
        Me.Text = "პაციენტის დამატება"
    End Sub

    ' Edit რეჟიმი: არსებული პაციენტის რედაქტირება
    Public Sub New(patient As Patient)
        InitializeComponent()
        _editPatient = patient
        _isEditMode = True
        Me.Text = "პაციენტის რედაქტირება"
    End Sub

    Private Sub FrmPatientEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDob.MaxDate = DateTime.Today
        LoadGenders()

        If _isEditMode Then
            PopulateFields(_editPatient)
        Else
            dtpDob.Value = DateTime.Today
        End If
    End Sub

    Private Sub LoadGenders()
        Dim genders As List(Of Gender) = GenderRepository.GetAll()
        cmbGender.DataSource = genders
        cmbGender.DisplayMember = "GenderName"
        cmbGender.ValueMember = "GenderId"
        cmbGender.SelectedIndex = -1
    End Sub

    Private Sub PopulateFields(patient As Patient)
        txtPersonalNumber.Text = If(patient.PersonalNumber, String.Empty)
        Dim parts As String() = SplitFullName(patient.FullName)
        txtFirstName.Text = parts(0)
        txtLastName.Text = parts(1)
        dtpDob.Value = patient.Dob
        cmbGender.SelectedValue = patient.GenderID
        txtPhone.Text = If(patient.Phone, String.Empty)
        txtAddress.Text = If(patient.Address, String.Empty)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateInput() Then Return

        Dim patient As Patient = BuildPatientFromInput()

        Try
            If _isEditMode Then
                patient.ID = _editPatient.ID
                PatientRepository.Update(patient)
            Else
                PatientRepository.Add(patient)
            End If

            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch sqlEx As System.Data.SqlClient.SqlException When sqlEx.Number = 50001
            MessageBox.Show("პაციენტი ამ პირადი ნომრით უკვე არსებობს ბაზაში.",
                            "დუბლიკატი", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPersonalNumber.Focus()
            txtPersonalNumber.SelectAll()
        Catch sqlEx As System.Data.SqlClient.SqlException When sqlEx.Number = 2627 OrElse sqlEx.Number = 2601
            MessageBox.Show("პაციენტი ამ პირადი ნომრით უკვე არსებობს ბაზაში.",
                            "დუბლიკატი", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPersonalNumber.Focus()
            txtPersonalNumber.SelectAll()
        Catch ex As Exception
            MessageBox.Show("შეცდომა შენახვისას: " & ex.Message, "შეცდომა",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function ValidateInput() As Boolean
        Dim pn As String = txtPersonalNumber.Text.Trim()
        If pn.Length > 0 Then
            If pn.Length <> 11 OrElse Not pn.All(AddressOf Char.IsDigit) Then
                ShowValidationError("პირადი ნომერი უნდა შედგებოდეს 11 ციფრისგან.", txtPersonalNumber)
                Return False
            End If
        End If

        If String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            ShowValidationError("სახელი სავალდებულოა.", txtFirstName)
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtLastName.Text) Then
            ShowValidationError("გვარი სავალდებულოა.", txtLastName)
            Return False
        End If

        If cmbGender.SelectedIndex = -1 Then
            ShowValidationError("სქესი სავალდებულოა.", cmbGender)
            Return False
        End If

        Dim phone As String = txtPhone.Text.Trim()
        If phone.Length > 0 Then
            If phone.Length <> 9 OrElse Not phone.All(AddressOf Char.IsDigit) OrElse Not phone.StartsWith("5") Then
                ShowValidationError("მობ. ნომერი უნდა იწყებოდეს 5-ით და შედგებოდეს 9 ციფრისგან.", txtPhone)
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub ShowValidationError(message As String, control As Control)
        MessageBox.Show(message, "ვალიდაცია", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        control.Focus()
    End Sub

    Private Function BuildPatientFromInput() As Patient
        Return New Patient() With {
            .PersonalNumber = txtPersonalNumber.Text.Trim(),
            .FullName = (txtFirstName.Text.Trim() & " " & txtLastName.Text.Trim()).Trim(),
            .Dob = dtpDob.Value.Date,
            .GenderID = CInt(cmbGender.SelectedValue),
            .Phone = txtPhone.Text.Trim(),
            .Address = txtAddress.Text.Trim()
        }
    End Function

    ' FullName-ში პირველი სიტყვა = სახელი, დანარჩენი = გვარი
    Private Function SplitFullName(fullName As String) As String()
        If String.IsNullOrWhiteSpace(fullName) Then
            Return {String.Empty, String.Empty}
        End If

        Dim trimmed As String = fullName.Trim()
        Dim spaceIdx As Integer = trimmed.IndexOf(" "c)

        If spaceIdx = -1 Then
            Return {trimmed, String.Empty}
        End If

        Dim first As String = trimmed.Substring(0, spaceIdx)
        Dim last As String = trimmed.Substring(spaceIdx + 1).Trim()
        Return {first, last}
    End Function

End Class
