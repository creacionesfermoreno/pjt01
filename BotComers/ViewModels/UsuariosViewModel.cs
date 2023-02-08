using System.Collections.Generic;

namespace BotComers.ViewModels
{
    public class UsuariosViewModel
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }
        public string ConfimarContraseña { get; set; }

        public List<UsuariosViewModel> ListaUsuarios { get; set; }


    }
}