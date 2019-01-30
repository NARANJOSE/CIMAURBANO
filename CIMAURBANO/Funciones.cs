using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Globalization;


namespace CIMAURBANO
{
    class Funciones
    {
        CultureInfo culture;

        Conexion con = new Conexion();
        private Parametros[] parametros;

        public Boolean Validar_Formato(string formato, string valor)
        {
            bool retorno;
            retorno = true;
            switch (formato)
            {

                case "N":

                    String cadena = valor;
                    foreach (char car in cadena)
                    {
                        if (car < '0' || car > '9')
                        {
                            retorno = false;
                            break;
                        }
                    }
                    break;
            }
            return retorno;
        }

        public Boolean Dependiente(string tablaDependiente, string campo, string valor)
        {
            //Esta función recibe la tabla donde se buscará si el regitro existe en ella
            bool retorno;
            retorno = false;

            con.ConsultaSimple(tablaDependiente, campo, valor, tablaDependiente);
            if (con.tabla.Rows.Count > 0)
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }

        public Boolean DependienteCaso(string tablaPrincipal, string tablaDependiente, string campo, string valor, string caso)
        {
            //Según el caso la función realiza validaciones de: 
            //Activo: Este caso valida si el campo está activo y si se está usando en otra tabla, no se puede desactivar

            bool retorno;
            retorno = false;
            switch (caso)
            {
                //Este caso recibe la tabla (tablaPrincipal) cuyo campo principal es foráneo en la otra tablaDependiente
                case "activo":
                    switch (tablaPrincipal)
                    {
                        case "ORAREANEG":
                        case "GETPCAUSAL":
                        case "NOTPASIGNACION":
                            retorno = Dependiente(tablaDependiente, campo, valor);
                            if (retorno)
                            {
                                MessageBox.Show("El regitro permanecerá activo. Se está utlizando actualmente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                    }
                    break;
            }
            return retorno;
        }

        public void FormatoNumerico(object sender, EventArgs e, string evento, string caso)
        {
            //// El control TextBox ha tomado el foco.
            ////
            //// Referenciamos el control TextBox que ha
            //// desencadenado el evento.
            TextBox tb = (TextBox)sender;

            switch (evento)
            {
                case "Enter":
                    //// Mostramos en el control TextBox el valor
                    //// de la propiedad Tag sin formatear.
                    ////
                    tb.Text = Convert.ToString(tb.Tag);
                    //tb.Text = Convert.ToString(tb.Text);
                    break;
                case "Leave":
                    if (caso == "decimal")
                    {
                        // Primero verificamos si el valor se puede convertir a Decimal.
                        //
                        decimal numero = default(decimal);
                        bool bln = decimal.TryParse(tb.Text, out numero);

                        if ((!(bln)))
                        {
                            // No es un valor decimal válido; limpiamos el control.
                            tb.Clear();
                            return;
                        }

                        // En la propiedad Tag guardamos el valor con todos los decimales.
                        //
                        tb.Tag = numero;  //comentado

                        // Y acto seguido formateamos el valor
                        // a monetario con dos decimales.
                        //
                        //tb.Text = string.Format("{0:N2}", numero);

                        culture = CultureInfo.CreateSpecificCulture("es-ES");
                        tb.Text = numero.ToString("N", culture);
                    }

                    if (caso == "entero")
                    {
                        // Primero verificamos si el valor se puede convertir a Entero.
                        //
                        int numero = default(int);
                        bool bln = int.TryParse(tb.Text, out numero);

                        if ((!(bln)))
                        {
                            // No es un valor decimal válido; limpiamos el control.
                            tb.Clear();
                            return;
                        }

                        // En la propiedad Tag guardamos el valor con todos los decimales.
                        //
                        tb.Tag = numero;

                        // Y acto seguido formateamos el valor
                        // a monetario con dos decimales.
                        //
                        //tb.Text = string.Format("{0:N2}", numero);
                        tb.Text = numero.ToString();
                    }


                    break;
            }

        }

    }
}
