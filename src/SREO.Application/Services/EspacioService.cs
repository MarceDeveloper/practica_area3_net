using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SREO.Application.DTOs;
using SREO.Application.Interfaces;
using SREO.Domain.Entities;
using SREO.Domain.Interfaces;

namespace SREO.Application.Services
{
    public class EspacioService : IEspacioService
    {
        private readonly IEspacioRepository _espacioRepository;

        public EspacioService(IEspacioRepository espacioRepository)
        {
            _espacioRepository = espacioRepository;
        }

        public async Task<EspacioDto> GetByIdAsync(Guid id)
        {
            var espacio = await _espacioRepository.GetByIdAsync(id);
            if (espacio == null) return null;

            return new EspacioDto
            {
                Id = espacio.Id,
                Nombre = espacio.Nombre,
                Tipo = espacio.Tipo,
                Capacidad = espacio.Capacidad,
                Caracteristicas = espacio.Caracteristicas,
                Disponible = espacio.Disponible
            };
        }

        public async Task<IEnumerable<EspacioDto>> GetAllAsync()
        {
            var espacios = await _espacioRepository.GetAllAsync();
            var espacioDtos = new List<EspacioDto>();

            foreach (var espacio in espacios)
            {
                espacioDtos.Add(new EspacioDto
                {
                    Id = espacio.Id,
                    Nombre = espacio.Nombre,
                    Tipo = espacio.Tipo,
                    Capacidad = espacio.Capacidad,
                    Caracteristicas = espacio.Caracteristicas,
                    Disponible = espacio.Disponible
                });
            }

            return espacioDtos;
        }

        public async Task<IEnumerable<EspacioDto>> GetDisponiblesAsync()
        {
            var espacios = await _espacioRepository.GetDisponiblesAsync();
            var espacioDtos = new List<EspacioDto>();

            foreach (var espacio in espacios)
            {
                espacioDtos.Add(new EspacioDto
                {
                    Id = espacio.Id,
                    Nombre = espacio.Nombre,
                    Tipo = espacio.Tipo,
                    Capacidad = espacio.Capacidad,
                    Caracteristicas = espacio.Caracteristicas,
                    Disponible = espacio.Disponible
                });
            }

            return espacioDtos;
        }

        public async Task<EspacioDto> CreateAsync(CreateEspacioDto createEspacioDto)
        {
            var espacio = new Espacio
            {
                Id = Guid.NewGuid(),
                Nombre = createEspacioDto.Nombre,
                Tipo = createEspacioDto.Tipo,
                Capacidad = createEspacioDto.Capacidad,
                Caracteristicas = createEspacioDto.Caracteristicas,
                Disponible = createEspacioDto.Disponible
            };

            await _espacioRepository.AddAsync(espacio);

            return new EspacioDto
            {
                Id = espacio.Id,
                Nombre = espacio.Nombre,
                Tipo = espacio.Tipo,
                Capacidad = espacio.Capacidad,
                Caracteristicas = espacio.Caracteristicas,
                Disponible = espacio.Disponible
            };
        }

        public async Task UpdateAsync(Guid id, UpdateEspacioDto updateEspacioDto)
        {
            var existingEspacio = await _espacioRepository.GetByIdAsync(id);
            if (existingEspacio == null) throw new Exception("Espacio not found");

            existingEspacio.Nombre = updateEspacioDto.Nombre;
            existingEspacio.Tipo = updateEspacioDto.Tipo;
            existingEspacio.Capacidad = updateEspacioDto.Capacidad;
            existingEspacio.Caracteristicas = updateEspacioDto.Caracteristicas;
            existingEspacio.Disponible = updateEspacioDto.Disponible;

            await _espacioRepository.UpdateAsync(existingEspacio);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _espacioRepository.DeleteAsync(id);
        }
    }
}