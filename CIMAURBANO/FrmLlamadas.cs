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
    public partial class FrmLlamadas : Form
    {
        public FrmLlamadas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("GETPSUJETO", "IDGETPSUJETO");
            FrmDivxClasif.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SE PASA EL NOMBRE DE LA TABLA Y SU CAMPO ID PRINCIPAL
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("GETPENTIDAD", "IDGETPENTIDAD");
            FrmDivxClasif.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SE PASA EL NOMBRE DE LA TABLA QUE SE VA A TRABAJAR Y SU CAMPO ID PRINCIPAL
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("GEAREADEPART", "IDAREADEPARTAMENTAL");
            FrmDivxClasif.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("NOTPFAMILIAR", "IDNOTPFAMILIAR");
            FrmDivxClasif.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("NOTPINDICADOR", "IDNOTPINDICADOR");
            FrmDivxClasif.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("VETPCLIENTE", "IDVETPCLIENTE");
            FrmDivxClasif.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("NOTPASIGNACION", "IDNOTPASIGNACION");
            FrmDivxClasif.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("NOTPJUSTIFICATIVO", "IDNOTPJUSTIFICATIVO");
            FrmDivxClasif.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FrmIGEgrdx1 FrmDivxClasif = new FrmIGEgrdx1("NOTPDEDUCCION", "IDNOTPDEDUCCION");
            FrmDivxClasif.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FrmDGEgrdx1 FrmDivxClasif = new FrmDGEgrdx1("GECLASEDOCUMENTO", "IDCLASEDOCUMENTO");
            FrmDivxClasif.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmDGEgrdx1 FrmDivxClasif = new FrmDGEgrdx1("GETPCAUSAL", "IDGETPCAUSAL");
            FrmDivxClasif.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FrmDGEgrdx1a FrmDivxClasif = new FrmDGEgrdx1a("NOCIA_CARGO", "IDNOCIA_CARGO");
            FrmDivxClasif.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FrmDGEgrdx1b FrmDivxClasif = new FrmDGEgrdx1b("FITPPOLIZA", "IDFITPPOLIZA");
            FrmDivxClasif.Show();
        }

        
        private void button15_Click(object sender, EventArgs e)
        {
            FrmDivxClasif3 FrmDivxClasif = new FrmDivxClasif3("ORAREANEG", "IDORAREANEGOCIO");
            FrmDivxClasif.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FrmDivxClasif2 FrmDivxClasif = new FrmDivxClasif2("ORDIVNEG", "IDORAREA_DIVNEG");
            FrmDivxClasif.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FrmDivxClasif2 FrmDivxClasif = new FrmDivxClasif2("GEDETALLECAUSAL", "IDGEDETALLECAUSAL");
            FrmDivxClasif.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FrmDivxClasif4 FrmDivxClasif = new FrmDivxClasif4("NODET_TPASIGNACION", "IDNODET_TPASIGNACION");
            FrmDivxClasif.Show();
        }



        private void button14_Click(object sender, EventArgs e)
        {
            //FrmTapControl FrmDivxClasif = new FrmTapControl();
            //FrmDivxClasif.Show();
            FrmEmpleado FrmEmpleado = new FrmEmpleado();
            FrmEmpleado.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            FrmDivxClasif5 FrmDivxClasif = new FrmDivxClasif5("NODETPASIG_CARGO", "IDNODETPASIG_CARGO");
            FrmDivxClasif.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Frm1x1Grid FrmDivxClasif = new Frm1x1Grid();
            FrmDivxClasif.Show();
        }


        //RECONSTRUIDOS
        private void button23_Click(object sender, EventArgs e)
        {
            //FrmDivNuevo FrmDivxClasif = new FrmDivNuevo("GETPSUJETO", "IDGETPSUJETO");
            //FrmDivxClasif.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            FrmCarga FrmCargas = new FrmCarga();
            FrmCargas.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //FrmFiltro FrmDivxClasif = new FrmFiltro();
            //FrmDivxClasif.Show();
        }
    }
}
