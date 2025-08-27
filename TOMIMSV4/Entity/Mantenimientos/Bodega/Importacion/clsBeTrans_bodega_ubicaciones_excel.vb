Public Class clsBeTrans_bodega_ubicaciones_excel
    Implements ICloneable

    Public Property IdBodegaExcel() As Integer = 0
    Public Property Registro() As Integer = 0
    'Public Property Cod_bodega() As Integer = 0
    Public Property Cod_bodega() As String = ""
    Public Property Cod_area() As Integer = 0
    Public Property Cod_sector() As Integer = 0
    Public Property Tipo_ubicacion() As String = ""
    Public Property Tipo_rack() As String = ""
    Public Property Numero() As Integer = 0
    Public Property Ingreso_por() As String = ""
    Public Property Orientacion() As String = ""
    Public Property Orden() As String = ""
    Public Property X() As Double = 0.00
    Public Property Y() As Double = 0.00
    Public Property Nivel() As String = ""
    Public Property Col1() As String = ""
    Public Property Col2() As String = ""
    Public Property Col3() As String = ""
    Public Property Col4() As String = ""
    Public Property Col5() As String = ""
    Public Property Col6() As String = ""
    Public Property Col7() As String = ""
    Public Property Col8() As String = ""
    Public Property Col9() As String = ""
    Public Property Col10() As String = ""
    Public Property Col11() As String = ""
    Public Property Col12() As String = ""
    Public Property Col13() As String = ""
    Public Property Col14() As String = ""
    Public Property Col15() As String = ""
    Public Property Col16() As String = ""
    Public Property Col17() As String = ""
    Public Property Col18() As String = ""
    Public Property Col19() As String = ""
    Public Property Col20() As String = ""
    'GT19042022: se agregan mas columnas
    Public Property Col21() As String = ""
    Public Property Col22() As String = ""
    Public Property Col23() As String = ""
    Public Property Col24() As String = ""
    Public Property Col25() As String = ""
    Public Property Col26() As String = ""
    Public Property Col27() As String = ""
    Public Property Col28() As String = ""
    Public Property Col29() As String = ""
    Public Property Col30() As String = ""

    Public Property Col31() As String = ""
    Public Property Col32() As String = ""
    Public Property Col33() As String = ""
    Public Property Col34() As String = ""
    Public Property Col35() As String = ""
    Public Property Col36() As String = ""
    Public Property Col37() As String = ""
    Public Property Col38() As String = ""
    Public Property Col39() As String = ""
    Public Property Col40() As String = ""

    Public Property Col41() As String = ""
    Public Property Col42() As String = ""
    Public Property Col43() As String = ""
    Public Property Col44() As String = ""
    Public Property Col45() As String = ""
    Public Property Col46() As String = ""
    Public Property Col47() As String = ""
    Public Property Col48() As String = ""
    Public Property Col49() As String = ""
    Public Property Col50() As String = ""

    Public Property Col51() As String = ""
    Public Property Col52() As String = ""
    Public Property Col53() As String = ""
    Public Property Col54() As String = ""
    Public Property Col55() As String = ""
    Public Property Col56() As String = ""
    Public Property Col57() As String = ""
    Public Property Col58() As String = ""
    Public Property Col59() As String = ""
    Public Property Col60() As String = ""

    Public Property Col61() As String = ""
    Public Property Col62() As String = ""
    Public Property Col63() As String = ""
    Public Property Col64() As String = ""
    Public Property Col65() As String = ""
    Public Property Col66() As String = ""
    Public Property Col67() As String = ""
    Public Property Col68() As String = ""
    Public Property Col69() As String = ""
    Public Property Col70() As String = ""


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
