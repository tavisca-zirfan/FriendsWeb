
using System.Collections.Generic;
using System.Linq;
using DomainService;
using BusinessDomain.DomainObjects;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IUserService
    {
        //User Authenticate(LoginRequest request);
        void Post(UserDTO request);
        UserDTO Get(string email, string password);
        void Delete();
        void ChangePassword(string oldpassword, string newpassword);
        void ChangePassword(string email ,string oldpassword, string newpassword);
    }
    public class MockUserService : IUserService
    {
       IUserController UserController { get; set; }
        public string UserId { get; set; }

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
                Roles = new List<Role>{new Role{RoleId = 2}},
                DOB = request.DOB,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender
            });

        }
        

        public void Delete()
        {
            throw new System.NotImplementedException();
        }


        public UserDTO Get(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public void ChangePassword( string oldpassword, string newpassword)
        {
            var user = new User {Id = UserId};
            user.ChangePassword(oldpassword,newpassword);
            user.Save();
        }

        public void ChangePassword(string email, string oldpassword, string newpassword)
        {
            var user = UserController.GetUser(email, oldpassword);
            user.ChangePassword(newpassword);
            user.Save();
        }
    }

}
