using SREO.Domain.Entities;

namespace SREO.Application.DTOs
{
    public class UpdateReservaDto
    {
        public EstadoReserva Estado { get; set; }
        public string MotivoRechazo { get; set; }
    }
}