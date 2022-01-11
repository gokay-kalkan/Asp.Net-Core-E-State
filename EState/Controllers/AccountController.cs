using EntityLayer.Entities;
using EState.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System.Security.Claims;

namespace EState.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<UserAdmin> userManager, SignInManager<UserAdmin> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", " Hatalı lütfen bilgilerinizi kontrol ediniz");
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Id", user.Id);
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }

        public IActionResult GoogleLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("ExternalResponse", "Account", new { returnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
           
        }

        public async Task<IActionResult> ExternalResponse(string returnUrl="/")
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                SignInResult result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);



                if (result.Succeeded)
                {
                    return Redirect(returnUrl);
                }
               
                else
                {
                    UserAdmin user = new UserAdmin();

                   
                    user.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;

                    string ExternalUserId = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;

                    if (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name))
                    {
                        string userName = info.Principal.FindFirst(ClaimTypes.Name).Value;

                        userName = userName.Replace(' ','-').ToLower()+ExternalUserId.Substring(0,5).ToString();
                        user.UserName = userName;
                    }
                    else
                    {
                        user.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    }



                    IdentityResult createresult = await _userManager.CreateAsync(user);
                   

                 
                    if (createresult.Succeeded )
                    {
                       IdentityResult loginresult= await _userManager.AddLoginAsync(user, info);

                        if (loginresult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, true);
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            foreach (var item in loginresult.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }

                    else
                    {
                        foreach (var item in createresult.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }


                }
            }

            return NotFound();
        }

        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserAdmin user = new UserAdmin
            {
                Email = model.Email,
                UserName = model.UserName,
                FullName = model.FullName

            };

            IdentityRole role = new IdentityRole()
            {
                Name = "User"
            };
            await _roleManager.CreateAsync(role);

            var result = await _userManager.CreateAsync(user, model.Password);
            var resultt = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded || resultt.Succeeded)
            {
                TempData["kayitmesaj"] = "Sitemize Başarıyla Kayıt Oldunuz";
                return RedirectToAction("Login");
            }
            else
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
                return View(model);
            }
            
        }
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            HttpContext.Session.Remove("FullName");
            return RedirectToAction("Login");
        }
        public IActionResult Profile()
        {
            UserAdmin user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            RegisterModel userViewModel = user.Adapt<RegisterModel>();
            return View(userViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(RegisterModel userModel)
        {
            ModelState.Remove("Password");
            ModelState.Remove("RePassword");
           
            if (ModelState.IsValid)
            {

                UserAdmin user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.UserName = userModel.UserName;
                user.Email = userModel.Email;
                user.FullName = userModel.FullName;
              IdentityResult result=  await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                    ViewBag.success = "Bilgileriniz Başarıyla Güncellenmiştir";
                   
                   
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", "Bir Hata Oluştu");
                    }
                }
            }
           
            return View(userModel);
        }
        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordModel());
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            UserAdmin user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string passwordResetLink = Url.Action("UpdatePassword", "Account", new { userId = user.Id, token = resetToken }, HttpContext.Request.Scheme);
              
                MailHelper.ResetPassword.PasswordResetSendMail(passwordResetLink);
                ViewBag.State = true;
            }
            else
                ViewBag.State = false;

            return View(model);
        }

        [HttpGet]
        public IActionResult UpdatePassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            //UserAdmin userr = new UserAdmin();
            //UpdatePasswordModel resetpassword = new UpdatePasswordModel() { Email = userr.Email };
            //var check = _userManager.FindByEmailAsync(userr.Email);
            //TempData["check"] = check;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword([Bind("NewPassword")] ResetPasswordModel model)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();
            UserAdmin user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    TempData["Success"] = "Şifreniz Başarıyla Güncellenmiştir";

                }
            }
            else
            {

                ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı");
            }
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                UserAdmin user = await _userManager.FindByNameAsync(User.Identity.Name);
               
                    bool exist = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                    if (exist)
                    {
                        IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword,model.NewPassword);
                        if (result.Succeeded)
                        {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, model.NewPassword,true,false);
                            ViewBag.success = "Şifre Başarılı Bir Şekilde Değişti";
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Eski Şifreniz Yanlış");
                    }
                

            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
