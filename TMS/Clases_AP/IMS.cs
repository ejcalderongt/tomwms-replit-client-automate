using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace TOMWMS
{

    public class IMS
    {

        public clsBeUsuario UsuarioAp { get; set; } = new clsBeUsuario();
        public virtual int IdEmpresa { get; set; }
        public virtual string IdRol { get; set; }

        public string HostName { get; set; } = "";
        public int IdBodega { get; set; }
        public string NomBodega { get; set; }
        public string NomEmpresa { get; set; }
        public bool Exigir_Politica_Contraseñas { get; set; } = false;
        public int No_Tareas { get; set; }
        public bool LicenciaServidor { get; set; }
        public int IdBodegaAnterior { get; set; }
        public int IdConfiguracionInterface { get; set; }

        public string vSQL = "";

        public bool Existe_Ini()
        {
            bool Existe_IniRet = default;
            string AppPath = FileSystem.CurDir() + @"\";
            if (System.IO.File.Exists(AppPath + "Conn.ini"))
            {
                Existe_IniRet = true;
            }
            else
            {
                Existe_IniRet = false;
            }

            return Existe_IniRet;
        }

        public static void WaitCur()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public static void DefCur()
        {
            Cursor.Current = Cursors.Default;
        }

        public static void EsNumerico(ref object Texto)
        {
            if (Conversions.ToBoolean(Operators.AndObject(Operators.ConditionalCompareObjectNotEqual(Texto.Text, "", true), !Information.IsNumeric(Texto.Text))))
            {
                Texto.Text = "";
                XtraMessageBox.Show("Ingrese un dato numérico!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Texto.Focus();
            }
        }

        public static bool Listar_Operadores(ref LookUpEdit Cmb)
        {
            bool Listar_OperadoresRet = default;

            Listar_OperadoresRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnOperador.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdOperador";
                    Cmb.Properties.DataSource = DT;
                    Cmb.EditValue = DT.Rows[0]["IdOperador"];
                    Cmb.ItemIndex = 0;
                }

                Listar_OperadoresRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_OperadoresRet;

        }

        public static bool Listar_OperadoresByInventario(ref LookUpEdit Cmb, int IdUbic, int IdInventario)
        {
            bool Listar_OperadoresByInventarioRet = default;

            Listar_OperadoresByInventarioRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTrans_inv_operador.GetAllForCombo(IdUbic, IdInventario);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombres";
                    Cmb.Properties.ValueMember = "IdOperador";
                    Cmb.Properties.DataSource = DT;
                    Cmb.EditValue = DT.Rows[0]["IdOperador"];
                    Cmb.ItemIndex = 0;
                }

                Listar_OperadoresByInventarioRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_OperadoresByInventarioRet;

        }

        public static bool Listar_OperadoresByStock(ref LookUpEdit Cmb, int IdUbic, int IdInventario, int IdStock)
        {
            bool Listar_OperadoresByStockRet = default;

            Listar_OperadoresByStockRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTrans_inv_ciclico.GetAllForCombo(IdUbic, IdInventario, IdStock);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombres";
                    Cmb.Properties.ValueMember = "IdOperador";
                    Cmb.Properties.DataSource = DT;
                    Cmb.EditValue = DT.Rows[0]["IdOperador"];
                    Cmb.ItemIndex = 0;
                }

                Listar_OperadoresByStockRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_OperadoresByStockRet;

        }

        public static bool Listar_Areas_By_Bodega(ref LookUpEdit Cmb, int pIdBodega)
        {
            bool Listar_Areas_By_BodegaRet = default;

            Listar_Areas_By_BodegaRet = false;

            var Dt = new DataTable();

            try
            {

                Dt = clsLnBodega_area.Get_All_Areas_By_IdBodega_For_Combo(pIdBodega);

                if (Dt.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdArea";
                    Cmb.Properties.DataSource = Dt;
                    Cmb.ItemIndex = 0;
                    Cmb.EditValue = Dt.Rows[0]["IdArea"];
                }

                Listar_Areas_By_BodegaRet = Dt.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_Areas_By_BodegaRet;

        }

        public static bool Listar_Sectores_By_Area(ref LookUpEdit Cmb, int pIdArea, int pIdBodega)
        {
            bool Listar_Sectores_By_AreaRet = default;

            Listar_Sectores_By_AreaRet = false;
            var Dt = new DataTable();

            try
            {

                Dt = clsLnBodega_sector.Get_All_Sector_By_IdArea_And_IdBodega_For_Combo(pIdArea, pIdBodega);

                if (Dt.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdSector";
                    Cmb.Properties.DataSource = Dt;
                    Cmb.ItemIndex = 0;
                    Cmb.EditValue = Dt.Rows[0]["IdSector"];
                }

                Listar_Sectores_By_AreaRet = Dt.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_Sectores_By_AreaRet;

        }

        public static bool Listar_TramosBySector(ref LookUpEdit Cmb, int pIdSector, int pIdBodega)
        {
            bool Listar_TramosBySectorRet = default;

            Listar_TramosBySectorRet = false;

            var Dt = new DataTable();

            try
            {

                Dt = clsLnBodega_tramo.GetAllTramosBySectorForCombo(pIdSector, pIdBodega);

                if (Dt.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTramo";
                    Cmb.Properties.DataSource = Dt;
                }

                Listar_TramosBySectorRet = Dt.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TramosBySectorRet;

        }

        public static bool Listar_TramosByBodega(ref System.Windows.Forms.ComboBox Cmb, int pIdBodega)
        {
            bool Listar_TramosByBodegaRet = default;

            Listar_TramosByBodegaRet = false;

            var Dt = new List<clsBeBodega_tramo>();

            try
            {

                Dt = clsLnBodega_tramo.Get_All_By_IdBodega(true, pIdBodega);

                if (Dt.Count > 0)
                {
                    Cmb.DisplayMember = "Descripcion";
                    Cmb.ValueMember = "IdTramo";
                    Cmb.DataSource = Dt;
                }

                Listar_TramosByBodegaRet = Dt.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TramosByBodegaRet;

        }

        public static bool Listar_TramosInventario(ref LookUpEdit Cmb)
        {
            bool Listar_TramosInventarioRet = default;

            Listar_TramosInventarioRet = false;

            DataTable Dt;

            try
            {

                Dt = clsLnBodega_tramo.GetAllTramosForCombo();

                if (Dt.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTramo";
                    Cmb.Properties.DataSource = Dt;
                }

                Listar_TramosInventarioRet = Dt.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TramosInventarioRet;

        }

        public static bool Listar_Empresas(ref System.Windows.Forms.ComboBox Cmb)
        {
            bool Listar_EmpresasRet = default;

            Listar_EmpresasRet = false;

            var DT = new List<clsBeEmpresa>();

            try
            {

                DT = clsLnEmpresa.GetAll();

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "Nombre";
                    Cmb.ValueMember = "IdEmpresa";
                    Cmb.DataSource = DT;
                }

                Listar_EmpresasRet = DT.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_EmpresasRet;

        }

        public static bool Listar_Empresas(ref LookUpEdit Cmb)
        {
            bool Listar_EmpresasRet = default;

            Listar_EmpresasRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnEmpresa.GetAllForComboBox();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdEmpresa";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_EmpresasRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_EmpresasRet;

        }

        public static bool Listar_Unidad_Medida(ref LookUpEdit Cmb)
        {
            bool Listar_Unidad_MedidaRet = default;

            Listar_Unidad_MedidaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnUnidad_medida.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdUnidadMedida";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_Unidad_MedidaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_Unidad_MedidaRet;

        }

        public static bool Listar_Presentaciones(ref LookUpEdit Cmb, int pIdProducto)
        {
            bool Listar_PresentacionesRet = default;

            Listar_PresentacionesRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnProducto_presentacion.Get_All_By_IdProducto_For_Combo(pIdProducto);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombre";
                    Cmb.Properties.ValueMember = "IdPresentacion";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_PresentacionesRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_PresentacionesRet;

        }

        public static bool Listar_ProductoEstado(ref LookUpEdit Cmb)
        {
            bool Listar_ProductoEstadoRet = default;

            Listar_ProductoEstadoRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnProducto_estado.GetAllByForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombre";
                    Cmb.Properties.ValueMember = "IdEstado";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_ProductoEstadoRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_ProductoEstadoRet;

        }

        public static bool Listar_ProductoEstadoNE(ref LookUpEdit Cmb)
        {
            bool Listar_ProductoEstadoNERet = default;

            Listar_ProductoEstadoNERet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnProducto_estado.Get_All_For_Combo_NE();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombre";
                    Cmb.Properties.ValueMember = "IdEstado";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_ProductoEstadoNERet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_ProductoEstadoNERet;

        }

        public static bool Listar_Paises(ref LookUpEdit Cmb)
        {
            bool Listar_PaisesRet = default;

            Listar_PaisesRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnPaises.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdPais";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                    return true;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_PaisesRet;

        }

        public static bool Listar_EmpresaTransportePorEmpresa(ref LookUpEdit Cmb, int pIdEmpresa)
        {
            bool Listar_EmpresaTransportePorEmpresaRet = default;

            Listar_EmpresaTransportePorEmpresaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnEmpresa_transporte.Get_All_For_Combo(pIdEmpresa);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombre";
                    Cmb.Properties.ValueMember = "IdEmpresaTransporte";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_EmpresaTransportePorEmpresaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_EmpresaTransportePorEmpresaRet;

        }

        public static bool Listar_TipoContenedor(ref LookUpEdit Cmb)
        {
            bool Listar_TipoContenedorRet = default;

            Listar_TipoContenedorRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTipo_contenedor.Get_All_For_Combo(true);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTipoContenedor";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_TipoContenedorRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TipoContenedorRet;

        }

        public static bool Listar_TipoTarima(ref LookUpEdit Cmb)
        {
            bool Listar_TipoTarimaRet = default;

            Listar_TipoTarimaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTipo_tarima.GetAllForCombo(true);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTipoTarima";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_TipoTarimaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TipoTarimaRet;

        }

        public static bool Listar_TipoTarea(ref LookUpEdit Cmb)
        {
            bool Listar_TipoTareaRet = default;

            Listar_TipoTareaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnSis_tipo_tarea.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTipoTarea";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_TipoTareaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TipoTareaRet;

        }

        /// <summary>
    /// Creada por Bismarck Traña
    /// Funcion retorna el listado de los propietarios que se encuentran registrados en una empresa
    /// </summary>
    /// <param name="Cmb">Combobox donde se cargara la lista de Propietarios</param>
    /// ''' <param name="pIdEmpresa">idempresa que sea filtrar los propietarios</param>
    /// <remarks></remarks>
        public static bool Listar_Propietarios_By_IdEmpresa(ref LookUpEdit Cmb, int pIdEmpresa)
        {

            var DT = new DataTable();

            try
            {

                DT = clsLnPropietarios.Get_All_By_IdEmpresa_For_Combo(pIdEmpresa);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdPropietario";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                    Cmb.Properties.PopupWidth = 700;
                    Cmb.Properties.PopulateColumns();
                    Cmb.Properties.Columns[0].Visible = false;
                    Cmb.Properties.BestFit();
                    Cmb.Properties.NullText = "";
                }

                return DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static bool Listar_PropietariosByEmpresaExcel(ref System.Windows.Forms.ComboBox Cmb, int pIdEmpresa)
        {

            var DT = new List<clsBePropietarios>();

            try
            {

                DT = clsLnPropietarios.Get_All_By_IdEmpresa_Class(pIdEmpresa);

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "nombre_comercial";
                    Cmb.ValueMember = "IdPropietario";
                    Cmb.DataSource = DT;
                }

                return DT.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static bool Listar_TipoCliente(ref LookUpEdit Cmb)
        {
            bool Listar_TipoClienteRet = default;

            Listar_TipoClienteRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnCliente_tipo.GetAllForCombo(true);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "NombreTipoCliente";
                    Cmb.Properties.ValueMember = "IdTipoCliente";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_TipoClienteRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TipoClienteRet;

        }

        public static bool Listar_Parametro(ref LookUpEdit Cmb)
        {
            bool Listar_ParametroRet = default;

            Listar_ParametroRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnP_parametro.GetAllForCombo(true);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "descripcion";
                    Cmb.Properties.ValueMember = "IdParametro";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_ParametroRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_ParametroRet;

        }

        public static bool Listar_PerfilSerializado(ref LookUpEdit Cmb)
        {
            bool Listar_PerfilSerializadoRet = default;

            Listar_PerfilSerializadoRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnPerfil_serializado.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdPerfilSerializado";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_PerfilSerializadoRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_PerfilSerializadoRet;

        }

        public static bool Listar_Camara(ref LookUpEdit Cmb)
        {
            bool Listar_CamaraRet = default;

            Listar_CamaraRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnCamara.GetAllForCombo(true);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdCamara";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_CamaraRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_CamaraRet;

        }

        public static bool Listar_Proveedor(ref System.Windows.Forms.ComboBox Cmb)
        {
            bool Listar_ProveedorRet = default;

            Listar_ProveedorRet = false;

            var DT = new List<clsBeProveedor>();

            try
            {

                DT = clsLnProveedor.GetAll();

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "nombre";
                    Cmb.ValueMember = "IdProveedor";
                    Cmb.DataSource = DT;
                }

                Listar_ProveedorRet = DT.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_ProveedorRet;

        }

        public static bool Listar_TipoIngresoOC(ref LookUpEdit Cmb)
        {
            bool Listar_TipoIngresoOCRet = default;

            Listar_TipoIngresoOCRet = false;

            var DT = new DataTable();

            try
            {


                // DT = clsLnTrans_oc_ti.GetAllForCombo()
                // GT 28042021 el error es por los guiones bajos. Pero es prueba.
                DT = clsLnTrans_oc_ti.Get_All_ForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTipoIngreso";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }
                else
                {
                    throw new Exception("No hay Tipo Ingresos Definidos");
                }

                Listar_TipoIngresoOCRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TipoIngresoOCRet;

        }

        public static bool Listar_Muelles(ref LookUpEdit CmbMuelles, int IdBodega)
        {
            bool Listar_MuellesRet = default;

            Listar_MuellesRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnBodega_muelles.Get_All_By_IdBodega_For_Combo(IdBodega);

                if (DT.Rows.Count > 0)
                {
                    CmbMuelles.Properties.DisplayMember = "nombre";
                    CmbMuelles.Properties.ValueMember = "IdMuelle";
                    CmbMuelles.Properties.DataSource = DT;
                    CmbMuelles.ItemIndex = 0;
                }
                else
                {
                    throw new Exception("No existen muelles para la bodega seleccionada");
                }

                Listar_MuellesRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_MuellesRet;

        }

        public static bool Listar_RoadRutas(ref LookUpEdit CmbRoadRutas)
        {
            bool Listar_RoadRutasRet = default;

            Listar_RoadRutasRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnRoad_ruta.Listar_RoadRutas(clsLnRoad_ruta.TipoRuta.Pedido);

                if (DT.Rows.Count > 0)
                {
                    CmbRoadRutas.Properties.DisplayMember = "Nombre";
                    CmbRoadRutas.Properties.ValueMember = "IdRuta";
                    CmbRoadRutas.Properties.DataSource = DT;
                    CmbRoadRutas.ItemIndex = 0;
                    // Else
                    // Throw New Exception("No existen rutas road para la bodega seleccionada")
                }

                Listar_RoadRutasRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_RoadRutasRet;

        }

        public static bool Listar_EstadosOC(ref LookUpEdit Cmb)
        {
            bool Listar_EstadosOCRet = default;

            Listar_EstadosOCRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTrans_oc_estado.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdEstado";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_EstadosOCRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_EstadosOCRet;

        }

        public static bool Listar_Aranceles(ref LookUpEdit Cmb)
        {
            bool Listar_ArancelesRet = default;

            Listar_ArancelesRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnArancel.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdArancel";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_ArancelesRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_ArancelesRet;

        }

        public static bool Listar_Regimen_Fiscal(ref LookUpEdit Cmb)
        {
            bool Listar_Regimen_FiscalRet = default;

            Listar_Regimen_FiscalRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnRegimen_fiscal.Get_All_For_Combo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "descripcion";
                    Cmb.Properties.ValueMember = "codigo_regimen";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_Regimen_FiscalRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_Regimen_FiscalRet;

        }

        public bool Listar_Bodegas_Login(ref System.Windows.Forms.ComboBox Cmb)
        {
            bool Listar_Bodegas_LoginRet = default;

            Listar_Bodegas_LoginRet = false;

            var DT = new DataTable();

            if (IdEmpresa == 0)
                return Listar_Bodegas_LoginRet;

            try
            {

                DT = clsLnBodega.Get_All_By_Empresa_ForCombo(IdEmpresa);

                if (DT.Rows.Count > 0)
                {
                    Cmb.DisplayMember = "Nombre";
                    Cmb.ValueMember = "IdBodega";
                    Cmb.DataSource = DT;
                }

                Listar_Bodegas_LoginRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_Bodegas_LoginRet;

        }

        public bool Listar_BodegasLogin(ref LookUpEdit Cmb)
        {
            bool Listar_BodegasLoginRet = default;

            Listar_BodegasLoginRet = false;

            var DT = new DataTable();

            if (IdEmpresa == 0)
                return Listar_BodegasLoginRet;

            try
            {

                DT = clsLnBodega.Get_All_By_Empresa_ForCombo(IdEmpresa);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdBodega";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_BodegasLoginRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_BodegasLoginRet;

        }

        public bool Listar_Bodegas_By_Usuario(ref LookUpEdit Cmb)
        {
            bool Listar_Bodegas_By_UsuarioRet = default;

            Listar_Bodegas_By_UsuarioRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnBodega.Get_All_By_IdEmpresa_And_IdUsuario_DT(IdEmpresa, TOMWMS.m_Global.AP.UsuarioAp.IdUsuario);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "Codigo";
                    Cmb.Properties.DataSource = DT;
                    Cmb.EditValue = (object)TOMWMS.m_Global.AP.IdBodega;
                }
                else
                {
                    throw new Exception("No hay definidas bodegas para la empresa: " + IdEmpresa);
                }

                Listar_Bodegas_By_UsuarioRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                string vMsgError = string.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message);
                clsLnLog_error_wms.Agregar_Error(vMsgError);
                throw new Exception(string.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
            }

            return Listar_Bodegas_By_UsuarioRet;

        }

        public static DataTable Listar_Bodegas()
        {

            try
            {

                var DT = new DataTable("Bodega");

                DT = clsLnBodega.Listar_Bodegas_Activas();

                return DT;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Listar_Bodegas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new DataTable("Error");
            }

        }

        public static bool Listar_Bodegas_Por_Empresa(ref LookUpEdit Cmb, int pIdEmpresa)
        {
            bool Listar_Bodegas_Por_EmpresaRet = default;

            Listar_Bodegas_Por_EmpresaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnBodega.Get_All_By_Empresa_ForCombo(pIdEmpresa);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdBodega";
                    Cmb.Properties.DataSource = DT;
                    // Cmb.ItemIndex = 0
                }

                Listar_Bodegas_Por_EmpresaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_Bodegas_Por_EmpresaRet;

        }

        public static bool Lista_Pedido_Ingreso(ref LookUpEdit Cmb)
        {
            bool Lista_Pedido_IngresoRet = default;

            Lista_Pedido_IngresoRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTrans_oc_ti.Get_All_ForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTipoIngresoOC";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Lista_Pedido_IngresoRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Lista_Pedido_IngresoRet;

        }

        public static bool Lista_Pedido_Salida(ref LookUpEdit Cmb)
        {
            bool Lista_Pedido_SalidaRet = default;

            Lista_Pedido_SalidaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTrans_pe_tipo.Get_All_ForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTipoPedido";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Lista_Pedido_SalidaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Lista_Pedido_SalidaRet;

        }

        public static bool Listar_Tipo_Ajuste(ref LookUpEdit Cmb)
        {
            bool Listar_Tipo_AjusteRet = default;

            Listar_Tipo_AjusteRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnAjuste_tipo.Get_All_ForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTipoAjuste";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_Tipo_AjusteRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_Tipo_AjusteRet;

        }

        public static bool Listar_ClientesByEmpresa(ref LookUpEdit Cmb, int pIdEmpresa)
        {
            bool Listar_ClientesByEmpresaRet = default;

            Listar_ClientesByEmpresaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnCliente.Get_All_By_IdEmpresa_For_Combo(pIdEmpresa);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdCliente";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_ClientesByEmpresaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_ClientesByEmpresaRet;

        }

        public static bool Listar_Producto_Familia(ref LookUpEdit Cmb, int pIdPropietario)
        {
            bool Listar_Producto_FamiliaRet = default;

            Listar_Producto_FamiliaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnProducto_familia.Get_All_By_IdPropietario(true, pIdPropietario);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdFamilia";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_Producto_FamiliaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_Producto_FamiliaRet;

        }

        /// <summary>
    /// Metodo Creado por Bismarck
    /// 
    /// Carga el listado de Bodega en los cuales es encuntra asignado un propietario
    /// 
    /// </summary>
    /// <param name="cmb">Combobox que sera llenado con las Bodega</param>
    /// <param name="pIdPropietario">Filtro para llenar el combobox</param>
    /// <returns></returns>
    /// <remarks></remarks>
        public static bool Listar_BodegasPorPropietario(ref LookUpEdit cmb, int pIdPropietario, int idBodega = -1)
        {
            bool Listar_BodegasPorPropietarioRet = default;

            Listar_BodegasPorPropietarioRet = false;

            try
            {

                var lBod = new List<clsBeBodega>();

                lBod = clsLnBodega.Get_All_By_IdPropietario(pIdPropietario);

                if (lBod is not null)
                {
                    cmb.Properties.DisplayMember = "Nombre";
                    cmb.Properties.ValueMember = "IdBodega";
                    cmb.Properties.DataSource = lBod;
                    cmb.ItemIndex = 0;
                    Listar_BodegasPorPropietarioRet = true;
                }
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_BodegasPorPropietarioRet;

        }

        public static bool Listar_JornadasPorBodega(ref LookUpEdit Cmb, int pIdBodega)
        {
            bool Listar_JornadasPorBodegaRet = default;

            Listar_JornadasPorBodegaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnJornada_laboral.GetAllForCombo(Conversions.ToInteger(true));

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombre_jornada";
                    Cmb.Properties.ValueMember = "IdJornada";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_JornadasPorBodegaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_JornadasPorBodegaRet;

        }

        public static bool Listar_Turnos(ref LookUpEdit Cmb)
        {
            bool Listar_TurnosRet = default;

            Listar_TurnosRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTurno.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombre";
                    Cmb.Properties.ValueMember = "IdTurno";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_TurnosRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_TurnosRet;

        }

        public static bool Listar_Departamentos(ref LookUpEdit CmbDepto, int IdPais)
        {
            bool Listar_DepartamentosRet = default;

            Listar_DepartamentosRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnPais_departamento.GetAllForCombo(IdPais);

                if (DT.Rows.Count > 0)
                {
                    CmbDepto.Properties.DisplayMember = "Nombre";
                    CmbDepto.Properties.ValueMember = "IdDepartamento";
                    CmbDepto.Properties.DataSource = DT;
                    CmbDepto.ItemIndex = 0;
                }

                Listar_DepartamentosRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_DepartamentosRet;

        }

        public static bool Listar_Region(ref LookUpEdit CmbRegion, int IdPais)
        {
            bool Listar_RegionRet = default;

            Listar_RegionRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnPais_region.GetAllForCombo(IdPais);

                if (DT.Rows.Count > 0)
                {
                    CmbRegion.Properties.DisplayMember = "Nombre";
                    CmbRegion.Properties.ValueMember = "IdRegion";
                    CmbRegion.Properties.DataSource = DT;
                    CmbRegion.ItemIndex = 0;
                }

                Listar_RegionRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_RegionRet;

        }

        public static bool Listar_Municipios(ref LookUpEdit CmbMuni, int IdDepartamento)
        {
            bool Listar_MunicipiosRet = default;

            Listar_MunicipiosRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnPais_municipio.GetAllForCombo(IdDepartamento);

                if (DT.Rows.Count > 0)
                {
                    CmbMuni.Properties.DisplayMember = "Nombre";
                    CmbMuni.Properties.ValueMember = "IdMunicipio";
                    CmbMuni.Properties.DataSource = DT;
                    CmbMuni.ItemIndex = 0;
                }

                Listar_MunicipiosRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_MunicipiosRet;

        }

        public static bool Listar_MotivoUbicacion(ref System.Windows.Forms.ComboBox Cmb)
        {
            bool Listar_MotivoUbicacionRet = default;

            Listar_MotivoUbicacionRet = false;

            var DT = new List<clsBeMotivo_ubicacion>();

            try
            {

                DT = clsLnMotivo_ubicacion.GetAll(true);

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "Nombre";
                    Cmb.ValueMember = "IdMotivoUbicacion";
                    Cmb.DataSource = DT;
                }

                Listar_MotivoUbicacionRet = DT.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_MotivoUbicacionRet;

        }

        public static bool Listar_Reglas(ref System.Windows.Forms.ComboBox Cmb)
        {
            bool Listar_ReglasRet = default;

            Listar_ReglasRet = false;

            var DT = new List<clsBeReglas_recepcion>();

            try
            {

                DT = clsLnReglas_recepcion.GetAll(true);

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "Nombre";
                    Cmb.ValueMember = "IdReglaRecepcion";
                    Cmb.DataSource = DT;
                }

                Listar_ReglasRet = DT.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_ReglasRet;

        }

        public static bool Listar_Mensaje(ref System.Windows.Forms.ComboBox Cmb)
        {
            bool Listar_MensajeRet = default;

            Listar_MensajeRet = false;

            var DT = new List<clsBeMensaje_regla>();

            try
            {

                DT = clsLnMensaje_regla.GetAll(true);

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "Nombre";
                    Cmb.ValueMember = "IdMensajeRegla";
                    Cmb.DataSource = DT;
                }

                Listar_MensajeRet = DT.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_MensajeRet;

        }

        public static bool Listar_DestinatarioByPropietario(ref System.Windows.Forms.ComboBox Cmb, int pIdPropietario)
        {
            bool Listar_DestinatarioByPropietarioRet = default;

            Listar_DestinatarioByPropietarioRet = false;

            var DT = new List<clsBePropietario_destinatario>();

            try
            {

                DT = clsLnPropietario_destinatario.GetAllByIdPropietario(pIdPropietario);

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "Nombre";
                    Cmb.ValueMember = "IdDestinatarioPropietario";
                    Cmb.DataSource = DT;
                }

                Listar_DestinatarioByPropietarioRet = DT.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_DestinatarioByPropietarioRet;

        }

        public static void Cerrar_Ventana(Form frm)
        {

            try
            {

                if (XtraMessageBox.Show(string.Format("¿Salir de {0}?", frm.Text), "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    frm.Close();
                }
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static bool Confirma_Transaccion()
        {
            bool Confirma_TransaccionRet = default;

            if (XtraMessageBox.Show("¿Esta seguro de guardar el registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Confirma_TransaccionRet = true;
            }
            else
            {
                Confirma_TransaccionRet = false;
            }

            return Confirma_TransaccionRet;

        }

        public static bool Listar_VendedoresByRuta(ref LookUpEdit cmbVendedor, int IdRuta)
        {
            bool Listar_VendedoresByRutaRet = default;

            Listar_VendedoresByRutaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnRoad_p_vendedor.GetAllByRutaForCombo(IdRuta);

                if (DT is not null)
                {
                    cmbVendedor.Properties.DisplayMember = "nombre";
                    cmbVendedor.Properties.ValueMember = "IdRuta";
                    cmbVendedor.Properties.DataSource = DT;
                    cmbVendedor.ItemIndex = 0;
                }

                Listar_VendedoresByRutaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_VendedoresByRutaRet;

        }

        public static bool Listar_TiposPedido(ref LookUpEdit cmbTipoPedido)
        {
            bool Listar_TiposPedidoRet = default;

            Listar_TiposPedidoRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTrans_pe_tipo.Get_All_ForCombo();

                if (DT.Rows.Count > 0)
                {
                    cmbTipoPedido.Properties.DisplayMember = "Descripcion";
                    cmbTipoPedido.Properties.ValueMember = "IdTipoPedido";
                    cmbTipoPedido.Properties.DataSource = DT;
                    cmbTipoPedido.ItemIndex = 2;
                }

                Listar_TiposPedidoRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TiposPedidoRet;

        }

        public static int Get_IdPropietario_By_IdBodega(int pIdBodega, int pIdPropietarioBodega)
        {
            int Get_IdPropietario_By_IdBodegaRet = default;

            Get_IdPropietario_By_IdBodegaRet = 0;

            try
            {

                Get_IdPropietario_By_IdBodegaRet = clsLnPropietarios.Get_IdPropietario(pIdBodega, pIdPropietarioBodega);
            }

            catch (Exception ex)
            {
                string vMsgError = string.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message);
                clsLnLog_error_wms.Agregar_Error(vMsgError);
                throw new Exception(string.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
            }

            return Get_IdPropietario_By_IdBodegaRet;

        }

        public static bool Listar_Propietarios_By_IdBodega(ref LookUpEdit Cmb, int pIdBodega, bool ValueMemberIsIdPropietarioBodega = true)
        {
            bool Listar_Propietarios_By_IdBodegaRet = default;

            Listar_Propietarios_By_IdBodegaRet = false;

            var DT1 = new DataTable();

            try
            {

                if (ValueMemberIsIdPropietarioBodega)
                {

                    DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(pIdBodega);

                    Cmb.Properties.ValueMember = "IdPropietarioBodega";
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.DataSource = DT1;
                    Cmb.Properties.PopulateColumns();

                    if (Cmb.Properties.Columns.Count > 0)
                    {
                        Cmb.Properties.Columns[0].Visible = false;
                        Cmb.Properties.Columns[1].Visible = false;
                    }

                    Cmb.Properties.PopupWidth = 700;
                    Cmb.Properties.BestFit();
                    Cmb.Properties.NullText = "";

                    if (DT1.Rows.Count > 0)
                    {
                        Cmb.EditValue = Interaction.IIf(DT1.Rows[0]["IdPropietarioBodega"] is DBNull, 0, DT1.Rows[0]["IdPropietarioBodega"]);
                        Listar_Propietarios_By_IdBodegaRet = DT1.Rows.Count > 0;
                    }
                }

                else
                {

                    Cmb.Properties.ValueMember = "IdPropietario";
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.DataSource = DT1;
                    Cmb.Properties.PopulateColumns();
                    Cmb.Properties.Columns[0].Visible = false;
                    Cmb.Properties.Columns[1].Visible = false;
                    Cmb.Properties.PopupWidth = 700;
                    Cmb.Properties.BestFit();
                    Cmb.Properties.NullText = "";
                    Cmb.ItemIndex = 0;
                    Listar_Propietarios_By_IdBodegaRet = DT1.Rows.Count > 0;

                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_Propietarios_By_IdBodegaRet;

        }

        public static bool Listar_IndiceRotacion(ref LookUpEdit Cmb)
        {
            bool Listar_IndiceRotacionRet = default;

            Listar_IndiceRotacionRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnIndice_rotacion.GetAllForCombo(true);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdIndiceRotacion";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_IndiceRotacionRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_IndiceRotacionRet;

        }

        public static bool Listar_TipoInventario(ref LookUpEdit Cmb)
        {
            bool Listar_TipoInventarioRet = default;

            Listar_TipoInventarioRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTrans_inv_enc.GetAllForComboTipoInv();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Descripcion";
                    Cmb.Properties.ValueMember = "IdTipoInv";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_TipoInventarioRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TipoInventarioRet;

        }

        public static bool Listar_TipoConteo(ref LookUpEdit Cmb)
        {
            bool Listar_TipoConteoRet = default;

            Listar_TipoConteoRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTrans_inv_enc.GetAllForComboTipoConteo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Descripcion";
                    Cmb.Properties.ValueMember = "IdTipoConteo";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_TipoConteoRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TipoConteoRet;

        }

        public static bool Listar_TipoRotacion(ref LookUpEdit Cmb)
        {
            bool Listar_TipoRotacionRet = default;

            Listar_TipoRotacionRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnTipo_rotacion.GetAllForCombo(true);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdTipoRotacion";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_TipoRotacionRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_TipoRotacionRet;

        }

        public static bool Listar_FontTramo(ref LookUpEdit Cmb)
        {
            bool Listar_FontTramoRet = default;

            Listar_FontTramoRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnFont_enc.GetAllForCombo();

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdFont";
                    Cmb.Properties.DataSource = DT;
                }

                Listar_FontTramoRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_FontTramoRet;

        }

        public static bool Listar_MotivosAjuste(ref LookUpEdit Cmb)
        {
            bool Listar_MotivosAjusteRet = default;

            Listar_MotivosAjusteRet = false;

            var Dt = new DataTable();

            try
            {

                Dt = clsLnAjuste_motivo.GetAllForCombo();

                if (Dt.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "nombre";
                    Cmb.Properties.ValueMember = "idmotivoajuste";
                    Cmb.Properties.DataSource = Dt;
                }

                Listar_MotivosAjusteRet = Dt.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Listar_MotivosAjusteRet;

        }

        public static bool Listar_ClientesByEmpresaSistema(ref LookUpEdit Cmb, int pIdEmpresa)
        {
            bool Listar_ClientesByEmpresaSistemaRet = default;

            Listar_ClientesByEmpresaSistemaRet = false;

            var DT = new DataTable();

            try
            {

                DT = clsLnCliente.Get_All_By_IdEmpresa_For_Combo(pIdEmpresa, true);

                if (DT.Rows.Count > 0)
                {
                    Cmb.Properties.DisplayMember = "Nombre";
                    Cmb.Properties.ValueMember = "IdCliente";
                    Cmb.Properties.DataSource = DT;
                    Cmb.ItemIndex = 0;
                }

                Listar_ClientesByEmpresaSistemaRet = DT.Rows.Count > 0;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_ClientesByEmpresaSistemaRet;

        }

        #region Funciones interface

        public static bool Listar_BodegasPorEmpresa(ref System.Windows.Forms.ComboBox Cmb, int pIdEmpresa)
        {
            bool Listar_BodegasPorEmpresaRet = default;

            Listar_BodegasPorEmpresaRet = false;

            var DT = new List<clsBeBodega>();

            try
            {

                DT = clsLnBodega.Get_All_By_IdEmpresa(pIdEmpresa);

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "nombre";
                    Cmb.ValueMember = "idbodega";
                    Cmb.DataSource = DT;
                }

                Listar_BodegasPorEmpresaRet = DT.Count > 0;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Listar_BodegasPorEmpresaRet;

        }

        public static bool Listar_PropietariosByEmpresa(ref System.Windows.Forms.ComboBox Cmb, int pIdEmpresa)
        {

            var DT = new List<clsBePropietarios>();

            try
            {

                DT = clsLnPropietarios.Get_All_By_IdEmpresa_Class(pIdEmpresa);

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "nombre_comercial";
                    Cmb.ValueMember = "IdPropietario";
                    Cmb.DataSource = DT;
                }

                return DT.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static bool Listar_UsuariosSistemas(ref System.Windows.Forms.ComboBox Cmb, int pIdEmpresa)
        {

            var DT = new List<clsBeUsuario>();

            try
            {

                DT = clsLnUsuario.GetAllUsuariosSistema(pIdEmpresa);

                if (DT.Count > 0)
                {
                    Cmb.DisplayMember = "nombres";
                    Cmb.ValueMember = "IdUsuario";
                    Cmb.DataSource = DT;
                }

                return DT.Count > 0;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion

    }
}