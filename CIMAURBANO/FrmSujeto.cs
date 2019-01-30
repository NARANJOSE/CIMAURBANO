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
    public partial class FrmSujeto : Form
    {
        private int AlturaBase = 440;

        //Define qué ancho será visible el Scroll
        private int AnchoVisibleScroll = 400;

        //Define el ancho Mayor
        private int AnchoMayor = 1077;

        //Define el ancho Medio
        private int AnchoMedio = 722;

        private void FrmSujeto_Resize(object sender, EventArgs e)
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

                if ((panel1.Visible && panel2.Visible && panel3.Visible) || (panel1.Visible && panel2.Visible && !panel3.Visible))
                {
                    panel1.Width = (flowLPScroll.Width - 9) / 2;
                    panel2.Width = (flowLPScroll.Width - 9) / 2;
                    panel3.Width = (flowLPScroll.Width - 6);
                }
            }

            if (flowLPScroll.Width < AnchoMedio)
            {
                if ((panel1.Visible && panel2.Visible && panel3.Visible) || (panel1.Visible && panel2.Visible && !panel3.Visible))
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

        Conexion con = new Conexion();
        private Parametros[] parametros;

        public FrmSujeto()
        {
            InitializeComponent();

            flowLPScroll.BackColor = Color.White;
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            //panel1.BackColor = Color.White;

            con.EjecutarSP_SQL("SELECT * FROM ORPAIS ", null, "dtSelect", "ORPAIS");
            cboPais.DataSource = con.tabla;
            //con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

            cboPais.DisplayMember = "NOMBRE";
            cboPais.ValueMember = "IDORPAIS";

            cambiarTipo();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void radioBSujeto_CheckedChanged(object sender, EventArgs e)
        {
            cambiarTipo();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (txtRut.Text == "" || txtNombre.Text == "" || cboTipo.Text.Trim() == "")
            {
                MessageBox.Show("Algunos datos son requeridos. Revise...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRut.Focus();
                return;
            }

            if (cboECivil.Text.Trim() == "" || cboTipoI.Text.Trim() == "")
            {
                MessageBox.Show("Seleccione Edo. Civil o Tipo Identificación.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboECivil.Focus();
                return;
            }

            procesar();
        }

        private void procesar()
        {

            DateTime Hoy = DateTime.Now;
            string dActual = Hoy.ToString("yyyy-MM-dd H:mm:ss");

            bool bResult = false;
            if (radioBSujeto.Checked)
            {
                bResult = con.Insertar("GESUJETO", "IDORPAIS,IDGETPSUJETO,NOMBRE,APELLIDOPATERNO,APELLIDOMATERNO,RUT,DIRECCION,MAEDOCIVIL,FENACIMIENTO,CORREOELECPPAL,CORREOELECSEC,DIRREFDIGITAL,TPIDENTIFICACION,NUIDENTIFICACION,TELEFONOMOVIL,TELEFONOAUX1,TELEFONOAUX2,FEREGISTRO,FEDESACTIVACION,OBSERVACION",
                   " " + cboPais.SelectedValue.ToString() + "," + cboTipo.SelectedValue.ToString() + ",'" + txtNombre.Text.Trim() + "','" + txtApeP.Text.Trim() + "','" + txtApeM.Text.Trim() + "','"
                + txtRut.Text.Trim() + "', '" + txtDireccion.Text.Trim() + "' , '" + cboECivil.Text.Trim().Substring(0, 1) + "', '" +
                Convert.ToDateTime(dateTimeFNac.Value.ToShortDateString()).ToString("yyyy-MM-dd H:mm:ss") + "' , '" + txtCorreoP.Text.Trim() + "' , '" + txtCorreoA.Text.Trim() + "' ,'" +
                txtDirDigital.Text.Trim() + "' , '" + cboTipoI.Text.Trim().Substring(0, 1) + "' , '" + txtNumeroI.Text.Trim() + "' , '" + txtMovil.Text.Trim() + "' , '" +
                txtTelf1.Text.Trim() + "' , '" + txtTelf2.Text.Trim() + "' , '" + dActual + "' , '" +
                Convert.ToDateTime(dateTimeFDes.Value.ToShortDateString()).ToString("yyyy-MM-dd H:mm:ss") + "', '" + txtObservacion.Text.Trim() + "' ");
            }
            else
            {
                bResult = con.Insertar("GESUJETO", "IDORPAIS,IDGEENTIDAD,NOMBRE,APELLIDOPATERNO,APELLIDOMATERNO,RUT,DIRECCION,MAEDOCIVIL,FENACIMIENTO,CORREOELECPPAL,CORREOELECSEC,DIRREFDIGITAL,TPIDENTIFICACION,NUIDENTIFICACION,TELEFONOMOVIL,TELEFONOAUX1,TELEFONOAUX2,FEREGISTRO,FEDESACTIVACION,OBSERVACION",
                  " " + cboPais.SelectedValue.ToString() + "," + cboTipo.SelectedValue.ToString() + ",'" + txtNombre.Text.Trim() + "','" + txtApeP.Text.Trim() + "','" + txtApeM.Text.Trim() + "','"
                + txtRut.Text.Trim() + "', '" + txtDireccion.Text.Trim() + "' , '" + cboECivil.Text.Trim().Substring(0, 1) + "', '" +
                Convert.ToDateTime(dateTimeFNac.Value.ToShortDateString()).ToString("yyyy-MM-dd H:mm:ss") + "' , '" + txtCorreoP.Text.Trim() + "' , '" + txtCorreoA.Text.Trim() + "' ,'" +
                txtDirDigital.Text.Trim() + "' , '" + cboTipoI.Text.Trim().Substring(0, 1) + "' , '" + txtNumeroI.Text.Trim() + "' , '" + txtMovil.Text.Trim() + "' , '" +
                txtTelf1.Text.Trim() + "' , '" + txtTelf2.Text.Trim() + "' , '" + dActual + "' , '" +
                Convert.ToDateTime(dateTimeFDes.Value.ToShortDateString()).ToString("yyyy-MM-dd H:mm:ss") + "', '" + txtObservacion.Text.Trim() + "' ");
            }

            //if (bResult)
            //{
            //    string cUltimoID;
            //    con.ConsultaSQL("SELECT MAX(IDGEDIVXCLASIF)  FROM  GEDIVXCLASIF ", "dtUltimoId");

            //    cUltimoID = con.tabla.Rows[0][0].ToString().Trim();
            //    con.Insertar("GETPSUJETO", "IDGEDIVXCLASIF", cUltimoID);

            //    MessageBox.Show("Los datos se han procesado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    limpiar();
            //}
            //else
            //{
            //    MessageBox.Show("Problemas al agregar el registro. Revise...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //txtDescripcion.Focus();
        }

        private void limpiar()
        {
            //txtDescripcion.Text = "";
            //txtObservacion.Text = "";
            //txtCodigo.Text = "";
        }

        private void cambiarTipo()
        {
            if (radioBSujeto.Checked)
            {
                con.EjecutarSP_SQL("SELECT  GD.*, GT.IDGETPSUJETO FROM GEDIVXCLASIF GD, GETPSUJETO GT WHERE GD.IDGEDIVXCLASIF=GT.IDGEDIVXCLASIF AND GD.IDGEDIVXCLASIF IN (SELECT IDGEDIVXCLASIF FROM GETPSUJETO) ", null, "dtSelect", "GEDIVXCLASIF");
                cboTipo.DataSource = con.tabla;
                con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                cboTipo.DisplayMember = "NOMBRE";
                cboTipo.ValueMember = "IDGETPSUJETO";                
            }
            else
            {
                con.EjecutarSP_SQL("SELECT  GD.*, GT.IDGETPENTIDAD FROM GEDIVXCLASIF GD, GETPENTIDAD GT WHERE GD.IDGEDIVXCLASIF=GT.IDGEDIVXCLASIF AND GD.IDGEDIVXCLASIF IN (SELECT IDGEDIVXCLASIF FROM GETPENTIDAD) ", null, "dtSelect", "GEDIVXCLASIF");
                cboTipo.DataSource = con.tabla;
                con.tabla.Rows.InsertAt(con.tabla.NewRow(), 0);

                cboTipo.DisplayMember = "NOMBRE";
                cboTipo.ValueMember = "IDGETPENTIDAD";
            }
            cboTipo.SelectedIndex = 0;
        }

       
    }
}
