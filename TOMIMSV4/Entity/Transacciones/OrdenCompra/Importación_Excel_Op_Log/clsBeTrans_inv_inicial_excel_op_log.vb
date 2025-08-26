Public Class clsBeTrans_inv_inicial_excel_op_log
    Implements ICloneable

    Public Property IdInvIniExcel() As Integer = 0
    Public Property No_linea() As Integer = 0
    Public Property Barra() As String = ""
    Public Property Codigo_producto() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Nit_facturar_a() As String = ""
    Public Property Nit_propietario() As String = ""
    Public Property Propietario() As String = ""
    Public Property Shipper() As String = ""
    Public Property Pieces() As Double = 0.0
    Public Property Peso_kgs() As Double = 0.0
    Public Property Cbms() As String = ""
    Public Property Valor_aduana() As Double = 0.0
    Public Property Fob() As Double = 0.0
    Public Property Flete() As Double = 0.0
    Public Property Seguro() As Double = 0.0
    Public Property Dai() As Double = 0.0
    Public Property Iva() As Double = 0.0
    Public Property Tipo_cambio() As Double = 0.0
    Public Property Umbas() As String = ""
    Public Property Presentacion() As String = ""
    Public Property Factor_presentacion() As Double = 0.0
    Public Property Bultos_por_pallet() As Double = 0.0
    Public Property Clasificacion() As String = ""
    Public Property Pol_scan_poliza() As String = ""
    Public Property Scan_ticket() As String = ""
    Public Property Nombre_operador() As String = ""
    Public Property Nit_consolidador() As String = ""
    Public Property Pol_numero_orden() As String = ""
    Public Property Pol_numero_duca() As String = ""
    Public Property Pol_clave_aduana() As String = ""
    Public Property Pol_nit_importador() As String = ""
    Public Property Pol_regimen() As String = ""
    Public Property Pol_clase() As String = ""
    Public Property Pol_pais_procedencia() As String = ""
    Public Property Pol_modo_transporte() As String = ""
    Public Property Pol_tipo_cambio() As Double = 0.0
    Public Property Pol_total_valor_aduana() As Double = 0.0
    Public Property Pol_total_peso_bruto() As Double = 0.0
    Public Property Pol_totalfobusd() As Double = 0.0
    Public Property Pol_total_flete_usd() As Double = 0.0
    Public Property Pol_total_seguro_usd() As Double = 0.0
    Public Property Pol_totalotrosgastosusd() As Double = 0.0
    Public Property Pol_total_liquidar() As Double = 0.0
    Public Property Pol_total_general() As Double = 0.0
    Public Property Pol_codigo_poliza() As String = ""

    Public Property Pol_fecha_llegada() As Date = New Date(1900, 1, 1)
    Public Property Codigo_ubicacion() As String = ""
    Public Property Codigo_bodega() As String = ""
    Public Property Referencia() As String = ""
    Public Property Regularizado() As Boolean = False
    Public Property Fecha_procesado() As String = New Date(1900, 1, 1)
    Public Property Id_documento_ingreso() As Integer = 0

    Public Property Consolidado() As Boolean = False
    Public Property Licencia As String = ""

    Public Property Posiciones As Integer = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
