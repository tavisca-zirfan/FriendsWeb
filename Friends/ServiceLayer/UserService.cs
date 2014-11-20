
using System.Collections.Generic;
using System.Linq;
using BLL;
using Infrastructure.Model;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IUserService
    {
        User Authenticate(LoginRequest request);
        User RegisterUser(UserRegistrationRequest request);
        Profile GetProfile(string userid);
    }
    public class MockUserService : IUserService
    {
       IUserController UserController { get; set; }

        public MockUserService()
        {
            UserController = new UserController();
        }
        public User Authenticate(LoginRequest request)
        {
            return UserController.GetUser(request.Username, request.Password);
        }

        public User RegisterUser(UserRegistrationRequest request)
        {
            return (this.UserController.RegisterUser(new User
            {
                Email = request.Email,
                Password = request.Password,
                Roles = request.Roles.Select(r=>new Role{RoleId = r}).ToList()
            },
                new Profile
                {
                    DOB = request.DOB,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender
                }));

        }


        public Profile GetProfile(string userid)
        {
            return UserController.GetProfile(userid);
        }

        
    }

}
