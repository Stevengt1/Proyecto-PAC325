using Microsoft.AspNetCore.Identity;

namespace Proyecto_PAC325.Repository
{
    public class AuthRepository
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> Register(string correo, string password, string rol)
        {
            try
            {
                var user = new IdentityUser { UserName = correo, Email = correo };

                var respuesta = await _userManager.CreateAsync(user, password);
                if (!respuesta.Succeeded) { return null; }

                respuesta = await _userManager.AddToRoleAsync(user, rol);
                if (!respuesta.Succeeded) { return null; }

                return user.Id;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Login(string correo, string password)
        {
            try
            {
                var resultado = await _signInManager.PasswordSignInAsync(
                    correo,
                    password,
                    false,        // para recordar la sesión se tiene que cambiar esto a true
                    false         // este es el bloqueo tras los intentos fallidos
                );

                return resultado.Succeeded;
            }catch(Exception ex)
            {
                return false;
            }
        }


    }
}
