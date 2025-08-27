Public Class clsCasosUsoReserva

    Shared ReadOnly Property Cliente_Pruebas As String = "TEST_WMS_231001"

    Public Shared Property CASO_1_IDEAL_20231002011101() As String =
    "CASO_1_IDEAL_20231002011101: 

    Hay cajas en almacenamiento que vencen primero Vrs las que están en zona de picking o ubicación de picking.
    
    Modelado de inventario:
    Insertar una caja en ubicación de almacenaje con fecha corta. 01/01/2026
    Insertar una caja en ubicación de picking con fecha superior.  02/01/2026

    Resultado esperado: Reservar cajas desde almacenamiento, respetando el producto que vence antes."

    Public Shared Property CASO_2_IDEAL_20231002011120() As String =
    "CASO_2_IDEAL_20231002011120: 

    Hay cajas en almacenamiento que vencen primero Vrs las que están en picking, pero no completan el pedido.
    
    Modelado de inventario:
    Insertar una caja en ubicación de almacenaje con fecha corta. 01/01/2026 (75)
    Insertar una caja en ubicación de picking con fecha superior.  02/01/2026 (200)

    Solicitado: 80
    Disponible en Almacenaje: 75
    Disponible en Picking: 200

    Resultado esperado: reservar cajas desde almacenamiento y completar con zona de picking.
    Observación: Respetar la fecha de vencimiento antes que la posición."


    Public Shared Property CASO_3_IDEAL_20231002011128() As String =
    "CASO_3_IDEAL_20231002011128: 

    Hay cajas en almacenamiento que vencen primero Vrs. las que están en zona de picking, 
    pero no completan el pedido, y tampoco las de picking.

    Modelado de inventario:
    Insertar 75 CJS en ubicación de almacenaje con fecha corta. 01/01/2026 (75)
    Insertar 25 cjs en ubicación de picking con fecha superior.  02/01/2026 (25)

    Solicitado: 150
    Disponible Total: 100

    Resultado esperado: Si la bandera de rechazo por pedido incompleto está activa, rechazar el pedido."


    Public Shared Property CASO_4_IDEAL_20231002011132() As String =
    "CASO_4_IDEAL_20231002011132: 

    Hay cajas en almacenamiento que vencen primero Vrs. las que están en zona de picking, 
    pero no completan el pedido, y tampoco las de picking.
                                                                        
    Modelado de inventario:
    Insertar una caja en ubicación de almacenaje con fecha corta. 01/01/2026 (75)
    Insertar una caja en ubicación de picking con fecha superior.  02/01/2026 (25)
    Insertar una caja en ubicación de almacenaje con fecha superior.  03/01/2026 (50)

    Solicitado: 150
    Disponible Total: 150
    Resultado esperado: Reserva Almacenaje, cambia a zona de picking y luego regresa a almacenaje."

    Public Shared Property CASO_5_IDEAL_20231002011134() As String =
    "CASO_5_IDEAL_20231002011134: 

    Hay cajas en picking que vencen primero Vrs. las que están en zona de almacemiento, pero no completan el pedido, y tampoco las de picking.

    Modelado de inventario:
    Insertar 75 caja en ubicación de picking con fecha corta. 01/01/2026
    Insertar 25 caja en ubicación de almacenaje con fecha superior.  02/01/2026
    Insertar 50 caja en ubicación de picking con fecha superior.  03/01/2026

    Solicitado: 150
    Disponible Total: 150

    Resultado esperado: Empezar por picking, subir al almacenamiento y volver a bajar al picking."

    Public Shared Property CASO_6_IDEAL_20231002011136() As String =
    "CASO_6_IDEAL_20231002011136: 

     Si las fechas de las cajas en zona de picking son superiores a las de almacenaje, 
     no debe permitir la explosión de unidades fuera de la zona de picking, 
     devolver error, existencia no disponible.

    Modelado de inventario:
    Insertar una caja en ubicación de picking con fecha superior. 03/01/2026 (1)
    Insertar una caja en ubicación de almacenaje con fecha corta.  02/01/2026 (1)

    Solicitado: 5 UDS
    Resultado esperado: No explosionar y mostrar mensaje de error."

    Public Shared Property CASO_7_IDEAL_20231002011140() As String =
    "CASO_7_IDEAL_20231002011140: 

     No hay unidades en picking, y si hay en almacenamiento.
     
    Modelado de inventario:
    No insertar inventario en ubicación de picking.
    Insertar en almacenamiento la misma cantidad solicitada  02/01/2026.

    Resultado esperado: Reservar de almacenamiento la cantidad solicitada."

    Public Shared Property CASO_8_IDEAL_20231002011142() As String =
    "CASO_8_IDEAL_20231002011142: 

     No hay unidades en picking, y si hay en almacenamiento.

    Modelado de inventario:
    No insertar inventario en ubicación de picking.
    Insertar en almacenamiento menor cantidad (4) que la solicitada  Vence: 02/01/2026.

    Resultado esperado: Si la bandera de rechazo por pedido incompleto está activa, rechazar el pedido.

    Solicitado: 5"
    Public Shared Property CASO_9_IDEAL_20231002011144() As String =
    "CASO_9_IDEAL_20231002011144: 

    Las unidades de almacenamiento no alcanzan.

    Modelado de inventario:
    Insertar 9 unidades en almacenamiento.
    Insertar 1 caja en ubicación de picking.

    Resultado esperado: Se deben tomar las 9 unidades de almacenamiento, debe explosionar la caja insertada 
    en ubicación de picking, reservar el restante.

    Solicitado: 11"
    Public Shared Property CASO_10_IDEAL_20231002011146() As String =
    "CASO_10_IDEAL_20231002011146:
    
    Las unidades de almacenamiento no alcanzan.

    Modelado de inventario:
    Insertar 9 unidades en almacenamiento.
    Insertar 1 caja en ubicación de picking.

    Resultado esperado: Se deben tomar las 9 unidades de almacenamiento, debe explosionar la caja insertada en ubicación de picking, 
    reservar el restante y si no hay más inventario debe rechazar el pedido.

    Solicitado: 34"
    Public Shared Property CASO_11_IDEAL_20231002011153() As String =
    "CASO_11_IDEAL_20231002011153: 
    
    Hay unidades en picking y en almacenamiento, pero las de almacenamiento tienen fecha más corta:

    Modelado de inventario:
    Insertar inventario en ubicación de picking por 26 UDS, con Vence. 30/12/2026
    Insertar en almacenamiento una posición por 15 con fecha 01/12/2026.
    Insertar en almacenamiento una posición por 10 con fecha 10/12/2026.

    Resultado esperado: reservar x,y antes que w.

    Solicitado: 25 UDS"

    Public Shared Property CASO_12_IDEAL_20231002011159() As String =
    "CASO_12_IDEAL_20231002011159:

    Hay unidades en picking y en almacenamiento, pero las de almacenamiento tienen fecha más corta:

    Modelado de inventario:
    Insertar inventario en ubicación de picking por 26 Uds, con Vence. 30/12/2026
    Insertar en almacenamiento una posición por 15 con fecha 01/12/2026.
    Insertar en almacenamiento una posición por 10 con fecha 10/12/2026.

    Resultado esperado: reservar aa). bb) antes que z.

    Solicitado: 26 UDS"

    Public Shared Property CASO_13_IDEAL_20231002011201() As String =
    "CASO_13_IDEAL_20231002011201: 

    Las unidades en almacenamiento + picking, no alcanzan para cubrir el pedido.  Debe explosionar cajas desde picking.

    Modelado de inventario:
    Insertar inventario en ubicación de picking 10 Uds, con fecha vence: 30/12/2026
    Insertar en almacenamiento una posición por 15 con fecha 01/12/2026.
    Insertar en almacenamiento 1 caja de almacenamiento con fecha 10/12/2026.

    Resultado esperado: reservar cc). dd) y luego validar si el producto en presentación, 
    está en una ubicación que se pueda explosionar, si no tengo producto en zona de picking entonces, rechazar el pedido por incompleto.

    Solicitado: 26 UDS"

    Public Shared Property CASO_14_IDEAL_20231002011201() As String =
    "CASO_14_IDEAL_20231002011201: 
    
    Las unidades en almacenamiento + picking, no alcanzan para cubrir el pedido.  Debe explosionar cajas desde picking.

    Modelado de inventario:
    Insertar inventario en ubicación de picking 10 Uds, con fecha vence: 30/12/2026
    Insertar en almacenamiento una posición por 15 con fecha 01/12/2026.
    Insertar en picking 1 caja con fecha 10/12/2026. (Fecha más corta que ALM)

    Resultado esperado: reservar ff). gg) y luego validar si el producto en presentación, está en una ubicación que se pueda explosionar, 
    complementar las unidades faltantes con el inventario de hh).

    Código: 203258L31 
    Solicitado: 26 UDS

    Validar que no reserve los 26 de la explosión."

    Public Shared Property CASO_15_IDEAL_20231018130000() As String =
    "CASO_15_IDEAL_20231018130000: 
    
    Hay unidades en picking y en almacenamiento, las unidades no alcanzan debe explosionar las fechas mas cortas de almacenamiento

    Modelado de inventario.
    A - Insertar inventario en ubicación de picking por 8 UDS, con Vence. 02/07/2026
    B - Insertar inventario en ubicación de picking por 4 UDS, con Vence. 04/07/2026
    C - Insertar inventario en ubicación de picking por 10 CJS, con Vence. 04/07/2026
    D - Insertar inventario en ubicación de picking por 5 CJS, con Vence. 06/07/2026
    E - Insertar inventario en almacenamiento 1 UND con fecha 01/07/2026.
    F - Insertar inventario en almacenamiento 2 UND con fecha 03/07/2026.
    G - Insertar inventario en almacenamiento 25 CJS con fecha 03/07/2026.
    
    Resultado esperado: reservar E, A, F, B y luego validar si el producto en presentación, 
    está en una ubicación que se pueda explosionar, si no tengo producto en zona de picking entonces, rechazar el pedido por incompleto.


    Solicitado: 16 UDS"


    Public Shared Property CASO_16_IDEAL_202310200156() As String =
    "CASO_16_IDEAL_202310200156: 
    
    Hay unidades en picking y en almacenamiento, las unidades no alcanzan debe explosionar las fechas mas cortas de almacenamiento

    Modelado de inventario.
    A - Insertar inventario en ubicación de picking por 8 UDS, con Vence. 02/07/2026
    B - Insertar inventario en ubicación de picking por 4 UDS, con Vence. 04/07/2026
    C - Insertar inventario en ubicación de picking por 10 CJS, con Vence. 03/07/2026
    D - Insertar inventario en ubicación de picking por 5 CJS, con Vence. 06/07/2026
    E - Insertar inventario en almacenamiento 1 UND con fecha 01/07/2026.
    F - Insertar inventario en almacenamiento 2 UND con fecha 03/07/2026.
    G - Insertar inventario en almacenamiento 25 CJS con fecha 04/07/2026.
    
    Resultado esperado: reservar E, A, F, B, Explosión C

    Solicitado: 16 UDS"

    Public Shared Property CASO_17_IDEAL_202311040904() As String =
    "CASO_17_IDEAL_202311040904: 
    
    Hay 7 unidades en almacenamiento y 1 caja en picking, con la misma fecha de vencimiento debe
    tomar las unidades de almacenamiento

    Modelado de inventario.
    A - Insertar inventario en picking 1 CJ con fecha vence 30/12/2023
    B - Insertar inventario en ubicación de almacenamiento por 7 UDS, con fecha vence 30/12/2023
    Resultado esperado: reservar B
    Solicitado: 7 UDS"

    Public Shared Property CASO_18_BYB_202311141034() As String =
    "CASO_18_BYB_202311141034: 
    
    Hay 

    Modelado de inventario.
    A - 
    B - 
    Resultado esperado: reservar 
    Solicitado: "

    Public Shared Property CASO_19_BYB_202311162103() As String =
    "CASO_19_BYB_202311162103: 
    
    Hay 35 CJS en almacenamiento con fecha corta, y 36 CJS en picking y 72 CJS en almacenamiento, con la misma fecha de vencimiento debe
    tomar las CJS con fecha corta de almacenamiento y completar con almacenamiento

    Modelado de inventario.
    A - Insertar inventario en ubicación de almacenamiento(174) 34 CJS con fecha vence 14/12/224
    B - Insertar inventario en ubicación de almacenamiento(174) 1 CJ con fecha vence 14/12/224
    C - Insertar inventario en ubicación de picking(512) por 36 CJS, con fecha vence 08/02/2025
    D - Insertar inventario en ubicación de almacenamiento(556) por 72 CJS, con fecha vence 08/02/2025
    Resultado esperado: reservar A, B, D
    Solicitado: 45 CJS"

    Public Shared Property CASO_20_BYB_202311171219() As String =
    "CASO_20_BYB_202311171219 : 
    
    Hay 10 CJS en picking con fecha menor a almacenamiento y 59 CJS en almacenamiento y 136 CJS en almacenamiento, con la misma fecha de vencimiento ambas, debe
    tomar las unidades de picking primero y luego completar almacenamiento tomando el mas cercano por nivel

    Modelado de inventario.
    A - Insertar inventario en ubicación de picking(3111) 10 CJS con fecha vence 07/03/2025
    B - Insertar inventario en ubicación de almacenamiento(2892) 136 CJS con fecha vence 24/04/2025
    C - Insertar inventario en ubicación de almacenamient(2884) por 59 CJS con fecha vence  24/04/2025
    Resultado esperado: reservar 10 de A y 15 de C
    Solicitado: 25 CJS"

End Class
