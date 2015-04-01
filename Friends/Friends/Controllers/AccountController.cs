using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Friends.Models;
using BusinessDomain.DomainObjects;
using Newtonsoft.Json;
using ServiceLayer;
using ServiceLayer.Model;

namespace Friends.Controllers
{
    public class AccountController : Controller
    {
        IUserService UserService { get; set; }

        public AccountController()
        {
            UserService = new UserService();
        }

        private ActionResult AuthorizeUser(UserDTO user)
        {
            if (user != null)
            {
                var roles = user.Roles.Select(r => r.RoleName).ToArray();

                var serializeModel = new UserModel();
                serializeModel.UserId = user.Id;
                serializeModel.FirstName = user.FirstName;
                serializeModel.LastName = user.LastName;
                serializeModel.Roles = user.Roles.Select(r => r.RoleName).ToArray();

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    user.Email,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(15),
                    false, //pass here true, if you want to implement remember me functionality
                    userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Home", "Home");
                }
                if (roles.Contains("User"))
                {
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return null;
        }
        //
        // GET: /Account/
        [HttpPost]
        public ActionResult Login(LoginModel login, string returnUrl = "")
        {
            //UserService = new MockUserService();
            var user = UserService.Get(login.Username, login.Password);
            var result = AuthorizeUser(user);
            if (result != null)
                return result;
            ModelState.AddModelError("", "Incorrect username and/or password");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            var model = new SignUpModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(SignUpModel model)
        {
            var a = Request.Form;
            var request = new UserDTO
            {
                DOB = new DateTime(model.YearDOB, model.MonthDOB, model.DateDOB),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Password = model.Password,
                Roles = new List<RolesDTO>{new RolesDTO{Id = 2}}
            };
            UserService.Post(request);
            return RedirectToAction("Index","Home");
        }

    }
}
