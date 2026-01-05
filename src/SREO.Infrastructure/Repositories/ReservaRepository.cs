using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SREO.Domain.Entities;
using SREO.Domain.Interfaces;

namespace SREO.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private static readonly List<Reserva> _reservas = new();

        public async Task<Reserva> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_reservas.FirstOrDefault(r => r.Id == id));
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await Task.FromResult(_reservas.AsEnumerable());
        }

        public async Task<IEnumerable<Reserva>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            return await Task.FromResult(_reservas.Where(r => r.UsuarioId == usuarioId));
        }

        public async Task<IEnumerable<Reserva>> GetPendientesAsync()
        {
            return await Task.FromResult(_reservas.Where(r => r.Estado == EstadoReserva.Pendiente));
        }

        public async Task<IEnumerable<Reserva>> GetByEspacioAndFechaAsync(Guid espacioId, DateTime fechaInicio, DateTime fechaFin)
        {
            return await Task.FromResult(_reservas.Where(r => r.EspacioId == espacioId &&
                ((r.FechaInicio <= fechaFin && r.FechaFin >= fechaInicio))));
        }

        public async Task AddAsync(Reserva reserva)
        {
            _reservas.Add(reserva);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Reserva reserva)
        {
            var existing = _reservas.FirstOrDefault(r => r.Id == reserva.Id);
            if (existing != null)
            {
                _reservas.Remove(existing);
                _reservas.Add(reserva);
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var reserva = _reservas.FirstOrDefault(r => r.Id == id);
            if (reserva != null)
            {
                _reservas.Remove(reserva);
            }
            await Task.CompletedTask;
        }
    }
}