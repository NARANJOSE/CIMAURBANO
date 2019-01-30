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
    public partial class FrmDivxClasif2 : Form
    {
        private int AlturaBase = 440;

        //Define qué ancho será visible el Scroll
        private int AnchoVisibleScroll = 400;

        //Define el ancho Mayor
        private int AnchoMayor = 718;

        //Define el ancho Medio
        private int AnchoMedio = 718;

        Conexion con = new Conexion();
        Funciones fun = new Funciones();

        private Parametros[] parametros;

        private void FrmDivxClasif2_Resize(object sender, EventArgs e)
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
                if ((panel1.Visible && panel2.Visible && !panel3.Visible) || (panel1.Visible && panel2.Visible && panel3.Visible))
                {
                    panel1.Width = (flowLPScroll.Width - 9) / 2;
                    panel2.Width = (flowLPScroll.Width - 9) / 2;
                    panel3.Width = (flowLPScroll.Width - 6);
                }
            }

            if (flowLPScroll.Width < AnchoMedio)
            {
                if ((panel1.Visible && panel2.Visible && !panel3.Visible) || (panel1.Visible && panel2.Visible && panel3.Visible))
                {
                    panel1.Width = (flowLPScroll.Width - 6);
                    panel2.Width = (flowLPScroll.Width - 6);
                    panel3.Width = (flowLPScroll.Width - 6);
                }
            }


            //Se ajusta el ancho del panel central           
            if (panel4.Visible)
                panel4.Width = (flowLPScroll.Width - 6);


            //Se valida si existe uno de los 3 panes de arriba
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
        public FrmDivxClasif2(string pcTablaCaso, string pcCampoIdTablaCaso)
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

            //Dependiendo del caso se llena loa valores del combo o se muestra el campo texto
            switch (this.cTablaCaso)
            {
                case "GECLASEDOCUMENTO":
                    lblTipo.Text = "Clase Documento";
                    DataGridMain.Columns[1].HeaderText = "Clase";
                    //cboTipo.Items.Add("EMPLEADO");
                    //cboTipo.Items.Add("CLIENTE");


                    parametros = new Parametros[2];
                    parametros[0] = new Parametros("TABLA", "GEAREADEPART");
                    parametros[1] = new Parametros("CASO", "1");

                    con.EjecutarSP_SQL("SPSELECTDIVXCLASIF", parametros, "dtTIPOS", "GEDIVXCLASIF");

                    //if (con.tabla.Rows.Count > 0)
                    cboAreaD.DataSource = con.tabla;
                    con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                    cboAreaD.DisplayMember = "NOMBRE";
                    cboAreaD.ValueMember = "IDAREADEPARTAMENTAL";

                    break;

                case "GETPCAUSAL":
                    lblTipo.Text = "Tipo Causal";

                    parametros = new Parametros[2];
                    parametros[0] = new Parametros("TABLA", "GEAREADEPART");
                    parametros[1] = new Parametros("CASO", "1");

                    con.EjecutarSP_SQL("SPSELECTDIVXCLASIF", parametros, "dtTIPOS", "GEDIVXCLASIF");

                    cboAreaD.DataSource = con.tabla;
                    con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                    cboAreaD.DisplayMember = "NOMBRE";
                    cboAreaD.ValueMember = "IDAREADEPARTAMENTAL";

                    break;
                case "NOCIA_CARGO":
                    lblTipo.Text = "Cargo";
                    lblArea.Text = "Empresa";

                    DataGridMain.Columns[1].HeaderText = "Cargo";

                    con.EjecutarSP_SQL("SELECT * FROM ORCIA", parametros, "dtTIPOS", "ORCIA");

                    cboAreaD.DataSource = con.tabla;
                    con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                    cboAreaD.DisplayMember = "NOMBRE";
                    cboAreaD.ValueMember = "IDORCIA";

                    break;
                case "FITPPOLIZA":
                    lblTipo.Text = "Tipo Poliza";
                    tableLPAnexo1.Visible = false;

                    tableLPAnexo2.Visible = true;
                    panel1.Controls.Add(tableLPAnexo2);
                    tableLPAnexo2.Location = new Point(0, 125);

                    cboAreaD.Items.Add("");
                    break;

                case "ORDIVNEG":
                    //Area de división de negocio
                    lblTipo.Text = "División Negocio ";
                    lblArea.Text = "Area Negocio";
                    DataGridMain.Columns[1].HeaderText = "División";

                    //Se obtienen las areas de negocio
                    parametros = new Parametros[2];
                    parametros[0] = new Parametros("TABLA", "ORAREANEG");
                    parametros[1] = new Parametros("CASO", "1");

                    con.EjecutarSP_SQL("SPSELECTDIVXCLASIF", parametros, "dtTIPOS", "GEDIVXCLASIF");

                    cboAreaD.DataSource = con.tabla;
                    con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                    cboAreaD.DisplayMember = "NOMBRE";
                    cboAreaD.ValueMember = "IDORAREANEGOCIO";

                    break;
                case "GEDETALLECAUSAL":
                    //Area de división de negocio
                    lblTipo.Text = "Detalle Causal";
                    lblArea.Text = "Tipo Causal";
                    DataGridMain.Columns[1].HeaderText = "Detalle";

                    //Se obtienen las areas de negocio
                    parametros = new Parametros[2];
                    parametros[0] = new Parametros("TABLA", "GETPCAUSAL");
                    parametros[1] = new Parametros("CASO", "1");

                    con.EjecutarSP_SQL("SPSELECTDIVXCLASIF", parametros, "dtTIPOS", "GEDIVXCLASIF");

                    cboAreaD.DataSource = con.tabla;
                    con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                    cboAreaD.DisplayMember = "NOMBRE";
                    cboAreaD.ValueMember = "IDGETPCAUSAL";

                    break;

            }

            cargarGrid();
        }

        private void cargarGrid()
        {
            limpiar();

            //con.EjecutarSP_SQL("SELECT  GD.*, GT.IDGETPSUJETO FROM GEDIVXCLASIF GD, GETPSUJETO GT WHERE GD.IDGEDIVXCLASIF=GT.IDGEDIVXCLASIF AND GD.IDGEDIVXCLASIF IN (SELECT IDGEDIVXCLASIF FROM GETPSUJETO) ", null, "dtSelect", "GEDIVXCLASIF");
            con.EjecutarSP_SQL("SELECT  GD.*, GT." + cCampoIdTablaCaso + " FROM GEDIVXCLASIF GD, " + this.cTablaCaso + " GT WHERE GD.IDGEDIVXCLASIF=GT.IDGEDIVXCLASIF AND GD.IDGEDIVXCLASIF IN (SELECT IDGEDIVXCLASIF FROM " + this.cTablaCaso + ") ", null, "dtSelect", "GEDIVXCLASIF");

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
            cboAreaD.SelectedIndex = 0;

            txtTipo.Text = "";
            txtDescripcion.Text = "";
            txtObservacion.Text = "";
            txtCodigo.Text = "";
            txtAuxiliar.Text = "";
            this.idActual = "";
            this.valorIdTablaCaso = "";

            txtTipo.Focus();

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(txtAuxiliar.Text);
            //Si se está tabajando con el combo se hace la validación para que éste esté lleno, sino se valida el texto
            switch (this.cTablaCaso)
            {
                case "GECLASEDOCUMENTO":
                case "GETPCAUSAL":
                case "NOCIA_CARGO":
                case "ORDIVNEG":
                case "GEDETALLECAUSAL":
                    if (txtDescripcion.Text == "" || txtTipo.Text == "" || cboAreaD.Text == "")
                    {
                        MessageBox.Show("Algunos datos son requeridos. Revise...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTipo.Focus();
                        return;
                    }
                    break;
                case "FITPPOLIZA":
                    if (txtDescripcion.Text == "" || txtTipo.Text == "" || txtAuxiliar.Text == "")
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
                bResult = con.Insertar("GEDIVXCLASIF", "NOMBRE,DESCRIPCION,FEREGISTRO,IDTABLAS,INACTIVO,INUTILIZACION,OBSERVACION,CODINTERFAZ",
               " '" + txtTipo.Text.Trim() + "','" + txtDescripcion.Text.Trim() + "','" + dActual + "'," + idTabla + "," + (chkActivo.Checked ? 1 : 0) + "," + (chkUtil.Checked ? 1 : 0) + ", '" + txtObservacion.Text.Trim() + "' , '" + txtCodigo.Text.Trim() + "' ");
            }
            else //De lo contrario es porque se va a modificar
            {
                if (MessageBox.Show("¿ Confirma procesar los cambios al registro ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    bResult = con.Actualizar("GEDIVXCLASIF", "NOMBRE= '" + txtTipo.Text.Trim() + "', DESCRIPCION='" + txtDescripcion.Text.Trim() + "',INACTIVO=" + (chkActivo.Checked ? 1 : 0)
                      + ", INUTILIZACION= " + (chkUtil.Checked ? 1 : 0) + ",OBSERVACION='" + txtObservacion.Text.Trim() + "' ,CODINTERFAZ='" + txtCodigo.Text.Trim() + "' ", "IDGEDIVXCLASIF=" + this.idActual);
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
                        case "GECLASEDOCUMENTO":
                        case "GETPCAUSAL":
                            con.Insertar(this.cTablaCaso, "IDGEDIVXCLASIF,IDAREADEPARTAMENTAL", cUltimoID + "," + cboAreaD.SelectedValue.ToString());
                            break;
                        case "NOCIA_CARGO":
                            con.Insertar(this.cTablaCaso, "IDGEDIVXCLASIF,IDORCIA", cUltimoID + "," + cboAreaD.SelectedValue.ToString());
                            break;
                        case "ORDIVNEG":
                            con.Insertar(this.cTablaCaso, "IDGEDIVXCLASIF,IDORAREANEGOCIO", cUltimoID + "," + cboAreaD.SelectedValue.ToString());
                            break;
                        case "FITPPOLIZA":
                            con.Insertar(this.cTablaCaso, "IDGEDIVXCLASIF,SIGLAS", cUltimoID + ",'" + txtAuxiliar.Text.Trim() + "'");
                            break;
                        case "GEDETALLECAUSAL":
                            con.Insertar(this.cTablaCaso, "IDGEDIVXCLASIF,IDGETPCAUSAL", cUltimoID + "," + cboAreaD.SelectedValue.ToString());
                            break;
                    }
                }
                else
                {
                    switch (this.cTablaCaso)
                    {
                        case "GECLASEDOCUMENTO":
                        case "GETPCAUSAL":
                            con.Actualizar(this.cTablaCaso, "IDAREADEPARTAMENTAL=" + cboAreaD.SelectedValue.ToString(), "IDGEDIVXCLASIF=" + this.idActual);
                            break;
                        case "NOCIA_CARGO":
                            con.Actualizar(this.cTablaCaso, "IDORCIA=" + cboAreaD.SelectedValue.ToString(), "IDGEDIVXCLASIF=" + this.idActual);
                            break;
                        case "ORDIVNEG":
                            con.Actualizar(this.cTablaCaso, "IDORAREANEGOCIO=" + cboAreaD.SelectedValue.ToString(), "IDGEDIVXCLASIF=" + this.idActual);
                            break;
                        case "FITPPOLIZA":
                            con.Actualizar(this.cTablaCaso, "SIGLAS='" + txtAuxiliar.Text.Trim() + "'", "IDGEDIVXCLASIF=" + this.idActual);
                            break;
                        case "GEDETALLECAUSAL":
                            con.Actualizar(this.cTablaCaso, "IDGETPCAUSAL=" + cboAreaD.SelectedValue.ToString(), "IDGEDIVXCLASIF=" + this.idActual);
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
            con.ConsultaSimple("GEDIVXCLASIF ", "IDGEDIVXCLASIF", this.idActual, "dtRegistros");

            txtTipo.Text = con.tabla.Rows[0]["NOMBRE"].ToString().Trim();
            txtDescripcion.Text = con.tabla.Rows[0]["DESCRIPCION"].ToString().Trim();
            chkActivo.Checked = (con.tabla.Rows[0]["INACTIVO"].ToString().Trim() == "1" ? true : false);
            chkUtil.Checked = (con.tabla.Rows[0]["INUTILIZACION"].ToString().Trim() == "1" ? true : false);
            txtObservacion.Text = con.tabla.Rows[0]["OBSERVACION"].ToString().Trim();
            txtCodigo.Text = con.tabla.Rows[0]["CODINTERFAZ"].ToString().Trim();

            con.ConsultaSimple(this.cTablaCaso, "IDGEDIVXCLASIF", this.idActual, "dtRegistros");

            //Dependiendo el caso, se extrae el valor del campo extra
            switch (this.cTablaCaso)
            {
                case "GECLASEDOCUMENTO":
                case "GETPCAUSAL":                    
                    cboAreaD.SelectedValue = con.tabla.Rows[0]["IDAREADEPARTAMENTAL"].ToString().Trim();
                    this.valorIdTablaCaso = con.tabla.Rows[0][this.cCampoIdTablaCaso].ToString().Trim();

                    break;
                case "NOCIA_CARGO":                   
                    cboAreaD.SelectedValue = con.tabla.Rows[0]["IDORCIA"].ToString().Trim();
                    this.valorIdTablaCaso = con.tabla.Rows[0][this.cCampoIdTablaCaso].ToString().Trim();

                    break;
                case "ORDIVNEG":
                    cboAreaD.SelectedValue = con.tabla.Rows[0]["IDORAREANEGOCIO"].ToString().Trim();
                    this.valorIdTablaCaso = con.tabla.Rows[0][this.cCampoIdTablaCaso].ToString().Trim();

                    break;
                case "FITPPOLIZA":
                    txtAuxiliar.Text = con.tabla.Rows[0]["SIGLAS"].ToString().Trim();
                    this.valorIdTablaCaso = con.tabla.Rows[0][this.cCampoIdTablaCaso].ToString().Trim();

                    break;
                case "GEDETALLECAUSAL":
                    cboAreaD.SelectedValue = con.tabla.Rows[0]["IDGETPCAUSAL"].ToString().Trim();
                    this.valorIdTablaCaso = con.tabla.Rows[0][this.cCampoIdTablaCaso].ToString().Trim();

                    break;
            }

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
                        case "ORAREANEG":                            
                            chkActivo.Checked = fun.DependienteCaso(this.cTablaCaso, "ORDIVNEG", this.cCampoIdTablaCaso, this.valorIdTablaCaso, "activo") ;
                            if (chkActivo.Checked) { return; } //Sino está chequeado, se hace otra validadción y así sucesivamente

                            //chkActivo.Checked = fun.DependienteCaso(this.cTablaCaso, "ORDIVNEG", this.cCampoIdTablaCaso, this.valorIdTablaCaso, "activo");
                            //if (chkActivo.Checked) { return; }

                            break;
                        case "GETPCAUSAL":
                            chkActivo.Checked = fun.DependienteCaso(this.cTablaCaso, "GEDETALLECAUSAL", this.cCampoIdTablaCaso, this.valorIdTablaCaso, "activo");
                            if (chkActivo.Checked) { return; }

                            break;
                    }
                    
                   
                   
                }

            }
        }
    }
}
