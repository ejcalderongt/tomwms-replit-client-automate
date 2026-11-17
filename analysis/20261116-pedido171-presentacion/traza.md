# #EJC20261116_PEDIDO_PRESENTACION

Alcance: BOF, dev_2028_merge, pedido 171 de la base de desarrollo indicada por Carolina.
Lectura SQL: detalle 1009, linea 1, IdPresentacion NULL, stock 841, reserva 889 con IdPresentacion=0 y cantidad=12. Detalle 1010, linea 2, IdPresentacion=9 CAJA, stock 1509, reserva 890 con IdPresentacion=9 y cantidad=1760 (88 x factor 20).

Traza: Cargar_Detalle_Pedido -> Llena_Presentacion_Grid(index, conexion, transaccion, IdPresentacion) -> Get_Presentaciones_Cacheadas -> DataGridViewComboBoxCell.Value.
La sobrecarga con transaccion elige la primera presentacion en modo Editar antes de leer el Id recibido. Tambien se detecta sustitucion de cero explicito al seleccionar stock especifico mediante la sobrecarga sin conexion. La sobrecarga de kits ignora un Id recibido mientras Nuevo/Editar.

Candidato: compartir seleccion entre las tres sobrecargas generales, conservar ID positivo disponible, conservar cero explicito sin presentacion y reservar -1 para el argumento omitido (producto nuevo sin seleccion). ID positivo ausente queda vacio, sin sustituirlo por otro.
Sin cambios de BD, cantidades, reservas, conversiones ni condiciones por empresa. Sin cambios de HH. Las consultas muestran que la discrepancia es visual: no justifican convertir la linea 1 a CAJA.

Resultado: cambio aplicado solo a frmPedido.vb; UTF-8 BOM y CRLF preservados. La variante de kits se reviso y se dejo fuera del cambio por no pertenecer al recorrido observado. Ocho pruebas locales compiladas con .NET Framework 4.8 y controles reales de WinForms: cero explicito, CAJA, presentacion distinta del primer elemento, argumento omitido, ID no disponible, listas vacias y dos filas del mismo producto con selecciones distintas. BOF Debug/AnyCPU compilo con BuildProjectReferences=false, salida 0; referencias de proyecto reutilizadas. Advertencias de ensamblados, licencia DevExpress y API obsoleta. No se ejecuto el flujo completo de guardar/reservar en la aplicacion ni se desplego.
