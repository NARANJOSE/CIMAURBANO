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
    public partial class FrmFiltro : Form
    {

        // Delegado
        public delegate void DelegadoReturn(string IdSeleccion);
        //Evento
        public event DelegadoReturn ReturnID;


        //Size 1174, 614
        //Define qué ancho será visible el Scroll
        private int AnchoVisibleScroll = 400;

        //Define el ancho Mayor
        private int AnchoMayor = 677;

        //Define el ancho Medio
        private int AnchoMedio = 600;

        Funciones fun = new Funciones();
        Conexion con = new Conexion();
        private Parametros[] parametros;

        private void FrmFiltro_Resize(object sender, EventArgs e)
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

        private void FrmFiltro_Shown(object sender, EventArgs e)
        {
            //Se mandan a ejecutar las acciones de adaptación de tamaño
            this.Width = this.Width + 1;
            this.Height = this.Height + 1;
        }

        private string cTablaCaso = "";
        private string cCampoIdTablaCaso = "";
        private string idActual = "";
        private string valorIdTablaCaso = "";

        public FrmFiltro(string pcTablaCaso, string pcCampoIdTablaCaso)
        {
            InitializeComponent();

            this.cTablaCaso = pcTablaCaso;
            this.cCampoIdTablaCaso = pcCampoIdTablaCaso;

            switch (this.cTablaCaso)
            {
                case "GEAREADEPART":
                    DataGridMain.Columns[2].HeaderText = "Area Departamental";
                    DataGridMain.Columns[1].Visible = false;
                    break;
            }

            cargarGrid();
        }

        private void cargarGrid()
        {

            if (txtBuscar.Text.Trim() == "")
            {
                if (this.cTablaCaso == "")
                {
                    con.EjecutarSP_SQL("SELECT  TB0.* FROM GEDIVXCLASIF TB0 ORDER BY TB0.NOMBRE DESC ", null, "dtRegistros", "GEDIVXCLASIF");
                }
                else
                {
                    con.EjecutarSP_SQL("SELECT  TB0.NOMBRE, TB0.DESCRIPCION, TB0.FEREGISTRO, TB0.INACTIVO, TB0.INUTILIZACION, TB0.IDTABLAS, TB0.OBSERVACION, TB0.CODINTERFAZ, TB1.* " +
                    "FROM GEDIVXCLASIF TB0, " + this.cTablaCaso + " TB1  WHERE (TB0.IDGEDIVXCLASIF=TB1.IDGEDIVXCLASIF) ORDER BY TB0.NOMBRE DESC ", null, "dtRegistros", "GEDIVXCLASIF");
                }
                
            }
            else
            {
                con.EjecutarSP_SQL("SELECT TB0.NOMBRE, TB0.DESCRIPCION, TB0.FEREGISTRO, TB0.INACTIVO, TB0.INUTILIZACION, TB0.IDTABLAS, TB0.OBSERVACION, TB0.CODINTERFAZ, TB1.* " +
                    "FROM GEDIVXCLASIF TB0, " + this.cTablaCaso + " TB1  WHERE (TB0.IDGEDIVXCLASIF=TB1.IDGEDIVXCLASIF) AND (TB0.NOMBRE LIKE '%" + txtBuscar.Text.Trim() + "%' OR TB0.DESCRIPCION LIKE '%" + txtBuscar.Text.Trim() + "%' OR TB0.OBSERVACION LIKE '%" + txtBuscar.Text.Trim() + "%' OR FORMAT(TB0.FEREGISTRO,'dd/MM/yyyy hh:mm:ss') LIKE  '%" + txtBuscar.Text.Trim() + "%' ) ORDER BY TB0.NOMBRE DESC ", null, "dtRegistros", "GEDIVXCLASIF");
            }

            //'Si no pongo esta línea, se crean automáticamente los campos del grid dependiendo de los campos del DataTable
            DataGridMain.AutoGenerateColumns = false;
            if (con.tabla.Rows.Count > 0)
            {
                DataGridMain.DataSource = con.tabla;

                //'Aquí le indico cuales campos del Select van con las columnas de mi DataGrid
                DataGridMain.Columns["iddivxclasif"].DataPropertyName = "IDGEDIVXCLASIF";
                DataGridMain.Columns["nombre"].DataPropertyName = "NOMBRE";
                DataGridMain.Columns["descripcion"].DataPropertyName = "DESCRIPCION";
                DataGridMain.Columns["observacion"].DataPropertyName = "OBSERVACION";
                DataGridMain.Columns["activo"].DataPropertyName = "INACTIVO";
                DataGridMain.Columns["fregistro"].DataPropertyName = "FEREGISTRO";
                DataGridMain.Columns["elegir"].DataPropertyName = "";
                DataGridMain.Columns["idtablacaso"].DataPropertyName = this.cCampoIdTablaCaso;

            }
            else
            {
                DataGridMain.DataSource = null;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            cargarGrid();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (DataGridMain.Rows.Count > 0)
                {
                    //DataGridMain.Rows[1].Selected = true;
                    seleccionar();
                }
            }
        }

        private void DataGridMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DataGridMain.Columns[e.ColumnIndex].Name == "elegir")
                {
                    seleccionar();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error de aplicación. Comuníquese con TI  " + ex.ToString(), "Sistema para Control Operacional", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionar();
        }

        private void seleccionar()
        {
            this.idActual = (DataGridMain.CurrentRow.Cells["iddivxclasif"].Value).ToString();
            this.valorIdTablaCaso= (DataGridMain.CurrentRow.Cells["idtablacaso"].Value).ToString();

            //MessageBox.Show(this.idActual);
            //Se retorna ID 
            this.ReturnID(this.valorIdTablaCaso);
            this.Close();
        }

        
    }
}
