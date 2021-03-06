﻿using System;
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
    public partial class Frm1x1Grid : Form
    {
        private int AlturaBase = 440;

        //Define qué ancho será visible el Scroll
        private int AnchoVisibleScroll = 360;

        //Define el ancho Mayor
        private int AnchoMayor = 360;

        //Define el ancho Medio
        private int AnchoMedio = 360;


        private void Frm1x1Grid_Resize(object sender, EventArgs e)
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
                if (panel1.Visible && panel2.Visible && !panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 9) / 2;
                    panel2.Width = (flowLPScroll.Width - 9) / 2;
                    panel3.Width = (flowLPScroll.Width - 6);
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
                if (panel1.Visible && panel2.Visible && !panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 6);
                    panel2.Width = (flowLPScroll.Width - 6);
                    panel3.Width = (flowLPScroll.Width - 6);
                }

                if (panel1.Visible && panel2.Visible && panel3.Visible)
                {
                    panel1.Width = (flowLPScroll.Width - 6);
                    panel2.Width = (flowLPScroll.Width - 6);
                    panel3.Width = (flowLPScroll.Width - 6);
                }
            }


            if (panel1.Visible && !panel2.Visible && !panel3.Visible && flowLPScroll.Width <= AnchoVisibleScroll)
                panel1.Width = AnchoVisibleScroll;

            if (panel1.Visible && panel2.Visible && !panel3.Visible && flowLPScroll.Width <= AnchoVisibleScroll)
            {
                panel1.Width = AnchoVisibleScroll;
                panel2.Width = AnchoVisibleScroll;
            }

            if (panel1.Visible && panel2.Visible && panel3.Visible && flowLPScroll.Width <= AnchoVisibleScroll)
            {
                panel1.Width = AnchoVisibleScroll;
                panel2.Width = AnchoVisibleScroll;
                panel3.Width = AnchoVisibleScroll;
            }

            //Se ajusta el ancho del panel central           
            if (panel4.Visible && flowLPScroll.Width <= AnchoVisibleScroll)
            {
                panel4.Width = (AnchoVisibleScroll);
            }
            else
            {
                panel4.Width = (flowLPScroll.Width - 4);
            }

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
            if (panel5.Visible && flowLPScroll.Width <= AnchoVisibleScroll)
            {
                //De este ancho (AnchoVisibleScroll) no se reducirá más el panel5
                panel5.Width = (AnchoVisibleScroll);
            }
            else
            {
                //if (this.flag == 1)
                //{
                //    this.flag = 2;
                //    int porcentaje = Convert.ToInt32(Math.Floor(((flowLPScroll.Width * 49.6) / 100) + flowLPScroll.Width));
                //    panel5.Width = porcentaje - 1;
                //}

                //if (this.flag == 2)
                //{
                panel5.Width = flowLPScroll.Width - 4;
                //}

            }

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
            //MessageBox.Show("1");
        }


        public Frm1x1Grid()
        {
            InitializeComponent();

        }


    }
}
