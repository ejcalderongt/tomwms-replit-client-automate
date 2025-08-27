Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ITareaHH" in both code and config file together.
<ServiceContract()>
Public Interface ITareaHH

    ''' <summary>
    ''' WCF creado por Ricardo García 09-05-2016
    ''' </summary>
    ''' <param name="pBeTarea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function Guardar(ByRef pBeTarea As clsBeTarea_hh) As Integer

    <OperationContract()>
    Function Eliminar(ByRef pBeTarea As clsBeTarea_hh) As Integer

    <OperationContract()>
    Function Exists(ByVal pIdTareaHH As Integer) As Boolean

    <OperationContract()>
    Function GetSingle(ByVal pIdTareaHH As Integer) As clsBeTarea_hh

    <OperationContract()>
    Function GetAll() As List(Of clsBeTarea_hh)

    <OperationContract()>
    Function GetTarea(ByVal pIdBodega As Integer) As DataTable

End Interface
