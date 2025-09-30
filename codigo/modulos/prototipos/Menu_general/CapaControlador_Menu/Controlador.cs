using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaModelo;
using CapaModelo_Menu;

namespace CapaControlador
{
    public class ControladorFacultades
    {
        private readonly SentenciasFacultades _dao = new SentenciasFacultades();

        public DataTable ObtenerLista() => _dao.Listar();

        public bool Guardar(string codigo, string nombre, string estatus, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(codigo)) { error = "Código requerido."; return false; }
            if (codigo.Length > 5) { error = "Código máximo 5 caracteres."; return false; }
            if (string.IsNullOrWhiteSpace(estatus) || estatus.Length != 1) { error = "Estatus debe ser 1 carácter (A/I)."; return false; }

            try { return _dao.Insertar(new Facultad { Codigo = codigo.Trim(), Nombre = nombre?.Trim(), Estatus = estatus.Trim().ToUpper() }); }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        public bool Actualizar(string codigo, string nombre, string estatus, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(codigo)) { error = "Código requerido."; return false; }
            if (string.IsNullOrWhiteSpace(estatus) || estatus.Length != 1) { error = "Estatus debe ser 1 carácter (A/I)."; return false; }

            try { return _dao.Actualizar(new Facultad { Codigo = codigo.Trim(), Nombre = nombre?.Trim(), Estatus = estatus.Trim().ToUpper() }); }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        public bool Eliminar(string codigo, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(codigo)) { error = "Código requerido."; return false; }
            try { return _dao.Eliminar(codigo.Trim()); }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        public Facultad Buscar(string codigo) => string.IsNullOrWhiteSpace(codigo) ? null : _dao.BuscarPorCodigo(codigo.Trim());
    }
}
