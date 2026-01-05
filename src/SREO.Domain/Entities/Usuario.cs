using System;

namespace SREO.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string ContrasenaHash { get; set; }
        public RolUsuario Rol { get; set; }
    }

    public enum RolUsuario
    {
        Miembro,
        Administrador
    }
}