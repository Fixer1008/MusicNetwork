using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DAL.Implementations;
using DAL.Interfaces;
using NetworkDatabase;

namespace MusicSocialNetwork.Providers
{
    public class MusicNetworkRoleProvider : RoleProvider
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public MusicNetworkRoleProvider()
        //{

        //}

        //public MusicNetworkRoleProvider(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public override bool IsUserInRole(string username, string roleName)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                bool outputResult = false;

                    try
                    {
                        var user = unitOfWork.UserRepository.All.FirstOrDefault(
                            u => (u.UserName == username && u.Role.RoleName == roleName));

                        if (user != null)
                        {
                           outputResult = true;
                        }
                    }
                    catch
                    {
                        outputResult = false;
                    }
                return outputResult;                
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] role = new string[]{};

            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    var user = unitOfWork.UserRepository.All.FirstOrDefault(
                        u => u.UserName == username);

                    if (user != null)
                    {
                        role[0] = user.Role.RoleName;
                    }
                }
                catch
                {
                    role = new string[] { };
                }
                return role;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}