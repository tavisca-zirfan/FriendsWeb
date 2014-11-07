using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using FriendsDb.Models;
using Infrastructure.Data;
using Infrastructure.Model;
using Role = FriendsDb.Models.Role;

namespace DAL
{
    public class UserRepository:EfBaseRepository<User>,IUserRepository
    {
        private FriendsContext Db;
        private EfBaseRepository<UserCredential> _credentialRepository;
        private EfBaseRepository<UserProfile> _profileRepository;
        private EfBaseRepository<Role> _roleRepository;
        public UserRepository(IUnitOfWork uow):base(uow)
        {
            Db = Context as FriendsContext;
            _credentialRepository = new EfBaseRepository<UserCredential>(uow);
            _profileRepository=new EfBaseRepository<UserProfile>(uow);
            _roleRepository=new EfBaseRepository<Role>(uow);
        } 

        public UserRepository()
        {
            Db = new FriendsContext();
        }
        public User GetUser(string username)
        {
            var user = from uc in Db.UserCredentials
                join up in Db.UserProfiles on uc.UserId equals up.UserId
                join r in Db.UserRoles on uc.UserId equals r.UserId
                select uc.ToBusinessModel(up, Db.Roles.Where(rn=>rn.RoleId==r.RoleId)
            );
            return user.FirstOrDefault();

        }

        public bool RegisterUser(User user,IEnumerable<int> roles)
        {
            using (var db = new FriendsContext())
            {
                var credential = new UserCredential();
                user.ToDbModel(credential);
                _credentialRepository.Add(credential);
                var profile = new UserProfile();
                user.ToDbModel(profile);
                var responseProfile = db.UserProfiles.Add(profile);
                var userRoles = roles.Select(r => new UserRole {RoleId = r, UserId = response.UserId});
                db.UserRoles.AddRange(userRoles);
                return db.SaveChanges()>0;
            }
        }

        public Profile GetProfile(int userId)
        {
            using (var db = new FriendsContext())
            {
                return db.UserCredentials.Include("UserProfile").Where(uc => uc.UserId == userId)
                    .Select(uc => new Profile
                    {
                        Email = uc.Email,
                        FirstName = uc.UserProfile.FirstName,
                        Gender = uc.UserProfile.Gender,
                        LastName = uc.UserProfile.LastName,
                        About = uc.UserProfile.About,
                        DOB = uc.UserProfile.DOB,
                        LastSeen = uc.LastSeen,
                        Location = ""
                    }).First();
            }
        }
        public Profile GetProfile(string username)
        {
            using (var db = new FriendsContext())
            {
                return db.UserCredentials.Include("UserProfile").Where(uc => uc.Username == username)
                    .Select(uc => new Profile
                    {
                        Email = uc.Email,
                        FirstName = uc.UserProfile.FirstName,
                        Gender = uc.UserProfile.Gender,
                        LastName = uc.UserProfile.LastName,
                        About = uc.UserProfile.About,
                        DOB = uc.UserProfile.DOB,
                        LastSeen = uc.LastSeen,
                        Location = ""
                    }).First();
            }
        }

        

        public User GetUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
