using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SREO.Application.DTOs;
using SREO.Application.Interfaces;
using SREO.Domain.Entities;
using SREO.Domain.Interfaces;

namespace SREO.Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IEspacioRepository _espacioRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ReservaService(IReservaRepository reservaRepository, IEspacioRepository espacioRepository, IUsuarioRepository usuarioRepository)
        {
            _reservaRepository = reservaRepository;
            _espacioRepository = espacioRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ReservaDto> GetByIdAsync(Guid id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null) return null;

            return new ReservaDto
            {
                Id = reserva.Id,
                UsuarioId = reserva.UsuarioId,
                EspacioId = reserva.EspacioId,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                Estado = reserva.Estado,
                MotivoRechazo = reserva.MotivoRechazo,
                FechaCreacion = reserva.FechaCreacion
            };
        }

        public async Task<IEnumerable<ReservaDto>> GetAllAsync()
        {
            var reservas = await _reservaRepository.GetAllAsync();
            var reservaDtos = new List<ReservaDto>();

            foreach (var reserva in reservas)
            {
                reservaDtos.Add(new ReservaDto
                {
                    Id = reserva.Id,
                    UsuarioId = reserva.UsuarioId,
                    EspacioId = reserva.EspacioId,
                    FechaInicio = reserva.FechaInicio,
                    FechaFin = reserva.FechaFin,
                    Estado = reserva.Estado,
                    MotivoRechazo = reserva.MotivoRechazo,
                    FechaCreacion = reserva.FechaCreacion
                });
            }

            return reservaDtos;
        }

        public async Task<IEnumerable<ReservaDto>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            var reservas = await _reservaRepository.GetByUsuarioIdAsync(usuarioId);
            var reservaDtos = new List<ReservaDto>();

            foreach (var reserva in reservas)
            {
                reservaDtos.Add(new ReservaDto
                {
                    Id = reserva.Id,
                    UsuarioId = reserva.UsuarioId,
                    EspacioId = reserva.EspacioId,
                    FechaInicio = reserva.FechaInicio,
                    FechaFin = reserva.FechaFin,
                    Estado = reserva.Estado,
                    MotivoRechazo = reserva.MotivoRechazo,
                    FechaCreacion = reserva.FechaCreacion
                });
            }

            return reservaDtos;
        }

        public async Task<IEnumerable<ReservaDto>> GetPendientesAsync()
        {
            var reservas = await _reservaRepository.GetPendientesAsync();
            var reservaDtos = new List<ReservaDto>();

            foreach (var reserva in reservas)
            {
                reservaDtos.Add(new ReservaDto
                {
                    Id = reserva.Id,
                    UsuarioId = reserva.UsuarioId,
                    EspacioId = reserva.EspacioId,
                    FechaInicio = reserva.FechaInicio,
                    FechaFin = reserva.FechaFin,
                    Estado = reserva.Estado,
                    MotivoRechazo = reserva.MotivoRechazo,
                    FechaCreacion = reserva.FechaCreacion
                });
            }

            return reservaDtos;
        }

        public async Task<ReservaDto> CreateAsync(CreateReservaDto createReservaDto)
        {
            // Validate usuario exists
            var usuario = await _usuarioRepository.GetByIdAsync(createReservaDto.UsuarioId);
            if (usuario == null) throw new Exception("Usuario not found");

            // Validate espacio exists and is available
            var espacio = await _espacioRepository.GetByIdAsync(createReservaDto.EspacioId);
            if (espacio == null) throw new Exception("Espacio not found");
            if (!espacio.Disponible) throw new Exception("Espacio not available");

            // Check for conflicting reservations
            var conflictingReservas = await _reservaRepository.GetByEspacioAndFechaAsync(
                createReservaDto.EspacioId, createReservaDto.FechaInicio, createReservaDto.FechaFin);

            foreach (var res in conflictingReservas)
            {
                if (res.Estado == EstadoReserva.Aprobada)
                    throw new Exception("Espacio already reserved for this time");
            }

            var reserva = new Reserva
            {
                Id = Guid.NewGuid(),
                UsuarioId = createReservaDto.UsuarioId,
                EspacioId = createReservaDto.EspacioId,
                FechaInicio = createReservaDto.FechaInicio,
                FechaFin = createReservaDto.FechaFin,
                Estado = EstadoReserva.Pendiente,
                FechaCreacion = DateTime.UtcNow
            };

            await _reservaRepository.AddAsync(reserva);

            return new ReservaDto
            {
                Id = reserva.Id,
                UsuarioId = reserva.UsuarioId,
                EspacioId = reserva.EspacioId,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                Estado = reserva.Estado,
                FechaCreacion = reserva.FechaCreacion
            };
        }

        public async Task UpdateEstadoAsync(Guid id, UpdateReservaDto updateReservaDto)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null) throw new Exception("Reserva not found");

            reserva.Estado = updateReservaDto.Estado;
            reserva.MotivoRechazo = updateReservaDto.MotivoRechazo;

            await _reservaRepository.UpdateAsync(reserva);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _reservaRepository.DeleteAsync(id);
        }
    }
}