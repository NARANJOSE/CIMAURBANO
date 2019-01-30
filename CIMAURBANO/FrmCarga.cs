using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;

namespace CIMAURBANO
{
    public partial class FrmCarga : Form
    {

        Funciones fun = new Funciones();

        Conexion con = new Conexion();
        private string rutaFile = "";
        private Parametros[] parametros;
        public FrmCarga()
        {
            InitializeComponent();

            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 5000;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.picBox, "Seleccionar Archivo");
            toolTip.SetToolTip(this.btnSubir, "Subir Archivo");
            toolTip.SetToolTip(this.btnCargar, "Iniciar carga de registros a la BD");

            con.EjecutarSP_SQL("SELECT name AS TABLAS FROM sysobjects WHERE TYPE='U'", null, "dtTablas", "TABLAS");
            cboTablas.DataSource = con.tabla;
            con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);
            cboTablas.DisplayMember = "TABLAS";
            cboTablas.ValueMember = "TABLAS";
            cboTablas.SelectedIndex = 0;

            cargarGrid();
        }

        private void picBox_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(fun.cUsuarioActual);
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Archivos Excel |*.xlsx;*.xlsx;*.xls";
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtRuta.Text = openFile.FileName;
                rutaFile = openFile.FileName;
                //MessageBox.Show(rutaFile);
            }
            else
            {
                txtRuta.Text = "";
                dataGridV.DataSource = null;
            }
        }

        private void cargarGrid()
        {
            DataTable dtRegistros = new DataTable();
            con.EjecutarSP_SQL("SELECT H.*,(SELECT NOMBREUSUARIO FROM ORUSUARIO U WHERE U.IDORUSUARIO=H.IDUSUARIO ) USUARIO FROM GEHISTORIALCARGA H", null, "dtRegistros", "USUARIO");
            dtRegistros = con.tabla;


            //'Si no pongo esta línea, se crean automáticamente los campos del grid dependiendo de los campos del DataTable
            DataGridCarga.AutoGenerateColumns = false;


            DataGridCarga.DataSource = dtRegistros;
            //'Aquí le indico cuales campos del Select van con las columnas de mi DataGrid

            DataGridCarga.Columns["Column1"].DataPropertyName = "IDHISTORIALC";
            DataGridCarga.Columns["Column2"].DataPropertyName = "DESCRIPCION";
            DataGridCarga.Columns["Column3"].DataPropertyName = "ARCHIVO";
            DataGridCarga.Columns["Column4"].DataPropertyName = "TIPOARCHIVO";
            DataGridCarga.Columns["Column5"].DataPropertyName = "FCARGA";
            DataGridCarga.Columns["Column6"].DataPropertyName = "REGISTROSC";
            DataGridCarga.Columns["Column7"].DataPropertyName = "REGISTROSR";
            DataGridCarga.Columns["Column8"].DataPropertyName = "REGISTROST";
            DataGridCarga.Columns["Column9"].DataPropertyName = "USUARIO";
            DataGridCarga.Columns["Column10"].DataPropertyName = "ESTADO";
            DataGridCarga.Columns["Column11"].DataPropertyName = "";

        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            if (rutaFile == "")
            {
                MessageBox.Show("Debe seleccionar un archivo...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                picBox.Focus();
            }
            else
            {
                if (cboOrigen.Text.Trim() == "")
                {
                    MessageBox.Show("Debe seleccionar el origen del archivo...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboOrigen.Focus();
                    return;
                }
                DateTime Hoy = DateTime.Now;
                string dActual = Hoy.ToString("yyyy-MM-dd H:mm:ss");

                //*********************************
                string conexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaFile + ";Extended Properties=Excel 12.0;";

                OleDbConnection origen = default(OleDbConnection);
                origen = new OleDbConnection(conexion);

                OleDbCommand seleccion = default(OleDbCommand);
                seleccion = new OleDbCommand("SELECT * FROM [Hoja1$]", origen);

                OleDbDataAdapter adaptador = new OleDbDataAdapter();
                adaptador.SelectCommand = seleccion;
                dataGridV.DataSource = con.RetornarTabla(adaptador, "Registros");

                origen.Close();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Debe colocar una descripción para la operación de carga", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcion.Focus();
                return;
            }
            if (dataGridV.Rows.Count <= 0)
            {
                MessageBox.Show("La rejilla no contiene datos para cargar. Revise...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cboTablas.Text.Trim() == "")
            {
                MessageBox.Show("Debe seleccionar la tabla donde se cargarán los datos...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboTablas.Focus();
                return;
            }
            else
            {
                int nRegA = 0;
                int nRegR = 0;

                string cArchivo = rutaFile.Substring(rutaFile.LastIndexOf("\\") + 1);

                DateTime Hoy = DateTime.Now;
                string dActual = Hoy.ToString("yyyy-MM-dd H:mm:ss");

                parametros = new Parametros[11];

                parametros[0] = new Parametros("IDHISTORIALC", "0");
                parametros[1] = new Parametros("DESCRIPCION", txtDescripcion.Text.Trim());
                parametros[2] = new Parametros("ARCHIVO", cArchivo);
                parametros[3] = new Parametros("TIPOARCHIVO", rutaFile.Substring(rutaFile.LastIndexOf(".") + 1));
                parametros[4] = new Parametros("FCARGA", dActual);
                parametros[5] = new Parametros("REGISTROSC", "0");
                parametros[6] = new Parametros("REGISTROSR", "0");
                parametros[7] = new Parametros("REGISTROST", "0");
                parametros[8] = new Parametros("IDUSUARIO", Program.globalIdUsuario);
                parametros[9] = new Parametros("ESTADO", "1");
                parametros[10] = new Parametros("ID", "0");

                int nIdHistorial;
                string cIdHistorial;

                DataTable dtHistorial = new DataTable();

                nIdHistorial = con.EjecutarSP_RUD("SPINSERTUPDATEGEHISTORIALCARGA", parametros, "", "GEHISTORIALCARGA");
                dtHistorial = con.tabla;

                DataTable dtUltimoId = new DataTable();
                con.ConsultaSQL("SELECT MAX(IDHISTORIALC) as ID FROM  GEHISTORIALCARGA ", "dtUltimoId");
                dtUltimoId = con.tabla;
                cIdHistorial = dtUltimoId.Rows[0][0].ToString().Trim();
                cargarGrid();

                DateTime fecha; string dFecha;

                switch (cboTablas.Text.Trim())
                {

                    case "DBIMPORTADA":
                        int sumador = 0;
                        //Form500a.ShowDialog(this);
                        if (cboOrigen.Text.Trim() == "SOLOVERDE" || cboOrigen.Text.Trim() == "ECOVERDE")
                        {
                            parametros = new Parametros[88];
                            foreach (DataGridViewRow row in dataGridV.Rows)
                            {

                                //CONSIDERAR QUE LAS FECHAS CUANDO VIENEN DEL FORMATO ORIGINAL, SE CARGAN MAL EN EL GRID
                                sumador = 0;
                                //MessageBox.Show(row.Cells["sdo_ini"].Value.ToString());
                                parametros[sumador] = new Parametros("@IDHISTORIALC", cIdHistorial);
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@EMPRESA", cboOrigen.Text.Trim());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@SDO_INI_A", cboOrigen.Text.Trim());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@SDO_INI_B", "0");
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@SDO_INI2", "0");
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@SDO_INI1", "0");
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@NUMREG", string.IsNullOrEmpty(row.Cells["NUMREG"].Value.ToString()) ? "0" : row.Cells["NUMREG"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@PGNUMRECOR", string.IsNullOrEmpty(row.Cells["PGNUMRECOR"].Value.ToString()) ? "0" : row.Cells["PGNUMRECOR"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@PGITEM", string.IsNullOrEmpty(row.Cells["PGITEM"].Value.ToString()) ? "0" : row.Cells["PGITEM"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@TDOCNETEO", string.IsNullOrEmpty(row.Cells["TDOCNETEO"].Value.ToString()) ? "0" : row.Cells["TDOCNETEO"].Value.ToString());

                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DOC_COD", row.Cells["DOC_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DOC_NOM", row.Cells["DOC_NOM"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@NUMFACT", string.IsNullOrEmpty(row.Cells["NUMFACT"].Value.ToString()) ? "0" : row.Cells["NUMFACT"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@NRUTFACT", string.IsNullOrEmpty(row.Cells["NRUTFACT"].Value.ToString()) ? "0" : row.Cells["NRUTFACT"].Value.ToString());


                                if (DateTime.TryParse(row.Cells["FECHA"].Value.ToString(), out fecha))
                                {
                                    dFecha = Convert.ToDateTime(row.Cells["FECHA"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss");
                                }
                                else
                                {
                                    dFecha = "1900-01-01 00:00:00.000";
                                }
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@FECHA", dFecha);


                                if (DateTime.TryParse(row.Cells["VENCIMIE"].Value.ToString(), out fecha))
                                {
                                    dFecha = Convert.ToDateTime(row.Cells["VENCIMIE"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss");
                                }
                                else
                                {
                                    dFecha = "1900-01-01 00:00:00.000";
                                }
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@VENCIMIE", dFecha);


                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DEBE", string.IsNullOrEmpty(row.Cells["DEBE"].Value.ToString()) ? "0" : row.Cells["DEBE"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@HABER", string.IsNullOrEmpty(row.Cells["HABER"].Value.ToString()) ? "0" : row.Cells["HABER"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DOC_SDO", string.IsNullOrEmpty(row.Cells["DOC_SDO"].Value.ToString()) ? "0" : row.Cells["DOC_SDO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DEBE2", string.IsNullOrEmpty(row.Cells["DEBE2"].Value.ToString()) ? "0" : row.Cells["DEBE2"].Value.ToString());

                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@HABER2", string.IsNullOrEmpty(row.Cells["HABER2"].Value.ToString()) ? "0" : row.Cells["HABER2"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DEBE1", string.IsNullOrEmpty(row.Cells["DEBE1"].Value.ToString()) ? "0" : row.Cells["DEBE1"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@HABER1", string.IsNullOrEmpty(row.Cells["HABER1"].Value.ToString()) ? "0" : row.Cells["HABER1"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@MONYEXT", row.Cells["MONYEXT"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@TOTAL", string.IsNullOrEmpty(row.Cells["TOTAL"].Value.ToString()) ? "0" : row.Cells["TOTAL"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@TOTNETO", string.IsNullOrEmpty(row.Cells["TOTNETO"].Value.ToString()) ? "0" : row.Cells["TOTNETO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@AFECTO", string.IsNullOrEmpty(row.Cells["AFECTO"].Value.ToString()) ? "0" : row.Cells["AFECTO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@EXENTO", string.IsNullOrEmpty(row.Cells["EXENTO"].Value.ToString()) ? "0" : row.Cells["EXENTO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@TOTIVA", string.IsNullOrEmpty(row.Cells["TOTIVA"].Value.ToString()) ? "0" : row.Cells["TOTIVA"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@IVA29", string.IsNullOrEmpty(row.Cells["IVA29"].Value.ToString()) ? "0" : row.Cells["IVA29"].Value.ToString());

                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@IMP1", string.IsNullOrEmpty(row.Cells["IMP1"].Value.ToString()) ? "0" : row.Cells["IMP1"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@IMP2", string.IsNullOrEmpty(row.Cells["IMP2"].Value.ToString()) ? "0" : row.Cells["IMP2"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DCTOPJE", string.IsNullOrEmpty(row.Cells["DCTOPJE"].Value.ToString()) ? "0" : row.Cells["DCTOPJE"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DESCTO2", string.IsNullOrEmpty(row.Cells["DESCTO2"].Value.ToString()) ? "0" : row.Cells["DESCTO2"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DESCTO3", string.IsNullOrEmpty(row.Cells["DESCTO3"].Value.ToString()) ? "0" : row.Cells["DESCTO3"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DESCTO4", string.IsNullOrEmpty(row.Cells["DESCTO4"].Value.ToString()) ? "0" : row.Cells["DESCTO4"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@GASTO", string.IsNullOrEmpty(row.Cells["GASTO"].Value.ToString()) ? "0" : row.Cells["GASTO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CARGO2", string.IsNullOrEmpty(row.Cells["CARGO2"].Value.ToString()) ? "0" : row.Cells["CARGO2"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CARGO3", string.IsNullOrEmpty(row.Cells["CARGO3"].Value.ToString()) ? "0" : row.Cells["CARGO3"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CARGO4", string.IsNullOrEmpty(row.Cells["CARGO4"].Value.ToString()) ? "0" : row.Cells["CARGO4"].Value.ToString());

                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@NUMCORR", string.IsNullOrEmpty(row.Cells["NUMCORR"].Value.ToString()) ? "0" : row.Cells["NUMCORR"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CTA", string.IsNullOrEmpty(row.Cells["CTA"].Value.ToString()) ? "0" : row.Cells["CTA"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CC", string.IsNullOrEmpty(row.Cells["CC"].Value.ToString()) ? "0" : row.Cells["CC"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@GTO", string.IsNullOrEmpty(row.Cells["GTO"].Value.ToString()) ? "0" : row.Cells["GTO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CODVEND", string.IsNullOrEmpty(row.Cells["CODVEND"].Value.ToString()) ? "0" : row.Cells["CODVEND"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@GL_COM", row.Cells["GLOSACON"].Value.ToString().ToUpper());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@ATT", row.Cells["ATT"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CARTOLA", string.IsNullOrEmpty(row.Cells["CARTOLA"].Value.ToString()) ? "0" : row.Cells["CARTOLA"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CARTOANO", string.IsNullOrEmpty(row.Cells["CARTOAÑO"].Value.ToString()) ? "0" : row.Cells["CARTOAÑO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DEBITO", string.IsNullOrEmpty(row.Cells["DEBITO"].Value.ToString()) ? "0" : row.Cells["DEBITO"].Value.ToString());

                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@RAZSOC", row.Cells["RAZSOC"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@RAZSOC2", row.Cells["RAZSOC2"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@RUT", row.Cells["RUT"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@DIR", row.Cells["DIR"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@FONO", row.Cells["FONO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@FAX", row.Cells["FAX"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CLASE1", row.Cells["CLASE1"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CLASE2", row.Cells["CLASE2"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CLASE3", row.Cells["CLASE3"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CLASE4", row.Cells["CLASE4"].Value.ToString());

                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CTA_COD", row.Cells["CTA_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CTA_NOM", row.Cells["CTA_NOM"].Value.ToString().ToUpper());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CTA_NOM2", row.Cells["CTA_NOM2"].Value.ToString().ToUpper());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CC_COD", row.Cells["CC_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CC_NOM", row.Cells["CC_NOM"].Value.ToString().ToUpper());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@GTO_COD", row.Cells["GTO_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@GTO_NOM", row.Cells["GTO_NOM"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@PERS_COD", row.Cells["PERS_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@PERS_NOM", row.Cells["PERS_NOM"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@PERS_APELL", row.Cells["PERS_APELL"].Value.ToString());

                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@PERS_CARGO", row.Cells["PERS_CARGO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@PERS_GRUPO", row.Cells["PERS_GRUPO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COBRAD_COD", row.Cells["COBRAD_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COTIPO", row.Cells["COTIPO"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CONAME", row.Cells["CONAME"].Value.ToString().ToUpper());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@CONUM", row.Cells["CONUM"].Value.ToString());


                                if (DateTime.TryParse(row.Cells["COFECHA"].Value.ToString(), out fecha))
                                {
                                    dFecha = Convert.ToDateTime(row.Cells["COFECHA"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss");
                                }
                                else
                                {
                                    dFecha = "1900-01-01 00:00:00.000";
                                }
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COFECHA", dFecha);


                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COGLOSA", row.Cells["COGLOSA"].Value.ToString().ToUpper());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@TIPODOC", string.IsNullOrEmpty(row.Cells["TIPODOC"].Value.ToString()) ? "0" : row.Cells["TIPODOC"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@SUCUR", string.IsNullOrEmpty(row.Cells["SUCUR"].Value.ToString()) ? "0" : row.Cells["SUCUR"].Value.ToString());

                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@SUC_COD", string.IsNullOrEmpty(row.Cells["SUC_COD"].Value.ToString()) ? "0" : row.Cells["SUC_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@SUC_NOM", row.Cells["SUC_NOM"].Value.ToString().ToUpper());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COMPCLS1_COD", row.Cells["COMPCLS1_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COMPCLS1_DESCRIP", row.Cells["COMPCLS1_DESCRIP"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COMPCLS2_COD", row.Cells["COMPCLS2_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COMPCLS2_DESCRIP", row.Cells["COMPCLS2_DESCRIP"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COMPCLS3_COD", row.Cells["COMPCLS3_COD"].Value.ToString());
                                sumador = sumador + 1;
                                parametros[sumador] = new Parametros("@COMPCLS3_DESCRIP", row.Cells["COMPCLS3_DESCRIP"].Value.ToString());

                                bool ejecutado;
                                if (row.Cells["CTA_COD"].Value.ToString().Trim() != "")
                                {
                                    //con.actualizarIdUsuario("DBIMPORTADA");
                                    ejecutado = con.EjecutarSP_SQL("SPINSERTDBIMPORTADA", parametros, "","GESUJETO");
                                    if (ejecutado)
                                    {
                                        nRegA = nRegA + 1;
                                    }
                                    else
                                    {
                                        //Form500a.Close();
                                        MessageBox.Show("Se rechazó" + row.Cells["numreg"].Value.ToString());
                                        nRegR = nRegR + 1;
                                    }
                                }

                            };
                        }
                        if (cboOrigen.Text.Trim() == "NUBOX")
                        {
                            
                        }
                        ////SE LLENA LA TABLA PRELIMINAR
                        //parametros = new Parametros[1];
                        //parametros[0] = new Parametros("IDHISTORIALC", cIdHistorial);

                        //con.EjecutarSP_SQL("SPCARGARPRELIMINAR", parametros, "", "GESUJETO");

                        break;

                    case "GESUJETO":
                        parametros = new Parametros[25];
                        foreach (DataGridViewRow row in dataGridV.Rows)
                        {
                            string cIdPais;
                            string cIdSujeto;
                            string cIdEntidad;
                            string cIdProfesion;
                            string cIdCiudad;
                            string cIdRegion;

                            con.ConsultaSimple("ORPAIS", "NOMBRE", "'CHILE'", "dtRegistros");
                            cIdPais = con.tabla.Rows[0]["IDORPAIS"].ToString().Trim();

                            //con.ConsultaSimple("GETPSUJETO", "NOMBRE", "CHILE", "dtRegistros");
                            cIdSujeto = "2";
                            cIdEntidad = "4";
                            cIdProfesion = "1";

                            con.ConsultaSimple("ORCIUDAD", "NOMBRE", "'SANTIAGO'", "dtRegistros");
                            cIdCiudad = con.tabla.Rows[0]["IDORCIUDAD"].ToString().Trim();

                            con.ConsultaSimple("ORREGION", "NOMBRE", "'REGION METROPOLITANA'", "dtRegistros");
                            cIdRegion = con.tabla.Rows[0]["IDORREGION"].ToString().Trim();

                            string cNombreSujeto = row.Cells["NOMBRE"].Value.ToString();
                            string[] aNombres = cNombreSujeto.Split(' ');
                            int nLon = aNombres.Length;

                            string cNombre = "";
                            if (aNombres.Length == 4)
                            {
                                cNombre = aNombres[2].Trim() + ' ' + aNombres[3].Trim();
                            }
                            if (aNombres.Length == 3)
                            {
                                cNombre = aNombres[2].Trim();
                            }

                            parametros[0] = new Parametros("@IDORPAIS", cIdPais);                                               //
                            parametros[1] = new Parametros("@IDGETPSUJETO", cIdSujeto);
                            parametros[2] = new Parametros("@IDGEENTIDAD", cIdEntidad);
                            parametros[3] = new Parametros("@IDGEPROFESION", cIdProfesion);
                            parametros[4] = new Parametros("@IDORCIUDAD", cIdCiudad);
                            parametros[5] = new Parametros("@IDORREGION", cIdRegion);
                            parametros[6] = new Parametros("@NOMBRE", cNombre);              //
                            parametros[7] = new Parametros("@APELLIDOMATERNO", (aNombres.Length == 2) ? aNombres[1].Trim() : "");
                            parametros[8] = new Parametros("@APELLIDOPATERNO", (aNombres.Length == 1) ? aNombres[0].Trim() : "");
                            parametros[9] = new Parametros("@RUT", row.Cells["RUT"].Value.ToString());                          //
                            parametros[10] = new Parametros("@DIRECCION", row.Cells["DIRECCION"].Value.ToString());
                            parametros[11] = new Parametros("@MAEDOCIVIL", row.Cells["ESTADO CIVIL"].Value.ToString());
                            //FECHA NACIM NO VIENE CON PUNTO (.) SINO CON (#) SEGÚN APARECE EN  ROW/CELLS/LIST/SE TOMA UNA Y CLIC EN ACCESSIBILITYOBJECT (PROPIEDAD NAME)
                            if (DateTime.TryParse(row.Cells["FECHA NACIM#"].Value.ToString(), out fecha))
                            {
                                dFecha = Convert.ToDateTime(row.Cells["FECHA NACIM#"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss");
                            }
                            else
                            {
                                dFecha = "1900-01-01 00:00:00.000";
                            }
                            parametros[12] = new Parametros("@FENACIMIENTO", dFecha);

                            parametros[13] = new Parametros("@CORREOELECPPAL", row.Cells["EMAIL"].Value.ToString());
                            parametros[14] = new Parametros("@CORREOELECSEC", row.Cells["EMAIL"].Value.ToString());
                            parametros[15] = new Parametros("@DIRREFDIGITAL", "ABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJ");     // ABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJ
                            parametros[16] = new Parametros("@MATPIDENTIFICACION", row.Cells["EXPATRIADO?"].Value.ToString());
                            parametros[17] = new Parametros("@NUIDENTIFICACION", row.Cells["CUENTA BANCO"].Value.ToString());
                            parametros[18] = new Parametros("@TELEFONOMOVIL", row.Cells["TELEFONO"].Value.ToString());
                            parametros[19] = new Parametros("@TELEFONOAUX1", row.Cells["TELEFONO"].Value.ToString());
                            parametros[20] = new Parametros("@TELEFONOAUX2", row.Cells["TELEFONO"].Value.ToString());

                            if (DateTime.TryParse(row.Cells["FECHA INICIO CONTRATO"].Value.ToString(), out fecha))
                            {
                                dFecha = Convert.ToDateTime(row.Cells["FECHA INICIO CONTRATO"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss");
                            }
                            else
                            {
                                dFecha = "1900-01-01 00:00:00.000";
                            }
                            parametros[21] = new Parametros("@FEREGISTRO", dFecha);             //


                            if (DateTime.TryParse(row.Cells["FECHA TERMINO CONTRATO"].Value.ToString(), out fecha))
                            {
                                dFecha = Convert.ToDateTime(row.Cells["FECHA TERMINO CONTRATO"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss");
                            }
                            else
                            {
                                dFecha = "1900-01-01 00:00:00.000";
                            }
                            parametros[22] = new Parametros("@FEDESACTIVACION", dFecha);                            
                            parametros[23] = new Parametros("@OBSERVACION", "ABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJ");
                            parametros[24] = new Parametros("@MASEXO", row.Cells["SEXO"].Value.ToString());


                            //parametros[0] = new Parametros("@IDORPAIS", cIdPais);                                               //
                            //parametros[1] = new Parametros("@IDGETPSUJETO", cIdSujeto); 
                            //parametros[2] = new Parametros("@IDGEENTIDAD", cIdEntidad);
                            //parametros[3] = new Parametros("@IDGEPROFESION", cIdProfesion);
                            //parametros[4] = new Parametros("@IDORCIUDAD", cIdCiudad);
                            //parametros[5] = new Parametros("@IDORREGION", cIdRegion);
                            //parametros[6] = new Parametros("@NOMBRE", cNombre);              //
                            //parametros[7] = new Parametros("@APELLIDOMATERNO", (aNombres.Length == 2)? aNombres[1].Trim():"");
                            //parametros[8] = new Parametros("@APELLIDOPATERNO", (aNombres.Length == 1)? aNombres[0].Trim():"");
                            //parametros[9] = new Parametros("@RUT", row.Cells["RUT"].Value.ToString());                          //
                            //parametros[10] = new Parametros("@DIRECCION", row.Cells["DIRECCION"].Value.ToString());
                            //parametros[11] = new Parametros("@MAEDOCIVIL", row.Cells["ESTADO CIVIL"].Value.ToString());
                            ////FECHA NACIM NO VIENE CON PUNTO (.) SINO CON (#) SEGÚN APARECE EN  ROW/CELLS/LIST/SE TOMA UNA Y CLIC EN ACCESSIBILITYOBJECT (PROPIEDAD NAME)
                            //parametros[12] = new Parametros("@FENACIMIENTO", Convert.ToDateTime(row.Cells["FECHA NACIM#"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss"));
                            //parametros[13] = new Parametros("@CORREOELECPPAL",row.Cells["EMAIL"].Value.ToString());
                            //parametros[14] = new Parametros("@CORREOELECSEC", row.Cells["EMAIL"].Value.ToString());
                            //parametros[15] = new Parametros("@DIRREFDIGITAL", row.Cells["FORMA PAGO"].Value.ToString());     // ABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJ
                            //parametros[16] = new Parametros("@MATPIDENTIFICACION", row.Cells["EXPATRIADO?"].Value.ToString());
                            //parametros[17] = new Parametros("@NUIDENTIFICACION", row.Cells["CUENTA BANCO"].Value.ToString());
                            //parametros[18] = new Parametros("@TELEFONOMOVIL", row.Cells["TELEFONO"].Value.ToString());
                            //parametros[19] = new Parametros("@TELEFONOAUX1", row.Cells["TELEFONO"].Value.ToString());
                            //parametros[20] = new Parametros("@TELEFONOAUX2", row.Cells["TELEFONO"].Value.ToString());
                            //parametros[21] = new Parametros("@FEREGISTRO", Convert.ToDateTime(row.Cells["FECHA INICIO CONTRATO"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss"));             //
                            ////parametros[22] = new Parametros("@FEDESACTIVACION", Convert.ToDateTime(row.Cells["FECHA TERMINO CONTRATO"].Value.ToString()).ToString("yyyy-MM-dd H:mm:ss"));
                            //parametros[22] = new Parametros("@FEDESACTIVACION", "NULL");
                            //parametros[23] = new Parametros("@OBSERVACION", row.Cells["CENTRO CAOSTO"].Value.ToString());
                            //parametros[24] = new Parametros("@MASEXO", row.Cells["SEXO"].Value.ToString());

                            bool ejecutado;
                            ejecutado = con.EjecutarSP_SQL("spInsertEmpleado", parametros, "", "ORUSUARIO");

                            
                            if (ejecutado)
                            {
                                nRegA = nRegA + 1;
                            }
                            else
                            {
                                MessageBox.Show(row.Cells["RUT"].Value.ToString());
                                nRegR = nRegR + 1;
                            }

                        };
                        break;
                }

                cargarGrid();

                if ((nRegA + nRegR) > 0)
                {
                    //ACTUALIZAMOS EL REGISTRO DE HISTORIAL
                    parametros = new Parametros[11];

                    parametros[0] = new Parametros("IDHISTORIALC", cIdHistorial);
                    parametros[1] = new Parametros("DESCRIPCION", "");
                    parametros[2] = new Parametros("ARCHIVO", "");
                    parametros[3] = new Parametros("TIPOARCHIVO", "");
                    parametros[4] = new Parametros("FCARGA", dActual);
                    parametros[5] = new Parametros("REGISTROSC", nRegA.ToString());
                    parametros[6] = new Parametros("REGISTROSR", nRegR.ToString());
                    parametros[7] = new Parametros("REGISTROST", (nRegA + nRegR).ToString());
                    parametros[8] = new Parametros("IDUSUARIO", Program.globalIdUsuario);
                    parametros[9] = new Parametros("ESTADO", "1");
                    parametros[10] = new Parametros("ID", "0");
                    nIdHistorial = con.EjecutarSP_RUD("SPINSERTUPDATEGEHISTORIALCARGA", parametros, "", "GEHISTORIALCARGA");

                    cargarGrid();

                    MessageBox.Show("Los datos se han almacenado correctamente en la BD.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("No se registraron inserciones del archivo Excel. \n¿Desea eliminar del historial esta carga?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Eliminar("GEHISTORIALCARGA", "IDHISTORIALC = " + cIdHistorial);
                        //con.Eliminar("DBIMPORTADA", "IDHISTORIALC = " + cIdHistorial);
                        cargarGrid();
                        MessageBox.Show("El registro de historial se ha elimnado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                dataGridV.DataSource = null;
                txtRuta.Text = "";
                cboOrigen.Text = "";
                txtDescripcion.Text = "";
                cboTablas.Text = "";

            }
        }



    }
}
