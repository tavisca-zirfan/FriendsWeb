
using System.Collections.Generic;
using System.Linq;
using BLL;
using Infrastructure.Model;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IUserService
    {
        //User Authenticate(LoginRequest request);
        void Post(UserDTO request);
        UserDTO Get(string username, string password);
        List<UserDTO> Get();
        void Delete(string userId);
    }
    public class MockUserService : IUserService
    {
       IUserController UserController { get; set; }

        public MockUserService()
        {
            UserController = new UserController();
        }
        
        public void Post(UserDTO request)
        {
            this.UserController.RegisterUser(new User
            {
                Email = request.Email,
                ChangedPassword = request.Password,
                Roles = new List<Role>{new Role{RoleId = 2}}
            },
                new Profile
                {
                    DOB = request.DOB,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender
                });

        }
        

        public void Delete(string userId)
        {
            throw new System.NotImplementedException();
        }


        public UserDTO Get(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public List<UserDTO> Get()
        {
            throw new System.NotImplementedException();
        }
    }

}
