using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{
    public class UsuarioBusiness
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioBusiness(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            return await _usuarioRepository.GetAllUsuarios();
        }

        public async Task<UsuarioModel> GetUsuario(int id)
        {
            return await _usuarioRepository.GetUsuario(id);
        }

        public async Task<UsuarioModel> Add(UsuarioModel usuario)
        {
            return await _usuarioRepository.Add(usuario);
        }

        public async Task<UsuarioModel> Update(UsuarioModel usuario)
        {
            return await _usuarioRepository.Update(usuario);
        }
    }
}