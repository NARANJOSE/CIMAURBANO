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
    public partial class FrmIGEgrdx1a : Form
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
        private void FrmIGEgrdx1a_Resize(object sender, EventArgs e)
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

        private void FrmIGEgrdx1a_Shown(object sender, EventArgs e)
        {
            //Se mandan a ejecutar las acciones de adaptación de tamaño
            this.Width = this.Width + 1;
            this.Height = this.Height + 1;
        }

        private string cTablaCaso = "";
        private string cCampoIdTablaCaso = "";
        private string idActual = "";
        private string valorIdTablaCaso = "";

        public FrmIGEgrdx1a()
        {
            InitializeComponent();
        }

        private void DataGridMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

       
    }
}
