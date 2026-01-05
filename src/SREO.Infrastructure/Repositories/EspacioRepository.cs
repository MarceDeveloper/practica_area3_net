using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SREO.Domain.Entities;
using SREO.Domain.Interfaces;

namespace SREO.Infrastructure.Repositories
{
    public class EspacioRepository : IEspacioRepository
    {
        private static readonly List<Espacio> _espacios = new();

        public async Task<Espacio> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_espacios.FirstOrDefault(e => e.Id == id));
        }

        public async Task<IEnumerable<Espacio>> GetAllAsync()
        {
            return await Task.FromResult(_espacios.AsEnumerable());
        }

        public async Task<IEnumerable<Espacio>> GetDisponiblesAsync()
        {
            return await Task.FromResult(_espacios.Where(e => e.Disponible));
        }

        public async Task AddAsync(Espacio espacio)
        {
            _espacios.Add(espacio);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Espacio espacio)
        {
            var existing = _espacios.FirstOrDefault(e => e.Id == espacio.Id);
            if (existing != null)
            {
                _espacios.Remove(existing);
                _espacios.Add(espacio);
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var espacio = _espacios.FirstOrDefault(e => e.Id == id);
            if (espacio != null)
            {
                _espacios.Remove(espacio);
            }
            await Task.CompletedTask;
        }
    }
}