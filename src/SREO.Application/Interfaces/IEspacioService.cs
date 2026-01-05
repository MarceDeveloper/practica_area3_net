using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SREO.Application.DTOs;

namespace SREO.Application.Interfaces
{
    public interface IEspacioService
    {
        Task<EspacioDto> GetByIdAsync(Guid id);
        Task<IEnumerable<EspacioDto>> GetAllAsync();
        Task<IEnumerable<EspacioDto>> GetDisponiblesAsync();
        Task<EspacioDto> CreateAsync(CreateEspacioDto createEspacioDto);
        Task UpdateAsync(Guid id, UpdateEspacioDto updateEspacioDto);
        Task DeleteAsync(Guid id);
    }
}