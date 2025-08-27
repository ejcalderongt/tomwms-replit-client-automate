Imports DevExpress.XtraPrinting.BarCode

Public Class xtrbc

    Public pBarCodeText As String
    Public psymbol As BarCodeGeneratorBase
    Public Property AltoBarra As Double = 0
    Public Property AnchoBarra As Double = 0


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'Public Function CreateQRCodeBarCode() As XRBarCode

    '    Try

    '        Dim barCode As New XRBarCode()

    '        If psymbol IsNot Nothing Then
    '            'barCode.Symbology = psymbol
    '            ' Adjust the bar code's main properties.
    '            barCode.Text = pBarCodeText
    '            'XrTableCell1.Text = barCode.Text 
    '            'barCode.Text = "12345"
    '            ' barCode.Text = "B789J5"
    '            ' barCode.Width = 450
    '            'barCode.Height = 150

    '            barCode.Width = AnchoBarra
    '            barCode.Height = AltoBarra

    '            ' If the AutoModule property is set to false, uncomment the next line.
    '            barCode.AutoModule = True
    '            ' barcode.Module = 3;

    '            Select Case psymbol.Name

    '                Case "QRCodeGenerator"

    '                    CType(barCode.Symbology, QRCodeGenerator).CompactionMode = QRCodeCompactionMode.AlphaNumeric
    '                    CType(barCode.Symbology, QRCodeGenerator).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H
    '                    CType(barCode.Symbology, QRCodeGenerator).Version = QRCodeVersion.AutoVersion

    '                    'Case "DataMatrix"

    '                    '    CType(barCode.Symbology, DataMatrixGenerator).CompactionMode = DataMatrixCompactionMode.Edifact

    '                Case Else
    '                    Exit Select
    '            End Select

    '            ' Adjust the properties specific to the bar code type.

    '        End If

    '        Return barCode

    '        ' Set the bar code's type to QRCode.


    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    'Private Sub Detail_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Detail.BeforePrint

    '    Try

    '        Me.Detail.Controls.Add(CreateQRCodeBarCode())

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

End Class