using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SREO.Domain.Entities;
using SREO.Domain.Interfaces;

namespace SREO.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private static readonly List<Usuario> _usuarios = new();

        public async Task<Usuario> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_usuarios.FirstOrDefault(u => u.Id == id));
        }

        public async Task<Usuario> GetByCorreoAsync(string correo)
        {
            return await Task.FromResult(_usuarios.FirstOrDefault(u => u.CorreoElectronico == correo));
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await Task.FromResult(_usuarios.AsEnumerable());
        }

        public async Task AddAsync(Usuario usuario)
        {
            _usuarios.Add(usuario);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            var existing = _usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            if (existing != null)
            {
                _usuarios.Remove(existing);
                _usuarios.Add(usuario);
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                _usuarios.Remove(usuario);
            }
            await Task.CompletedTask;
        }
    }
}