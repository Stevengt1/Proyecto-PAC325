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

        public async Task<bool> Register(string correo, string password, string rol)
        {
            var user = new IdentityUser { UserName = correo, Email = correo };
            
            var respuesta = await _userManager.CreateAsync(user, password);
            if (!respuesta.Succeeded) {return false;}

            respuesta = await _userManager.AddToRoleAsync(user, rol);
            if (!respuesta.Succeeded) { return false; }

            return true;
        }


    }
}
