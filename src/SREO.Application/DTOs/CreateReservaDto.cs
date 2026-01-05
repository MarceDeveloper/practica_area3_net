using System;

namespace SREO.Application.DTOs
{
    public class CreateReservaDto
    {
        public Guid UsuarioId { get; set; }
        public Guid EspacioId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}