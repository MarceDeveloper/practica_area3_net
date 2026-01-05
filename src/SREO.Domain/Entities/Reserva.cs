using System;

namespace SREO.Domain.Entities
{
    public class Reserva
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid EspacioId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoReserva Estado { get; set; }
        public string MotivoRechazo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public enum EstadoReserva
    {
        Pendiente,
        Aprobada,
        Rechazada
    }
}