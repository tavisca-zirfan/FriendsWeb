using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public static class Translator
    {
        public static User ToBusinessModel(this UserDTO userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DOB = userDto.DOB,
                Email = userDto.Email,
                Gender = userDto.Gender,
                Id = userDto.Id,
                Friends = userDto.Friends.Select(f => new Profile {Id = f}).ToList(),
                Password = userDto.Password,
                Roles = userDto.Roles.Select(r => new Role {RoleId = r.Id, RoleName = r.RoleName})
            };
        }
    }
}
