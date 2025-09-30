using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace CapaModelo_Menu
{
    public class Cls_ConexionBD
    {
        private const string ConnStr = "DSN=segundoparcial2k25;DATABASE=bdasis22p2k25;OPTION=3;";
        public OdbcConnection conexion()
        {
            var cn = new OdbcConnection(ConnStr);
            cn.Open();
            return cn;
        }

    }
}

