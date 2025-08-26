Imports System.ServiceModel

<ServiceContract()>
Public Interface IServiceProductoMarca

    <OperationContract()>
    Function Insert(ByRef oBeProducto_marca As clsBeProducto_marca) As Integer

    <OperationContract()>
    Sub Insert_Multiple(ByVal pListObjPM As List(Of clsBeProducto_marca))

    <OperationContract()>
    Function Update(ByVal oBeProducto_marca As clsBeProducto_marca) As Integer

    <OperationContract()>
    Function Disable(ByRef oBeProducto_marca As clsBeProducto_marca) As Integer

    <OperationContract()>
    Sub Delete(ByVal pIdMarca As Integer)

    <OperationContract()>
    Sub Delete_By_IdPropietario(ByVal pIdPropietario As Integer)

    '<OperationContract()>
    'Function Listar(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

    <OperationContract()>
    Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_marca)

    '<OperationContract()>
    'Function Obtener(ByVal oBeProducto_marca As clsBeProducto_marca) As clsBeProducto_marca

    <OperationContract()>
    Function Get_Single_By_IdMarca(ByVal pIdMarca As Integer) As clsBeProducto_marca

    <OperationContract()>
    Function Get_Single_By_IdMarca_And_IdPropietario(ByVal pIdMarca As Integer, ByVal pIdPropietario As Integer) As clsBeProducto_marca

    <OperationContract()>
    Function Exist(ByVal pIdMarca As Integer) As Boolean

    <OperationContract()>
    Function Exist_Producto_Asociado(ByVal pIdMarca As Integer) As Boolean

    <OperationContract()>
    Function Max_IdMarca() As Integer

    '<OperationContract()>
    'Function NuevaEntidad() As clsBeProducto_marca

    '<OperationContract()>
    'Function Nuevo() As clsLnProducto_marca

    ' TODO: agregue aquí sus operaciones de servicio

End Interface

' Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.

'<DataContract()>
'Public Class Marca

'    Private mIdMarca As Integer
'    Private mIdPropietario As Integer
'    Private mNombre As String
'    Private mActivo As Boolean
'    Private mUser_agr As String
'    Private mFec_agr As Date
'    Private mUser_mod As String
'    Private mFec_mod As Date
'    Private mNombrePropietario As String

'    <DataMember()>
'    Public Property IdMarca() As Integer
'        Get
'            Return mIdMarca
'        End Get
'        Set(ByVal Value As Integer)
'            mIdMarca = Value
'        End Set
'    End Property

'    <DataMember()>
'    Public Property IdPropietario() As Integer
'        Get
'            Return mIdPropietario
'        End Get
'        Set(ByVal Value As Integer)
'            mIdPropietario = Value
'        End Set
'    End Property

'    <DataMember()>
'    Public Property Nombre() As String
'        Get
'            Return mNombre
'        End Get
'        Set(ByVal Value As String)
'            mNombre = Value
'        End Set
'    End Property

'    <DataMember()>
'    Public Property Activo() As Boolean
'        Get
'            Return mActivo
'        End Get
'        Set(ByVal Value As Boolean)
'            mActivo = Value
'        End Set
'    End Property

'    <DataMember()>
'    Public Property User_agr() As String
'        Get
'            Return mUser_agr
'        End Get
'        Set(ByVal Value As String)
'            mUser_agr = Value
'        End Set
'    End Property

'    <DataMember()>
'    Public Property Fec_agr() As Date
'        Get
'            Return mFec_agr
'        End Get
'        Set(ByVal Value As Date)
'            mFec_agr = Value
'        End Set
'    End Property

'    <DataMember()>
'    Public Property User_mod() As String
'        Get
'            Return mUser_mod
'        End Get
'        Set(ByVal Value As String)
'            mUser_mod = Value
'        End Set
'    End Property

'    <DataMember()>
'    Public Property Fec_mod() As Date
'        Get
'            Return mFec_mod
'        End Get
'        Set(ByVal Value As Date)
'            mFec_mod = Value
'        End Set
'    End Property

'    <DataMember()>
'    Public Property NombrePorpietario() As String
'        Get
'            Return mNombrePropietario
'        End Get
'        Set(ByVal Value As String)
'            mNombre = Value
'        End Set
'    End Property

'End Class
