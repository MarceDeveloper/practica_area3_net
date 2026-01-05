using System;
using SREO.Domain.Entities;

namespace SREO.Application.DTOs
{
    public class ReservaDto
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
}