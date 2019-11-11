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
        
        //Get admin pages and retun a view
        [HttpGet]
        public ActionResult CreatePage()
        {
            return View();
        }

        //Post admin pages and retun a view
        [HttpPost]
        public ActionResult CreatePage(AdminPagesVm model)
        {
            //Validaate the model state
            if (! ModelState.IsValid)
            {
                return View(model);

            }

            using (MyDb db = new MyDb())
            {


                //Declare the Slug
                string slug;

                //Initialize the PageDTO
                PageDTO dTO = new PageDTO();
               
                //Display the DTO Title
                dTO.Title = model.Title;

                //Check and Set the Slug
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", " ").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", " ").ToLower();
                }
                //Make sure the Title and Slug are Unique from each other
                if (db.Pages.Any(x => x.Title == model.Title) || db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError(" ", "The Title or Slug entered already exist. ");
                    return View(model);
                }
                //DTO the rest of the pages

                dTO.Slug = slug;
                dTO.Body = model.Body;
                dTO.HasSidebar = model.HasSidebar;
                dTO.Sorting = 100;

                //Save the pageDTO
                db.Pages.Add(dTO);
                db.SaveChanges();
            }
            //Save the temporary message

            TempData["Success"] = "You have successfully added a new page!";
            //Redirect the page
            return RedirectToAction("CreatePage");
        }
    }
}