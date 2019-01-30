using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIMAURBANO
{
    static class Program
    {
        //Variable global para el ID de Usuario
        public static string globalIdUsuario = "";

        public static string globalIdGrupo = "";
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();  
            Application.SetCompatibleTextRenderingDefault(false);

            globalIdUsuario = "0";
            globalIdGrupo = "0";

            //Application.Run(new FrmListar());

            //Application.Run(new Frm1x3Grid());
            //Application.Run(new Frm1x2Grid());
            //Application.Run(new Frm1x1Grid());
            //Application.Run(new FrmSoporte());
            //Application.Run(new FrmListado());
            //Application.Run(new Form4());
            //Application.Run(new FrmSujeto());
            //Application.Run(new FrmDivxClasif1("SUJETO"));
            Application.Run(new FrmLlamadas());
            //Application.Run(new FrmDivxClasif2("SUJETO"));
        }
    }
}
