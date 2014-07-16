using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Implementations;
using DAL.Interfaces;
using MusicSocialNetwork.Models;
using NetworkDatabase;

namespace MusicSocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Add(UserViewModel user)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {

                unitOfWork.UserRepository.Create(new User()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Pass,
                    ImageData = new byte[3] { 1,2,3},
                    ImageMimeType = "geqwge",
                    RoleId = 2
                });
                unitOfWork.Commit();
            }
            
            return null;           
        }
    }
}