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
                    slug = model.Title.Replace(" ", "-").ToLower();
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

            TempData["SM"] = "You have successfully added a new page!";
            //Redirect the page
            return RedirectToAction("CreatePage");
        }

        //Edit Admin Pages (Edit Functionality).
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            //Declare the page view model
            AdminPagesVm model;

            using (MyDb db = new MyDb())
            {
                //Get the Page

                PageDTO dTO = db.Pages.Find(id);
                //Confirm if the page exist

                if (dTO == null)
                {
                    return Content("The Page requested does not exist!");
                }
                //Initialize the page view model
                model = new AdminPagesVm(dTO);
            }
            //Return the view with the model
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPage(AdminPagesVm model)
        {
            //Check model State
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            using (MyDb db = new MyDb())
            {
                //Get the Page Id
                int id = model.Id;
                //Delcare Slug
                string slug ="home";
                //Get the Page
                PageDTO dTO = db.Pages.Find(id);
                //Reference DTO to the Page Title
                dTO.Title = model.Title;
                //Check for the Slug on the page and set it if there is need
                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }
                //Make sure the Title and Slug are unique
                if (db.Pages.Where(x => x.Id !=id).Any(x => x.Title == model.Title) ||
                    db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError(" ", "The Slug or Title requested already exists!");
                    return View(model);
                }
                //DTO the rest of the Pages
                dTO.Slug = slug;
                dTO.Body = model.Body;
                dTO.HasSidebar = model.HasSidebar;
                //Save the DTO
                db.SaveChanges();
            }
            //Set Temp data message
            TempData["SM"] = "You have successfully Edited the page!";
            //Redirect the pages
            return RedirectToAction("EditPage");
        }

        public ActionResult PageDetails(int id)
        {
            //Declare the Page View model
            AdminPagesVm model;
            
            using (MyDb db = new MyDb())
            {
                //Get the Page using the Page Id

                PageDTO dTO = db.Pages.Find(id);

                //Confirm if the page necessarily exist
                if (dTO == null)
                {
                    return Content("The Page requested does not exist");

                }

                //Initialize the Page view model
                model = new AdminPagesVm(dTO);
            }
                //Return the view with the model
                return View(model);
        }

        //Delete Page functionality
        public ActionResult DeletePage(int id)
        {
            using (MyDb db = new MyDb())
            {
                //Get the Page
                PageDTO dTO = db.Pages.Find(id);
                //Remove the Page
                db.Pages.Remove(dTO);
                //Save the Page
                db.SaveChanges();
            }
            //and Redirect the page after deletion
            return RedirectToAction("Index");
        }
    }

}