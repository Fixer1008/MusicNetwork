using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Interfaces;
using MusicSocialNetwork.Models;

namespace MusicSocialNetwork.Controllers
{
    public class SongController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SongController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/song
        public IEnumerable<RoleViewModel> Get()
        {
            var dbRoles = _unitOfWork.RoleRepository.All;
            ////var 

            //return;
        }

        // GET api/song/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/song
        public void Post([FromBody]string value)
        {
        }

        // PUT api/song/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/song/5
        public void Delete(int id)
        {
        }
    }
}
