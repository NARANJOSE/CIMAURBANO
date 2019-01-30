using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//CIMAURBANO
//NOMBRE DEL PROYECTO: Registro del Empleado y control de asistencia
//NOMBRE DEL MODULO: Módulo para 
//FECHA DE ORIGEN DESARROLLO: Enero 2019
//VERSION: v1.0
//AUTOR: José Naranjo
//OBSERVACION: Mantenimiento para carga de datos básicos. Se carga la tabla GEDIVXCLASIF y las relacionadas a ellas, como: GETPSUJETO, GETPENTIDAD, GECLASEDOCUMENTO, ENTRE OTROS

namespace CIMAURBANO
{
    public partial class FrmDivxClasif1 : Form
    {
        private int AlturaBase = 440;

        //Define qué ancho será visible el Scroll
        private int AnchoVisibleScroll = 400;

        //Define el ancho Mayor
        private int AnchoMayor = 677;

        //Define el ancho Medio
        private int AnchoMedio = 600;

        Funciones fun = new Funciones();
        Conexion con = new Conexion();
        private Parametros[] parametros;

        private void FrmDivxClasif1_Resize(object sender, EventArgs e)
        {
            //Colocar en los paneles invisibles margin= 0; 0; 0; 0

            //if (panel3.Visible != true){ panel3.Margin = new Padding(0, 0, 0, 0); panel3.Width = 0; panel3.Height = 0;}

            int altura = 0;
            int contPanel = 0;

            //Panel panel1Copy = this.Controls.Find("panel1", true).FirstOrDefault() as Panel;
            //if (panel1Copy != null)    contPanel++;

            if (panel1.Visible)
                contPanel++;

            if (panel2.Visible)
                contPanel++;

            if (panel3.Visible)
                contPanel++;

            if (flowLPScroll.Width >= AnchoMayor)
            {
                if (panel1.Visible && !panel2.Visible && !panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 6) / contPanel;
                    panel2.Width = (flowLPScroll.Width - 6) / contPanel;
                    panel3.Width = (flowLPScroll.Width - 6) / contPanel;
                }

                if (panel1.Visible && panel2.Visible && !panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 8) / contPanel;
                    panel2.Width = (flowLPScroll.Width - 8) / contPanel;
                    panel3.Width = (flowLPScroll.Width - 8) / contPanel;
                }

