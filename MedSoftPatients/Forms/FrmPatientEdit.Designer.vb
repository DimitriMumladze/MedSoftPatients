<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPatientEdit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblFirstName = New System.Windows.Forms.Label()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.lblLastName = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.lblDob = New System.Windows.Forms.Label()
        Me.dtpDob = New System.Windows.Forms.DateTimePicker()
        Me.lblGender = New System.Windows.Forms.Label()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblFirstName
        '
        Me.lblFirstName.Location = New System.Drawing.Point(20, 30)
        Me.lblFirstName.Name = "lblFirstName"
        Me.lblFirstName.Size = New System.Drawing.Size(120, 25)
        Me.lblFirstName.TabIndex = 0
        Me.lblFirstName.Text = "სახელი: *"
        Me.lblFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(150, 30)
        Me.txtFirstName.MaxLength = 100
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(300, 23)
        Me.txtFirstName.TabIndex = 1
        '
        'lblLastName
        '
        Me.lblLastName.Location = New System.Drawing.Point(20, 65)
        Me.lblLastName.Name = "lblLastName"
        Me.lblLastName.Size = New System.Drawing.Size(120, 25)
        Me.lblLastName.TabIndex = 2
        Me.lblLastName.Text = "გვარი: *"
        Me.lblLastName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(150, 65)
        Me.txtLastName.MaxLength = 100
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(300, 23)
        Me.txtLastName.TabIndex = 3
        '
        'lblDob
        '
        Me.lblDob.Location = New System.Drawing.Point(20, 100)
        Me.lblDob.Name = "lblDob"
        Me.lblDob.Size = New System.Drawing.Size(120, 25)
        Me.lblDob.TabIndex = 4
        Me.lblDob.Text = "დაბ. თარიღი: *"
        Me.lblDob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpDob
        '
        Me.dtpDob.CustomFormat = "dd.MM.yyyy"
        Me.dtpDob.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDob.Location = New System.Drawing.Point(150, 100)
        Me.dtpDob.Name = "dtpDob"
        Me.dtpDob.Size = New System.Drawing.Size(300, 23)
        Me.dtpDob.TabIndex = 5
        '
        'lblGender
        '
        Me.lblGender.Location = New System.Drawing.Point(20, 135)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(120, 25)
        Me.lblGender.TabIndex = 6
        Me.lblGender.Text = "სქესი: *"
        Me.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbGender
        '
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.FormattingEnabled = True
        Me.cmbGender.Location = New System.Drawing.Point(150, 135)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(300, 24)
        Me.cmbGender.TabIndex = 7
        '
        'lblPhone
        '
        Me.lblPhone.Location = New System.Drawing.Point(20, 170)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(120, 25)
        Me.lblPhone.TabIndex = 8
        Me.lblPhone.Text = "მობ. ნომერი:"
        Me.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(150, 170)
        Me.txtPhone.MaxLength = 9
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(300, 23)
        Me.txtPhone.TabIndex = 9
        '
        'lblAddress
        '
        Me.lblAddress.Location = New System.Drawing.Point(20, 205)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(120, 25)
        Me.lblAddress.TabIndex = 10
        Me.lblAddress.Text = "მისამართი:"
        Me.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(150, 205)
        Me.txtAddress.MaxLength = 500
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(300, 50)
        Me.txtAddress.TabIndex = 11
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.Green
        Me.btnSave.Font = New System.Drawing.Font("Sylfaen", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(250, 370)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 35)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "შენახვა"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.Font = New System.Drawing.Font("Sylfaen", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(360, 370)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 35)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "გაუქმება"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'FrmPatientEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 421)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.lblAddress)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.lblPhone)
        Me.Controls.Add(Me.cmbGender)
        Me.Controls.Add(Me.lblGender)
        Me.Controls.Add(Me.dtpDob)
        Me.Controls.Add(Me.lblDob)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.lblLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.lblFirstName)
        Me.Font = New System.Drawing.Font("Sylfaen", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPatientEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "პაციენტის დამატება"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblFirstName As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents lblLastName As Label
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents lblDob As Label
    Friend WithEvents dtpDob As DateTimePicker
    Friend WithEvents lblGender As Label
    Friend WithEvents cmbGender As ComboBox
    Friend WithEvents lblPhone As Label
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents lblAddress As Label
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
End Class
