using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SREO.Domain.Entities;

namespace SREO.Domain.Interfaces
{
    public interface IEspacioRepository
    {
        Task<Espacio> GetByIdAsync(Guid id);
        Task<IEnumerable<Espacio>> GetAllAsync();
        Task<IEnumerable<Espacio>> GetDisponiblesAsync();
        Task AddAsync(Espacio espacio);
        Task UpdateAsync(Espacio espacio);
        Task DeleteAsync(Guid id);
    }
}