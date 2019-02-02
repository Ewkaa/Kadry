using Kadry.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Kadry.Controllers
{
    public class AdminController : Controller
    {
        private readonly SQLConnection _sqlConnection = new SQLConnection();

        public ActionResult EmployerList()
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");

            var list = _sqlConnection.GetAllEmployeers();
            return View(list);
        }
        public ActionResult CreateEmployer()
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");

            Employeer employeer = new Employeer();
            employeer.Medical = new Medical();
            employeer.ContractType = new ContractType();
            employeer.Holiday = new Holiday();
            employeer.Hours = new Hours();
            employeer.Salary = new Salary();
            employeer.Workplace = new Workplace();


            return View(employeer);
        }

        [HttpPost]
        public ActionResult Add(Employeer employer)
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");

            employer.ContractType = _sqlConnection.GetContractType(employer.ContractType.Id);
            employer.Workplace = _sqlConnection.GetWorkplace(employer.Workplace.Id);
            _sqlConnection.CreateEmployer(employer);

            return RedirectToAction("EmployerList");
        }

        public ActionResult RemoveEmployer(int id)
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");

            _sqlConnection.RemoveEmployeer(id);
            return RedirectToAction("EmployerList");
        }
        public ActionResult ContractTypeList()
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");


            var list = _sqlConnection.ContractTypeList();
            return View(list);
        }
        public ActionResult AddContract()
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");


            return View(new ContractType());
        }
        public ActionResult CreateContract(ContractType contract)
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");
            _sqlConnection.CreateOrUpdateContract(contract);
            return RedirectToAction("ContractTypeList");
        }
        public ActionResult WorkplaceList()
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");
            var list = _sqlConnection.WorkplaceList();
            return View(list);
        }
        public ActionResult AddWorkplace()
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");
            return View(new Workplace());
        }
        public ActionResult CreateWorkplace(Workplace workplace)
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");
            _sqlConnection.CreateOrUpdateWorkplace(workplace);
            return RedirectToAction("WorkplaceList");
        }

        public ActionResult ErrorPage()
        {
            return View();

        }

        private bool CheckIfAdmin()
        {
            if (Request.Cookies["LoginCookie"] != null)
            {
                var login = Int32.Parse(Request.Cookies["LoginCookie"].Value);
                return _sqlConnection.IsAdmin(login);
            }
            else return false;
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Abandon();
            HttpContext.Response.Cookies.Add(new HttpCookie("LoginCookie", "-1"));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetailsEmployer(int id)
        {
            if (!CheckIfAdmin()) return RedirectToAction("ErrorPage");
            Employeer employer = _sqlConnection.GetEmployer(id);
            return View(employer);
        }
    }
}