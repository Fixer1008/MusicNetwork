using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using DAL.Interfaces;
using MusicSocialNetwork.Models;
using NetworkDatabase;

namespace MusicSocialNetwork.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/user
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/user/5
        public UserViewModel Get(string name)
        {
            var dbUser = _unitOfWork.UserRepository.All.FirstOrDefault(
                user => user.UserName == name);

            UserViewModel userVM = new UserViewModel()
            {
                UserName = dbUser.UserName,
                Email = dbUser.Email,
            };

            return userVM;
        }

        // POST api/user
        public void Post([FromBody]string value)
        {
        }

        // PUT api/user/5
        public void Put([FromBody]RegisterViewModel editProfile)
        {
            if (ModelState.IsValid)
            {
                var passHash = Crypto.HashPassword(editProfile.Password);
                var dbUser = _unitOfWork.UserRepository.All.FirstOrDefault(
                            user => user.UserName == editProfile.UserName);
                dbUser.Password = passHash;
                _unitOfWork.UserRepository.Update(dbUser);
                _unitOfWork.Commit();
            }
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
