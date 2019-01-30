using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIMAURBANO
{
    public partial class FrmDGEgrdx1b : Form
    {
        //Define qué ancho será visible el Scroll
        private int AnchoVisibleScroll = 400;

        //Define el ancho Mayor
        private int AnchoMayor = 677;

        //Define el ancho Medio
        private int AnchoMedio = 600;

        Funciones fun = new Funciones();
        Conexion con = new Conexion();
        private Parametros[] parametros;

       
        private void FrmDGEgrdx1b_Resize(object sender, EventArgs e)
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
                    panel1.Width = (flowLPScroll.Width - 5) / contPanel;
                    panel2.Width = (flowLPScroll.Width - 5) / contPanel;
                    panel3.Width = (flowLPScroll.Width - 5) / contPanel;
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

                if (panel1.Visible && !panel2.Visible && !panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 5) / contPanel;
                    panel2.Width = (flowLPScroll.Width - 5) / contPanel;
                    panel3.Width = (flowLPScroll.Width - 5) / contPanel;
                }

                if (panel1.Visible && panel2.Visible && !panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 8) / contPanel;
                    panel2.Width = (flowLPScroll.Width - 8) / contPanel;
                    panel3.Width = (flowLPScroll.Width - 8) / contPanel;
                }

                if (panel1.Visible && panel2.Visible && panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 9) / 2;
                    panel2.Width = (flowLPScroll.Width - 9) / 2;
                    panel3.Width = (flowLPScroll.Width - 6);
                }


            }

            if (flowLPScroll.Width < AnchoMedio)
            {
                panel1.Width = (flowLPScroll.Width - 6);
                panel2.Width = (flowLPScroll.Width - 6);
                panel3.Width = (flowLPScroll.Width - 6);
            }


            //Se ajusta el ancho del panel central           
            if (panel4.Visible)
                panel4.Width = (flowLPScroll.Width - 6);

            //Se valida si existe uno de los 3 panes de arriba
            if (panel1.Visible) altura = panel1.Height;


            if (panel2.Visible) altura = panel2.Height;


            if (panel3.Visible) altura = panel3.Height;


            if (panel4.Visible) altura = (altura + panel4.Height);


            if (panel5.Visible)
                panel5.Width = (flowLPScroll.Width - 4);


            panel5.Height = flowLPScroll.Height - altura - 10;


            if (flowLPScroll.Width < AnchoVisibleScroll)
            {
                flowLPScroll.AutoScroll = true;
            }
            else
            {
                flowLPScroll.AutoScroll = false;
            }
        }

        private void FrmDGEgrdx1b_Shown(object sender, EventArgs e)
        {
            //Se mandan a ejecutar las acciones de adaptación de tamaño
            this.Width = this.Width + 1;
            this.Height = this.Height + 1;
        }

        private string cTablaCaso = "";
        private string cCampoIdTablaCaso = "";
        private string idActual = "";
        private string valorIdTablaCaso = "";

        public FrmDGEgrdx1b(string pcTablaCaso, string pcCampoIdTablaCaso)
        {
            InitializeComponent();

            this.cTablaCaso = pcTablaCaso;
            this.cCampoIdTablaCaso = pcCampoIdTablaCaso;

            flowLPScroll.BackColor = Color.White;
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;

            //Dependiendo del caso se llena loa valores del combo o se muestra el campo texto
            switch (this.cTablaCaso)
            {

                case "FITPPOLIZA":
                    lblTipo.Text = "Tipo Poliza";                                     
                    //tableLPAnexo2.Location = new Point(0, 125);

                    break;

                case "ORDIVNEG":
                    //Area de división de negocio
                    lblTipo.Text = "División Negocio ";
                    //lblArea.Text = "Area Negocio";
                    DataGridMain.Columns[1].HeaderText = "División";

                    //Se obtienen las areas de negocio
                    parametros = new Parametros[2];
                    parametros[0] = new Parametros("TABLA", "ORAREANEG");
                    parametros[1] = new Parametros("CASO", "1");

                    con.EjecutarSP_SQL("SPSELECTDIVXCLASIF", parametros, "dtTIPOS", "GEDIVXCLASIF");

                    //cboAreaD.DataSource = con.tabla;
                    //con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                    //cboAreaD.DisplayMember = "NOMBRE";
                    //cboAreaD.ValueMember = "IDORAREANEGOCIO";

                    break;
                case "GEDETALLECAUSAL":
                    //Area de división de negocio
                    lblTipo.Text = "Detalle Causal";
                    //lblArea.Text = "Tipo Causal";
                    DataGridMain.Columns[1].HeaderText = "Detalle";

                    //Se obtienen las areas de negocio
                    parametros = new Parametros[2];
                    parametros[0] = new Parametros("TABLA", "GETPCAUSAL");
                    parametros[1] = new Parametros("CASO", "1");

                    con.EjecutarSP_SQL("SPSELECTDIVXCLASIF", parametros, "dtTIPOS", "GEDIVXCLASIF");

                    //cboAreaD.DataSource = con.tabla;
                    //con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                    //cboAreaD.DisplayMember = "NOMBRE";
                    //cboAreaD.ValueMember = "IDGETPCAUSAL";

                    break;

            }

            cargarGrid();
        }

        private void cargarGrid()
        {
            limpiar();
            
            parametros = new Parametros[11];
            parametros[0] = new Parametros("@PTB1", this.cTablaCaso);
            parametros[1] = new Parametros("@PCAMPOSTB1", "TB1.*");
            parametros[2] = new Parametros("@PTB2", "");
            parametros[3] = new Parametros("@PCAMPOSTB2", "");
            parametros[4] = new Parametros("@PCAMPOIDTB2", "");
            parametros[5] = new Parametros("@PTB3", "");
            parametros[6] = new Parametros("@PCAMPOSTB3", "");
            parametros[7] = new Parametros("@PCAMPOIDTB3", "");
            parametros[8] = new Parametros("@PTB4", "");
            parametros[9] = new Parametros("@PCAMPOSTB4", "");
            parametros[10] = new Parametros("@PCAMPOIDTB4", "");

            con.EjecutarSP_SQL("SPSELECTTABLASDIVXCLASIF", parametros, "dtRegistros", "GEDIVXCLASIF");



            //'Si no pongo esta línea, se crean automáticamente los campos del grid dependiendo de los campos del DataTable
            DataGridMain.AutoGenerateColumns = false;
            if (con.tabla.Rows.Count > 0)
            {
                DataGridMain.DataSource = con.tabla;

                //'Aquí le indico cuales campos del Select van con las columnas de mi DataGrid
                DataGridMain.Columns["siglas"].DataPropertyName = "SIGLAS";
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
            txtSiglas.Text = "";

            this.idActual = "";
            this.valorIdTablaCaso = "";

            txtTipo.Focus();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(txtAuxiliar.Text);
            switch (this.cTablaCaso)
            {
                case "FITPPOLIZA":
                    if (txtDescripcion.Text == "" || txtTipo.Text == "")
                    {
                        MessageBox.Show("Algunos datos son requeridos. Revise...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTipo.Focus();
                        return;
                    }
                    break;
            }

            procesar();
        }


        private void procesar()
        {

            DateTime Hoy = DateTime.Now;
            string dActual = Hoy.ToString("yyyy-MM-dd H:mm:ss");

            //Se obtiene el ID de la tabla con la que se relacionará GEDIVXCLASIF
            string idTabla = "";
            con.ConsultaSimple("ORTABLAS", "NBTABLA", "'" + this.cTablaCaso + "'", "dtTabla");

            idTabla = con.tabla.Rows[0]["IDTABLAS"].ToString().Trim();


            bool bResult = false;
            if (this.idActual == "")    //Si está vacío es porque se creará nuevo
            {
                //INUTILIZACION se coloca global 1 
                bResult = con.Insertar("GEDIVXCLASIF", "NOMBRE,DESCRIPCION,FEREGISTRO,IDTABLAS,INACTIVO,INUTILIZACION,OBSERVACION",
              " '" + txtTipo.Text.Trim() + "','" + txtDescripcion.Text.Trim() + "','" + dActual + "'," + idTabla + "," + (chkActivo.Checked ? 1 : 0) + ",1, '" + txtObservacion.Text.Trim() + "'  ");
            }
            else //De lo contrario es porque se va a modificar
            {
                if (MessageBox.Show("¿ Confirma procesar los cambios al registro ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
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
                if (this.idActual == "")
                {
                    //this.cTablaCaso trae el nombre de la Tabla
                    switch (this.cTablaCaso)
                    {
                        case "FITPPOLIZA":
                            con.Insertar(this.cTablaCaso, "IDGEDIVXCLASIF,SIGLAS", cUltimoID + ",'" + txtSiglas.Text.Trim() + "'");
                            break;
                        case "GEDETALLECAUSAL":
                            //con.Insertar(this.cTablaCaso, "IDGEDIVXCLASIF,IDGETPCAUSAL", cUltimoID + "," + cboAreaD.SelectedValue.ToString());
                            break;
                    }
                }
                else
                {
                    switch (this.cTablaCaso)
                    {
                        case "FITPPOLIZA":
                            con.Actualizar(this.cTablaCaso, "SIGLAS='" + txtSiglas.Text.Trim() + "'", "IDGEDIVXCLASIF=" + this.idActual);
                            break;
                        case "GEDETALLECAUSAL":
                          //  con.Actualizar(this.cTablaCaso, "IDGETPCAUSAL=" + cboAreaD.SelectedValue.ToString(), "IDGEDIVXCLASIF=" + this.idActual);
                            break;
                    }
                }
                cargarGrid();
                MessageBox.Show("Los datos se han procesado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Problemas al agregar el registro. Revise...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void DataGridMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.idActual = DataGridMain.Rows[e.RowIndex].Cells["iddivxclasif"].Value.ToString();
                //MessageBox.Show(this.idActual);               
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

        private void DataGridMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.idActual = DataGridMain.Rows[e.RowIndex].Cells["iddivxclasif"].Value.ToString();
                cargarDatos();
            }
            catch (Exception ex)
            {

            }
        }

        private void cargarDatos()
        {
            if (this.idActual != "")
            {
                con.ConsultaSimple("GEDIVXCLASIF ", "IDGEDIVXCLASIF", this.idActual, "dtRegistros");

                txtTipo.Text = con.tabla.Rows[0]["NOMBRE"].ToString().Trim();
                txtDescripcion.Text = con.tabla.Rows[0]["DESCRIPCION"].ToString().Trim();
                chkActivo.Checked = (con.tabla.Rows[0]["INACTIVO"].ToString().Trim() == "1" ? true : false);
                txtObservacion.Text = con.tabla.Rows[0]["OBSERVACION"].ToString().Trim();

                con.ConsultaSimple(this.cTablaCaso, "IDGEDIVXCLASIF", this.idActual, "dtRegistros");

                //Dependiendo el caso, se extrae el valor del campo extra
                switch (this.cTablaCaso)
                {
                    case "FITPPOLIZA":
                        txtSiglas.Text = con.tabla.Rows[0]["SIGLAS"].ToString().Trim();
                        this.valorIdTablaCaso = con.tabla.Rows[0][this.cCampoIdTablaCaso].ToString().Trim();

                        break;
                    case "GEDETALLECAUSAL":
                        // cboAreaD.SelectedValue = con.tabla.Rows[0]["IDGETPCAUSAL"].ToString().Trim();
                        this.valorIdTablaCaso = con.tabla.Rows[0][this.cCampoIdTablaCaso].ToString().Trim();

                        break;
                }
                txtTipo.Focus();
            }            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void picBuscar_Click(object sender, EventArgs e)
        {
            FrmFiltro FrmDivxClasif = new FrmFiltro(this.cTablaCaso,"");
            FrmDivxClasif.ReturnID += new FrmFiltro.DelegadoReturn(CargarID);
            FrmDivxClasif.ShowDialog();

            cargarDatos();
        }

        void CargarID(string valorID)
        {
            this.idActual = valorID;
        }

    }
}
