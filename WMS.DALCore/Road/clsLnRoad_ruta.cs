namespace WMS.DALCore.Road
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Reflection;
    using WMS.EntityCore.Road;

    public partial class clsLnRoad_ruta
    {
        private static clsInsert Ins = new clsInsert();
        private static clsUpdate Upd = new clsUpdate();
        public static void Cargar(ref clsBeRoad_ruta oBeRoad_ruta, DataRow dr)
        {
            int GetInt(string col) { return dr[col] is DBNull ? 0 : Convert.ToInt32(dr[col]); }
            bool GetBool(string col) { return dr[col] is DBNull ? false : Convert.ToBoolean(dr[col]); }
            string GetString(string col) { return dr[col] is DBNull ? "" : (Convert.ToString(dr[col]) ?? ""); }
            double GetDouble(string col) { return dr[col] is DBNull ? 0 : Convert.ToDouble(dr[col]); }

            try
            {
                oBeRoad_ruta.IdRuta = GetInt("IdRuta");
                oBeRoad_ruta.IdPropietarioBodega = GetInt("IdPropietarioBodega");
                oBeRoad_ruta.IdUbicacionTransito = GetInt("IdUbicacionTransito");
                oBeRoad_ruta.CODIGO = GetString("CODIGO");
                oBeRoad_ruta.NOMBRE = GetString("NOMBRE");
                oBeRoad_ruta.ACTIVO = GetString("ACTIVO");
                oBeRoad_ruta.VENDEDOR = GetString("VENDEDOR");
                oBeRoad_ruta.VENTA = GetString("VENTA");
                oBeRoad_ruta.FORANIA = GetString("FORANIA");
                oBeRoad_ruta.SUCURSAL = GetString("SUCURSAL");
                oBeRoad_ruta.TIPO = GetString("TIPO");
                oBeRoad_ruta.SUBTIPO = GetString("SUBTIPO");
                oBeRoad_ruta.BODEGA = GetString("BODEGA");
                oBeRoad_ruta.SUBBODEGA = GetString("SUBBODEGA");
                oBeRoad_ruta.DESCUENTO = GetString("DESCUENTO");
                oBeRoad_ruta.BONIF = GetString("BONIF");
                oBeRoad_ruta.KILOMETRAJE = GetString("KILOMETRAJE");
                oBeRoad_ruta.IMPRESION = GetString("IMPRESION");
                oBeRoad_ruta.RECIBOPROPIO = GetString("RECIBOPROPIO");
                oBeRoad_ruta.CELULAR = GetString("CELULAR");
                oBeRoad_ruta.RENTABIL = GetString("RENTABIL");
                oBeRoad_ruta.OFERTA = GetString("OFERTA");
                oBeRoad_ruta.PERCRENT = GetDouble("PERCRENT");
                oBeRoad_ruta.PASARCREDITO = GetString("PASARCREDITO");
                oBeRoad_ruta.TECLADO = GetString("TECLADO");
                oBeRoad_ruta.EDITDEVPREC = GetString("EDITDEVPREC");
                oBeRoad_ruta.EDITDESC = GetString("EDITDESC");
                oBeRoad_ruta.PARAMS = GetString("PARAMS");
                oBeRoad_ruta.SEMANA = GetInt("SEMANA");
                oBeRoad_ruta.OBJANO = GetInt("OBJANO");
                oBeRoad_ruta.OBJMES = GetInt("OBJMES");
                oBeRoad_ruta.SYNCFOLD = GetString("SYNCFOLD");
                oBeRoad_ruta.WLFOLD = GetString("WLFOLD");
                oBeRoad_ruta.FTPFOLD = GetString("FTPFOLD");
                oBeRoad_ruta.EMAIL = GetString("EMAIL");
                oBeRoad_ruta.LASTIMP = GetInt("LASTIMP");
                oBeRoad_ruta.LASTCOM = GetInt("LASTCOM");
                oBeRoad_ruta.LASTEXP = GetInt("LASTEXP");
                oBeRoad_ruta.IMPSTAT = GetString("IMPSTAT");
                oBeRoad_ruta.EXPSTAT = GetString("EXPSTAT");
                oBeRoad_ruta.COMSTAT = GetString("COMSTAT");
                oBeRoad_ruta.PARAM1 = GetString("PARAM1");
                oBeRoad_ruta.PARAM2 = GetString("PARAM2");
                oBeRoad_ruta.PESOLIM = GetDouble("PESOLIM");
                oBeRoad_ruta.INTERVALO_MAX = GetInt("INTERVALO_MAX");
                oBeRoad_ruta.LECTURAS_VALID = GetInt("LECTURAS_VALID");
                oBeRoad_ruta.INTENTOS_LECT = GetInt("INTENTOS_LECT");
                oBeRoad_ruta.HORA_INI = GetInt("HORA_INI");
                oBeRoad_ruta.HORA_FIN = GetInt("HORA_FIN");
                oBeRoad_ruta.APLICACION_USA = GetInt("APLICACION_USA");
                oBeRoad_ruta.PUERTO_GPS = GetInt("PUERTO_GPS");
                oBeRoad_ruta.ES_RUTA_OFICINA = GetBool("ES_RUTA_OFICINA");
                oBeRoad_ruta.DILUIR_BON = GetBool("DILUIR_BON");
                oBeRoad_ruta.PREIMPRESION_FACTURA = GetBool("PREIMPRESION_FACTURA");
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public static int Insertar(ref clsBeRoad_ruta oBeRoad_ruta, IConfiguration config, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Ins.Init("road_ruta");
                Ins.Add("idruta", "@idruta", "F");
                Ins.Add("idpropietariobodega", "@idpropietariobodega", "F");
                Ins.Add("idubicaciontransito", "@idubicaciontransito", "F");
                Ins.Add("codigo", "@codigo", "F");
                Ins.Add("nombre", "@nombre", "F");
                Ins.Add("activo", "@activo", "F");
                Ins.Add("vendedor", "@vendedor", "F");
                Ins.Add("venta", "@venta", "F");
                Ins.Add("forania", "@forania", "F");
                Ins.Add("sucursal", "@sucursal", "F");
                Ins.Add("tipo", "@tipo", "F");
                Ins.Add("subtipo", "@subtipo", "F");
                Ins.Add("bodega", "@bodega", "F");
                Ins.Add("subbodega", "@subbodega", "F");
                Ins.Add("descuento", "@descuento", "F");
                Ins.Add("bonif", "@bonif", "F");
                Ins.Add("kilometraje", "@kilometraje", "F");
                Ins.Add("impresion", "@impresion", "F");
                Ins.Add("recibopropio", "@recibopropio", "F");
                Ins.Add("celular", "@celular", "F");
                Ins.Add("rentabil", "@rentabil", "F");
                Ins.Add("oferta", "@oferta", "F");
                Ins.Add("percrent", "@percrent", "F");
                Ins.Add("pasarcredito", "@pasarcredito", "F");
                Ins.Add("teclado", "@teclado", "F");
                Ins.Add("editdevprec", "@editdevprec", "F");
                Ins.Add("editdesc", "@editdesc", "F");
                Ins.Add("params", "@params", "F");
                Ins.Add("semana", "@semana", "F");
                Ins.Add("objano", "@objano", "F");
                Ins.Add("objmes", "@objmes", "F");
                Ins.Add("syncfold", "@syncfold", "F");
                Ins.Add("wlfold", "@wlfold", "F");
                Ins.Add("ftpfold", "@ftpfold", "F");
                Ins.Add("email", "@email", "F");
                Ins.Add("lastimp", "@lastimp", "F");
                Ins.Add("lastcom", "@lastcom", "F");
                Ins.Add("lastexp", "@lastexp", "F");
                Ins.Add("impstat", "@impstat", "F");
                Ins.Add("expstat", "@expstat", "F");
                Ins.Add("comstat", "@comstat", "F");
                Ins.Add("param1", "@param1", "F");
                Ins.Add("param2", "@param2", "F");
                Ins.Add("pesolim", "@pesolim", "F");
                Ins.Add("intervalo_max", "@intervalo_max", "F");
                Ins.Add("lecturas_valid", "@lecturas_valid", "F");
                Ins.Add("intentos_lect", "@intentos_lect", "F");
                Ins.Add("hora_ini", "@hora_ini", "F");
                Ins.Add("hora_fin", "@hora_fin", "F");
                Ins.Add("aplicacion_usa", "@aplicacion_usa", "F");
                Ins.Add("puerto_gps", "@puerto_gps", "F");
                Ins.Add("es_ruta_oficina", "@es_ruta_oficina", "F");
                Ins.Add("diluir_bon", "@diluir_bon", "F");
                Ins.Add("preimpresion_factura", "@preimpresion_factura", "F");

                string sp = Ins.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);
                oBeRoad_ruta.IdRuta = MaxID(pConection, pTransaction) + 1;

                cmd.Parameters.Add(new SqlParameter("@IDRUTA", oBeRoad_ruta.IdRuta));
                cmd.Parameters.Add(new SqlParameter("@IDPROPIETARIOBODEGA", oBeRoad_ruta.IdPropietarioBodega));
                cmd.Parameters.Add(new SqlParameter("@IDUBICACIONTRANSITO", oBeRoad_ruta.IdUbicacionTransito));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeRoad_ruta.CODIGO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", oBeRoad_ruta.NOMBRE ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeRoad_ruta.ACTIVO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@VENDEDOR", oBeRoad_ruta.VENDEDOR ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@VENTA", oBeRoad_ruta.VENTA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FORANIA", oBeRoad_ruta.FORANIA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SUCURSAL", oBeRoad_ruta.SUCURSAL ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@TIPO", oBeRoad_ruta.TIPO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SUBTIPO", oBeRoad_ruta.SUBTIPO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@BODEGA", oBeRoad_ruta.BODEGA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SUBBODEGA", oBeRoad_ruta.SUBBODEGA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DESCUENTO", oBeRoad_ruta.DESCUENTO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@BONIF", oBeRoad_ruta.BONIF ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@KILOMETRAJE", oBeRoad_ruta.KILOMETRAJE ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@IMPRESION", oBeRoad_ruta.IMPRESION ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@RECIBOPROPIO", oBeRoad_ruta.RECIBOPROPIO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CELULAR", oBeRoad_ruta.CELULAR ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@RENTABIL", oBeRoad_ruta.RENTABIL ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@OFERTA", oBeRoad_ruta.OFERTA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PERCRENT", oBeRoad_ruta.PERCRENT));
                cmd.Parameters.Add(new SqlParameter("@PASARCREDITO", oBeRoad_ruta.PASARCREDITO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@TECLADO", oBeRoad_ruta.TECLADO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EDITDEVPREC", oBeRoad_ruta.EDITDEVPREC ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EDITDESC", oBeRoad_ruta.EDITDESC ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PARAMS", oBeRoad_ruta.PARAMS ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SEMANA", oBeRoad_ruta.SEMANA));
                cmd.Parameters.Add(new SqlParameter("@OBJANO", oBeRoad_ruta.OBJANO));
                cmd.Parameters.Add(new SqlParameter("@OBJMES", oBeRoad_ruta.OBJMES));
                cmd.Parameters.Add(new SqlParameter("@SYNCFOLD", oBeRoad_ruta.SYNCFOLD ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@WLFOLD", oBeRoad_ruta.WLFOLD ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FTPFOLD", oBeRoad_ruta.FTPFOLD ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", oBeRoad_ruta.EMAIL ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LASTIMP", oBeRoad_ruta.LASTIMP));
                cmd.Parameters.Add(new SqlParameter("@LASTCOM", oBeRoad_ruta.LASTCOM));
                cmd.Parameters.Add(new SqlParameter("@LASTEXP", oBeRoad_ruta.LASTEXP));
                cmd.Parameters.Add(new SqlParameter("@IMPSTAT", oBeRoad_ruta.IMPSTAT ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EXPSTAT", oBeRoad_ruta.EXPSTAT ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@COMSTAT", oBeRoad_ruta.COMSTAT ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PARAM1", oBeRoad_ruta.PARAM1 ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PARAM2", oBeRoad_ruta.PARAM2 ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PESOLIM", oBeRoad_ruta.PESOLIM));
                cmd.Parameters.Add(new SqlParameter("@INTERVALO_MAX", oBeRoad_ruta.INTERVALO_MAX));
                cmd.Parameters.Add(new SqlParameter("@LECTURAS_VALID", oBeRoad_ruta.LECTURAS_VALID));
                cmd.Parameters.Add(new SqlParameter("@INTENTOS_LECT", oBeRoad_ruta.INTENTOS_LECT));
                cmd.Parameters.Add(new SqlParameter("@HORA_INI", oBeRoad_ruta.HORA_INI));
                cmd.Parameters.Add(new SqlParameter("@HORA_FIN", oBeRoad_ruta.HORA_FIN));
                cmd.Parameters.Add(new SqlParameter("@APLICACION_USA", oBeRoad_ruta.APLICACION_USA));
                cmd.Parameters.Add(new SqlParameter("@PUERTO_GPS", oBeRoad_ruta.PUERTO_GPS));
                cmd.Parameters.Add(new SqlParameter("@ES_RUTA_OFICINA", oBeRoad_ruta.ES_RUTA_OFICINA));
                cmd.Parameters.Add(new SqlParameter("@DILUIR_BON", oBeRoad_ruta.DILUIR_BON));
                cmd.Parameters.Add(new SqlParameter("@PREIMPRESION_FACTURA", oBeRoad_ruta.PREIMPRESION_FACTURA));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Actualizar(ref clsBeRoad_ruta oBeRoad_ruta, IConfiguration config, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                Upd.Init("road_ruta");
                Upd.Add("idruta", "@idruta", "F");
                Upd.Add("idpropietariobodega", "@idpropietariobodega", "F");
                Upd.Add("idubicaciontransito", "@idubicaciontransito", "F");
                Upd.Add("codigo", "@codigo", "F");
                Upd.Add("nombre", "@nombre", "F");
                Upd.Add("activo", "@activo", "F");
                Upd.Add("vendedor", "@vendedor", "F");
                Upd.Add("venta", "@venta", "F");
                Upd.Add("forania", "@forania", "F");
                Upd.Add("sucursal", "@sucursal", "F");
                Upd.Add("tipo", "@tipo", "F");
                Upd.Add("subtipo", "@subtipo", "F");
                Upd.Add("bodega", "@bodega", "F");
                Upd.Add("subbodega", "@subbodega", "F");
                Upd.Add("descuento", "@descuento", "F");
                Upd.Add("bonif", "@bonif", "F");
                Upd.Add("kilometraje", "@kilometraje", "F");
                Upd.Add("impresion", "@impresion", "F");
                Upd.Add("recibopropio", "@recibopropio", "F");
                Upd.Add("celular", "@celular", "F");
                Upd.Add("rentabil", "@rentabil", "F");
                Upd.Add("oferta", "@oferta", "F");
                Upd.Add("percrent", "@percrent", "F");
                Upd.Add("pasarcredito", "@pasarcredito", "F");
                Upd.Add("teclado", "@teclado", "F");
                Upd.Add("editdevprec", "@editdevprec", "F");
                Upd.Add("editdesc", "@editdesc", "F");
                Upd.Add("params", "@params", "F");
                Upd.Add("semana", "@semana", "F");
                Upd.Add("objano", "@objano", "F");
                Upd.Add("objmes", "@objmes", "F");
                Upd.Add("syncfold", "@syncfold", "F");
                Upd.Add("wlfold", "@wlfold", "F");
                Upd.Add("ftpfold", "@ftpfold", "F");
                Upd.Add("email", "@email", "F");
                Upd.Add("lastimp", "@lastimp", "F");
                Upd.Add("lastcom", "@lastcom", "F");
                Upd.Add("lastexp", "@lastexp", "F");
                Upd.Add("impstat", "@impstat", "F");
                Upd.Add("expstat", "@expstat", "F");
                Upd.Add("comstat", "@comstat", "F");
                Upd.Add("param1", "@param1", "F");
                Upd.Add("param2", "@param2", "F");
                Upd.Add("pesolim", "@pesolim", "F");
                Upd.Add("intervalo_max", "@intervalo_max", "F");
                Upd.Add("lecturas_valid", "@lecturas_valid", "F");
                Upd.Add("intentos_lect", "@intentos_lect", "F");
                Upd.Add("hora_ini", "@hora_ini", "F");
                Upd.Add("hora_fin", "@hora_fin", "F");
                Upd.Add("aplicacion_usa", "@aplicacion_usa", "F");
                Upd.Add("puerto_gps", "@puerto_gps", "F");
                Upd.Add("es_ruta_oficina", "@es_ruta_oficina", "F");
                Upd.Add("diluir_bon", "@diluir_bon", "F");
                Upd.Add("preimpresion_factura", "@preimpresion_factura", "F");
                Upd.Where("IdRuta = @IdRuta");

                string sp = Upd.SQL();

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction);

                cmd.Parameters.Add(new SqlParameter("@IDRUTA", oBeRoad_ruta.IdRuta));
                cmd.Parameters.Add(new SqlParameter("@IDPROPIETARIOBODEGA", oBeRoad_ruta.IdPropietarioBodega));
                cmd.Parameters.Add(new SqlParameter("@IDUBICACIONTRANSITO", oBeRoad_ruta.IdUbicacionTransito));
                cmd.Parameters.Add(new SqlParameter("@CODIGO", oBeRoad_ruta.CODIGO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", oBeRoad_ruta.NOMBRE ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ACTIVO", oBeRoad_ruta.ACTIVO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@VENDEDOR", oBeRoad_ruta.VENDEDOR ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@VENTA", oBeRoad_ruta.VENTA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FORANIA", oBeRoad_ruta.FORANIA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SUCURSAL", oBeRoad_ruta.SUCURSAL ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@TIPO", oBeRoad_ruta.TIPO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SUBTIPO", oBeRoad_ruta.SUBTIPO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@BODEGA", oBeRoad_ruta.BODEGA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SUBBODEGA", oBeRoad_ruta.SUBBODEGA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DESCUENTO", oBeRoad_ruta.DESCUENTO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@BONIF", oBeRoad_ruta.BONIF ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@KILOMETRAJE", oBeRoad_ruta.KILOMETRAJE ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@IMPRESION", oBeRoad_ruta.IMPRESION ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@RECIBOPROPIO", oBeRoad_ruta.RECIBOPROPIO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CELULAR", oBeRoad_ruta.CELULAR ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@RENTABIL", oBeRoad_ruta.RENTABIL ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@OFERTA", oBeRoad_ruta.OFERTA ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PERCRENT", oBeRoad_ruta.PERCRENT));
                cmd.Parameters.Add(new SqlParameter("@PASARCREDITO", oBeRoad_ruta.PASARCREDITO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@TECLADO", oBeRoad_ruta.TECLADO ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EDITDEVPREC", oBeRoad_ruta.EDITDEVPREC ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EDITDESC", oBeRoad_ruta.EDITDESC ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PARAMS", oBeRoad_ruta.PARAMS ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SEMANA", oBeRoad_ruta.SEMANA));
                cmd.Parameters.Add(new SqlParameter("@OBJANO", oBeRoad_ruta.OBJANO));
                cmd.Parameters.Add(new SqlParameter("@OBJMES", oBeRoad_ruta.OBJMES));
                cmd.Parameters.Add(new SqlParameter("@SYNCFOLD", oBeRoad_ruta.SYNCFOLD ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@WLFOLD", oBeRoad_ruta.WLFOLD ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FTPFOLD", oBeRoad_ruta.FTPFOLD ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", oBeRoad_ruta.EMAIL ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LASTIMP", oBeRoad_ruta.LASTIMP));
                cmd.Parameters.Add(new SqlParameter("@LASTCOM", oBeRoad_ruta.LASTCOM));
                cmd.Parameters.Add(new SqlParameter("@LASTEXP", oBeRoad_ruta.LASTEXP));
                cmd.Parameters.Add(new SqlParameter("@IMPSTAT", oBeRoad_ruta.IMPSTAT ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EXPSTAT", oBeRoad_ruta.EXPSTAT ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@COMSTAT", oBeRoad_ruta.COMSTAT ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PARAM1", oBeRoad_ruta.PARAM1 ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PARAM2", oBeRoad_ruta.PARAM2 ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PESOLIM", oBeRoad_ruta.PESOLIM));
                cmd.Parameters.Add(new SqlParameter("@INTERVALO_MAX", oBeRoad_ruta.INTERVALO_MAX));
                cmd.Parameters.Add(new SqlParameter("@LECTURAS_VALID", oBeRoad_ruta.LECTURAS_VALID));
                cmd.Parameters.Add(new SqlParameter("@INTENTOS_LECT", oBeRoad_ruta.INTENTOS_LECT));
                cmd.Parameters.Add(new SqlParameter("@HORA_INI", oBeRoad_ruta.HORA_INI));
                cmd.Parameters.Add(new SqlParameter("@HORA_FIN", oBeRoad_ruta.HORA_FIN));
                cmd.Parameters.Add(new SqlParameter("@APLICACION_USA", oBeRoad_ruta.APLICACION_USA));
                cmd.Parameters.Add(new SqlParameter("@PUERTO_GPS", oBeRoad_ruta.PUERTO_GPS));
                cmd.Parameters.Add(new SqlParameter("@ES_RUTA_OFICINA", oBeRoad_ruta.ES_RUTA_OFICINA));
                cmd.Parameters.Add(new SqlParameter("@DILUIR_BON", oBeRoad_ruta.DILUIR_BON));
                cmd.Parameters.Add(new SqlParameter("@PREIMPRESION_FACTURA", oBeRoad_ruta.PREIMPRESION_FACTURA));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Eliminar(ref clsBeRoad_ruta oBeRoad_ruta, SqlConnection pConection, SqlTransaction pTransaction)
        {
            try
            {
                string sp = "DELETE FROM Road_ruta WHERE (IdRuta = @IdRuta)";

                using SqlCommand cmd = new SqlCommand(sp, pConection, pTransaction)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.Add(new SqlParameter("@IDRUTA", oBeRoad_ruta.IdRuta));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Obtener(ref clsBeRoad_ruta oBeRoad_ruta, IConfiguration config)
        {
            SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST"));
            SqlTransaction? lTransaction = null;

            bool result = false;

            try
            {
                lConnection.Open(); lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted);

                string sp = "SELECT * FROM Road_ruta" +
                " Where(IdRuta = @IdRuta)";

                using (SqlCommand cmd = new SqlCommand(sp, lConnection, lTransaction) { CommandType = CommandType.Text })
                {
                    using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                    {
                        dad.SelectCommand.Parameters.Add(new SqlParameter("@IDRUTA", oBeRoad_ruta.IdRuta));

                        DataTable dt = new DataTable();
                        dad.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            Cargar(ref oBeRoad_ruta, dt.Rows[0]);
                            result = true;
                        }
                    }
                }

                lTransaction.Commit();
            }
            catch (Exception)
            {
                if (lTransaction != null) lTransaction.Rollback();
                throw;
            }
            finally
            {
                if (lConnection.State == ConnectionState.Open) lConnection.Close();
                lTransaction?.Dispose();
                lConnection?.Dispose();
            }

            return result;
        }
        public static clsBeRoad_ruta? GetSingle(int pIdRuta, IConfiguration config)
        {
            try
            {
                string vSQL = "SELECT TOP 1 * FROM road_ruta WHERE idruta=@idruta";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;
                        lDTA.SelectCommand.Parameters.AddWithValue("@Idruta", pIdRuta);

                        DataTable lDT = new DataTable();
                        lDTA.Fill(lDT);

                        if (lDT != null && lDT.Rows.Count > 0)
                        {
                            DataRow lRow = lDT.Rows[0];
                            clsBeRoad_ruta Obj = new clsBeRoad_ruta();
                            Cargar(ref Obj, lRow);
                            return Obj;
                        }
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static clsBeRoad_ruta? GetSingle(int pIdRuta,
                                               SqlConnection lConnection,
                                               SqlTransaction lTransaction)
        {
            clsBeRoad_ruta? result = null;

            try
            {
                string vSQL = "SELECT TOP 1 * FROM road_ruta WHERE idruta=@idruta";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@Idruta", pIdRuta);

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        DataRow lRow = lDT.Rows[0];
                        clsBeRoad_ruta Obj = new clsBeRoad_ruta();
                        Cargar(ref Obj, lRow);
                        result = Obj;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static clsBeRoad_ruta? Get_IdRuta_By_Codigo(string pCodigoRuta,
                                                          SqlConnection lConnection,
                                                          SqlTransaction lTransaction)
        {
            clsBeRoad_ruta? result = null;

            try
            {
                string vSQL = "SELECT TOP 1 * FROM road_ruta WHERE codigo=@codigo";

                using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                {
                    lDTA.SelectCommand.CommandType = CommandType.Text;
                    lDTA.SelectCommand.Transaction = lTransaction;
                    lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigoRuta ?? (object)DBNull.Value);

                    DataTable lDT = new DataTable();
                    lDTA.Fill(lDT);

                    if (lDT != null && lDT.Rows.Count > 0)
                    {
                        DataRow lRow = lDT.Rows[0];
                        clsBeRoad_ruta Obj = new clsBeRoad_ruta();
                        Cargar(ref Obj, lRow);
                        result = Obj;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static List<clsBeRoad_ruta> GetAllFiltro(bool pActivo, IConfiguration config)
        {
            List<clsBeRoad_ruta> lReturnList = new List<clsBeRoad_ruta>();

            try
            {
                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    string vSQL = "SELECT * FROM road_ruta WHERE 1 > 0 ";

                    if (pActivo == true)
                    {
                        vSQL += " AND activo='S'";
                    }
                    else
                    {
                        vSQL += " AND activo='N'";
                    }

                    using (SqlDataAdapter lDTA = new SqlDataAdapter(vSQL, lConnection))
                    {
                        lDTA.SelectCommand.CommandType = CommandType.Text;

                        DataTable lDataTable = new DataTable();
                        lDTA.Fill(lDataTable);

                        if (lDataTable != null && lDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow lRow in lDataTable.Rows)
                            {
                                clsBeRoad_ruta Obj = new clsBeRoad_ruta();
                                Cargar(ref Obj, lRow);
                                lReturnList.Add(Obj);
                            }
                        }
                    }
                }

                return lReturnList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int MaxID(IConfiguration config)
        {
            try
            {
                int lMax = 0;
                string vSQL = "SELECT ISNULL(Max(IdRuta),0) FROM road_ruta";

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection))
                    {
                        lCommand.CommandType = CommandType.Text;
                        lConnection.Open();

                        object lReturnValue = lCommand.ExecuteScalar();
                        lConnection.Close();

                        if (lReturnValue != DBNull.Value && lReturnValue != null)
                        {
                            lMax = Convert.ToInt32(lReturnValue) + 1;
                        }
                    }
                }

                return lMax;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public enum TipoRuta
        {
            Venta = 0,
            Preventa = 1,
            Despacho = 2,
            Pedido = 3,
            Todas = 4
        }

        public static DataTable Listar_RoadRutas(TipoRuta pTipoRuta, IConfiguration config)
        {
            DataTable DT = new DataTable();

            try
            {
                string vSQL = "SELECT IdRuta, ISNULL(Codigo,'') + ' ' + ISNULL(Nombre,'') AS Nombre " +
                             " FROM road_ruta WHERE Activo='S' ";

                switch (pTipoRuta)
                {
                    case TipoRuta.Despacho:
                        vSQL += "And Venta = 'D' ";
                        break;
                    case TipoRuta.Preventa:
                        vSQL += "And Venta = 'P' ";
                        break;
                    case TipoRuta.Pedido:
                        vSQL += "And Venta IN('V','P','T') ";
                        break;
                    case TipoRuta.Venta:
                        vSQL += "And Venta = 'V' ";
                        break;
                    case TipoRuta.Todas:
                        break;
                }

                using (SqlConnection lConnection = new SqlConnection(config.GetConnectionString("CST")))
                {
                    using (SqlCommand cmd = new SqlCommand(vSQL, lConnection) { CommandType = CommandType.Text })
                    {
                        using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                        {
                            dad.Fill(DT);
                        }
                    }
                }

                return DT;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable Listar_RoadRutas(TipoRuta pTipoRuta,
                                                SqlConnection lConnection,
                                                SqlTransaction lTransaction)
        {
            DataTable DT = new DataTable();

            try
            {
                string vSQL = "SELECT IdRuta, ISNULL(Codigo,'') + ' ' + ISNULL(Nombre,'') AS Nombre " +
                    " FROM road_ruta WHERE Activo='S' ";

                switch (pTipoRuta)
                {
                    case TipoRuta.Despacho:
                        vSQL += "And Venta = 'D' ";
                        break;
                    case TipoRuta.Preventa:
                        vSQL += "And Venta = 'P' ";
                        break;
                    case TipoRuta.Pedido:
                        vSQL += "And Venta IN('V','P','T') ";
                        break;
                    case TipoRuta.Venta:
                        vSQL += "And Venta = 'V' ";
                        break;
                    case TipoRuta.Todas:
                        break;
                }

                using (SqlCommand cmd = new SqlCommand(vSQL, lConnection, lTransaction) { CommandType = CommandType.Text })
                {
                    using (SqlDataAdapter dad = new SqlDataAdapter(cmd))
                    {
                        dad.Fill(DT);
                    }
                }

                return DT;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int MaxID(SqlConnection lConnection, SqlTransaction lTransaction)
        {
            try
            {
                int lMax = 0;
                string vSQL = "SELECT ISNULL(Max(IdRuta),0) FROM road_ruta";

                using (SqlCommand lCommand = new SqlCommand(vSQL, lConnection, lTransaction) { CommandType = CommandType.Text })
                {
                    object lReturnValue = lCommand.ExecuteScalar();

                    if (lReturnValue != DBNull.Value && lReturnValue != null)
                    {
                        lMax = Convert.ToInt32(lReturnValue) + 1;
                    }
                }

                return lMax;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
