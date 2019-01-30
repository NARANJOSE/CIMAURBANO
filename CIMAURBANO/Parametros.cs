using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMAURBANO
{
    class Parametros
    {
        private string cNombre;

        public string CNombre
        {
            get
            {
                return cNombre;
            }

            set
            {
                cNombre = value;
            }
        }

        private string cValor;

        public string CValor
        {
            get
            {
                return cValor;
            }

            set
            {
                cValor = value;
            }
        }

        public Parametros(string cN, string cV)
        {
            this.cNombre = cN;
            this.cValor = cV;
        }

    }
}
