using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CIMAURBANO
{
    class Conexion
    {

        //SqlConnection con = new SqlConnection("Data Source=DESKTOP-6JAKM3A\\SA; initial catalog = CIMAURBANO; user id = SA; password = 123; integrated security=true");
        SqlConnection con = new SqlConnection("Network Library=DBMSSOCN;Data Source=192.168.226.115,1433; initial catalog = CIMAURBANO1; user id = SA; password = 123; ");

        private SqlCommandBuilder cmb;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand comando;
        public DataTable tabla;


        public bool Abrir()
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    return true;
                }
            }
            catch (System.IO.IOException e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        public bool Cerrar()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    return true;
                }
            }
            catch (System.IO.IOException e)
            {
                MessageBox.Show(e.Message);
            }
            return false;

        }

        public void ConsultaSQL(string cSQL, string cDataTable)
        {
            Abrir();
            ds.Tables.Clear();
            da = new SqlDataAdapter(cSQL, con);
            cmb = new SqlCommandBuilder(da);
            tabla = new DataTable(cDataTable);
            da.Fill(tabla);
            Cerrar();
        }

        public void ConsultaSimple(string cTabla, string cCampo, string cValor, string cDataTable)
        {
            Abrir();
            ds.Tables.Clear();
            string cSQL = "SELECT * FROM " + cTabla + " WHERE " + cCampo + "=" + cValor;
            da = new SqlDataAdapter(cSQL, con);
            cmb = new SqlCommandBuilder(da);
            tabla = new DataTable(cDataTable);
            da.Fill(tabla);
            Cerrar();
        }

        public bool QueryIDU(string queryPersonalizado, string cTabla)  //Query InsertDeleteUpdate. cTabla. Así se obliga a pasar la tabla afectada en caso de Insertar, Actualizar o Eliminar
        {
            //Se actualiza el ID del usuario para la tabla afectada. Si hay varias tablas afectadas; se llama entonces a ActualizarIdUsuario desde el módulo.
            ActualizarIdUsuario(cTabla);

            Abrir();
            try
            {
                comando = new SqlCommand(queryPersonalizado, con);
                int i = comando.ExecuteNonQuery();
                Cerrar();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Error(ex);
                return false;
            }
            finally
            {
                Cerrar();
            }

        }

        public bool Insertar(string cTabla, string cCampos, string cValores) //cTabla. Se toma la tabla para de una vez ActualizarIdUsuario
        {
            //Se actualiza el ID del usuario para la tabla afectada. Si hay varias tablas afectadas; se llama entonces a ActualizarIdUsuario desde el módulo.
            ActualizarIdUsuario(cTabla);

            Abrir();
            try
            {
                string cSQL = "INSERT INTO  " + cTabla + " ( " + cCampos + " ) VALUES ( " + cValores + " ) ";
                comando = new SqlCommand(cSQL, con);
                int i = comando.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Error(ex);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }

        public bool Eliminar(string cTabla, string cCondicion) //cTabla. Se toma la tabla para de una vez ActualizarIdUsuario
        {
            //Se actualiza el ID del usuario para la tabla afectada. Si hay varias tablas afectadas; se llama entonces a ActualizarIdUsuario desde el módulo.
            ActualizarIdUsuario(cTabla);

            Abrir();
            try
            {
                string elimina = "DELETE FROM " + cTabla + " WHERE " + cCondicion;
                comando = new SqlCommand(elimina, con);
                int i = comando.ExecuteNonQuery();
                Cerrar();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Error(ex);
                return false;
            }
            finally
            {
                Cerrar();
            }

        }

        public bool Actualizar(string cTabla, string cCampos, string cCondicion) //cTabla. Se toma la tabla para de una vez ActualizarIdUsuario 
        {
            //Se actualiza el ID del usuario para la tabla afectada. Si hay varias tablas afectadas; se llama entonces a ActualizarIdUsuario desde el módulo.
            ActualizarIdUsuario(cTabla);

            Abrir();
            try
            {
                string actualiza = "UPDATE " + cTabla + " SET " + cCampos + " WHERE " + cCondicion;
                comando = new SqlCommand(actualiza, con);
                int i = comando.ExecuteNonQuery();

                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Error(ex);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }

        public bool EjecutaSp(string cNombreSP, string cNombre, int nID, string cTabla) //cTabla. Así se obliga a pasar la tabla afectada.
        {
            //Se actualiza el ID del usuario para la tabla afectada. Si hay varias tablas afectadas; se llama entonces a ActualizarIdUsuario desde el módulo.
            ActualizarIdUsuario(cTabla);

            Abrir();
            try
            {
                comando = new SqlCommand(cNombreSP, con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@provId", nID);
                comando.Parameters.AddWithValue("@provNombre", cNombre);

                int i = comando.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Error(ex);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }


        public bool EjecutarSP_SQL(string cNombreSP, Parametros[] pDatos, string cDataTable, string cTabla) //cTabla. Así se obliga a pasar la tabla afectada en caso de Insertar, Actualizar o Eliminar
        {
            //Se actualiza el ID del usuario para la tabla afectada. Si hay varias tablas afectadas; se llama entonces a ActualizarIdUsuario desde el módulo.
            ActualizarIdUsuario(cTabla);

            if (String.IsNullOrEmpty(cNombreSP))
            {
                return false;
            }

            try
            {
                Abrir();
                comando = new SqlCommand();
                comando.Connection = con;
                comando.CommandText = cNombreSP;
                comando.CommandType = CommandType.StoredProcedure;

                if (pDatos != null && pDatos.Length != 0)
                {
                    for (int i = 0; i < pDatos.Length; i++)
                    {
                        comando.Parameters.AddWithValue(pDatos[i].CNombre, pDatos[i].CValor);
                    }
                }

                if (String.IsNullOrEmpty(cDataTable))
                {
                    int i = comando.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (pDatos != null && pDatos.Length != 0)           //Caso para un SP (Stored Procedure)
                    {
                        da = new SqlDataAdapter(comando);
                    }
                    else
                    {                                                   //Caso para una VW (View)                                             
                        da = new SqlDataAdapter(cNombreSP, con);
                    }
                    tabla = new DataTable(cDataTable);
                    da.Fill(tabla);
                    return true;
                }

            }
            catch (SqlException ex)
            {
                Error(ex);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }


        public DataTable RetornarTabla(OleDbDataAdapter cAdaptador, string cDataTable)
        {
            //ds.Tables.Clear();
            //da = new SqlDataAdapter(sql, con);
            //cmb = new SqlCommandBuilder(da);
            //da.Fill(ds, tabla);
            Abrir();
            DataTable dt = new DataTable(cDataTable);
            cAdaptador.Fill(dt);
            Cerrar();

            return dt;
        }


        public int EjecutarSP_RUD(string cNombreSP, Parametros[] pDatos, string cDataTable, string cTabla) //cTabla. Así se obliga a pasar la tabla afectada en caso de Insertar, Actualizar o Eliminar
        {
            //Se actualiza el ID del usuario para la tabla afectada. Si hay varias tablas afectadas; se llama entonces a ActualizarIdUsuario desde el módulo.
            ActualizarIdUsuario(cTabla);

            if (String.IsNullOrEmpty(cNombreSP))
            {
                return 0;
            }

            try
            {
                Abrir();
                comando = new SqlCommand();
                comando.Connection = con;
                comando.CommandText = cNombreSP;
                comando.CommandType = CommandType.StoredProcedure;

                if (pDatos != null && pDatos.Length != 0)
                {
                    for (int i = 0; i < pDatos.Length; i++)
                    {
                        comando.Parameters.AddWithValue(pDatos[i].CNombre, pDatos[i].CValor);
                    }
                }

                if (String.IsNullOrEmpty(cDataTable))
                {
                    int i = comando.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return i;
                    }
                    else
                    {
                        return i;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException ex)
            {
                Error(ex);
                return 0;
            }
            finally
            {
                Cerrar();
            }
        }


        public void ActualizarIdUsuario(string cTabla)
        {
            //Esta función actualiza el ID del usuario que actualmente gestiona la cTabla X en la configuración de auditoría.
            Abrir();
            try
            {
                string actualiza = "UPDATE ORCONFAUD SET IDUSUARIO= " + Program.globalIdUsuario + " WHERE RTRIM(LTRIM(TXNBTABLA))='" + cTabla + "' ";
                comando = new SqlCommand(actualiza, con);
                int i = comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Error(ex);
            }
            finally
            {
                Cerrar();
            }
        }


        private void Error(SqlException Error)
        {
            //MessageBox.Show(Error.Message);
            int nError = Error.Number;

            switch (nError)
            {
                case 2601:
                    MessageBox.Show("Error de Integridad de Datos. \n\nNo puede ingresar Datos Duplicados a la Base de Datos.\n\nRevise...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 8114:
                    MessageBox.Show("Error de conversión de Datos. Del tipo nvarchar a numeric o algún otro tipo. Revise...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 547:
                    MessageBox.Show("La Clave Principal tiene registros relacionados. Revise...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 156:
                    MessageBox.Show("Error SQL. Comuníquese con TI", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 515:
                    MessageBox.Show("Campo requerido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 207:
                    MessageBox.Show("El campo no existe o mal escrito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 208:
                    MessageBox.Show("La tabla no existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 109:
                    MessageBox.Show("Más valores que columnas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 110:
                    MessageBox.Show("El número de valores debe concordar con el número de columnas en el INSERT", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }





    }
}
