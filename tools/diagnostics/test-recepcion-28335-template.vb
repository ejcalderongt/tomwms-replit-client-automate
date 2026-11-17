Imports System
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports TOMWMS

' In-memory SQL adapter. No SqlClient reference and no network capability.
Class SqlConnection
End Class
Class SqlTransaction
End Class
Class SqlCommand
    Implements IDisposable
    Public Text As String
    Public Parameters As New Dictionary(Of String,Object)
    Sub New(s As String,c As SqlConnection,t As SqlTransaction)
        Text=s
    End Sub
    Function ExecuteNonQuery() As Integer
        If Not Text.StartsWith("UPDATE trans_oc_det_lote SET cantidad_recibida=") Then Throw New Exception("unexpected SQL")
        Dim r=FakeDb.Rows(CInt(Parameters("@Lote")))
        If CDbl(r("cantidad_recibida"))<>CDbl(Parameters("@Anterior")) Then Return 0
        r("cantidad_recibida")=Parameters("@Nueva")
        Return 1
    End Function
    Sub Dispose() Implements IDisposable.Dispose
    End Sub
End Class
Class SqlDataAdapter
    Implements IDisposable
    Private cmd As SqlCommand
    Sub New(c As SqlCommand)
        cmd=c
    End Sub
    Sub Fill(dt As DataTable)
        If Not cmd.Text.StartsWith("SELECT cantidad,cantidad_recibida,") Then Throw New Exception("unexpected query")
        Dim r=FakeDb.Rows(CInt(cmd.Parameters("@Lote")))
        For Each c As DataColumn In r.Table.Columns
            dt.Columns.Add(c.ColumnName,c.DataType)
        Next
        dt.Rows.Add(r.ItemArray)
    End Sub
    Sub Dispose() Implements IDisposable.Dispose
    End Sub
End Class
Module Extensions
    <Runtime.CompilerServices.Extension>
    Sub AddWithValue(p As Dictionary(Of String,Object),k As String,v As Object)
        p.Add(k,v)
    End Sub
End Module
Class FakeDb
    Public Shared Rows As New Dictionary(Of Integer,DataRow)
End Class
Class Subject
    'PRODUCTION_HELPER
    Public Shared Sub Run(l As List(Of clsBeTrans_oc_det_lote),d As List(Of clsBeTrans_re_det),s As List(Of clsBeStock_rec),Optional controlTC As Boolean=True)
        ActualizarLotesCajaMaster(28335,28359,controlTC,l,d,s,New SqlConnection,New SqlTransaction)
    End Sub
