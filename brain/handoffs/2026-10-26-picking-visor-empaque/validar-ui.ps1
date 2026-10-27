param([string]$Bin='C:\Users\yejc2\OneDrive\Documents\ChatGPT\TOMWMS\deployments\picking-empaque-20261026')
$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Data
Set-Location $Bin
[void][Reflection.Assembly]::LoadFrom((Join-Path $Bin 'Entity.dll'))
[void][Reflection.Assembly]::LoadFrom((Join-Path $Bin 'DAL.dll'))
[void][Reflection.Assembly]::LoadFrom((Join-Path $Bin 'WMS_MHS.exe'))
$flags=[Reflection.BindingFlags]'Instance,NonPublic'
$out=$PSScriptRoot
$form=$null;$hostForm=$null;$bitmap=$null
try{
 $form=New-Object TOMWMS.frmPicking
 $type=$form.GetType()
 $data=New-Object Data.DataSet
 [void]$data.ReadXml((Join-Path $out 'fixture-ui.xml'))
 $type.GetField('DSEmpaque',$flags).SetValue($form,$data)
 $grid=$form.Controls.Find('dgridEmpaque',$true)[0]
 $tab=$form.Controls.Find('tabEmpaque',$true)[0]
 if($tab.PageVisible){throw 'Tab visible sin configuración inicial'}
 # No se muestra frmPicking ni se ejecuta su Shown: sin acceso operativo a BD.
 $e=New-Object TOMWMS.clsBeTrans_picking_enc;$e.IdPickingEnc=88;$e.IdBodega=7;$e.Requiere_Preparacion=$true
 $form.BePickingEnc=$e
 $toggle=$type.GetProperty('chkEmpaquePorTarima',[Reflection.BindingFlags]'Instance,Public,NonPublic').GetValue($form,$null)
 $toggle.Checked=$true
 if(-not $tab.PageVisible){throw 'Tab oculto con Tarima'}
 Write-Output 'PASS tab oculto sin empaque y visible al configurar Tarima'
 [void]$type.GetMethod('Set_Formato_Grid_Empaque',$flags).Invoke($form,@())
 $grid.DataSource=$data.Tables['Licencias']
 $view=$grid.MainView;$view.PopulateColumns()
 [void]$type.GetMethod('Set_Columnas_Grid_Empaque',$flags).Invoke($form,@($view.PSObject.BaseObject,$false))
 $hostForm=New-Object Windows.Forms.Form
 $hostForm.ShowInTaskbar=$false;$hostForm.StartPosition='Manual';$hostForm.Location=New-Object Drawing.Point(-30000,-30000)
 $hostForm.ClientSize=New-Object Drawing.Size(1580,760)
 $grid.Parent=$hostForm;$grid.Dock='Fill'
 $hostForm.Show();$grid.ForceInitialize();[Windows.Forms.Application]::DoEvents()
 if($tab.Controls.Find('lblResumenEmpaque',$true).Count -gt 0){throw 'Resumen temporal sigue visible'}
 if($view.VisibleColumns.Count -ne 12 -or $view.VisibleColumns[$view.VisibleColumns.Count-1].FieldName -ne 'Operadores'){throw 'Vista no termina en Operadores'}
 if($view.Columns['MinGeneracionImpresion'].Visible){throw 'Métricas visibles'}
 Write-Output 'PASS vista simplificada: 12 columnas hasta Operadores, sin resumen temporal'
 $row=$view.LocateByValue('LicenciaEmpaque','JM000141')
 if($row -lt 0){throw 'No se localizó licencia de fixture'}
 $view.SetMasterRowExpanded($row,$true);[Windows.Forms.Application]::DoEvents()
 $child=$view.GetDetailView($row,0)
 if($null -eq $child -or $child.RowCount -ne 2){throw 'Expansión maestro/detalle no mostró las 2 líneas'}
 if($child.Columns['FechaRegistroPacking'].DisplayFormat.FormatString -ne 'dd/MM/yyyy HH:mm:ss'){throw 'Fecha/hora truncada en detalle'}
 if($view.OptionsBehavior.Editable -or $child.OptionsBehavior.Editable){throw 'Grid editable'}
 if($view.Columns['ClaveLicencia'].Visible){throw 'Clave interna expuesta'}
 Write-Output 'PASS grid real: expansión de licencia con 2 líneas, solo lectura y claves ocultas'
 $bitmap=New-Object Drawing.Bitmap($hostForm.ClientSize.Width,$hostForm.ClientSize.Height)
 $hostForm.DrawToBitmap($bitmap,(New-Object Drawing.Rectangle(0,0,$hostForm.Width,$hostForm.Height)))
 $bitmap.Save((Join-Path $out 'visor-empaque-preview.png'),[Drawing.Imaging.ImageFormat]::Png)
 Write-Output 'PASS render de vista maestro/detalle generado'
 $emptyRow=$view.LocateByValue('LicenciaEmpaque','JM000144')
 $view.SetMasterRowExpanded($emptyRow,$true);[Windows.Forms.Application]::DoEvents()
 $emptyChild=$view.GetDetailView($emptyRow,0)
 if($null -eq $emptyChild -or $emptyChild.RowCount -ne 0){throw 'Licencia vacía no se puede inspeccionar'}
 Write-Output 'PASS expansión de licencia vacía sin filas ficticias'
 $toggle.Checked=$false;$e.Requiere_Preparacion=$false
 [void]$type.GetMethod('Actualizar_Visibilidad_Empaque',$flags).Invoke($form,@())
 if($tab.PageVisible){throw 'Tab visible después de desactivar empaque'}
 Write-Output 'PASS tab oculto al quedar sin empaque'

}finally{
 if($bitmap){$bitmap.Dispose()}
 if($hostForm){$hostForm.Dispose()}
 if($form){$form.Dispose()}
}
