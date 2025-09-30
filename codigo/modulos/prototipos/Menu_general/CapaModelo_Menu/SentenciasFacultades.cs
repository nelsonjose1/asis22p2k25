using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;
using CapaModelo_Menu;
namespace CapaModelo
{
    public class SentenciasFacultades
    {
        private readonly Cls_ConexionBD _cxn = new Cls_ConexionBD();

        public DataTable Listar()
        {
            using (var cn = _cxn.conexion())
            using (var da = new OdbcDataAdapter("SELECT codigo_facultad, nombre_facultad, estatus_facultad FROM facultades ORDER BY codigo_facultad;", cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool Insertar(Facultad f)
        {
            const string sql = @"INSERT INTO facultades(codigo_facultad, nombre_facultad, estatus_facultad)
                                 VALUES(?,?,?);";
            using (var cn = _cxn.conexion())
            using (var cmd = new OdbcCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", f.Codigo);
                cmd.Parameters.AddWithValue("@p2", f.Nombre);
                cmd.Parameters.AddWithValue("@p3", f.Estatus);
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Actualizar(Facultad f)
        {
            const string sql = @"UPDATE facultades
                                 SET nombre_facultad = ?, estatus_facultad = ?
                                 WHERE codigo_facultad = ?;";
            using (var cn = _cxn.conexion())
            using (var cmd = new OdbcCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", f.Nombre);
                cmd.Parameters.AddWithValue("@p2", f.Estatus);
                cmd.Parameters.AddWithValue("@p3", f.Codigo);
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Eliminar(string codigo)
        {
            const string sql = "DELETE FROM facultades WHERE codigo_facultad = ?;";
            using (var cn = _cxn.conexion())
            using (var cmd = new OdbcCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", codigo);
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public Facultad BuscarPorCodigo(string codigo)
        {
            const string sql = @"SELECT codigo_facultad, nombre_facultad, estatus_facultad
                                 FROM facultades WHERE codigo_facultad = ?;";
            using (var cn = _cxn.conexion())
            using (var cmd = new OdbcCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@p1", codigo);
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Facultad
                    {
                        Codigo = rd.GetString(0),
                        Nombre = rd.IsDBNull(1) ? "" : rd.GetString(1),
                        Estatus = rd.IsDBNull(2) ? "" : rd.GetString(2)
                    };
                }
            }
        }
    }
}