End Class
Module Program
    Dim lots As List(Of clsBeTrans_oc_det_lote)
    Dim dets As List(Of clsBeTrans_re_det)
    Dim stocks As List(Of clsBeStock_rec)
    Sub Setup(Optional q1 As Double=4, Optional q2 As Double=3)
        lots=New List(Of clsBeTrans_oc_det_lote)
        dets=New List(Of clsBeTrans_re_det)
        stocks=New List(Of clsBeStock_rec)
        FakeDb.Rows.Clear()
        For i=0 To 1
            Dim q=If(i=0,q1,q2)
            Dim l As New clsBeTrans_oc_det_lote With {.IdOrdenCompraEnc=28359,.IdOrdenCompraDet=2,.IdOrdenCompraDetLote=1348+i,.IdProductoBodega=1431878,.IdProductoTallaColor=40159,.No_linea=1,.Lic_Plate="J100000635",.Cantidad=q,.Cantidad_recibida=0}
            lots.Add(l)
            dets.Add(New clsBeTrans_re_det With {.IdRecepcionEnc=28335,.IdOrdenCompraEnc=28359,.IdOrdenCompraDet=2,.IdRecepcionDet=-(i+1),.IdProductoBodega=1431878,.IdProductoTallaColor=40159,.No_Linea=1,.IsNew=True,.cantidad_recibida=q,.Lic_plate=""})
            stocks.Add(New clsBeStock_rec With {.IdRecepcionEnc=28335,.IdRecepcionDet=-(i+1),.IdProductoBodega=1431878,.IdProductoTallaColor=40159,.No_linea=1,.Cantidad=q})
            Dim t As New DataTable
            For Each name In {"cantidad","cantidad_recibida","IdProductoBodega","no_linea","IdProductoTallaColor","lic_plate","activo"}
                t.Columns.Add(name,If(name="lic_plate",GetType(String),If(name="activo",GetType(Boolean),GetType(Double))))
            Next
            FakeDb.Rows.Add(1348+i,t.Rows.Add(q,0,1431878,1,40159,"J100000635",True))
        Next
    End Sub
    Sub Check(ok As Boolean,s As String)
        If Not ok Then Throw New Exception(s)
    End Sub
    Sub Reject(label As String)
        Dim rejected=False
        Try
            Subject.Run(lots,dets,stocks)
        Catch ex As ApplicationException
            rejected=True
        End Try
        Check(rejected,"accepted invalid case: " & label)
        Console.WriteLine("PASS reject: " & label)
    End Sub
    Sub Main()
        Setup():Subject.Run(lots,dets,stocks)
        Check(CDbl(FakeDb.Rows(1348)("cantidad_recibida"))=4 AndAlso CDbl(FakeDb.Rows(1349)("cantidad_recibida"))=3,"4+3 allocation")
        Console.WriteLine("PASS server: 4+3 with empty physical LP, distinct lot PKs")
        Reject("stale replay after successful receipt")
        Setup(3,4):Subject.Run(lots,dets,stocks)
        Check(CDbl(FakeDb.Rows(1348)("cantidad_recibida"))=3 AndAlso CDbl(FakeDb.Rows(1349)("cantidad_recibida"))=4,"reverse order")
        Setup()
        lots(0).Cantidad_recibida=3:FakeDb.Rows(1348)("cantidad_recibida")=3
        lots.RemoveAt(1):dets.RemoveAt(1):stocks.RemoveAt(1)
        dets(0).cantidad_recibida=1:stocks(0).Cantidad=1
        Subject.Run(lots,dets,stocks)
        Check(CDbl(FakeDb.Rows(1348)("cantidad_recibida"))=4,"recovery increment")
        Console.WriteLine("PASS server: recovery saldo 1 increments 3 to 4")
        Setup():dets(0).cantidad_recibida=5:stocks(0).Cantidad=5:Reject("quantity exceeds sum of pending lots")
        Setup():dets(0).IdRecepcionDet=0:dets(1).IdRecepcionDet=0:stocks(0).IdRecepcionDet=0:stocks(1).IdRecepcionDet=0:Reject("legacy duplicate temporary identities")
        Setup():FakeDb.Rows(1348)("IdProductoTallaColor")=40160:Reject("variant changed")
        Setup():FakeDb.Rows(1348)("cantidad_recibida")=1:Reject("concurrent saldo changed")
        Setup():lots(1).Lic_Plate="OTHER":Reject("mixed scanned licenses")
        Setup():dets(0).Lic_plate="OTHER":Reject("physical LP does not match scanned license")
        Setup():stocks(0).Cantidad=99:Reject("stock quantity mismatch")
        Setup():dets(0).cantidad_recibida=Double.NaN:Reject("NaN quantity")
        Setup():lots.Add(lots(0)):Reject("duplicate lot PK")
        Setup():stocks.Add(stocks(0)):Reject("extra stock")
        Setup():Subject.Run(lots,dets,stocks,False)
        Check(CDbl(FakeDb.Rows(1348)("cantidad_recibida"))=4 AndAlso CDbl(FakeDb.Rows(1349)("cantidad_recibida"))=3,"no talla/color")
        Console.WriteLine("PASS server: without talla/color control")
        Console.WriteLine("PASS: production helper tested in memory; no SQL Server connection. Transaction locking/rollback not simulated.")
    End Sub
End Module
