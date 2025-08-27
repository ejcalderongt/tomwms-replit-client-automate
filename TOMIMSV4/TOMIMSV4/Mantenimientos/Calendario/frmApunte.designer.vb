<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmApunte
    Inherits DevExpress.XtraScheduler.UI.AppointmentForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbUbicacion = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.chkAllDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtLabel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtShowTimeAs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtResource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtResources.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtResources.ResourcesCheckedListBoxControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReminder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbReminder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel1.SuspendLayout()
        Me.progressPanel.SuspendLayout()
        CType(Me.tbProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbProgress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSubject
        '
        Me.lblSubject.Text = "&Asunto:"
        '
        'lblLocation
        '
        Me.lblLocation.Size = New System.Drawing.Size(60, 13)
        Me.lblLocation.Text = "&Localización:"
        '
        'lblLabel
        '
        Me.lblLabel.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel.Size = New System.Drawing.Size(37, 13)
        Me.lblLabel.TabIndex = 0
        Me.lblLabel.Text = "Estado:"
        '
        'lblStartTime
        '
        Me.lblStartTime.Size = New System.Drawing.Size(29, 13)
        Me.lblStartTime.Text = "Inicio:"
        '
        'lblEndTime
        '
        Me.lblEndTime.Size = New System.Drawing.Size(18, 13)
        Me.lblEndTime.Text = "Fin:"
        '
        'lblShowTimeAs
        '
        Me.lblShowTimeAs.Size = New System.Drawing.Size(38, 13)
        Me.lblShowTimeAs.Text = "Tiempo:"
        '
        'chkAllDay
        '
        Me.chkAllDay.Properties.Caption = "&Todo el Día"
        Me.chkAllDay.Size = New System.Drawing.Size(75, 19)
        Me.chkAllDay.TabIndex = 2
        '
        'btnOk
        '
        Me.btnOk.TabIndex = 17
        Me.btnOk.Text = "Guardar"
        '
        'btnCancel
        '
        Me.btnCancel.TabIndex = 18
        '
        'btnDelete
        '
        Me.btnDelete.TabIndex = 19
        Me.btnDelete.Text = "Eliminar"
        '
        'btnRecurrence
        '
        Me.btnRecurrence.TabIndex = 20
        Me.btnRecurrence.Text = "&Recurrencia"
        '
        'edtStartDate
        '
        Me.edtStartDate.EditValue = New Date(2005, 3, 31, 0, 0, 0, 0)
        '
        'edtEndDate
        '
        Me.edtEndDate.EditValue = New Date(2005, 3, 31, 0, 0, 0, 0)
        '
        'edtStartTime
        '
        Me.edtStartTime.EditValue = New Date(2005, 3, 31, 0, 0, 0, 0)
        Me.edtStartTime.Properties.Mask.EditMask = "t"
        '
        'edtEndTime
        '
        Me.edtEndTime.EditValue = New Date(2005, 3, 31, 0, 0, 0, 0)
        Me.edtEndTime.Properties.Mask.EditMask = "t"
        '
        'edtLabel
        '
        Me.edtLabel.TabIndex = 1
        '
        'edtShowTimeAs
        '
        '
        'tbSubject
        '
        '
        'edtResource
        '
        Me.edtResource.TabIndex = 4
        '
        'lblResource
        '
        Me.lblResource.Size = New System.Drawing.Size(48, 13)
        Me.lblResource.TabIndex = 3
        Me.lblResource.Text = "Recursos:"
        '
        'edtResources
        '
        '
        '
        '
        Me.edtResources.ResourcesCheckedListBoxControl.CheckOnClick = True
        Me.edtResources.ResourcesCheckedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.edtResources.ResourcesCheckedListBoxControl.Location = New System.Drawing.Point(0, 0)
        Me.edtResources.ResourcesCheckedListBoxControl.Name = ""
        Me.edtResources.ResourcesCheckedListBoxControl.Size = New System.Drawing.Size(200, 100)
        Me.edtResources.ResourcesCheckedListBoxControl.TabIndex = 0
        Me.edtResources.TabIndex = 5
        '
        'chkReminder
        '
        Me.chkReminder.Properties.Caption = "&Recordar"
        Me.chkReminder.Size = New System.Drawing.Size(66, 19)
        Me.chkReminder.TabIndex = 6
        '
        'tbDescription
        '
        Me.tbDescription.Size = New System.Drawing.Size(501, 65)
        Me.tbDescription.TabIndex = 14
        '
        'cbReminder
        '
        Me.cbReminder.TabIndex = 7
        '
        'tbLocation
        '
        '
        'progressPanel
        '
        Me.progressPanel.TabIndex = 13
        '
        'tbProgress
        '
        Me.tbProgress.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbProgress.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbProgress.TabIndex = 1
        '
        'lblPercentComplete
        '
        Me.lblPercentComplete.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lblPercentComplete.Location = New System.Drawing.Point(3, 10)
        Me.lblPercentComplete.Size = New System.Drawing.Size(75, 13)
        Me.lblPercentComplete.TabIndex = 0
        Me.lblPercentComplete.Text = "% &Completado:"
        '
        'lblPercentCompleteValue
        '
        Me.lblPercentCompleteValue.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.lblPercentCompleteValue.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(19, 272)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(56, 13)
        Me.LabelControl1.TabIndex = 15
        Me.LabelControl1.Text = "Ubicaciones"
        '
        'tbUbicacion
        '
        Me.tbUbicacion.Location = New System.Drawing.Point(16, 287)
        Me.tbUbicacion.Name = "tbUbicacion"
        Me.tbUbicacion.Size = New System.Drawing.Size(501, 47)
        Me.tbUbicacion.TabIndex = 16
        '
        'frmApunte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(528, 382)
        Me.Controls.Add(Me.tbUbicacion)
        Me.Controls.Add(Me.LabelControl1)
        Me.MinimumSize = New System.Drawing.Size(518, 306)
        Me.Name = "frmApunte"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Controls.SetChildIndex(Me.edtShowTimeAs, 0)
        Me.Controls.SetChildIndex(Me.edtEndTime, 0)
        Me.Controls.SetChildIndex(Me.edtEndDate, 0)
        Me.Controls.SetChildIndex(Me.btnRecurrence, 0)
        Me.Controls.SetChildIndex(Me.btnDelete, 0)
        Me.Controls.SetChildIndex(Me.btnCancel, 0)
        Me.Controls.SetChildIndex(Me.lblShowTimeAs, 0)
        Me.Controls.SetChildIndex(Me.lblEndTime, 0)
        Me.Controls.SetChildIndex(Me.tbLocation, 0)
        Me.Controls.SetChildIndex(Me.lblSubject, 0)
        Me.Controls.SetChildIndex(Me.lblLocation, 0)
        Me.Controls.SetChildIndex(Me.tbSubject, 0)
        Me.Controls.SetChildIndex(Me.lblStartTime, 0)
        Me.Controls.SetChildIndex(Me.btnOk, 0)
        Me.Controls.SetChildIndex(Me.edtStartDate, 0)
        Me.Controls.SetChildIndex(Me.edtStartTime, 0)
        Me.Controls.SetChildIndex(Me.panel1, 0)
        Me.Controls.SetChildIndex(Me.progressPanel, 0)
        Me.Controls.SetChildIndex(Me.tbDescription, 0)
        Me.Controls.SetChildIndex(Me.LabelControl1, 0)
        Me.Controls.SetChildIndex(Me.tbUbicacion, 0)
        CType(Me.chkAllDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtLabel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtShowTimeAs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtResource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtResources.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtResources.ResourcesCheckedListBoxControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReminder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbReminder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.progressPanel.ResumeLayout(False)
        Me.progressPanel.PerformLayout()
        CType(Me.tbProgress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbUbicacion As DevExpress.XtraEditors.MemoEdit

End Class
