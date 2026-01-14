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

        public async Task<string> Exist(string correo)
        {
            try
            {
                var respuesta = await _userManager.FindByEmailAsync(correo);
                if (respuesta != null)
                {
                    return "Correo en uso";
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "error a la hora de verificar la existencia";
            }
        }

        public async Task<string> Register(string correo, string password, string rol)
        {
            try
            {
                var user = new IdentityUser { UserName = correo, Email = correo };

                var respuesta = await _userManager.CreateAsync(user, password);
                if (!respuesta.Succeeded) { return "ERROR"; }

                respuesta = await _userManager.AddToRoleAsync(user, rol);
                if (!respuesta.Succeeded) { return "ERROR"; }

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

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


    }
}
