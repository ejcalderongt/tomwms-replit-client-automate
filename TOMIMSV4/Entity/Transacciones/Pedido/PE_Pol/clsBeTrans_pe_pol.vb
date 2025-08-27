Public Class clsBeTrans_pe_pol

    Implements ICloneable
    Implements IDisposable

    Public Property IdOrdenPedidoPol() As Integer = 0
    Public Property IdOrdenPedidoEnc() As Integer = 0
    Public Property Bl_No() As String = ""
    Public Property NoPoliza() As String = ""
    Public Property Pto_Descarga() As String = ""
    Public Property Viaje_no() As String = ""
    Public Property Buque_no() As String = ""
    Public Property Remitente() As String = ""
    Public Property Fecha_abordaje() As Date = Date.Now
    Public Property Destino() As String = ""
    Public Property Dir_destino() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Po_number() As String = ""
    Public Property Cantidad() As Integer = 0
    Public Property Piezas() As Integer = 0
    Public Property Total_kgs() As Double = 0.0
    Public Property Cbm() As Double = 0.0
    Public Property Dua() As String = ""
    Public Property Fecha_poliza() As Date = Date.Now
    Public Property Pais_procede() As String = ""
    Public Property Tipo_cambio() As Double = 0.0
    Public Property Total_valoraduana() As Double = 0.0
    Public Property Total_lineas() As Integer = 0
    Public Property Total_bultos() As Integer = 0

    'Public Property Total_bultos_Peso_Bruto() As Double = 0.0

    Public Property Total_bultos_Peso() As Double = 0.0
    Public Property Total_bultos_Peso_Neto() As Double = 0.0
    Public Property Total_usd() As Double = 0.0
    Public Property Total_flete() As Double = 0.0
    Public Property Total_seguro() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    Public Property clave_aduana() As String = ""
    Public Property nit_imp_exp() As String = ""
    Public Property clase() As String = ""
    Public Property mod_transporte() As String = ""
    Public Property total_liquidar() As Double = 0.0
    Public Property total_general() As Double = 0.0


    'campos no existen en tabla
    Public Property codigo_poliza() As String = ""
    Public Property ticket() As String = ""
    Public Property numero_orden() As String = ""
    Public Property fecha_aceptacion() As DateTime = Now
    Public Property fecha_llegada() As DateTime = Now
    Public Property total_otros() As Double = 0

    Public Property activo As Boolean = 0

    Sub New()
    End Sub

    Sub New(ByRef IdOrdenPedidoPol As Integer, ByVal IdOrdenPedidoEnc As Integer, ByVal bl_no As String, ByVal NoPoliza As String,
            ByVal pto_descarga As String, ByVal viaje_no As String, ByVal buque_no As String, ByVal remitente As String,
            ByVal fecha_abordaje As Date, ByVal destino As String, ByVal dir_destino As String, ByVal descripcion As String,
            ByVal po_number As String, ByVal cantidad As Integer, ByVal piezas As Integer, ByVal total_kgs As Double, ByVal cbm As Double,
            ByVal dua As String, ByVal fecha_poliza As Date, ByVal pais_procede As String, ByVal tipo_cambio As Double, ByVal total_valoraduana As Double,
            ByVal total_lineas As Integer, ByVal total_bultos As Integer, ByVal total_bultos_peso As Double, ByVal total_usd As Double,
            ByVal total_flete As Double, ByVal total_seguro As Double, ByVal user_agr As String, ByVal fec_agr As Date,
            ByVal user_mod As String, ByVal fec_mod As Date,
            ByVal clave_aduana As String, ByVal nit_imp_exp As String, ByVal clase As String, ByVal mod_transporte As String,
            ByVal total_liquidar As Double, ByVal total_general As Double)
        Me.IdOrdenPedidoPol = IdOrdenPedidoPol
        Me.IdOrdenPedidoEnc = IdOrdenPedidoEnc
        Me.Bl_No = bl_no
        Me.NoPoliza = NoPoliza
        Me.Pto_Descarga = pto_descarga
        Me.Viaje_no = viaje_no
        Me.Buque_no = buque_no
        Me.Remitente = remitente
        Me.Fecha_abordaje = fecha_abordaje
        Me.Destino = destino
        Me.Dir_destino = dir_destino
        Me.Descripcion = descripcion
        Me.Po_number = po_number
        Me.Cantidad = cantidad
        Me.Piezas = piezas
        Me.Total_kgs = total_kgs
        Me.Cbm = cbm
        Me.Dua = dua
        Me.Fecha_poliza = fecha_poliza
        Me.Pais_procede = pais_procede
        Me.Tipo_cambio = tipo_cambio
        Me.Total_valoraduana = total_valoraduana
        Me.Total_lineas = total_lineas
        Me.Total_bultos = total_bultos
        'Me.Total_bultos_Peso_Bruto = total_bultos_peso
        'Me.Total_bultos_Peso_Neto = Total_bultos_Peso_Neto
        Me.Total_bultos_Peso = total_bultos_peso
        Me.Total_usd = total_usd
        Me.Total_flete = total_flete
        Me.Total_seguro = total_seguro
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod

        Me.clave_aduana = clave_aduana
        Me.nit_imp_exp = nit_imp_exp
        Me.clase = clase
        Me.mod_transporte = mod_transporte
        Me.total_liquidar = total_liquidar
        Me.total_general = total_general
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
