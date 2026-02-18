Imports System.Runtime.Serialization

Public Class clsDataContractDI
    ' Función para determinar el tipo de ajuste
    Public Shared Function GetTipoAjuste(detalle As Object) As tTipoAjusteWMS
        If detalle.Cant_stock < detalle.Cantidad Then
            Return tTipoAjusteWMS.Ajuste_Positivo
        ElseIf detalle.Cant_stock > detalle.Cantidad Then
            Return tTipoAjusteWMS.Ajuste_Negativo
        ElseIf detalle.Lote_stock <> detalle.Lote Then
            Return tTipoAjusteWMS.Ajuste_Lote
        ElseIf detalle.Fecha_vence_stock <> detalle.Fecha_vence Then
            Return tTipoAjusteWMS.Ajuste_Vencimiento
        ElseIf detalle.IdProductoEstado <> detalle.IdProductoEstado Then
            Return tTipoAjusteWMS.Ajuste_Estado
        Else
            Throw New Exception("No se pudo determinar el tipo de ajuste.")
        End If
    End Function

    <DataContract(Name:="EstadoOC")>
    Public Enum tEstadoOC
        <EnumMember>
        NUEVA = 1
        <EnumMember>
        ASIGNADA = 2
        <EnumMember>
        EN_PROCESO = 3
        <EnumMember>
        CERRADA = 4
        <EnumMember>
        ANULADA = 5
        <EnumMember>
        BACK_ORDER = 6
    End Enum

    <DataContract(Name:="IdTipoDocumentoIngreso")>
    Public Enum tTipoDocumentoIngreso

        <EnumMember>
        NoDefinido = 0
        <EnumMember>
        Ingreso = 1
        <EnumMember>
        Devolucion = 2
        <EnumMember>
        Transferencia = 3
        <EnumMember>
        Transferencia_WMS = 4
        <EnumMember>
        Poliza_DUCA = 5
        <EnumMember>
        Orden_De_Produccion = 6
        <EnumMember>
        Ingreso_Consolidado = 7
        <EnumMember>
        Transferencia_de_Ingreso = 8
        <EnumMember>
        Orden_De_Compra_Interna = 9
        <EnumMember>
        Ingreso_Almacén_General_Con_Póliza = 10
        <EnumMember>
        Liquidacion_De_Ruta_Devolucion = 11
        <EnumMember>
        Devolucion_Venta = 12
        <EnumMember>
        Ingreso_importación = 13
        <EnumMember>
        Ingreso_Inventario_Inicial = 14
        <EnumMember>
        Ingreso_Por_NC_Anulada = 15
        <EnumMember>
        Factura_Reserva_Proveedor = 16
        <EnumMember>
        Ingreso_nota_credito = 17
    End Enum

    <DataContract(Name:="IdTipoDocumentoSalida")>
    Public Enum tTipoDocumentoSalida

        <EnumMember>
        Pedido_De_Bodega = 1
        <EnumMember>
        Pedido_Consolidado = 2
        <EnumMember>
        Pedido_De_Cliente = 3
        <EnumMember>
        Transferencia_Interna_WMS = 4
        <EnumMember>
        Pedido_De_Exportacion = 5
        <EnumMember>
        Transferencia_Directa = 6
        <EnumMember>
        Envio_Consolidado_Almacen = 7
        <EnumMember>
        Requisicion = 8
        <EnumMember>
        Pedido_De_Venta_NAV = 9
        <EnumMember>
        Transferencia_Directa_WMS = 10
        <EnumMember>
        Pedido_De_Venta_WM = 11
        <EnumMember>
        Devolucion_Proveedor = 12
        ''' <summary>
        '''#EJC20240527: 13	TRAS_SAP	Traslado_Por_Estados_SAp - Becofarma.
        ''' </summary>
        <EnumMember>
        Traslado_Por_Estados_SAP = 13
        <EnumMember>
        Factura_Deudor = 14

        Factura_Reserva_Cliente = 15
    End Enum

    <DataContract(Name:="IdTipoRubroERP")>
    Public Enum tRubroERP

        <EnumMember>
        CantidadUMBas = 1
        <EnumMember>
        Posicion = 2
        <EnumMember>
        Cantidad = 3
        <EnumMember>
        Presentacion = 4
        <EnumMember>
        Tarimas = 5
        <EnumMember>
        CIF_DAI_IVA = 6
        <EnumMember>
        Clasificacion = 7

    End Enum

    <DataContract(Name:="IdTipoAlmacen")>
    Public Enum tTipoAlmacen

        <EnumMember>
        General = 1
        <EnumMember>
        Fiscal = 3

    End Enum

    <DataContract(Name:="ReferenciaAjusteERP")>
    Public Enum tReferenciaAjusteERP

        <EnumMember>
        AjustesDiarioAlmacen = 1
        <EnumMember>
        CalculaAjusteAlmacen = 2
        <EnumMember>
        RegistrasAjustesDiarios = 3

    End Enum

    <DataContract(Name:="TipoRotacion")>
    Public Enum tTipoRotacion

        <EnumMember>
        FIFO = 1
        <EnumMember>
        LIFO = 2
        <EnumMember>
        FEFO = 3
        <EnumMember>
        UPSR = 4

    End Enum

    <DataContract(Name:="TipoTarea")>
    Public Enum tTipoTarea

        RECE = 1
        UBIC = 2
        CEST = 3
        TRAS = 4
        DESP = 5
        INVE = 6
        AJUS = 7
        PIK = 8
        DEVP = 9
        PICK = 10
        VERI = 11
        PACK = 12
        AJCANTP = 13
        AJPESO = 14
        AJVENC = 15
        AJLOTE = 16
        AJCANTN = 17
        AJCANTNI = 18
        AJCANTPI = 19
        EXPLOSION = 20
        UBIC_PICK = 21
        ANUL_PICKING = 22
        REABMAN = 23
        REUBICSTOCKRES = 24
        REEMP_BE_PICK = 25
        REEMP_ME_PICK = 26
        REEMP_NE_PICK = 27
        REEMP_BE_VERI = 28
        REEMP_ME_VERI = 29
        AJLOTENI = 30
        AJLOTEPI = 31
        AJVENCENI = 32
        AJVENCEPI = 33
        CESTI = 34
        CUBII = 35
        TALLACOLOR = 36

    End Enum

    <DataContract(Name:="OpcionLiberaStock")>
    Public Enum tOpcionLiberaStock

        Pedido = 1
        Despacho = 2
        StockReservado = 3

    End Enum

    <DataContract(Name:="OpcionLiberaStock")>
    Public Enum tSAP

        lote_defecto_entrada_mercancia_sap = 1
        Fecha_Vence_Defecto = 19900101
        StockReservado = 3

    End Enum

    <DataContract(Name:="SimbologiaEtiqueta")>
    Public Enum tSimbologiaEtiqueta
        <EnumMember>
        Codabar = 1
        <EnumMember>
        Code39 = 3
        <EnumMember>
        Code93 = 5
        <EnumMember>
        Code128 = 7
        <EnumMember>
        UPCA = 12
        <EnumMember>
        EAN8 = 13
        <EnumMember>
        EAN128 = 14
        <EnumMember>
        PDF417 = 20
        <EnumMember>
        QRCode = 22

    End Enum

    <DataContract(Name:="trans_re_tr")>
    Public Enum tTipo_Rec
        ''' <summary>
        ''' HCOC00: Ingreso con Orden de Compra, Permite realizar el ingreso con HH de un D.I.
        ''' </summary>
        <EnumMember>
        HCOC00 = 1
        ''' <summary>
        ''' HCOD00: Devolución de Pedido, Permite realizar el ingreso de una devolución con HH a partir de un pedido.
        ''' </summary>
        <EnumMember>
        HCOD00 = 2

        ''' <summary>
        ''' HHSR00: Ingreso sin referencia HH. Permite realizar un ingreso en la HH, sin detalle de lo que se va a recibir.
        ''' </summary>
        <EnumMember>
        HHSR00 = 3
        ''' <summary>
        ''' HSOC00. Ingreso sin Orden de Compra. Permite realizar un ingreso con HH "Ciego" sin O.C.
        ''' </summary>
        <EnumMember>
        HSOC00 = 4
        ''' <summary>
        ''' HSOD00. Ingreso de Devolución sin referencia. Permite realizar el ingreso con HH de una devolución sin referencia
        ''' </summary>
        <EnumMember>
        HSOD00 = 5
        ''' <summary>
        ''' MCOC00. Ingreso con Orden de Compra. Permite realizar el ingreso en PC de una O.C.
        ''' </summary>
        <EnumMember>
        MCOC00 = 6
        ''' <summary>
        ''' MCOD00. Devolución de Pedido (PC). Permite realizar el ingreso de una devolución en PC a partir de un pedido
        ''' </summary>
        <EnumMember>
        MCOD00 = 7
        ''' <summary>
        ''' MSOC00. Ingreso sin Orden de Compra  (PC). Permite realizar un ingreso en PC "Ciego" sin O.C.
        ''' </summary>
        <EnumMember>
        MSOC00 = 8
        ''' <summary>
        ''' MSOD00. Devolución sin referencia (PC). Permite realizar el ingreso en PC de una devolución sin referencia
        ''' </summary>
        <EnumMember>
        MSOD00 = 9
        ''' <summary>
        ''' PICH000. Ingreso sin referencia verificado con HH. Permite realizar la impresión de etiquetas para pre-ingreso
        ''' </summary>
        <EnumMember>
        PICH000 = 10
    End Enum

    <DataContract(Name:="Manufacturing_Process")>
    Public Enum Manufacturing_Process

        <EnumMember>
        Sin_Proceso_Nativo = 0
        <EnumMember>
        Pegar_Stickers = 1
        <EnumMember>
        Impresion_Inkjet = 2
        <EnumMember>
        Atado_Promocional = 3
        <EnumMember>
        Retirar_Stickers = 4
        <EnumMember>
        Varios = 5

    End Enum

    Public Enum Estado_Enviado_SAP
        No_Definido = 0
        Enviado = 1
        No_Enviado = 2
    End Enum

    Public Enum Tipo_Cuenta_Prefactura
        Prefactura = 1
        Precuenta = 0
    End Enum

    <DataContract(Name:="tTipoAjusteWMS")>
    Public Enum tTipoAjusteWMS

        <EnumMember>
        Ajuste_Lote = 1
        <EnumMember>
        Ajuste_Vencimiento = 2
        <EnumMember>
        Ajuste_Positivo = 3
        <EnumMember>
        Ajuste_Peso = 4
        <EnumMember>
        Ajuste_Negativo = 5
        <EnumMember>
        Ajuste_Estado = 6
        <EnumMember>
        Ajuste_TallaColor = 7

    End Enum

    <DataContract(Name:="tTipoMonedaPrefacturacion")>
    Public Enum tTipoMonedaPrefacturacion

        <EnumMember>
        Quetzal = 2
        <EnumMember>
        Dolar = 3

    End Enum

End Class