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
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            string[] additionalImages =
            {
                "glyphicons_432_plus"
            };

            var dbImages = _unitOfWork.ImageRepository.All.Where(
                image => image.FileName != additionalImages[0]).ToList();

            for (int i = 0; i < dbImages.Count; i++)
            {
                for (int j = 0; j < additionalImages.Length; j++)
                {
                    if (dbImages[i].FileName == additionalImages[j])
                    {
                        dbImages.Remove(dbImages[i]);
                    }                    
                }
            }

            return View(dbImages);
        }
    }
}