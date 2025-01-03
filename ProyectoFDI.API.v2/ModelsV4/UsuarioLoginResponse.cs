namespace ProyectoFDI.API.v2.ModelsV4
{
    public class UsuarioLoginResponse
    {
        public int IdUsu { get; set; }

        public string? NombreUsu { get; set; }

        public string? TokenUsu { get; set; }

        //public DateTime? FechaCreacion { get; set; }

        public string? RolesUsu { get; set; }

        public bool? ActivoUsu { get; set; }
    }
}
