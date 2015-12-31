using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dogs;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using Dogs.Models;
using Dogs.Services;
using Dogs.ViewModels.Account;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Dogs.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private const string LocalLoginProvider = "Local";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
      

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // POST api/Account/Logout
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
             var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                    return Ok(new{userId=user.Id, Name=user.UserName});
                return HttpUnauthorized();
            }
            return HttpUnauthorized();
        }

        // POST api/Account/Register
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var user = new ApplicationUser() {UserName = model.Email, Email = model.Email};

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(new { userId = user.Id, Name = user.UserName });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();

            }

            base.Dispose(disposing);
        }

        #region Helpers



        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return new HttpStatusCodeResult(500);
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequestResult.
                    return HttpBadRequest();
                }

                return HttpBadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }
            /*

            private static class RandomOAuthStateGenerator
            {
                private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

                public static string Generate(int strengthInBits)
                {
                    const int bitsPerByte = 8;

                    if (strengthInBits%bitsPerByte != 0)
                    {
                        throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                    }

                    int strengthInBytes = strengthInBits/bitsPerByte;

                    byte[] data = new byte[strengthInBytes];
                    _random.GetBytes(data);
                    return Base64UrlEncode(data);
                }
            }
            static string Base64UrlEncode(byte[] arg)
            {
                string s = Convert.ToBase64String(arg); // Regular base64 encoder
                s = s.Split('=')[0]; // Remove any trailing '='s
                s = s.Replace('+', '-'); // 62nd char of encoding
                s = s.Replace('/', '_'); // 63rd char of encoding
                return s;
            }*/
            #endregion
        }
    }
}
