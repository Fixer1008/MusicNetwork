using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Interfaces;
using MusicSocialNetwork.Helpers;
using MusicSocialNetwork.Models;
using NetworkDatabase;

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
            //var songs = _unitOfWork.SongRepository.All;
            //var roles = dbRoles.Select(dbRole => new RoleViewModel()
            //{
            //    RoleName = dbRole.RoleName
            //}).ToList();
            return null;
        }

        // GET api/song/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/song
        public void Post([FromBody]SongEntity track)
        {
            if (ModelState.IsValid)
            {
               var user = _unitOfWork.UserRepository.All.FirstOrDefault(
                             name => name.UserName == track.UserName);

               int duration = SongHelper.ParseDuration(track.Duration);

               user.Songs.Add(new Song()
               {
                   SongId = track.SongId,
                   SongName = track.SongName,
                   Artist = track.Artist,
                   Duration = duration,
                   Url = track.Url,
               });

               _unitOfWork.UserRepository.Update(user);
               _unitOfWork.Commit();
            }
        }

        // DELETE api/song/5
        public void Delete(int id)
        {
        }
    }
}
