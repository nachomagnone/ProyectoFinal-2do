using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    internal class Conexion
    {
        internal static string Cnn(EntidadesCompartidas.Usuario pUsu = null)
        {

            if (pUsu == null)
                //conexion estandar

                //Nico 
                // return "Data Source =DESKTOP-V8DJHSA; Initial Catalog = Proyecto; Integrated Security = true";

                //Nacho
                return "Data Source =NACHO; Initial Catalog = Proyecto; Integrated Security = true";

                //Robert
                //return "Data Source =DESKTOP-C8GSQO4; Initial Catalog = Proyecto; Integrated Security = true";
            else
                //si vino por parametro uso el usuario que vino por parametro 

                //Nico
                 //return "Data Source =DESKTOP-V8DJHSA; Initial Catalog = Proyecto; User=" + pUsu.UsuLog + "; Password='" + pUsu.Contrasena + "'";

                //Nacho
               return "Data Source =NACHO; Initial Catalog = Proyecto; User=" + pUsu.UsuLog + "; Password='" + pUsu.Contrasena + "'";

                //Robert
                //return "Data Source =DESKTOP-C8GSQO4; Initial Catalog = Proyecto; User=" + pUsu.UsuLog + "; Password='" + pUsu.Contrasena + "'";
        }

    }
}
