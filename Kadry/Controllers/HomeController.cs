
using System.Web;
using System.Web.Mvc;
using Kadry.Models;

namespace Kadry.Controllers
{
    public class HomeController : Controller
    {
        private readonly SQLConnection _sqlConnection = new SQLConnection();

        public ActionResult Index()
        {
            return View(new Login());
        }

        public ActionResult About(int id)
        {
            if (!CheckIsLogin()) return RedirectToAction("ErrorPage","Admin" );
            Employeer employer = _sqlConnection.GetEmployer(id);
            return View(employer);
        }

        public ActionResult Contact()
        {
            if (!CheckIsLogin()) return RedirectToAction("ErrorPage", "Admin");
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if (_sqlConnection.IsPasswordCorrect(login.Username, login.Password))
                {
                    int loginid = _sqlConnection.GetLoginId(login.Username);

                   HttpCookie cookie = new HttpCookie("LoginCookie", loginid.ToString());
                   Response.SetCookie(cookie);
                    
                    if (_sqlConnection.IsAdmin(loginid))
                    {
                        return RedirectToAction("EmployerList","Admin");
                    }
                    return RedirectToAction("About", new  { id = _sqlConnection.GetUserId ( loginid ) } );
                }
            }
            
                return RedirectToAction("Index");
            
        }
        

        private bool CheckIsLogin()
        {
            if (Request.Cookies["LoginCookie"] != null)
            {
               
                return true;
            }
            else return false;
        }
    }
}