                if (panel1.Visible && panel2.Visible && panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 12) / contPanel;
                    panel2.Width = (flowLPScroll.Width - 12) / contPanel;
                    panel3.Width = (flowLPScroll.Width - 12) / contPanel;
                }
            }

            if (flowLPScroll.Width >= AnchoMedio && flowLPScroll.Width < AnchoMayor)
            {
                //if ((panel1.Visible && panel2.Visible && !panel3.Visible) || (panel1.Visible && panel2.Visible && panel3.Visible))
                //{
                    panel1.Width = (flowLPScroll.Width - 9) / 2;
                    panel2.Width = (flowLPScroll.Width - 9) / 2;
                    panel3.Width = (flowLPScroll.Width - 6);
                //}
            }

            if (flowLPScroll.Width < AnchoMedio)
            {
                //if ((panel1.Visible && panel2.Visible && !panel3.Visible) || (panel1.Visible && panel2.Visible && panel3.Visible))
                //{
                    panel1.Width = (flowLPScroll.Width - 6);
                    panel2.Width = (flowLPScroll.Width - 6);
                    panel3.Width = (flowLPScroll.Width - 6);
                //}
               
            }


            //Se ajusta el ancho del panel central           
            if (panel4.Visible)
                panel4.Width = (flowLPScroll.Width - 6);


            //Se valida si existe uno de los 3 panes de arriba para sumar las alturas y ajustar la altura del panel 5
            if (panel1.Visible)
                altura = panel1.Height;

            if (panel2.Visible)
                altura = panel2.Height;

            if (panel3.Visible)
                altura = panel3.Height;

            if (panel4.Visible)
                altura = altura + panel4.Height;

            //Se ajusta el ancho de contenedor 5
            if (panel5.Visible)
                panel5.Width = (flowLPScroll.Width - 6);

            if (flowLPScroll.Height >= AlturaBase)
            {
                panel5.Height = flowLPScroll.Height - altura - 12;
            }

            if (flowLPScroll.Width < AnchoVisibleScroll)
            {
                flowLPScroll.AutoScroll = true;
            }
            else
            {
                flowLPScroll.AutoScroll = false;
            }
        }

        private string cTablaCaso = "";
        private string cCampoIdTablaCaso = "";
        private string idActual = "";
        private string valorIdTablaCaso = "";

        public FrmDivxClasif1(string pcTablaCaso,string pcCampoIdTablaCaso)
        {
            InitializeComponent();

            this.cTablaCaso = pcTablaCaso;
            this.cCampoIdTablaCaso = pcCampoIdTablaCaso;

            //flowLPScroll.BackColor = Color.White;
            //panel1.BackColor = Color.White;
            //panel2.BackColor = Color.White;
            //panel3.BackColor = Color.White;
            //panel4.BackColor = Color.White;
            //panel5.BackColor = Color.White;

            switch (this.cTablaCaso)
            {
                case "GETPSUJETO":
                    lblTipo.Text = "Tipo Sujeto";
                    //cboTipo.Items.Add("EMPLEADO");
                    //cboTipo.Items.Add("CLIENTE");
                    //cboTipo.Items.Add("PROVEEDOR");
                    //cboTipo.Items.Add("UNIVERSIDAD");
                    //cboTipo.Items.Add("ENTIDAD PUBLICA");
                    //cboTipo.Items.Add("ENTIDAD PRIVADA");
                    break;
                case "GETPENTIDAD":
                    lblTipo.Text = "Tipo Entidad";
                    //cboTipo.Items.Add("FINANCIERO");
                    //cboTipo.Items.Add("ADMINISTRATIVO");
                    //cboTipo.Items.Add("OPERATIVO");
                    //cboTipo.Items.Add("PERSONA");
                    break;
                case "GEAREADEPART":
                    lblTipo.Text = "Area Dptal";
                    DataGridMain.Columns[1].HeaderText = "Area";
                    //cboTipo.Items.Add("LEGAL");
                    //cboTipo.Items.Add("ADMINISTRATIVA");
                    //cboTipo.Items.Add("COMERCIAL");
                    break;
                case "GECLASEDOCUMENTO":
                    lblTipo.Text = "Tipo Documento";
                    //cboTipo.Items.Add("RECIBO PAGO");
                    break;
                case "NOTPFAMILIAR":
                    lblTipo.Text = "Tipo Familiar";
                    //cboTipo.Items.Add("PADRE");
                    //cboTipo.Items.Add("MADRE");
                    //cboTipo.Items.Add("CONYUGUE");
                    //cboTipo.Items.Add("HIJOS");
                    break;
                case "NOTPINDICADOR":
                    lblTipo.Text = "Tipo Indicador";
                    //cboTipo.Items.Add("NIVEL DE ESTUDIO");
                    //cboTipo.Items.Add("NACION");
                    //cboTipo.Items.Add("TIPO DE LICENCIA");
                    //cboTipo.Items.Add("FORMAS PAGO");
                    //cboTipo.Items.Add("NIVEL SENSE");
                    //cboTipo.Items.Add("SISTEMA PENSIONES");
                    //cboTipo.Items.Add("JUBILACION");
                    //cboTipo.Items.Add("IND SINDICAL");
                    //cboTipo.Items.Add("RETENCION JUDICIAL");
                    break;
                case "VETPCLIENTE":
                    lblTipo.Text = "Tipo Cliente";
                    //cboTipo.Items.Add("GUBERNAMENTALES");
                    //cboTipo.Items.Add("PRIVADO");
                    //cboTipo.Items.Add("INSTITUCIONES PUBLICAS");
                    break;
                case "NOTPASIGNACION":
                    lblTipo.Text = "Tipo Asignación";
                    //cboTipo.Items.Add("EQUIPO DE TRABAJO");
                    //cboTipo.Items.Add("EQUIPO DE COMUNICACION");
                    //cboTipo.Items.Add("VESTIMENTA");
                    //cboTipo.Items.Add("MEDIO DE TRANSPORTE");
                    //cboTipo.Items.Add("SUMINISTROS");
                    //cboTipo.Items.Add("SEGURIDAD");
                    break;
                case "NOTPJUSTIFICATIVO":
                    lblTipo.Text = "Tipo Justificativo";
                    //cboTipo.Items.Add("");                   
                    break;
                case "NOTPDEDUCCION":
                    lblTipo.Text = "Tipo Deducción";
                    //cboTipo.Items.Add("");                   
                    break;
            }

            cargarGrid();
        }

        private void cargarGrid()
        {
            limpiar();

            //con.EjecutarSP_SQL("SELECT  GD.*, GT.IDGETPSUJETO FROM GEDIVXCLASIF GD, GETPSUJETO GT WHERE GD.IDGEDIVXCLASIF=GT.IDGEDIVXCLASIF AND GD.IDGEDIVXCLASIF IN (SELECT IDGEDIVXCLASIF FROM GETPSUJETO) ", null, "dtSelect", "GEDIVXCLASIF");
            con.EjecutarSP_SQL("SELECT  GD.*, GT."+ cCampoIdTablaCaso + " FROM GEDIVXCLASIF GD, " + this.cTablaCaso + " GT WHERE GD.IDGEDIVXCLASIF=GT.IDGEDIVXCLASIF AND GD.IDGEDIVXCLASIF IN (SELECT IDGEDIVXCLASIF FROM " + this.cTablaCaso + ") ", null, "dtSelect", "GEDIVXCLASIF");

            //'Si no pongo esta línea, se crean automáticamente los campos del grid dependiendo de los campos del DataTable
            DataGridMain.AutoGenerateColumns = false;
            if (con.tabla.Rows.Count > 0)
            {
                DataGridMain.DataSource = con.tabla;

                //'Aquí le indico cuales campos del Select van con las columnas de mi DataGrid
                DataGridMain.Columns["tipo"].DataPropertyName = "NOMBRE";
                DataGridMain.Columns["descripcion"].DataPropertyName = "DESCRIPCION";
                DataGridMain.Columns["activo"].DataPropertyName = "INACTIVO";
                DataGridMain.Columns["observacion"].DataPropertyName = "OBSERVACION";
                DataGridMain.Columns["editar"].DataPropertyName = "";
                DataGridMain.Columns["eliminar"].DataPropertyName = "";
                DataGridMain.Columns["iddivxclasif"].DataPropertyName = "IDGEDIVXCLASIF";
            }
            else
            {
                DataGridMain.DataSource = null;
            }
        }
        private void limpiar()
        {
            txtTipo.Text = "";
            txtDescripcion.Text = "";
            txtObservacion.Text = "";
            //txtCodigo.Text = "";
            this.idActual = "";
            this.valorIdTablaCaso = "";

            txtTipo.Focus();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
           // int valo = txtDescripcion.TextLength;
            MessageBox.Show(txtDescripcion.TextLength.ToString());
            return;
            if (txtDescripcion.Text == "" || txtTipo.Text == "")
            {
                MessageBox.Show("Algunos datos son requeridos. Revise...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTipo.Focus();
                return;
            }

            procesar();
        }

        private void procesar()
        {

            DateTime Hoy = DateTime.Now;
            string dActual = Hoy.ToString("yyyy-MM-dd H:mm:ss");

            //Se obtiene el ID de la tabla con la que se relacionará GEDIVXCLASIF
            string idTabla = "";
            con.ConsultaSimple("ORTABLAS", "NBTABLA", "'" + this.cTablaCaso +"'" , "dtTabla");

            idTabla = con.tabla.Rows[0]["IDTABLAS"].ToString().Trim();


            bool bResult = false;
            if (this.idActual == "")    //Si está vacío es porque se creará nuevo
            {
                // bResult = con.Insertar("GEDIVXCLASIF", "NOMBRE,DESCRIPCION,FEREGISTRO,IDTABLAS,INACTIVO,INUTILIZACION,OBSERVACION,CODINTERFAZ",
                //" '" + txtTipo.Text.Trim() + "','" + txtDescripcion.Text.Trim() + "','" + dActual + "'," + idTabla + "," + (chkActivo.Checked ? 1 : 0) + "," + (chkUtil.Checked ? 1 : 0) + ", '" + txtObservacion.Text.Trim() + "' , '" + txtCodigo.Text.Trim() + "' ");
                //INUTILIZACION se coloca global 1
                bResult = con.Insertar("GEDIVXCLASIF", "NOMBRE,DESCRIPCION,FEREGISTRO,IDTABLAS,INACTIVO,INUTILIZACION,OBSERVACION",
              " '" + txtTipo.Text.Trim() + "','" + txtDescripcion.Text.Trim() + "','" + dActual + "'," + idTabla + "," + (chkActivo.Checked ? 1 : 0) + ",1, '" + txtObservacion.Text.Trim() + "'  ");
            }
            else //De lo contrario es porque se va a modificar
            {
                if (MessageBox.Show("¿ Confirma procesar los cambios al registro ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //bResult = con.Actualizar("GEDIVXCLASIF", "NOMBRE= '" + txtTipo.Text.Trim() + "', DESCRIPCION='" + txtDescripcion.Text.Trim() + "',INACTIVO=" + (chkActivo.Checked ? 1 : 0)
                    //  + ", INUTILIZACION= " + (chkUtil.Checked ? 1 : 0) + ",OBSERVACION='" + txtObservacion.Text.Trim() + "' ,CODINTERFAZ='" + txtCodigo.Text.Trim() + "' ", "IDGEDIVXCLASIF=" + this.idActual);
                    bResult = con.Actualizar("GEDIVXCLASIF", "NOMBRE= '" + txtTipo.Text.Trim() + "', DESCRIPCION='" + txtDescripcion.Text.Trim() + "',INACTIVO=" + (chkActivo.Checked ? 1 : 0)
                      + ",OBSERVACION='" + txtObservacion.Text.Trim() + "' ", "IDGEDIVXCLASIF=" + this.idActual);
                }
                else
                {
                    return;
                }               
            }

            if (bResult)
            {

                string cUltimoID;
                con.ConsultaSQL("SELECT MAX(IDGEDIVXCLASIF)  FROM  GEDIVXCLASIF ", "dtUltimoId");

                cUltimoID = con.tabla.Rows[0][0].ToString().Trim();
                if (this.idActual == "") { 
                    //this.cTablaCaso trae el nombre de la Tabla
                    con.Insertar(this.cTablaCaso, "IDGEDIVXCLASIF", cUltimoID);
                }
                cargarGrid();
                MessageBox.Show("Los datos se han procesado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            else
            {
                MessageBox.Show("Problemas al agregar el registro. Revise...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtDescripcion.Focus();
        }



        private void DataGridMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.idActual = DataGridMain.Rows[e.RowIndex].Cells[0].Value.ToString();
                //MessageBox.Show(this.idActual);                
                cargarDatos();
            }
            catch (Exception ex)
            {

            }
        }

        private void DataGridMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.idActual = DataGridMain.Rows[e.RowIndex].Cells["iddivxclasif"].Value.ToString();            
                if (DataGridMain.Columns[e.ColumnIndex].Name == "editar")
                {
                    cargarDatos();                    
                }
                if (DataGridMain.Columns[e.ColumnIndex].Name == "eliminar")
                {
                    if (MessageBox.Show("¿ Confirma eliminar el registro seleccionado ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        //Luego de eliminar la tabla principal se elimina el registro de GEDIVXCLASIF
                        if (con.Eliminar(this.cTablaCaso, "IDGEDIVXCLASIF=" + this.idActual))
                        {
                            con.Eliminar("GEDIVXCLASIF", "IDGEDIVXCLASIF=" + this.idActual);

                            cargarGrid();
                            MessageBox.Show("Se ha eliminado el registro de la aplicación.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Problemas al eliminar el registro. Revise...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error de aplicación. Comuníquese con TI  " + ex.ToString(), "Sistema para Control Operacional", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
   
        private void cargarDatos()
        {
            con.ConsultaSimple("GEDIVXCLASIF ", "IDGEDIVXCLASIF", this.idActual, "dtRegistros");

            txtTipo.Text = con.tabla.Rows[0]["NOMBRE"].ToString().Trim();
            txtDescripcion.Text = con.tabla.Rows[0]["DESCRIPCION"].ToString().Trim();
            chkActivo.Checked = (con.tabla.Rows[0]["INACTIVO"].ToString().Trim() == "1" ? true : false);
            //chkUtil.Checked = (con.tabla.Rows[0]["INUTILIZACION"].ToString().Trim() == "1" ? true : false);
            txtObservacion.Text = con.tabla.Rows[0]["OBSERVACION"].ToString().Trim();
            //txtCodigo.Text = con.tabla.Rows[0]["CODINTERFAZ"].ToString().Trim();

            con.ConsultaSimple(this.cTablaCaso, "IDGEDIVXCLASIF", this.idActual, "dtRegistros");
            this.valorIdTablaCaso = con.tabla.Rows[0][this.cCampoIdTablaCaso].ToString().Trim();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkActivo.Checked)
            {
                if (this.valorIdTablaCaso != "")
                {
                    switch (this.cTablaCaso)
                    {
                        //Se valida si el registro se usa en otra tabla (ORDIVNEG dependiente) para no desactivar
                        case "NOTPASIGNACION":
                            chkActivo.Checked = fun.DependienteCaso(this.cTablaCaso, "NODET_TPASIGNACION", this.cCampoIdTablaCaso, this.valorIdTablaCaso, "activo");
                            if (chkActivo.Checked) { return; } //Sino está chequeado, se hace otra validadción y así sucesivamente

                            //chkActivo.Checked = fun.DependienteCaso(this.cTablaCaso, "ORDIVNEG", this.cCampoIdTablaCaso, this.valorIdTablaCaso, "activo");
                            //if (chkActivo.Checked) { return; }

                            break;
                        
                    }

                }
            }

        }
    }
}
