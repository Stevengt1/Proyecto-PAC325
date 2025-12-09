using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{
    public class AuthBusiness
    {

        private readonly AuthRepository _authRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public AuthBusiness(AuthRepository authRepository, UsuarioRepository usuarioRepository)
        {
            _authRepository = authRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> Register(string correo, string password, string rol)
        {
            if(rol == "Cajero")
            {
                var usuario = await _usuarioRepository.GetUsuarioCorreo(correo);
                if (usuario == null) 
                {
                    return "Error: el usuario no puede registrarse ya que no esta afiliado a un negocio";
                }
                string id = await _authRepository.Register(correo, password, rol);
                if(id == null)
                {
                    return "Error: ocurrio un error y no se pudo registrar en el sistema";
                }
                usuario.IdNetUser = Guid.Parse(id);
                await _usuarioRepository.Update(usuario);
                return "Success: el usuario se registro correctamente";
            }
            else
            {
                string id = await _authRepository.Register(correo, password, rol);
                if (id == null)
                {
                    return "Error: ocurrio un error y no se pudo registrar en el sistema";
                }
                return "Success: el usuario se registro correctamente";
            }
        }

        public async Task<string> Login(string correo, string password)
        {
            if (await _authRepository.Login(correo, password))
            {
                return "Success: el usuario se registro correctamente";
            }
            return "Error: credenciales invalidas";
        }

    }
}
