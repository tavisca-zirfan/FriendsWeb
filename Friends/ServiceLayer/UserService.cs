
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
        UserDTO Post(UserDTO request);
        UserDTO Get(string email, string password);
        UserDTO Get(string id);
        void Delete();
        void ChangePassword(UserDTO authUser,string oldpassword, string newpassword);
        void ChangePassword(string email ,string oldpassword, string newpassword);
    }
    public class UserService : IUserService
    {
        public IUserController UserController { get; set; }

        public UserService()
        {
            UserController = new UserController();
        }
        
        public UserDTO Post(UserDTO request)
        {
            var user = request.ToBusinessModel();
            user.Roles = new List<Role> {new Role {RoleId = 2}};
            user=this.UserController.RegisterUser(user);
            request.Id = user.Id;
            return request;
        }
        

        public void Delete()
        {
            throw new System.NotImplementedException();
        }


        public UserDTO Get(string username, string password)
        {
            var user = UserController.GetUser(username, password);
            var userDto = Mapper.Map<UserDTO>(user);
            return userDto;
        }

        public void ChangePassword(UserDTO authUser, string oldpassword, string newpassword)
        {
            var user = authUser.ToBusinessModel();
            user.ChangePassword(newpassword);
            user.Save();
        }

        public void ChangePassword(string email, string oldpassword, string newpassword)
        {
            var user = UserController.GetUser(email, oldpassword);
            user.ChangePassword(newpassword);
            user.Save();
        }


        public UserDTO Get(string id)
        {
            var user = UserController.GetUserById(id);
            return Mapper.Map<UserDTO>(user);
        }
    }

}
