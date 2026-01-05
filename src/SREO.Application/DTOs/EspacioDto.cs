using System;
using SREO.Domain.Entities;

namespace SREO.Application.DTOs
{
    public class EspacioDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public TipoEspacio Tipo { get; set; }
        public int Capacidad { get; set; }
        public string Caracteristicas { get; set; }
        public bool Disponible { get; set; }
    }
}