using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Friends.Classes;
using ServiceLayer;
using ServiceLayer.Model;

namespace Friends.Controllers
{
    public class BaseApiController : ApiController
    {
        //
        // GET: /BaseApi/

        public IUserService UserService { get; set; }
        public CustomPrincipal AuthUser { get; set; }
        public UserDTO UserData { get; set; }

        public BaseApiController()
        {
            AuthUser = System.Web.HttpContext.Current.User as CustomPrincipal;
            UserService = new UserService();
            if(AuthUser!=null)
            UserData = HttpRuntime.Cache.Get("user" + AuthUser.UserId, UserService.Get);
        }

    }
}
