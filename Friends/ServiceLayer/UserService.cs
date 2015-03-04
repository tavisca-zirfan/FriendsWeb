
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DomainService;
using BusinessDomain.DomainObjects;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IUserService
    {
        //User Authenticate(LoginRequest request);
        UserDTO Post(UserDTO request);
        UserDTO Get(string email, string password);
        void Delete();
        void ChangePassword(string oldpassword, string newpassword);
        void ChangePassword(string email ,string oldpassword, string newpassword);
    }
    public class UserService : IUserService
    {
        public IUserController UserController { get; set; }
        public string UserId { get; set; }

        public UserService()
        {
            UserController = new UserController();
        }
        
        public UserDTO Post(UserDTO request)
        {
            var user = Mapper.Map<User>(request);
            user.Roles = new List<Role> {new Role {RoleId = 2}};
            this.UserController.RegisterUser(user);
            request.UserId = user.Id;
            return request;
        }
        

        public void Delete()
        {
            throw new System.NotImplementedException();
        }


        public UserDTO Get(string username, string password)
        {
            var user = UserController.GetUser(username, password);
            var userDto = Mapper.Map<User, UserDTO>(user);
            return userDto;
        }

        public void ChangePassword( string oldpassword, string newpassword)
        {
            var user = new User {Id = UserId};
            user.ChangePassword(newpassword);
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
