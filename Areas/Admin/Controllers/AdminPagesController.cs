using Online_Shopping.Models.AdminData;
using Online_Shopping.Models.AdminViewModels.AdminPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shopping.Areas.Admin.Controllers
{
    public class AdminPagesController : Controller
    {
        // GET: Admin/AdminPages
        public ActionResult Index()
        {

            //Have a list of Admin pages view models
            List<AdminPagesVm> pageslist;
            

            using (MyDb db = new MyDb())
            {
                //Initialize the list
                pageslist = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new AdminPagesVm(x)).ToList();

            }
            //Return view with the list
            return View(pageslist);
        }
    }
}