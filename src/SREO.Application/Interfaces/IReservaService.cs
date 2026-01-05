using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SREO.Application.DTOs;

namespace SREO.Application.Interfaces
{
    public interface IReservaService
    {
        Task<ReservaDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ReservaDto>> GetAllAsync();
        Task<IEnumerable<ReservaDto>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<IEnumerable<ReservaDto>> GetPendientesAsync();
        Task<ReservaDto> CreateAsync(CreateReservaDto createReservaDto);
        Task UpdateEstadoAsync(Guid id, UpdateReservaDto updateReservaDto);
        Task DeleteAsync(Guid id);
    }
}