using System;

namespace SREO.Domain.Entities
{
    public class Espacio
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public TipoEspacio Tipo { get; set; }
        public int Capacidad { get; set; }
        public string Caracteristicas { get; set; }
        public bool Disponible { get; set; }
    }

    public enum TipoEspacio
    {
        SalaReuniones,
        PuestoFlexible
    }
}