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
    public partial class Frm3x3Grid : Form
    {
        private void Frm3x3Grid_Resize(object sender, EventArgs e)
        {
            if (this.Width<780)
            {
                tableLayoutPanel3.Height =200;

                panel4.Height = tableLP1.Height;
                panel5.Height = tableLP1.Height;
                panel6.Height = tableLP1.Height;

                panel4.Controls.Add(tableLP1);
                panel5.Controls.Add(tableLP2);
                panel6.Controls.Add(tableLP3);
            }
            else
            {
                tableLayoutPanel3.Height  = tableLP1.Height;

                panel1.Controls.Add(tableLP1);
                panel2.Controls.Add(tableLP2);
                panel3.Controls.Add(tableLP3);

                panel4.Height = 5;
                panel5.Height = 5;
                panel6.Height = 5;
            }
        }

        public Frm3x3Grid()
        {
            InitializeComponent();
        }

        
    }
}
