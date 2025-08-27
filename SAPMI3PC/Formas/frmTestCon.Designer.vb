<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTestCon
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.cmdConectar = New System.Windows.Forms.Button()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblSAP_DB_PW = New DevExpress.XtraEditors.LabelControl()
        Me.txtSAP_DB_PW = New DevExpress.XtraEditors.TextEdit()
        Me.lblSAP_DB_USR = New DevExpress.XtraEditors.LabelControl()
        Me.txtSAP_DB_USR = New DevExpress.XtraEditors.TextEdit()
        Me.lblSAP_USR_PW = New DevExpress.XtraEditors.LabelControl()
        Me.txtSAP_USR_PW = New DevExpress.XtraEditors.TextEdit()
        Me.lblSAP_USR = New DevExpress.XtraEditors.LabelControl()
        Me.txtSAP_USR = New DevExpress.XtraEditors.TextEdit()
        Me.lblSAP_COMPANY_DB = New DevExpress.XtraEditors.LabelControl()
        Me.txtSAP_COMPANY_DB = New DevExpress.XtraEditors.TextEdit()
        Me.lblLicenseServerSAPBO = New DevExpress.XtraEditors.LabelControl()
        Me.txtLICENSESERVER_SAP_BO = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSERVER_BD_SAP = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtSAP_DB_PW.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSAP_DB_USR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSAP_USR_PW.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSAP_USR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSAP_COMPANY_DB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLICENSESERVER_SAP_BO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSERVER_BD_SAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdConectar
        '
        Me.cmdConectar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdConectar.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdConectar.Location = New System.Drawing.Point(0, 493)
        Me.cmdConectar.Name = "cmdConectar"
        Me.cmdConectar.Size = New System.Drawing.Size(710, 76)
        Me.cmdConectar.TabIndex = 0
        Me.cmdConectar.Text = "Connectar a SAP"
        Me.cmdConectar.UseVisualStyleBackColor = True
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.lblSAP_DB_PW)
        Me.GroupControl1.Controls.Add(Me.txtSAP_DB_PW)
        Me.GroupControl1.Controls.Add(Me.lblSAP_DB_USR)
        Me.GroupControl1.Controls.Add(Me.txtSAP_DB_USR)
        Me.GroupControl1.Controls.Add(Me.lblSAP_USR_PW)
        Me.GroupControl1.Controls.Add(Me.txtSAP_USR_PW)
        Me.GroupControl1.Controls.Add(Me.lblSAP_USR)
        Me.GroupControl1.Controls.Add(Me.txtSAP_USR)
        Me.GroupControl1.Controls.Add(Me.lblSAP_COMPANY_DB)
        Me.GroupControl1.Controls.Add(Me.txtSAP_COMPANY_DB)
        Me.GroupControl1.Controls.Add(Me.lblLicenseServerSAPBO)
        Me.GroupControl1.Controls.Add(Me.txtLICENSESERVER_SAP_BO)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.txtSERVER_BD_SAP)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(710, 493)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Parámetros"
        '
        'lblSAP_DB_PW
        '
        Me.lblSAP_DB_PW.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSAP_DB_PW.Appearance.Options.UseFont = True
        Me.lblSAP_DB_PW.Location = New System.Drawing.Point(122, 276)
        Me.lblSAP_DB_PW.Name = "lblSAP_DB_PW"
        Me.lblSAP_DB_PW.Size = New System.Drawing.Size(111, 24)
        Me.lblSAP_DB_PW.TabIndex = 14
        Me.lblSAP_DB_PW.Text = "SAP_DB_PW"
        '
        'txtSAP_DB_PW
        '
        Me.txtSAP_DB_PW.EditValue = "4P2N9RbzlqD6"
        Me.txtSAP_DB_PW.Location = New System.Drawing.Point(379, 270)
        Me.txtSAP_DB_PW.Name = "txtSAP_DB_PW"
        Me.txtSAP_DB_PW.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAP_DB_PW.Properties.Appearance.Options.UseFont = True
        Me.txtSAP_DB_PW.Size = New System.Drawing.Size(250, 30)
        Me.txtSAP_DB_PW.TabIndex = 15
        '
        'lblSAP_DB_USR
        '
        Me.lblSAP_DB_USR.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSAP_DB_USR.Appearance.Options.UseFont = True
        Me.lblSAP_DB_USR.Location = New System.Drawing.Point(122, 248)
        Me.lblSAP_DB_USR.Name = "lblSAP_DB_USR"
        Me.lblSAP_DB_USR.Size = New System.Drawing.Size(119, 24)
        Me.lblSAP_DB_USR.TabIndex = 12
        Me.lblSAP_DB_USR.Text = "SAP_DB_USR"
        '
        'txtSAP_DB_USR
        '
        Me.txtSAP_DB_USR.EditValue = "sa"
        Me.txtSAP_DB_USR.Location = New System.Drawing.Point(379, 242)
        Me.txtSAP_DB_USR.Name = "txtSAP_DB_USR"
        Me.txtSAP_DB_USR.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAP_DB_USR.Properties.Appearance.Options.UseFont = True
        Me.txtSAP_DB_USR.Size = New System.Drawing.Size(250, 30)
        Me.txtSAP_DB_USR.TabIndex = 13
        '
        'lblSAP_USR_PW
        '
        Me.lblSAP_USR_PW.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSAP_USR_PW.Appearance.Options.UseFont = True
        Me.lblSAP_USR_PW.Location = New System.Drawing.Point(122, 220)
        Me.lblSAP_USR_PW.Name = "lblSAP_USR_PW"
        Me.lblSAP_USR_PW.Size = New System.Drawing.Size(121, 24)
        Me.lblSAP_USR_PW.TabIndex = 10
        Me.lblSAP_USR_PW.Text = "SAP_USR_PW"
        '
        'txtSAP_USR_PW
        '
        Me.txtSAP_USR_PW.EditValue = "Prueba123@"
        Me.txtSAP_USR_PW.Location = New System.Drawing.Point(379, 214)
        Me.txtSAP_USR_PW.Name = "txtSAP_USR_PW"
        Me.txtSAP_USR_PW.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAP_USR_PW.Properties.Appearance.Options.UseFont = True
        Me.txtSAP_USR_PW.Size = New System.Drawing.Size(250, 30)
        Me.txtSAP_USR_PW.TabIndex = 11
        '
        'lblSAP_USR
        '
        Me.lblSAP_USR.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSAP_USR.Appearance.Options.UseFont = True
        Me.lblSAP_USR.Location = New System.Drawing.Point(122, 192)
        Me.lblSAP_USR.Name = "lblSAP_USR"
        Me.lblSAP_USR.Size = New System.Drawing.Size(82, 24)
        Me.lblSAP_USR.TabIndex = 8
        Me.lblSAP_USR.Text = "SAP_USR"
        '
        'txtSAP_USR
        '
        Me.txtSAP_USR.EditValue = "manager"
        Me.txtSAP_USR.Location = New System.Drawing.Point(379, 186)
        Me.txtSAP_USR.Name = "txtSAP_USR"
        Me.txtSAP_USR.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAP_USR.Properties.Appearance.Options.UseFont = True
        Me.txtSAP_USR.Size = New System.Drawing.Size(250, 30)
        Me.txtSAP_USR.TabIndex = 9
        '
        'lblSAP_COMPANY_DB
        '
        Me.lblSAP_COMPANY_DB.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSAP_COMPANY_DB.Appearance.Options.UseFont = True
        Me.lblSAP_COMPANY_DB.Location = New System.Drawing.Point(122, 164)
        Me.lblSAP_COMPANY_DB.Name = "lblSAP_COMPANY_DB"
        Me.lblSAP_COMPANY_DB.Size = New System.Drawing.Size(171, 24)
        Me.lblSAP_COMPANY_DB.TabIndex = 6
        Me.lblSAP_COMPANY_DB.Text = "SAP_COMPANY_DB"
        '
        'txtSAP_COMPANY_DB
        '
        Me.txtSAP_COMPANY_DB.EditValue = "SBO_GARESA"
        Me.txtSAP_COMPANY_DB.Location = New System.Drawing.Point(379, 158)
        Me.txtSAP_COMPANY_DB.Name = "txtSAP_COMPANY_DB"
        Me.txtSAP_COMPANY_DB.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAP_COMPANY_DB.Properties.Appearance.Options.UseFont = True
        Me.txtSAP_COMPANY_DB.Size = New System.Drawing.Size(250, 30)
        Me.txtSAP_COMPANY_DB.TabIndex = 7
        '
        'lblLicenseServerSAPBO
        '
        Me.lblLicenseServerSAPBO.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseServerSAPBO.Appearance.Options.UseFont = True
        Me.lblLicenseServerSAPBO.Location = New System.Drawing.Point(122, 105)
        Me.lblLicenseServerSAPBO.Name = "lblLicenseServerSAPBO"
        Me.lblLicenseServerSAPBO.Size = New System.Drawing.Size(226, 24)
        Me.lblLicenseServerSAPBO.TabIndex = 2
        Me.lblLicenseServerSAPBO.Text = "LICENSESERVER_SAP_BO"
        '
        'txtLICENSESERVER_SAP_BO
        '
        Me.txtLICENSESERVER_SAP_BO.EditValue = "192.168.2.211:40000"
        Me.txtLICENSESERVER_SAP_BO.Location = New System.Drawing.Point(379, 102)
        Me.txtLICENSESERVER_SAP_BO.Name = "txtLICENSESERVER_SAP_BO"
        Me.txtLICENSESERVER_SAP_BO.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLICENSESERVER_SAP_BO.Properties.Appearance.Options.UseFont = True
        Me.txtLICENSESERVER_SAP_BO.Size = New System.Drawing.Size(250, 30)
        Me.txtLICENSESERVER_SAP_BO.TabIndex = 3
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(122, 136)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(151, 24)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "SERVER_BD_SAP"
        '
        'txtSERVER_BD_SAP
        '
        Me.txtSERVER_BD_SAP.EditValue = "192.168.2.211"
        Me.txtSERVER_BD_SAP.Location = New System.Drawing.Point(379, 130)
        Me.txtSERVER_BD_SAP.Name = "txtSERVER_BD_SAP"
        Me.txtSERVER_BD_SAP.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSERVER_BD_SAP.Properties.Appearance.Options.UseFont = True
        Me.txtSERVER_BD_SAP.Size = New System.Drawing.Size(250, 30)
        Me.txtSERVER_BD_SAP.TabIndex = 5
        '
        'frmTestCon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 569)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.cmdConectar)
        Me.Name = "frmTestCon"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test conexión a SAPB1 - 10"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtSAP_DB_PW.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSAP_DB_USR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSAP_USR_PW.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSAP_USR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSAP_COMPANY_DB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLICENSESERVER_SAP_BO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSERVER_BD_SAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdConectar As Button
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtLICENSESERVER_SAP_BO As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblLicenseServerSAPBO As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSAP_DB_PW As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSAP_DB_PW As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblSAP_DB_USR As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSAP_DB_USR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblSAP_USR_PW As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSAP_USR_PW As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblSAP_USR As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSAP_USR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblSAP_COMPANY_DB As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSAP_COMPANY_DB As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSERVER_BD_SAP As DevExpress.XtraEditors.TextEdit
End Class
