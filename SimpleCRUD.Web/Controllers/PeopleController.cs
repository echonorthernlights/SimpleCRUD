using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Web.DataAccess;
using SimpleCRUD.Web.Models;
using SimpleCRUD.Web.Models.ViewModels;
using System;

namespace SimpleCRUD.Web.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly PeopleContext _db;
        public PeopleController(PeopleContext db)
        {
            _db = db;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {


            var people = _db.People.ToList();
            var viewModel = new PeopleViewModel
            {
                People = people
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Update(CreatePersonRequest personRequest, Guid Id)
        {
            var person = _db.People
                        .Include(p => p.EmailAddresses)
                        .Include(p => p.Addresses)
                        .FirstOrDefault(p => p.Id == Id);

            person.FirstName = personRequest.FirstName;
            person.LastName = personRequest.LastName;
            person.Age = personRequest.Age;
            person.Addresses = personRequest.Addresses.ToList(); // Assuming Addresses is a collection
            person.EmailAddresses = personRequest.EmailAddresses.ToList();// Assuming EmailAddresses is a collection

            _db.People.Update(person);
            _db.SaveChanges();



            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(Guid Id)
        {

           
            var person = _db.People
                        .Include(p => p.EmailAddresses)
                        .Include(p => p.Addresses)
                        .FirstOrDefault(p => p.Id == Id);


            return View("Details", person);
            // return View(person);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateNewPerson(CreatePersonRequest personRequest)
        {
            if (ModelState.IsValid)
            {
                // Create a new Person
                var person = new Person
                {
                    FirstName = personRequest.FirstName,
                    LastName = personRequest.LastName,
                    Age = personRequest.Age,
                    Addresses = personRequest.Addresses.ToList(), // Assuming Addresses is a collection
                    EmailAddresses = personRequest.EmailAddresses.ToList() // Assuming EmailAddresses is a collection
                };


                _db.People.Add(person);
                _db.SaveChanges();

            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid Id)
        {
            var person = new Person();
            person.Id = Id;
            return View("Delete",person);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePerson(Guid Id)
        {
            var person = _db.People
                       .Include(p => p.EmailAddresses)
                       .Include(p => p.Addresses)
                       .FirstOrDefault(p => p.Id == Id);
            if(person != null) {
                _db.Remove(person);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
