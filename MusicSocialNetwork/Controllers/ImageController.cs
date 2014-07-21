using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using NetworkDatabase;

namespace MusicSocialNetwork.Controllers
{
    public class ImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Index(HttpPostedFileBase postedImage)
        {
            if (ModelState.IsValid)
            {
                var image = new Image();

                if (postedImage != null)
                {
                    string postedFIleName = postedImage.FileName;
                    int index = postedFIleName.IndexOf('.');

                    image.FileName = postedFIleName.Substring(0, index);
                    image.ImageMimeType = postedImage.ContentType;
                    image.ImageData = new byte[postedImage.ContentLength];
                    postedImage.InputStream.Read(image.ImageData, 0, postedImage.ContentLength);
                }

                _unitOfWork.ImageRepository.Create(image);
                _unitOfWork.Commit();
            }
            else
            {
                return false;
            }

            return true;
        }

        public FileResult GetImage(int id)
        {
            var image = _unitOfWork.ImageRepository.GetById(id);
            return File(image.ImageData, image.ImageMimeType);
        }

        public FileResult GetPlusImage()
        {
            var image = _unitOfWork.ImageRepository.All.FirstOrDefault(
                dbImage => dbImage.FileName == "glyphicons_432_plus");
            if (image == null)
            {
                return null;
            }
            return File(image.ImageData, image.ImageMimeType);
        }
	}
}