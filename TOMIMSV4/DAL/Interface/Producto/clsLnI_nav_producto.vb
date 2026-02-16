Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_producto
    Public Shared Sub Cargar(ByRef oBeI_nav_producto As clsBeI_nav_producto, ByRef dr As DataRow)

        Try

            With oBeI_nav_producto

                .No = IIf(IsDBNull(dr.Item("No")), "", dr.Item("No"))
                .Description = IIf(IsDBNull(dr.Item("Description")), "", dr.Item("Description"))
                .Description_2 = IIf(IsDBNull(dr.Item("Description_2")), "", dr.Item("Description_2"))
                .Inventory = IIf(IsDBNull(dr.Item("Inventory")), 0.0, dr.Item("Inventory"))
                .Base_Unit_Of_Measure = IIf(IsDBNull(dr.Item("Base_Unit_Of_Measure")), "", dr.Item("Base_Unit_Of_Measure"))
                .Unit_Cost = IIf(IsDBNull(dr.Item("Unit_Cost")), 0.0, dr.Item("Unit_Cost"))
                .Inventory_Posting_Group = IIf(IsDBNull(dr.Item("Inventory_Posting_Group")), "", dr.Item("Inventory_Posting_Group"))
                .Gen_Prod_Posting_Group = IIf(IsDBNull(dr.Item("Gen_Prod_Posting_Group")), "", dr.Item("Gen_Prod_Posting_Group"))
                .Search_Description = IIf(IsDBNull(dr.Item("Search_Description")), "", dr.Item("Search_Description"))
                .Item_Category_Code = IIf(IsDBNull(dr.Item("Item_Category_Code")), "", dr.Item("Item_Category_Code"))
                .Product_Group_Code = IIf(IsDBNull(dr.Item("Product_Group_Code")), "", dr.Item("Product_Group_Code"))
                .Sales_Unit = IIf(IsDBNull(dr.Item("Sales_Unit")), "", dr.Item("Sales_Unit"))
                .Item_Tracking_Code = IIf(IsDBNull(dr.Item("Item_Tracking_Code")), "", dr.Item("Item_Tracking_Code"))
                .Item_Category_Name = IIf(IsDBNull(dr.Item("Item_Category_Name")), "", dr.Item("Item_Category_Name"))
                .Gen_Prod_Posting_Name = IIf(IsDBNull(dr.Item("Gen_Prod_Posting_Name")), "", dr.Item("Gen_Prod_Posting_Name"))
                .Producto_Group_Name = IIf(IsDBNull(dr.Item("Producto_Group_Name")), "", dr.Item("Producto_Group_Name"))
                .Manufacturing_Process = IIf(IsDBNull(dr.Item("Manufacturing_Process")), 0, dr.Item("Manufacturing_Process"))
                .BatchControl = IIf(IsDBNull(dr.Item("BatchControl")), False, dr.Item("BatchControl"))
                .Product_Class_Code = IIf(IsDBNull(dr.Item("Product_Class_Code")), "", dr.Item("Product_Class_Code"))
                .Product_Class_Name = IIf(IsDBNull(dr.Item("Product_Class_Name")), "", dr.Item("Product_Class_Name"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_producto As clsBeI_nav_producto,
                                    ByVal pConection As SqlConnection,
                                    ByVal pTransaction As SqlTransaction) As Integer

        Try

            Ins.Init("i_nav_producto")
            If Not oBeI_nav_producto.No Is Nothing Then Ins.Add("no", "@no", DataType.Parametro)
            If Not oBeI_nav_producto.Description Is Nothing Then Ins.Add("description", "@description", DataType.Parametro)
            If Not oBeI_nav_producto.Description_2 Is Nothing Then Ins.Add("description_2", "@description_2", DataType.Parametro)
            If Not oBeI_nav_producto.Inventory Is Nothing Then Ins.Add("inventory", "@inventory", DataType.Parametro)
            If Not oBeI_nav_producto.Base_Unit_Of_Measure Is Nothing Then Ins.Add("base_unit_of_measure", "@base_unit_of_measure", DataType.Parametro)
            If Not oBeI_nav_producto.Unit_Cost Is Nothing Then Ins.Add("unit_cost", "@unit_cost", DataType.Parametro)
            If Not oBeI_nav_producto.Inventory_Posting_Group Is Nothing Then Ins.Add("inventory_posting_group", "@inventory_posting_group", DataType.Parametro)
            If Not oBeI_nav_producto.Gen_Prod_Posting_Group Is Nothing Then Ins.Add("gen_prod_posting_group", "@gen_prod_posting_group", DataType.Parametro)
            If Not oBeI_nav_producto.Search_Description Is Nothing Then Ins.Add("search_description", "@search_description", DataType.Parametro)
            If Not oBeI_nav_producto.Item_Category_Code Is Nothing Then Ins.Add("item_category_code", "@item_category_code", DataType.Parametro)
            If Not oBeI_nav_producto.Product_Group_Code Is Nothing Then Ins.Add("product_group_code", "@product_group_code", DataType.Parametro)
            If Not oBeI_nav_producto.Sales_Unit Is Nothing Then Ins.Add("sales_unit", "@sales_unit", DataType.Parametro)
            If Not oBeI_nav_producto.Item_Tracking_Code Is Nothing Then Ins.Add("item_tracking_code", "@item_tracking_code", DataType.Parametro)

            If Not oBeI_nav_producto.Item_Category_Name Is Nothing Then Ins.Add("Item_Category_Name", "@Item_Category_Name", DataType.Parametro)
            If Not oBeI_nav_producto.Gen_Prod_Posting_Name Is Nothing Then Ins.Add("Gen_Prod_Posting_Name", "@Gen_Prod_Posting_Name", DataType.Parametro)
            If Not oBeI_nav_producto.Producto_Group_Name Is Nothing Then Ins.Add("Producto_Group_Name", "@Producto_Group_Name", DataType.Parametro)
            If Not oBeI_nav_producto.Manufacturing_Process Is Nothing Then Ins.Add("Manufacturing_Process", "@Manufacturing_Process", DataType.Parametro)
            Ins.Add("BATCHCONTROL", "@BATCHCONTROL", DataType.Parametro)

            If Not oBeI_nav_producto.Product_Class_Code = "" Then Ins.Add("PRODUCT_CLASS_CODE", "@PRODUCT_CLASS_CODE", DataType.Parametro)
            If Not oBeI_nav_producto.Product_Class_Name = "" Then Ins.Add("PRODUCT_CLASS_NAME", "@PRODUCT_CLASS_NAME", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}

            If Not oBeI_nav_producto.No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_producto.No))
            If Not oBeI_nav_producto.Description Is Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION", oBeI_nav_producto.Description))
            If Not oBeI_nav_producto.Description_2 Is Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION_2", oBeI_nav_producto.Description_2))
            If Not oBeI_nav_producto.Inventory Is Nothing Then cmd.Parameters.Add(New SqlParameter("@INVENTORY", oBeI_nav_producto.Inventory))
            If Not oBeI_nav_producto.Base_Unit_Of_Measure Is Nothing Then cmd.Parameters.Add(New SqlParameter("@BASE_UNIT_OF_MEASURE", oBeI_nav_producto.Base_Unit_Of_Measure))
            If Not oBeI_nav_producto.Unit_Cost Is Nothing Then cmd.Parameters.Add(New SqlParameter("@UNIT_COST", oBeI_nav_producto.Unit_Cost))
            If Not oBeI_nav_producto.Inventory_Posting_Group Is Nothing Then cmd.Parameters.Add(New SqlParameter("@INVENTORY_POSTING_GROUP", oBeI_nav_producto.Inventory_Posting_Group))
            If Not oBeI_nav_producto.Gen_Prod_Posting_Group Is Nothing Then cmd.Parameters.Add(New SqlParameter("@GEN_PROD_POSTING_GROUP", oBeI_nav_producto.Gen_Prod_Posting_Group))
            If Not oBeI_nav_producto.Search_Description Is Nothing Then cmd.Parameters.Add(New SqlParameter("@SEARCH_DESCRIPTION", oBeI_nav_producto.Search_Description))
            If Not oBeI_nav_producto.Item_Category_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_CATEGORY_CODE", oBeI_nav_producto.Item_Category_Code))
            If Not oBeI_nav_producto.Product_Group_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_GROUP_CODE", oBeI_nav_producto.Product_Group_Code))
            If Not oBeI_nav_producto.Sales_Unit Is Nothing Then cmd.Parameters.Add(New SqlParameter("@SALES_UNIT", oBeI_nav_producto.Sales_Unit))
            If Not oBeI_nav_producto.Item_Tracking_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_TRACKING_CODE", oBeI_nav_producto.Item_Tracking_Code))
            If Not oBeI_nav_producto.Item_Category_Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_CATEGORY_NAME", oBeI_nav_producto.Item_Category_Name))
            If Not oBeI_nav_producto.Gen_Prod_Posting_Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@GEN_PROD_POSTING_NAME", oBeI_nav_producto.Gen_Prod_Posting_Name))
            If Not oBeI_nav_producto.Producto_Group_Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@PRODUCTO_GROUP_NAME", oBeI_nav_producto.Producto_Group_Name))
            If Not oBeI_nav_producto.Manufacturing_Process Is Nothing Then cmd.Parameters.Add(New SqlParameter("@MANUFACTURING_PROCESS", oBeI_nav_producto.Manufacturing_Process))
            If Not oBeI_nav_producto.Product_Class_Code = "" Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_CLASS_CODE", oBeI_nav_producto.Product_Class_Code))
            If Not oBeI_nav_producto.Product_Class_Name = "" Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_CLASS_NAME", oBeI_nav_producto.Product_Class_Name))

            cmd.Parameters.Add(New SqlParameter("@BATCHCONTROL", oBeI_nav_producto.BatchControl))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_producto As clsBeI_nav_producto,
                                      ByVal pConection As SqlConnection,
                                      ByVal pTransaction As SqlTransaction) As Integer

        Try

            Upd.Init("i_nav_producto")
            If Not oBeI_nav_producto.No Is Nothing Then Upd.Add("no", "@no", DataType.Parametro)
            If Not oBeI_nav_producto.Description Is Nothing Then Upd.Add("description", "@description", DataType.Parametro)
            If Not oBeI_nav_producto.Description_2 Is Nothing Then Upd.Add("description_2", "@description_2", DataType.Parametro)
            If Not oBeI_nav_producto.Inventory Is Nothing Then Upd.Add("inventory", "@inventory", DataType.Parametro)
            If Not oBeI_nav_producto.Base_Unit_Of_Measure Is Nothing Then Upd.Add("base_unit_of_measure", "@base_unit_of_measure", DataType.Parametro)
            If Not oBeI_nav_producto.Unit_Cost Is Nothing Then Upd.Add("unit_cost", "@unit_cost", DataType.Parametro)
            If Not oBeI_nav_producto.Inventory_Posting_Group Is Nothing Then Upd.Add("inventory_posting_group", "@inventory_posting_group", DataType.Parametro)
            If Not oBeI_nav_producto.Gen_Prod_Posting_Group Is Nothing Then Upd.Add("gen_prod_posting_group", "@gen_prod_posting_group", DataType.Parametro)
            If Not oBeI_nav_producto.Search_Description Is Nothing Then Upd.Add("search_description", "@search_description", DataType.Parametro)
            If Not oBeI_nav_producto.Item_Category_Code Is Nothing Then Upd.Add("item_category_code", "@item_category_code", DataType.Parametro)
            If Not oBeI_nav_producto.Product_Group_Code Is Nothing Then Upd.Add("product_group_code", "@product_group_code", DataType.Parametro)
            If Not oBeI_nav_producto.Sales_Unit Is Nothing Then Upd.Add("sales_unit", "@sales_unit", DataType.Parametro)
            If Not oBeI_nav_producto.Item_Tracking_Code Is Nothing Then Upd.Add("item_tracking_code", "@item_tracking_code", DataType.Parametro)
            If Not oBeI_nav_producto.Item_Category_Name Is Nothing Then Upd.Add("Item_Category_Name", "@Item_Category_Name", DataType.Parametro)
            If Not oBeI_nav_producto.Gen_Prod_Posting_Name Is Nothing Then Upd.Add("Gen_Prod_Posting_Name", "@Gen_Prod_Posting_Name", DataType.Parametro)
            If Not oBeI_nav_producto.Producto_Group_Name Is Nothing Then Upd.Add("Producto_Group_Name", "@Producto_Group_Name", DataType.Parametro)
            If Not oBeI_nav_producto.Manufacturing_Process Is Nothing Then Upd.Add("Manufacturing_Process", "@Manufacturing_Process", DataType.Parametro)
            If Not oBeI_nav_producto.Product_Class_Code = "" Then Upd.Add("PRODUCT_CLASS_CODE", "@PRODUCT_CLASS_CODE", DataType.Parametro)
            If Not oBeI_nav_producto.Product_Class_Name = "" Then Upd.Add("PRODUCT_CLASS_NAME", "@PRODUCT_CLASS_NAME", DataType.Parametro)
            Upd.Add("BATCHCONTROL", "@Manufacturing_Process", DataType.Parametro)
            Upd.Where("No = @No")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}

            If Not oBeI_nav_producto.No Is Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_producto.No))
            If Not oBeI_nav_producto.Description Is Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION", oBeI_nav_producto.Description))
            If Not oBeI_nav_producto.Description_2 Is Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION_2", oBeI_nav_producto.Description_2))
            If Not oBeI_nav_producto.Inventory Is Nothing Then cmd.Parameters.Add(New SqlParameter("@INVENTORY", oBeI_nav_producto.Inventory))
            If Not oBeI_nav_producto.Base_Unit_Of_Measure Is Nothing Then cmd.Parameters.Add(New SqlParameter("@BASE_UNIT_OF_MEASURE", oBeI_nav_producto.Base_Unit_Of_Measure))
            If Not oBeI_nav_producto.Unit_Cost Is Nothing Then cmd.Parameters.Add(New SqlParameter("@UNIT_COST", oBeI_nav_producto.Unit_Cost))
            If Not oBeI_nav_producto.Inventory_Posting_Group Is Nothing Then cmd.Parameters.Add(New SqlParameter("@INVENTORY_POSTING_GROUP", oBeI_nav_producto.Inventory_Posting_Group))
            If Not oBeI_nav_producto.Gen_Prod_Posting_Group Is Nothing Then cmd.Parameters.Add(New SqlParameter("@GEN_PROD_POSTING_GROUP", oBeI_nav_producto.Gen_Prod_Posting_Group))
            If Not oBeI_nav_producto.Search_Description Is Nothing Then cmd.Parameters.Add(New SqlParameter("@SEARCH_DESCRIPTION", oBeI_nav_producto.Search_Description))
            If Not oBeI_nav_producto.Item_Category_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_CATEGORY_CODE", oBeI_nav_producto.Item_Category_Code))
            If Not oBeI_nav_producto.Product_Group_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_GROUP_CODE", oBeI_nav_producto.Product_Group_Code))
            If Not oBeI_nav_producto.Sales_Unit Is Nothing Then cmd.Parameters.Add(New SqlParameter("@SALES_UNIT", oBeI_nav_producto.Sales_Unit))
            If Not oBeI_nav_producto.Item_Tracking_Code Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_TRACKING_CODE", oBeI_nav_producto.Item_Tracking_Code))
            If Not oBeI_nav_producto.Item_Category_Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_CATEGORY_NAME", oBeI_nav_producto.Item_Category_Name))
            If Not oBeI_nav_producto.Gen_Prod_Posting_Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@GEN_PROD_POSTING_NAME", oBeI_nav_producto.Gen_Prod_Posting_Name))
            If Not oBeI_nav_producto.Producto_Group_Name Is Nothing Then cmd.Parameters.Add(New SqlParameter("@PRODUCTO_GROUP_NAME", oBeI_nav_producto.Producto_Group_Name))
            If Not oBeI_nav_producto.Manufacturing_Process Is Nothing Then cmd.Parameters.Add(New SqlParameter("@MANUFACTURING_PROCESS", oBeI_nav_producto.Manufacturing_Process))
            If Not oBeI_nav_producto.Product_Class_Code = "" Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_CLASS_CODE", oBeI_nav_producto.Product_Class_Code))
            If Not oBeI_nav_producto.Product_Class_Name = "" Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_CLASS_NAME", oBeI_nav_producto.Product_Class_Name))
            cmd.Parameters.Add(New SqlParameter("@BATCHCONTROL", oBeI_nav_producto.BatchControl))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeI_nav_producto As clsBeI_nav_producto, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "  Delete from I_nav_producto " &
                                 "  Where(No = @No) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_producto.No))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_producto"
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

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
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Insertar_Con_Presentacion(ByRef oBeI_nav_producto As clsBeI_nav_producto,
                                                     ByVal lBeI_Nav_Conversion As List(Of clsBeI_nav_conversion),
                                                     ByVal pConection As SqlConnection,
                                                     ByVal pTransaction As SqlTransaction) As Integer


        Try

            Insertar(oBeI_nav_producto,
                     pConection,
                     pTransaction)

            If Not lBeI_Nav_Conversion Is Nothing Then

                For Each UM In lBeI_Nav_Conversion

                    If Not clsLnI_nav_conversion.Exist(UM.Item_No,
                                                       UM.Code,
                                                       pConection,
                                                       pTransaction) Then

                        UM.IdConversion = clsLnI_nav_conversion.MaxID(pConection, pTransaction) + 1

                        clsLnI_nav_conversion.Insertar(UM,
                                                       pConection,
                                                       pTransaction)
                    End If

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ' Inserta sin recibir conexión/tx como parámetro.
    ' - Abre y cierra su propia conexión.
    ' - NO usa transacción explícita (cada INSERT es autocommit).
    Public Shared Function Insertar(ByRef oBeI_nav_producto As clsBeI_nav_producto) As Integer
        Dim cn As New SqlConnection(ConfigurationManager.AppSettings("CST"))

        Try
            cn.Open()

            Ins.Init("i_nav_producto")
            If oBeI_nav_producto.No IsNot Nothing Then Ins.Add("no", "@no", DataType.Parametro)
            If oBeI_nav_producto.Description IsNot Nothing Then Ins.Add("description", "@description", DataType.Parametro)
            If oBeI_nav_producto.Description_2 IsNot Nothing Then Ins.Add("description_2", "@description_2", DataType.Parametro)
            If oBeI_nav_producto.Inventory IsNot Nothing Then Ins.Add("inventory", "@inventory", DataType.Parametro)
            If oBeI_nav_producto.Base_Unit_Of_Measure IsNot Nothing Then Ins.Add("base_unit_of_measure", "@base_unit_of_measure", DataType.Parametro)
            If oBeI_nav_producto.Unit_Cost IsNot Nothing Then Ins.Add("unit_cost", "@unit_cost", DataType.Parametro)
            If oBeI_nav_producto.Inventory_Posting_Group IsNot Nothing Then Ins.Add("inventory_posting_group", "@inventory_posting_group", DataType.Parametro)
            If oBeI_nav_producto.Gen_Prod_Posting_Group IsNot Nothing Then Ins.Add("gen_prod_posting_group", "@gen_prod_posting_group", DataType.Parametro)
            If oBeI_nav_producto.Search_Description IsNot Nothing Then Ins.Add("search_description", "@search_description", DataType.Parametro)
            If oBeI_nav_producto.Item_Category_Code IsNot Nothing Then Ins.Add("item_category_code", "@item_category_code", DataType.Parametro)
            If oBeI_nav_producto.Product_Group_Code IsNot Nothing Then Ins.Add("product_group_code", "@product_group_code", DataType.Parametro)
            If oBeI_nav_producto.Sales_Unit IsNot Nothing Then Ins.Add("sales_unit", "@sales_unit", DataType.Parametro)
            If oBeI_nav_producto.Item_Tracking_Code IsNot Nothing Then Ins.Add("item_tracking_code", "@item_tracking_code", DataType.Parametro)

            If oBeI_nav_producto.Item_Category_Name IsNot Nothing Then Ins.Add("Item_Category_Name", "@Item_Category_Name", DataType.Parametro)
            If oBeI_nav_producto.Gen_Prod_Posting_Name IsNot Nothing Then Ins.Add("Gen_Prod_Posting_Name", "@Gen_Prod_Posting_Name", DataType.Parametro)
            If oBeI_nav_producto.Producto_Group_Name IsNot Nothing Then Ins.Add("Producto_Group_Name", "@Producto_Group_Name", DataType.Parametro)
            If oBeI_nav_producto.Manufacturing_Process IsNot Nothing Then Ins.Add("Manufacturing_Process", "@Manufacturing_Process", DataType.Parametro)

            Ins.Add("BATCHCONTROL", "@BATCHCONTROL", DataType.Parametro)

            If Not String.IsNullOrEmpty(oBeI_nav_producto.Product_Class_Code) Then Ins.Add("PRODUCT_CLASS_CODE", "@PRODUCT_CLASS_CODE", DataType.Parametro)
            If Not String.IsNullOrEmpty(oBeI_nav_producto.Product_Class_Name) Then Ins.Add("PRODUCT_CLASS_NAME", "@PRODUCT_CLASS_NAME", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Using cmd As New SqlCommand(sp, cn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 60 ' ajusta si aplica

                If oBeI_nav_producto.No IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_producto.No))
                If oBeI_nav_producto.Description IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION", oBeI_nav_producto.Description))
                If oBeI_nav_producto.Description_2 IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@DESCRIPTION_2", oBeI_nav_producto.Description_2))
                If oBeI_nav_producto.Inventory IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@INVENTORY", oBeI_nav_producto.Inventory))
                If oBeI_nav_producto.Base_Unit_Of_Measure IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@BASE_UNIT_OF_MEASURE", oBeI_nav_producto.Base_Unit_Of_Measure))
                If oBeI_nav_producto.Unit_Cost IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@UNIT_COST", oBeI_nav_producto.Unit_Cost))
                If oBeI_nav_producto.Inventory_Posting_Group IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@INVENTORY_POSTING_GROUP", oBeI_nav_producto.Inventory_Posting_Group))
                If oBeI_nav_producto.Gen_Prod_Posting_Group IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@GEN_PROD_POSTING_GROUP", oBeI_nav_producto.Gen_Prod_Posting_Group))
                If oBeI_nav_producto.Search_Description IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@SEARCH_DESCRIPTION", oBeI_nav_producto.Search_Description))
                If oBeI_nav_producto.Item_Category_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_CATEGORY_CODE", oBeI_nav_producto.Item_Category_Code))
                If oBeI_nav_producto.Product_Group_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_GROUP_CODE", oBeI_nav_producto.Product_Group_Code))
                If oBeI_nav_producto.Sales_Unit IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@SALES_UNIT", oBeI_nav_producto.Sales_Unit))
                If oBeI_nav_producto.Item_Tracking_Code IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_TRACKING_CODE", oBeI_nav_producto.Item_Tracking_Code))

                If oBeI_nav_producto.Item_Category_Name IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@ITEM_CATEGORY_NAME", oBeI_nav_producto.Item_Category_Name))
                If oBeI_nav_producto.Gen_Prod_Posting_Name IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@GEN_PROD_POSTING_NAME", oBeI_nav_producto.Gen_Prod_Posting_Name))
                If oBeI_nav_producto.Producto_Group_Name IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@PRODUCTO_GROUP_NAME", oBeI_nav_producto.Producto_Group_Name))
                If oBeI_nav_producto.Manufacturing_Process IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@MANUFACTURING_PROCESS", oBeI_nav_producto.Manufacturing_Process))

                If Not String.IsNullOrEmpty(oBeI_nav_producto.Product_Class_Code) Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_CLASS_CODE", oBeI_nav_producto.Product_Class_Code))
                If Not String.IsNullOrEmpty(oBeI_nav_producto.Product_Class_Name) Then cmd.Parameters.Add(New SqlParameter("@PRODUCT_CLASS_NAME", oBeI_nav_producto.Product_Class_Name))

                cmd.Parameters.Add(New SqlParameter("@BATCHCONTROL", oBeI_nav_producto.BatchControl))

                Return cmd.ExecuteNonQuery()
            End Using

        Catch ex1 As SqlException
            Throw
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Function


End Class