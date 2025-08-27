using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace WMSPortal.Library.Database
{
    public class Database
    {
        private SqlConnection Conexion;
        private readonly IConfiguration _Config;
        public Database(IConfiguration Config)
        {
            _Config = Config;
            SetConexion();
        }

        public string GetConnectionString()
        {
            string con = _Config.GetConnectionString("DefaultConnection");

            return con;
        }

        public void SetConexion()
        {
            string Cadena = GetConnectionString();
            Conexion = new SqlConnection(Cadena);
        }

        public SqlConnection GetConexion()
        {
            return Conexion;
        }

        public void AbrirConexion()
        {
            Conexion.Open();
        }

        public void CerrarConexion()
        {
            Conexion.Close();
        }


        public DataSet EjecutarQuery(string Query)
        {
            SqlCommand Command = new(Query, Conexion);
            DataSet Data = new();

            AbrirConexion();

            SqlDataAdapter DataAdapter = new(Command);
            DataAdapter.Fill(Data);

            CerrarConexion();
            return Data;
        }

        public object DataSetToList(DataSet ds)
        {

            List<List<Dictionary<string, object>>> listResponse = new List<List<Dictionary<string, object>>>();
            List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();

            //Iteramos cada DataTable dentro del DataSet
            foreach (DataTable dt in ds.Tables)
            {
                lst = new List<Dictionary<string, object>>();
                Dictionary<string, object> item;
                //Iteramos cada fila de la tabla
                foreach (DataRow row in dt.Rows)
                {
                    item = new Dictionary<string, object>();
                    //Iteramos cada columna de la fila 
                    foreach (DataColumn col in dt.Columns)
                    {
                        //agreamos un elemento al item, validando el valor que contiene
                        item.Add(col.ColumnName.ToLower(), (Convert.IsDBNull(row[col]) ? null : row[col]));
                    }
                    //atregamos el item a la lista
                    lst.Add(item);
                }
                listResponse.Add(lst);
            }
            //validamos las tablas con tiene el DataSet, y retornamos la lista
            if (ds.Tables.Count == 1)
                return lst;
            else
                return listResponse;
        }
    }
}
