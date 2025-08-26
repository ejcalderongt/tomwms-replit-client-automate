Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraReports.UI

Public Class XtraReport

    Public pBarCodeText As String
    Public psymbol As BarCodeGeneratorBase

    Public Property AltoBarra As Double = 0
    Public Property AnchoBarra As Double = 0


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Function CreateQRCodeBarCode() As XRBarCode

        Try

            Dim barCode As New XRBarCode()

            ' Set the bar code's type to QRCode.
            If psymbol IsNot Nothing Then
                'barCode.Symbology = psymbol
                ' Adjust the bar code's main properties.
                barCode.Text = pBarCodeText
                'barCode.Text = "12345"
                ' barCode.Width = 450
                'barCode.Height = 150

                barCode.Width = AnchoBarra
                barCode.Height = AltoBarra

                ' If the AutoModule property is set to false, uncomment the next line.
                barCode.AutoModule = True
                ' barcode.Module = 3;

                Select Case psymbol.Name

                    Case "QRCodeGenerator"

                        CType(barCode.Symbology, QRCodeGenerator).CompactionMode = QRCodeCompactionMode.AlphaNumeric
                        CType(barCode.Symbology, QRCodeGenerator).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H
                        CType(barCode.Symbology, QRCodeGenerator).Version = QRCodeVersion.AutoVersion

                        'Case "DataMatrix"

                        '    CType(barCode.Symbology, DataMatrixGenerator).CompactionMode = DataMatrixCompactionMode.Edifact

                    Case Else
                        Exit Select
                End Select

                ' Adjust the properties specific to the bar code type.

            End If

            Return barCode

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Sub Detail_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Detail.BeforePrint

        Try

            Me.Detail.Controls.Add(CreateQRCodeBarCode())

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub


End Class