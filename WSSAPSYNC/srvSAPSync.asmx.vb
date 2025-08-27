Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Services

'#EJC20200402
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class srvSAPSync
    Inherits System.Web.Services.WebService

    <WebMethod>
    Public Function Get_Stock_By_ItemCode_And_WsCode(ByVal ITEMCODE As String,
                                                     ByVal WHSCODE As String) As List(Of clsSBO_Stock_Item)

        Get_Stock_By_ItemCode_And_WsCode = Nothing

        Dim listaStock As New List(Of clsSBO_Stock_Item)
        Dim lConnection As SqlConnection = New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim sql As String = "EXEC dbo.SBO_STOCK_ITEMCODE_GET @ITEMCODE,@WHSCODE"
            'cambie el query a sdk.
            '#EJC20200315: Revisado, el cambio es correcto.
            Dim cmd As SqlCommand = New SqlCommand(sql, lConnection, lTransaction)
            cmd.Parameters.AddWithValue("ITEMCODE", ITEMCODE)
            cmd.Parameters.AddWithValue("WHSCODE", WHSCODE)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                listaStock.Add(New clsSBO_Stock_Item(reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetDecimal(3),
                    reader.GetDecimal(4),
                    reader.GetDecimal(5),
                    reader.GetDecimal(6),
                    reader.GetDecimal(7)))
            End While

            lTransaction.Commit()

            Return listaStock

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = System.Data.ConnectionState.Open Then lConnection.Close()
        End Try


    End Function

    <WebMethod>
    Public Function Get_Stock_By_WsCode(ByVal WHSCODE As String) As List(Of clsSBO_Stock_Item)

        Dim listaStock As New List(Of clsSBO_Stock_Item)
        Dim lConnection As SqlConnection = New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sql As String = "EXEC dbo.SBO_STOCK_WAREHOUSE_GET @WHSCODE"
            Dim cmd As SqlCommand = New SqlCommand(sql, lConnection, lTransaction)
            cmd.Parameters.AddWithValue("WHSCODE", WHSCODE)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                listaStock.Add(New clsSBO_Stock_Item(reader.GetString(0),
                    reader.GetDecimal(1),
                    reader.GetDecimal(2),
                    reader.GetDecimal(3),
                    reader.GetDecimal(4),
                    reader.GetDecimal(5),
                    0,
                    0))
            End While

            lTransaction.Commit()

            Return listaStock

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = System.Data.ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class