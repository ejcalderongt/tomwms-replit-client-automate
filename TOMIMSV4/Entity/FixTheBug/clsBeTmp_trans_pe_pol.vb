Public Class clsBeTmp_trans_pe_pol
    Implements ICloneable

    Public Property IdOrdenPedidoPol() As Integer = 0
    Public Property IdOrdenPedidoEnc() As Integer = 0
    Public Property Bl_no() As String = ""
    Public Property NoPoliza() As String = ""
    Public Property Pto_descarga() As String = ""
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
    Public Property Total_bultos_peso() As Double = 0.0
    Public Property Total_bultos_Peso_Neto() As Double = 0.0

    Public Property Total_usd() As Double = 0.0
    Public Property Total_flete() As Double = 0.0
    Public Property Total_seguro() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Clave_aduana() As String = ""
    Public Property Nit_imp_exp() As String = ""
    Public Property Clase() As String = ""
    Public Property Mod_transporte() As String = ""
    Public Property Total_liquidar() As Double = 0.0
    Public Property Total_general() As Double = 0.0
    Public Property Codigo_poliza() As String = ""
    Public Property Ticket() As String = ""
    Public Property Numero_orden() As String = ""
    Public Property Fecha_aceptacion() As Date = Date.Now
    Public Property Fecha_llegada() As Date = Date.Now
    Public Property Total_otros() As Double = 0.0
    Public Property IdRegimen() As Integer = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
