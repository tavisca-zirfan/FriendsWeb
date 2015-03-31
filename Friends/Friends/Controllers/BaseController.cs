using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer;
using Friends.Classes;
using ServiceLayer.Model;
using CustomPrincipal = Friends.Classes.CustomPrincipal;

namespace Friends.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public IProfileService ProfileService { get; set; }
        public IUserService UserService { get; set; }
        public CustomPrincipal AuthUser { get; set; }
        public UserDTO UserData { get; set; }
        public ProfileDTO UserProfile { get; set; }

        public BaseController()
        {
            AuthUser = System.Web.HttpContext.Current.User as CustomPrincipal;
            if (AuthUser != null)
            {
                UserService = new UserService();
                ProfileService = new ProfileService();
                if (AuthUser != null)
                    UserData = HttpRuntime.Cache.Get("user" + AuthUser.UserId, UserService.Get);
                if (HttpRuntime.Cache["profile"] == null)
                {
                    HttpRuntime.Cache["profile"] = ProfileService.Get(UserData.Id, UserData);
                }
                UserProfile = HttpRuntime.Cache["profile"] as ProfileDTO;
                ViewBag.UserProfile = UserProfile;
            }
        }

    }
}
