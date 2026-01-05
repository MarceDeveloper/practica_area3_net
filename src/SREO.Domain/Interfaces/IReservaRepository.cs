using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SREO.Domain.Entities;

namespace SREO.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task<Reserva> GetByIdAsync(Guid id);
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<IEnumerable<Reserva>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<IEnumerable<Reserva>> GetPendientesAsync();
        Task<IEnumerable<Reserva>> GetByEspacioAndFechaAsync(Guid espacioId, DateTime fechaInicio, DateTime fechaFin);
        Task AddAsync(Reserva reserva);
        Task UpdateAsync(Reserva reserva);
        Task DeleteAsync(Guid id);
    }
}