using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using DAL.Interfaces;
using MusicSocialNetwork.Models;
using NetworkDatabase;

namespace MusicSocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel userLogin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string userName = userLogin.UserName;
                string password = userLogin.Pass;
                if (ValidateUser(userName, password))
                {
                    FormsAuthentication.SetAuthCookie(userName, false);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный пароль или логин");
            }
            return RedirectToAction("Login");
        }

        private bool ValidateUser(string userName, string pass)
        {
            try
            {
                var user = _unitOfWork.UserRepository.All.FirstOrDefault(
                    u => (u.UserName == userName));

                if (user != null && (Crypto.VerifyHashedPassword(user.Password, pass)))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return null;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerUser)
        {
            var dbUser = _unitOfWork.UserRepository.All.FirstOrDefault(
                user=>user.UserName == registerUser.UserName);

            if (dbUser == null)
            {
                var p = Crypto.HashPassword(registerUser.Password);
                var length = p.Length;
                _unitOfWork.UserRepository.Create(new User()
                {
                    UserName = registerUser.UserName,
                    Email = registerUser.Email,
                    //Password = Crypto.Hash(registerUser.Password),
                    Password = p,
                    ImageData = new byte[3] {1, 2, 3},
                    ImageMimeType = "geqwge",
                    RoleId = 2
                });
                _unitOfWork.Commit();

                FormsAuthentication.SetAuthCookie(registerUser.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка при регистрации");
                return null;
            }
        }
    }
}