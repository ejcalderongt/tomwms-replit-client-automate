Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_bodega_ubicaciones_excel

    Public Shared Sub Cargar(ByRef oBeTrans_bodega_ubicaciones_excel As clsBeTrans_bodega_ubicaciones_excel, ByRef dr As DataRow)
        Try
            With oBeTrans_bodega_ubicaciones_excel
                .IdBodegaExcel = IIf(IsDBNull(dr.Item("IdBodegaExcel")), 0, dr.Item("IdBodegaExcel"))
                .Registro = IIf(IsDBNull(dr.Item("registro")), 0, dr.Item("registro"))
                .Cod_bodega = IIf(IsDBNull(dr.Item("cod_bodega")), 0, dr.Item("cod_bodega"))
                .Cod_area = IIf(IsDBNull(dr.Item("cod_area")), 0, dr.Item("cod_area"))
                .Cod_sector = IIf(IsDBNull(dr.Item("cod_sector")), 0, dr.Item("cod_sector"))
                .Tipo_ubicacion = IIf(IsDBNull(dr.Item("tipo_ubicacion")), "", dr.Item("tipo_ubicacion"))
                .Tipo_rack = IIf(IsDBNull(dr.Item("tipo_rack")), "", dr.Item("tipo_rack"))
                .Numero = IIf(IsDBNull(dr.Item("numero")), 0, dr.Item("numero"))
                .Ingreso_por = IIf(IsDBNull(dr.Item("ingreso_por")), "", dr.Item("ingreso_por"))
                .Orientacion = IIf(IsDBNull(dr.Item("orientacion")), "", dr.Item("orientacion"))
                .Orden = IIf(IsDBNull(dr.Item("orden")), "", dr.Item("orden"))
                .X = IIf(IsDBNull(dr.Item("x")), 0, dr.Item("x"))
                .Y = IIf(IsDBNull(dr.Item("y")), 0, dr.Item("y"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), "", dr.Item("nivel"))
                .Col1 = IIf(IsDBNull(dr.Item("col1")), "", dr.Item("col1"))
                .Col2 = IIf(IsDBNull(dr.Item("col2")), "", dr.Item("col2"))
                .Col3 = IIf(IsDBNull(dr.Item("col3")), "", dr.Item("col3"))
                .Col4 = IIf(IsDBNull(dr.Item("col4")), "", dr.Item("col4"))
                .Col5 = IIf(IsDBNull(dr.Item("col5")), "", dr.Item("col5"))
                .Col6 = IIf(IsDBNull(dr.Item("col6")), "", dr.Item("col6"))
                .Col7 = IIf(IsDBNull(dr.Item("col7")), "", dr.Item("col7"))
                .Col8 = IIf(IsDBNull(dr.Item("col8")), "", dr.Item("col8"))
                .Col9 = IIf(IsDBNull(dr.Item("col9")), "", dr.Item("col9"))
                .Col10 = IIf(IsDBNull(dr.Item("col10")), "", dr.Item("col10"))
                .Col11 = IIf(IsDBNull(dr.Item("col11")), "", dr.Item("col11"))
                .Col12 = IIf(IsDBNull(dr.Item("col12")), "", dr.Item("col12"))
                .Col13 = IIf(IsDBNull(dr.Item("col13")), "", dr.Item("col13"))
                .Col14 = IIf(IsDBNull(dr.Item("col14")), "", dr.Item("col14"))
                .Col15 = IIf(IsDBNull(dr.Item("col15")), "", dr.Item("col15"))
                .Col16 = IIf(IsDBNull(dr.Item("col16")), "", dr.Item("col16"))
                .Col17 = IIf(IsDBNull(dr.Item("col17")), "", dr.Item("col17"))
                .Col18 = IIf(IsDBNull(dr.Item("col18")), "", dr.Item("col18"))
                .Col19 = IIf(IsDBNull(dr.Item("col19")), "", dr.Item("col19"))
                .Col20 = IIf(IsDBNull(dr.Item("col20")), "", dr.Item("col20"))
                .Col21 = IIf(IsDBNull(dr.Item("col21")), "", dr.Item("col21"))
                .Col22 = IIf(IsDBNull(dr.Item("col22")), "", dr.Item("col22"))
                .Col23 = IIf(IsDBNull(dr.Item("col23")), "", dr.Item("col23"))
                .Col24 = IIf(IsDBNull(dr.Item("col24")), "", dr.Item("col24"))
                .Col25 = IIf(IsDBNull(dr.Item("col25")), "", dr.Item("col25"))
                .Col26 = IIf(IsDBNull(dr.Item("col26")), "", dr.Item("col26"))
                .Col27 = IIf(IsDBNull(dr.Item("col27")), "", dr.Item("col27"))
                .Col28 = IIf(IsDBNull(dr.Item("col28")), "", dr.Item("col28"))
                .Col29 = IIf(IsDBNull(dr.Item("col29")), "", dr.Item("col29"))
                .Col30 = IIf(IsDBNull(dr.Item("col30")), "", dr.Item("col30"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_bodega_ubicaciones_excel As clsBeTrans_bodega_ubicaciones_excel, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_bodega_ubicaciones_excel")
            Ins.Add("idbodegaexcel", "@idbodegaexcel", DataType.Parametro)
            Ins.Add("registro", "@registro", DataType.Parametro)
            Ins.Add("cod_bodega", "@cod_bodega", DataType.Parametro)
            Ins.Add("cod_area", "@cod_area", DataType.Parametro)
            Ins.Add("cod_sector", "@cod_sector", DataType.Parametro)
            Ins.Add("tipo_ubicacion", "@tipo_ubicacion", DataType.Parametro)
            Ins.Add("tipo_rack", "@tipo_rack", DataType.Parametro)
            Ins.Add("numero", "@numero", DataType.Parametro)
            Ins.Add("ingreso_por", "@ingreso_por", DataType.Parametro)
            Ins.Add("orientacion", "@orientacion", DataType.Parametro)
            Ins.Add("orden", "@orden", DataType.Parametro)
            Ins.Add("x", "@x", DataType.Parametro)
            Ins.Add("y", "@y", DataType.Parametro)
            Ins.Add("nivel", "@nivel", DataType.Parametro)
            Ins.Add("col1", "@col1", DataType.Parametro)
            Ins.Add("col2", "@col2", DataType.Parametro)
            Ins.Add("col3", "@col3", DataType.Parametro)
            Ins.Add("col4", "@col4", DataType.Parametro)
            Ins.Add("col5", "@col5", DataType.Parametro)
            Ins.Add("col6", "@col6", DataType.Parametro)
            Ins.Add("col7", "@col7", DataType.Parametro)
            Ins.Add("col8", "@col8", DataType.Parametro)
            Ins.Add("col9", "@col9", DataType.Parametro)
            Ins.Add("col10", "@col10", DataType.Parametro)
            Ins.Add("col11", "@col11", DataType.Parametro)
            Ins.Add("col12", "@col12", DataType.Parametro)
            Ins.Add("col13", "@col13", DataType.Parametro)
            Ins.Add("col14", "@col14", DataType.Parametro)
            Ins.Add("col15", "@col15", DataType.Parametro)
            Ins.Add("col16", "@col16", DataType.Parametro)
            Ins.Add("col17", "@col17", DataType.Parametro)
            Ins.Add("col18", "@col18", DataType.Parametro)
            Ins.Add("col19", "@col19", DataType.Parametro)
            Ins.Add("col20", "@col20", DataType.Parametro)
            Ins.Add("col21", "@col21", DataType.Parametro)
            Ins.Add("col22", "@col22", DataType.Parametro)
            Ins.Add("col23", "@col23", DataType.Parametro)
            Ins.Add("col24", "@col24", DataType.Parametro)
            Ins.Add("col25", "@col25", DataType.Parametro)
            Ins.Add("col26", "@col26", DataType.Parametro)
            Ins.Add("col27", "@col27", DataType.Parametro)
            Ins.Add("col28", "@col28", DataType.Parametro)
            Ins.Add("col29", "@col29", DataType.Parametro)
            Ins.Add("col30", "@col30", DataType.Parametro)
            Ins.Add("col31", "@col31", DataType.Parametro)
            Ins.Add("col32", "@col32", DataType.Parametro)
            Ins.Add("col33", "@col33", DataType.Parametro)
            Ins.Add("col34", "@col34", DataType.Parametro)
            Ins.Add("col35", "@col35", DataType.Parametro)
            Ins.Add("col36", "@col36", DataType.Parametro)
            Ins.Add("col37", "@col37", DataType.Parametro)
            Ins.Add("col38", "@col38", DataType.Parametro)
            Ins.Add("col39", "@col39", DataType.Parametro)
            Ins.Add("col40", "@col40", DataType.Parametro)
            Ins.Add("col41", "@col41", DataType.Parametro)
            Ins.Add("col42", "@col42", DataType.Parametro)
            Ins.Add("col43", "@col43", DataType.Parametro)
            Ins.Add("col44", "@col44", DataType.Parametro)
            Ins.Add("col45", "@col45", DataType.Parametro)
            Ins.Add("col46", "@col46", DataType.Parametro)
            Ins.Add("col47", "@col47", DataType.Parametro)
            Ins.Add("col48", "@col48", DataType.Parametro)
            Ins.Add("col49", "@col49", DataType.Parametro)
            Ins.Add("col50", "@col50", DataType.Parametro)
            Ins.Add("col51", "@col51", DataType.Parametro)
            Ins.Add("col52", "@col52", DataType.Parametro)
            Ins.Add("col53", "@col53", DataType.Parametro)
            Ins.Add("col54", "@col54", DataType.Parametro)
            Ins.Add("col55", "@col55", DataType.Parametro)
            Ins.Add("col56", "@col56", DataType.Parametro)
            Ins.Add("col57", "@col57", DataType.Parametro)
            Ins.Add("col58", "@col58", DataType.Parametro)
            Ins.Add("col59", "@col59", DataType.Parametro)
            Ins.Add("col60", "@col60", DataType.Parametro)
            Ins.Add("col61", "@col61", DataType.Parametro)
            Ins.Add("col62", "@col62", DataType.Parametro)
            Ins.Add("col63", "@col63", DataType.Parametro)
            Ins.Add("col64", "@col64", DataType.Parametro)
            Ins.Add("col65", "@col65", DataType.Parametro)
            Ins.Add("col66", "@col66", DataType.Parametro)
            Ins.Add("col67", "@col67", DataType.Parametro)
            Ins.Add("col68", "@col68", DataType.Parametro)
            Ins.Add("col69", "@col69", DataType.Parametro)
            Ins.Add("col70", "@col70", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGAEXCEL", oBeTrans_bodega_ubicaciones_excel.IdBodegaExcel))
            cmd.Parameters.Add(New SqlParameter("@REGISTRO", oBeTrans_bodega_ubicaciones_excel.Registro))
            cmd.Parameters.Add(New SqlParameter("@COD_BODEGA", oBeTrans_bodega_ubicaciones_excel.Cod_bodega))
            cmd.Parameters.Add(New SqlParameter("@COD_AREA", oBeTrans_bodega_ubicaciones_excel.Cod_area))
            cmd.Parameters.Add(New SqlParameter("@COD_SECTOR", oBeTrans_bodega_ubicaciones_excel.Cod_sector))
            cmd.Parameters.Add(New SqlParameter("@TIPO_UBICACION", oBeTrans_bodega_ubicaciones_excel.Tipo_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_RACK", oBeTrans_bodega_ubicaciones_excel.Tipo_rack))
            cmd.Parameters.Add(New SqlParameter("@NUMERO", oBeTrans_bodega_ubicaciones_excel.Numero))
            cmd.Parameters.Add(New SqlParameter("@INGRESO_POR", oBeTrans_bodega_ubicaciones_excel.Ingreso_por))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeTrans_bodega_ubicaciones_excel.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@ORDEN", oBeTrans_bodega_ubicaciones_excel.Orden))
            cmd.Parameters.Add(New SqlParameter("@X", oBeTrans_bodega_ubicaciones_excel.X))
            cmd.Parameters.Add(New SqlParameter("@Y", oBeTrans_bodega_ubicaciones_excel.Y))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeTrans_bodega_ubicaciones_excel.Nivel))
            cmd.Parameters.Add(New SqlParameter("@COL1", oBeTrans_bodega_ubicaciones_excel.Col1))
            cmd.Parameters.Add(New SqlParameter("@COL2", oBeTrans_bodega_ubicaciones_excel.Col2))
            cmd.Parameters.Add(New SqlParameter("@COL3", oBeTrans_bodega_ubicaciones_excel.Col3))
            cmd.Parameters.Add(New SqlParameter("@COL4", oBeTrans_bodega_ubicaciones_excel.Col4))
            cmd.Parameters.Add(New SqlParameter("@COL5", oBeTrans_bodega_ubicaciones_excel.Col5))
            cmd.Parameters.Add(New SqlParameter("@COL6", oBeTrans_bodega_ubicaciones_excel.Col6))
            cmd.Parameters.Add(New SqlParameter("@COL7", oBeTrans_bodega_ubicaciones_excel.Col7))
            cmd.Parameters.Add(New SqlParameter("@COL8", oBeTrans_bodega_ubicaciones_excel.Col8))
            cmd.Parameters.Add(New SqlParameter("@COL9", oBeTrans_bodega_ubicaciones_excel.Col9))
            cmd.Parameters.Add(New SqlParameter("@COL10", oBeTrans_bodega_ubicaciones_excel.Col10))
            cmd.Parameters.Add(New SqlParameter("@COL11", oBeTrans_bodega_ubicaciones_excel.Col11))
            cmd.Parameters.Add(New SqlParameter("@COL12", oBeTrans_bodega_ubicaciones_excel.Col12))
            cmd.Parameters.Add(New SqlParameter("@COL13", oBeTrans_bodega_ubicaciones_excel.Col13))
            cmd.Parameters.Add(New SqlParameter("@COL14", oBeTrans_bodega_ubicaciones_excel.Col14))
            cmd.Parameters.Add(New SqlParameter("@COL15", oBeTrans_bodega_ubicaciones_excel.Col15))
            cmd.Parameters.Add(New SqlParameter("@COL16", oBeTrans_bodega_ubicaciones_excel.Col16))
            cmd.Parameters.Add(New SqlParameter("@COL17", oBeTrans_bodega_ubicaciones_excel.Col17))
            cmd.Parameters.Add(New SqlParameter("@COL18", oBeTrans_bodega_ubicaciones_excel.Col18))
            cmd.Parameters.Add(New SqlParameter("@COL19", oBeTrans_bodega_ubicaciones_excel.Col19))
            cmd.Parameters.Add(New SqlParameter("@COL20", oBeTrans_bodega_ubicaciones_excel.Col20))
            cmd.Parameters.Add(New SqlParameter("@COL21", oBeTrans_bodega_ubicaciones_excel.Col21))
            cmd.Parameters.Add(New SqlParameter("@COL22", oBeTrans_bodega_ubicaciones_excel.Col22))
            cmd.Parameters.Add(New SqlParameter("@COL23", oBeTrans_bodega_ubicaciones_excel.Col23))
            cmd.Parameters.Add(New SqlParameter("@COL24", oBeTrans_bodega_ubicaciones_excel.Col24))
            cmd.Parameters.Add(New SqlParameter("@COL25", oBeTrans_bodega_ubicaciones_excel.Col25))
            cmd.Parameters.Add(New SqlParameter("@COL26", oBeTrans_bodega_ubicaciones_excel.Col26))
            cmd.Parameters.Add(New SqlParameter("@COL27", oBeTrans_bodega_ubicaciones_excel.Col27))
            cmd.Parameters.Add(New SqlParameter("@COL28", oBeTrans_bodega_ubicaciones_excel.Col28))
            cmd.Parameters.Add(New SqlParameter("@COL29", oBeTrans_bodega_ubicaciones_excel.Col29))
            cmd.Parameters.Add(New SqlParameter("@COL30", oBeTrans_bodega_ubicaciones_excel.Col30))
            cmd.Parameters.Add(New SqlParameter("@COL31", oBeTrans_bodega_ubicaciones_excel.Col31))
            cmd.Parameters.Add(New SqlParameter("@COL32", oBeTrans_bodega_ubicaciones_excel.Col32))
            cmd.Parameters.Add(New SqlParameter("@COL33", oBeTrans_bodega_ubicaciones_excel.Col33))
            cmd.Parameters.Add(New SqlParameter("@COL34", oBeTrans_bodega_ubicaciones_excel.Col34))
            cmd.Parameters.Add(New SqlParameter("@COL35", oBeTrans_bodega_ubicaciones_excel.Col35))
            cmd.Parameters.Add(New SqlParameter("@COL36", oBeTrans_bodega_ubicaciones_excel.Col36))
            cmd.Parameters.Add(New SqlParameter("@COL37", oBeTrans_bodega_ubicaciones_excel.Col37))
            cmd.Parameters.Add(New SqlParameter("@COL38", oBeTrans_bodega_ubicaciones_excel.Col38))
            cmd.Parameters.Add(New SqlParameter("@COL39", oBeTrans_bodega_ubicaciones_excel.Col39))
            cmd.Parameters.Add(New SqlParameter("@COL40", oBeTrans_bodega_ubicaciones_excel.Col40))
            cmd.Parameters.Add(New SqlParameter("@COL41", oBeTrans_bodega_ubicaciones_excel.Col41))
            cmd.Parameters.Add(New SqlParameter("@COL42", oBeTrans_bodega_ubicaciones_excel.Col42))
            cmd.Parameters.Add(New SqlParameter("@COL43", oBeTrans_bodega_ubicaciones_excel.Col43))
            cmd.Parameters.Add(New SqlParameter("@COL44", oBeTrans_bodega_ubicaciones_excel.Col44))
            cmd.Parameters.Add(New SqlParameter("@COL45", oBeTrans_bodega_ubicaciones_excel.Col45))
            cmd.Parameters.Add(New SqlParameter("@COL46", oBeTrans_bodega_ubicaciones_excel.Col46))
            cmd.Parameters.Add(New SqlParameter("@COL47", oBeTrans_bodega_ubicaciones_excel.Col47))
            cmd.Parameters.Add(New SqlParameter("@COL48", oBeTrans_bodega_ubicaciones_excel.Col48))
            cmd.Parameters.Add(New SqlParameter("@COL49", oBeTrans_bodega_ubicaciones_excel.Col49))
            cmd.Parameters.Add(New SqlParameter("@COL50", oBeTrans_bodega_ubicaciones_excel.Col50))
            cmd.Parameters.Add(New SqlParameter("@COL51", oBeTrans_bodega_ubicaciones_excel.Col51))
            cmd.Parameters.Add(New SqlParameter("@COL52", oBeTrans_bodega_ubicaciones_excel.Col52))
            cmd.Parameters.Add(New SqlParameter("@COL53", oBeTrans_bodega_ubicaciones_excel.Col53))
            cmd.Parameters.Add(New SqlParameter("@COL54", oBeTrans_bodega_ubicaciones_excel.Col54))
            cmd.Parameters.Add(New SqlParameter("@COL55", oBeTrans_bodega_ubicaciones_excel.Col55))
            cmd.Parameters.Add(New SqlParameter("@COL56", oBeTrans_bodega_ubicaciones_excel.Col56))
            cmd.Parameters.Add(New SqlParameter("@COL57", oBeTrans_bodega_ubicaciones_excel.Col57))
            cmd.Parameters.Add(New SqlParameter("@COL58", oBeTrans_bodega_ubicaciones_excel.Col58))
            cmd.Parameters.Add(New SqlParameter("@COL59", oBeTrans_bodega_ubicaciones_excel.Col59))
            cmd.Parameters.Add(New SqlParameter("@COL60", oBeTrans_bodega_ubicaciones_excel.Col60))
            cmd.Parameters.Add(New SqlParameter("@COL61", oBeTrans_bodega_ubicaciones_excel.Col61))
            cmd.Parameters.Add(New SqlParameter("@COL62", oBeTrans_bodega_ubicaciones_excel.Col62))
            cmd.Parameters.Add(New SqlParameter("@COL63", oBeTrans_bodega_ubicaciones_excel.Col63))
            cmd.Parameters.Add(New SqlParameter("@COL64", oBeTrans_bodega_ubicaciones_excel.Col64))
            cmd.Parameters.Add(New SqlParameter("@COL65", oBeTrans_bodega_ubicaciones_excel.Col65))
            cmd.Parameters.Add(New SqlParameter("@COL66", oBeTrans_bodega_ubicaciones_excel.Col66))
            cmd.Parameters.Add(New SqlParameter("@COL67", oBeTrans_bodega_ubicaciones_excel.Col67))
            cmd.Parameters.Add(New SqlParameter("@COL68", oBeTrans_bodega_ubicaciones_excel.Col68))
            cmd.Parameters.Add(New SqlParameter("@COL69", oBeTrans_bodega_ubicaciones_excel.Col69))
            cmd.Parameters.Add(New SqlParameter("@COL70", oBeTrans_bodega_ubicaciones_excel.Col70))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_bodega_ubicaciones_excel As clsBeTrans_bodega_ubicaciones_excel, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_bodega_ubicaciones_excel")
            Upd.Add("idbodegaexcel", "@idbodegaexcel", DataType.Parametro)
            Upd.Add("registro", "@registro", DataType.Parametro)
            Upd.Add("cod_bodega", "@cod_bodega", DataType.Parametro)
            Upd.Add("cod_area", "@cod_area", DataType.Parametro)
            Upd.Add("cod_sector", "@cod_sector", DataType.Parametro)
            Upd.Add("tipo_ubicacion", "@tipo_ubicacion", DataType.Parametro)
            Upd.Add("tipo_rack", "@tipo_rack", DataType.Parametro)
            Upd.Add("numero", "@numero", DataType.Parametro)
            Upd.Add("ingreso_por", "@ingreso_por", DataType.Parametro)
            Upd.Add("orientacion", "@orientacion", DataType.Parametro)
            Upd.Add("orden", "@orden", DataType.Parametro)
            Upd.Add("x", "@x", DataType.Parametro)
            Upd.Add("y", "@y", DataType.Parametro)
            Upd.Add("nivel", "@nivel", DataType.Parametro)
            Upd.Add("col1", "@col1", DataType.Parametro)
            Upd.Add("col2", "@col2", DataType.Parametro)
            Upd.Add("col3", "@col3", DataType.Parametro)
            Upd.Add("col4", "@col4", DataType.Parametro)
            Upd.Add("col5", "@col5", DataType.Parametro)
            Upd.Add("col6", "@col6", DataType.Parametro)
            Upd.Add("col7", "@col7", DataType.Parametro)
            Upd.Add("col8", "@col8", DataType.Parametro)
            Upd.Add("col9", "@col9", DataType.Parametro)
            Upd.Add("col10", "@col10", DataType.Parametro)
            Upd.Add("col11", "@col11", DataType.Parametro)
            Upd.Add("col12", "@col12", DataType.Parametro)
            Upd.Add("col13", "@col13", DataType.Parametro)
            Upd.Add("col14", "@col14", DataType.Parametro)
            Upd.Add("col15", "@col15", DataType.Parametro)
            Upd.Add("col16", "@col16", DataType.Parametro)
            Upd.Add("col17", "@col17", DataType.Parametro)
            Upd.Add("col18", "@col18", DataType.Parametro)
            Upd.Add("col19", "@col19", DataType.Parametro)
            Upd.Add("col20", "@col20", DataType.Parametro)
            Upd.Add("col21", "@col21", DataType.Parametro)
            Upd.Add("col22", "@col22", DataType.Parametro)
            Upd.Add("col23", "@col23", DataType.Parametro)
            Upd.Add("col24", "@col24", DataType.Parametro)
            Upd.Add("col25", "@col25", DataType.Parametro)
            Upd.Add("col26", "@col26", DataType.Parametro)
            Upd.Add("col27", "@col27", DataType.Parametro)
            Upd.Add("col28", "@col28", DataType.Parametro)
            Upd.Add("col29", "@col29", DataType.Parametro)
            Upd.Add("col30", "@col30", DataType.Parametro)
            Upd.Add("col31", "@col31", DataType.Parametro)
            Upd.Add("col32", "@col32", DataType.Parametro)
            Upd.Add("col33", "@col33", DataType.Parametro)
            Upd.Add("col34", "@col34", DataType.Parametro)
            Upd.Add("col35", "@col35", DataType.Parametro)
            Upd.Add("col36", "@col36", DataType.Parametro)
            Upd.Add("col37", "@col37", DataType.Parametro)
            Upd.Add("col38", "@col38", DataType.Parametro)
            Upd.Add("col39", "@col39", DataType.Parametro)
            Upd.Add("col40", "@col40", DataType.Parametro)
            Upd.Add("col41", "@col41", DataType.Parametro)
            Upd.Add("col42", "@col42", DataType.Parametro)
            Upd.Add("col43", "@col43", DataType.Parametro)
            Upd.Add("col44", "@col44", DataType.Parametro)
            Upd.Add("col45", "@col45", DataType.Parametro)
            Upd.Add("col46", "@col46", DataType.Parametro)
            Upd.Add("col47", "@col47", DataType.Parametro)
            Upd.Add("col48", "@col48", DataType.Parametro)
            Upd.Add("col49", "@col49", DataType.Parametro)
            Upd.Add("col50", "@col50", DataType.Parametro)
            Upd.Add("col51", "@col51", DataType.Parametro)
            Upd.Add("col52", "@col52", DataType.Parametro)
            Upd.Add("col53", "@col53", DataType.Parametro)
            Upd.Add("col54", "@col54", DataType.Parametro)
            Upd.Add("col55", "@col55", DataType.Parametro)
            Upd.Add("col56", "@col56", DataType.Parametro)
            Upd.Add("col57", "@col57", DataType.Parametro)
            Upd.Add("col58", "@col58", DataType.Parametro)
            Upd.Add("col59", "@col59", DataType.Parametro)
            Upd.Add("col60", "@col60", DataType.Parametro)
            Upd.Add("col61", "@col61", DataType.Parametro)
            Upd.Add("col62", "@col62", DataType.Parametro)
            Upd.Add("col63", "@col63", DataType.Parametro)
            Upd.Add("col64", "@col64", DataType.Parametro)
            Upd.Add("col65", "@col65", DataType.Parametro)
            Upd.Add("col66", "@col66", DataType.Parametro)
            Upd.Add("col67", "@col67", DataType.Parametro)
            Upd.Add("col68", "@col68", DataType.Parametro)
            Upd.Add("col69", "@col69", DataType.Parametro)
            Upd.Add("col70", "@col70", DataType.Parametro)
            Upd.Where("IdBodegaExcel = @IdBodegaExcel")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGAEXCEL", oBeTrans_bodega_ubicaciones_excel.IdBodegaExcel))
            cmd.Parameters.Add(New SqlParameter("@REGISTRO", oBeTrans_bodega_ubicaciones_excel.Registro))
            cmd.Parameters.Add(New SqlParameter("@COD_BODEGA", oBeTrans_bodega_ubicaciones_excel.Cod_bodega))
            cmd.Parameters.Add(New SqlParameter("@COD_AREA", oBeTrans_bodega_ubicaciones_excel.Cod_area))
            cmd.Parameters.Add(New SqlParameter("@COD_SECTOR", oBeTrans_bodega_ubicaciones_excel.Cod_sector))
            cmd.Parameters.Add(New SqlParameter("@TIPO_UBICACION", oBeTrans_bodega_ubicaciones_excel.Tipo_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_RACK", oBeTrans_bodega_ubicaciones_excel.Tipo_rack))
            cmd.Parameters.Add(New SqlParameter("@NUMERO", oBeTrans_bodega_ubicaciones_excel.Numero))
            cmd.Parameters.Add(New SqlParameter("@INGRESO_POR", oBeTrans_bodega_ubicaciones_excel.Ingreso_por))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeTrans_bodega_ubicaciones_excel.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@ORDEN", oBeTrans_bodega_ubicaciones_excel.Orden))
            cmd.Parameters.Add(New SqlParameter("@X", oBeTrans_bodega_ubicaciones_excel.X))
            cmd.Parameters.Add(New SqlParameter("@Y", oBeTrans_bodega_ubicaciones_excel.Y))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeTrans_bodega_ubicaciones_excel.Nivel))
            cmd.Parameters.Add(New SqlParameter("@COL1", oBeTrans_bodega_ubicaciones_excel.Col1))
            cmd.Parameters.Add(New SqlParameter("@COL2", oBeTrans_bodega_ubicaciones_excel.Col2))
            cmd.Parameters.Add(New SqlParameter("@COL3", oBeTrans_bodega_ubicaciones_excel.Col3))
            cmd.Parameters.Add(New SqlParameter("@COL4", oBeTrans_bodega_ubicaciones_excel.Col4))
            cmd.Parameters.Add(New SqlParameter("@COL5", oBeTrans_bodega_ubicaciones_excel.Col5))
            cmd.Parameters.Add(New SqlParameter("@COL6", oBeTrans_bodega_ubicaciones_excel.Col6))
            cmd.Parameters.Add(New SqlParameter("@COL7", oBeTrans_bodega_ubicaciones_excel.Col7))
            cmd.Parameters.Add(New SqlParameter("@COL8", oBeTrans_bodega_ubicaciones_excel.Col8))
            cmd.Parameters.Add(New SqlParameter("@COL9", oBeTrans_bodega_ubicaciones_excel.Col9))
            cmd.Parameters.Add(New SqlParameter("@COL10", oBeTrans_bodega_ubicaciones_excel.Col10))
            cmd.Parameters.Add(New SqlParameter("@COL11", oBeTrans_bodega_ubicaciones_excel.Col11))
            cmd.Parameters.Add(New SqlParameter("@COL12", oBeTrans_bodega_ubicaciones_excel.Col12))
            cmd.Parameters.Add(New SqlParameter("@COL13", oBeTrans_bodega_ubicaciones_excel.Col13))
            cmd.Parameters.Add(New SqlParameter("@COL14", oBeTrans_bodega_ubicaciones_excel.Col14))
            cmd.Parameters.Add(New SqlParameter("@COL15", oBeTrans_bodega_ubicaciones_excel.Col15))
            cmd.Parameters.Add(New SqlParameter("@COL16", oBeTrans_bodega_ubicaciones_excel.Col16))
            cmd.Parameters.Add(New SqlParameter("@COL17", oBeTrans_bodega_ubicaciones_excel.Col17))
            cmd.Parameters.Add(New SqlParameter("@COL18", oBeTrans_bodega_ubicaciones_excel.Col18))
            cmd.Parameters.Add(New SqlParameter("@COL19", oBeTrans_bodega_ubicaciones_excel.Col19))
            cmd.Parameters.Add(New SqlParameter("@COL20", oBeTrans_bodega_ubicaciones_excel.Col20))
            cmd.Parameters.Add(New SqlParameter("@COL21", oBeTrans_bodega_ubicaciones_excel.Col21))
            cmd.Parameters.Add(New SqlParameter("@COL22", oBeTrans_bodega_ubicaciones_excel.Col22))
            cmd.Parameters.Add(New SqlParameter("@COL23", oBeTrans_bodega_ubicaciones_excel.Col23))
            cmd.Parameters.Add(New SqlParameter("@COL24", oBeTrans_bodega_ubicaciones_excel.Col24))
            cmd.Parameters.Add(New SqlParameter("@COL25", oBeTrans_bodega_ubicaciones_excel.Col25))
            cmd.Parameters.Add(New SqlParameter("@COL26", oBeTrans_bodega_ubicaciones_excel.Col26))
            cmd.Parameters.Add(New SqlParameter("@COL27", oBeTrans_bodega_ubicaciones_excel.Col27))
            cmd.Parameters.Add(New SqlParameter("@COL28", oBeTrans_bodega_ubicaciones_excel.Col28))
            cmd.Parameters.Add(New SqlParameter("@COL29", oBeTrans_bodega_ubicaciones_excel.Col29))
            cmd.Parameters.Add(New SqlParameter("@COL30", oBeTrans_bodega_ubicaciones_excel.Col30))
            cmd.Parameters.Add(New SqlParameter("@COL31", oBeTrans_bodega_ubicaciones_excel.Col31))
            cmd.Parameters.Add(New SqlParameter("@COL32", oBeTrans_bodega_ubicaciones_excel.Col32))
            cmd.Parameters.Add(New SqlParameter("@COL33", oBeTrans_bodega_ubicaciones_excel.Col33))
            cmd.Parameters.Add(New SqlParameter("@COL34", oBeTrans_bodega_ubicaciones_excel.Col34))
            cmd.Parameters.Add(New SqlParameter("@COL35", oBeTrans_bodega_ubicaciones_excel.Col35))
            cmd.Parameters.Add(New SqlParameter("@COL36", oBeTrans_bodega_ubicaciones_excel.Col36))
            cmd.Parameters.Add(New SqlParameter("@COL37", oBeTrans_bodega_ubicaciones_excel.Col37))
            cmd.Parameters.Add(New SqlParameter("@COL38", oBeTrans_bodega_ubicaciones_excel.Col38))
            cmd.Parameters.Add(New SqlParameter("@COL39", oBeTrans_bodega_ubicaciones_excel.Col39))
            cmd.Parameters.Add(New SqlParameter("@COL40", oBeTrans_bodega_ubicaciones_excel.Col40))
            cmd.Parameters.Add(New SqlParameter("@COL41", oBeTrans_bodega_ubicaciones_excel.Col41))
            cmd.Parameters.Add(New SqlParameter("@COL42", oBeTrans_bodega_ubicaciones_excel.Col42))
            cmd.Parameters.Add(New SqlParameter("@COL43", oBeTrans_bodega_ubicaciones_excel.Col43))
            cmd.Parameters.Add(New SqlParameter("@COL44", oBeTrans_bodega_ubicaciones_excel.Col44))
            cmd.Parameters.Add(New SqlParameter("@COL45", oBeTrans_bodega_ubicaciones_excel.Col45))
            cmd.Parameters.Add(New SqlParameter("@COL46", oBeTrans_bodega_ubicaciones_excel.Col46))
            cmd.Parameters.Add(New SqlParameter("@COL47", oBeTrans_bodega_ubicaciones_excel.Col47))
            cmd.Parameters.Add(New SqlParameter("@COL48", oBeTrans_bodega_ubicaciones_excel.Col48))
            cmd.Parameters.Add(New SqlParameter("@COL49", oBeTrans_bodega_ubicaciones_excel.Col49))
            cmd.Parameters.Add(New SqlParameter("@COL50", oBeTrans_bodega_ubicaciones_excel.Col50))
            cmd.Parameters.Add(New SqlParameter("@COL51", oBeTrans_bodega_ubicaciones_excel.Col51))
            cmd.Parameters.Add(New SqlParameter("@COL52", oBeTrans_bodega_ubicaciones_excel.Col52))
            cmd.Parameters.Add(New SqlParameter("@COL53", oBeTrans_bodega_ubicaciones_excel.Col53))
            cmd.Parameters.Add(New SqlParameter("@COL54", oBeTrans_bodega_ubicaciones_excel.Col54))
            cmd.Parameters.Add(New SqlParameter("@COL55", oBeTrans_bodega_ubicaciones_excel.Col55))
            cmd.Parameters.Add(New SqlParameter("@COL56", oBeTrans_bodega_ubicaciones_excel.Col56))
            cmd.Parameters.Add(New SqlParameter("@COL57", oBeTrans_bodega_ubicaciones_excel.Col57))
            cmd.Parameters.Add(New SqlParameter("@COL58", oBeTrans_bodega_ubicaciones_excel.Col58))
            cmd.Parameters.Add(New SqlParameter("@COL59", oBeTrans_bodega_ubicaciones_excel.Col59))
            cmd.Parameters.Add(New SqlParameter("@COL60", oBeTrans_bodega_ubicaciones_excel.Col60))
            cmd.Parameters.Add(New SqlParameter("@COL61", oBeTrans_bodega_ubicaciones_excel.Col61))
            cmd.Parameters.Add(New SqlParameter("@COL62", oBeTrans_bodega_ubicaciones_excel.Col62))
            cmd.Parameters.Add(New SqlParameter("@COL63", oBeTrans_bodega_ubicaciones_excel.Col63))
            cmd.Parameters.Add(New SqlParameter("@COL64", oBeTrans_bodega_ubicaciones_excel.Col64))
            cmd.Parameters.Add(New SqlParameter("@COL65", oBeTrans_bodega_ubicaciones_excel.Col65))
            cmd.Parameters.Add(New SqlParameter("@COL66", oBeTrans_bodega_ubicaciones_excel.Col66))
            cmd.Parameters.Add(New SqlParameter("@COL67", oBeTrans_bodega_ubicaciones_excel.Col67))
            cmd.Parameters.Add(New SqlParameter("@COL68", oBeTrans_bodega_ubicaciones_excel.Col68))
            cmd.Parameters.Add(New SqlParameter("@COL69", oBeTrans_bodega_ubicaciones_excel.Col69))
            cmd.Parameters.Add(New SqlParameter("@COL70", oBeTrans_bodega_ubicaciones_excel.Col70))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_bodega_ubicaciones_excel As clsBeTrans_bodega_ubicaciones_excel, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_bodega_ubicaciones_excel" &
             "  Where(IdBodegaExcel = @IdBodegaExcel)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGAEXCEL", oBeTrans_bodega_ubicaciones_excel.IdBodegaExcel))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Delete_All(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " delete from Trans_bodega_ubicaciones_excel "

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_bodega_ubicaciones_excel"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_bodega_ubicaciones_excel)

        Dim lReturnList As New List(Of clsBeTrans_bodega_ubicaciones_excel)

        Try

            Const sp As String = "SELECT * FROM Trans_bodega_ubicaciones_excel"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_bodega_ubicaciones_excel As New clsBeTrans_bodega_ubicaciones_excel

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_bodega_ubicaciones_excel = New clsBeTrans_bodega_ubicaciones_excel()
                            Cargar(vBeTrans_bodega_ubicaciones_excel, dr)
                            lReturnList.Add(vBeTrans_bodega_ubicaciones_excel)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTrans_bodega_ubicaciones_excel As clsBeTrans_bodega_ubicaciones_excel)

        Try

            Const sp As String = "SELECT * FROM Trans_bodega_ubicaciones_excel" &
            " Where(IdBodegaExcel = @IdBodegaExcel)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_bodega_ubicaciones_excel As New clsBeTrans_bodega_ubicaciones_excel

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_bodega_ubicaciones_excel, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdBodegaExcel),0) FROM Trans_bodega_ubicaciones_excel"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdBodegaExcel),0) FROM Trans_bodega_ubicaciones_excel"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GuardarTransaccion(pListBodegaArea, lConnection, lTransaction)

        Try

            Dim vIdMaxImportExcel As Integer = MaxID(lConnection, lTransaction) + 1

            Delete_All(lConnection, lTransaction)

            For Each Obj As clsBeTrans_bodega_ubicaciones_excel In pListBodegaArea
                Obj.IdBodegaExcel = vIdMaxImportExcel
                Insertar(Obj, lConnection, lTransaction)
                vIdMaxImportExcel += 1
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Filter_by_Bodega(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As DataTable

        Try

            Const sp As String = "select p1.registro,p1.cod_bodega,p1.cod_area,p1.cod_sector,p1.tipo_ubicacion,p1.tipo_rack,p1.numero,
									p1.ingreso_por,p1.orientacion,p1.orden,p1.x,p1.y,
									nivel = (select (count(registro)-2) nivel from trans_bodega_ubicaciones_excel p2 where p2.registro =p1.registro )
									from trans_bodega_ubicaciones_excel p1 
									where p1.cod_bodega > '0'
									group by p1.registro,p1.cod_bodega,p1.cod_area,p1.cod_sector,p1.tipo_ubicacion,p1.tipo_rack,p1.numero,
									p1.ingreso_por,p1.orientacion,p1.orden,p1.x,p1.y "

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Bodega_Sector_Alto(ByVal pRegistro As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Double

        Try

            Dim lMax As Double = 0.00

            'GT3012021: se omiten campos vacios para que la conversión sume solo valores existentes
            'Const sp As String = "select sum(CAST(col1 AS DECIMAL(4,2))) as alto from trans_bodega_ubicaciones_excel 
            '							 where registro=@pRegistro and nivel not in('largo','profundidad') and col1<>'' "

            Const sp As String = "select sum( CAST( col1 AS DECIMAL(4,2))) as alto from trans_bodega_ubicaciones_excel 
										 where registro=@pRegistro and nivel not in('largo','profundidad')
										 and col1 <>'' "

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@pRegistro", pRegistro)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CDbl(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Bodega_Sector_Largo(ByVal pRegistro As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As DataTable

        Try

            Dim lMax As Integer = 0

            Const sp As String = "select top 1 *from trans_bodega_ubicaciones_excel
									     where registro=@pRegistro and nivel in('largo')"

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@pRegistro", pRegistro)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Bodega_Sector_Ancho(ByVal pRegistro As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As DataTable

        Try

            Dim lMax As Integer = 0

            Const sp As String = "select top 1 *from trans_bodega_ubicaciones_excel
									     where registro=@pRegistro and nivel in('profundidad')"

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@pRegistro", pRegistro)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Bodega_data_grupo(ByVal pRegistro As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As DataTable

        Try

            '#GT26042022_1448: le agrego el order by, para casos donde se ingresan en orden descendente los niveles
            Const sp As String = "select *from trans_bodega_ubicaciones_excel
								  where registro=@pRegistro and nivel not in('largo','profundidad')
								  order by  nivel asc"

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@pRegistro", pRegistro)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Limpiar_Todo(ByVal IdBodega As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Limpiar_Todo = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnEstructura_Tramo.Limpiar_Todo(IdBodega, lConnection, lTransaction)
            clsLnEstructura_grupo.Limpiar_Todo(IdBodega, lConnection, lTransaction)
            clsLnBodega_ubicacion.Limpiar_Todo(IdBodega, lConnection, lTransaction)
            clsLnBodega_tramo.Limpiar_Todo(IdBodega, lConnection, lTransaction)
            clsLnBodega_sector.Limpiar_Todo(IdBodega, lConnection, lTransaction)
            clsLnBodega_area.Limpiar_Todo(IdBodega, lConnection, lTransaction)

            lTransaction.Commit()

            Limpiar_Todo = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try
    End Function

    Public Shared Function Existe_Mov_By_IdBodega(ByVal pIdBodega As Integer) As Boolean

        Existe_Mov_By_IdBodega = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_movimientos where (IdBodegaOrigen= @pIdBodega or IdBodegaDestino=@pIdBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                        Return lExists

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
