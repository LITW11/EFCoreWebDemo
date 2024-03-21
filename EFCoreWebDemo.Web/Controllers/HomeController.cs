using EFCoreWebDemo.Data;
using EFCoreWebDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EFCoreWebDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new PersonRepository(_connectionString);

            return View(new IndexViewModel
            {
                People = repo.GetAll()
            });
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            var repo = new PersonRepository(_connectionString);
            repo.Add(person);
            return Redirect("/");
        }

        public IActionResult Edit(int personId)
        {
            var repo = new PersonRepository(_connectionString);
            var person = repo.GetById(personId);
            if(person == null)
            {
                return Redirect("/");
            }

            return View(new EditPersonViewModel
            {
                Person = person
            });
        }

        [HttpPost]
        public IActionResult Update(Person person)
        {
            var repo = new PersonRepository(_connectionString);
            repo.Update(person);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var repo = new PersonRepository(_connectionString);
            repo.Delete(id);
            return Redirect("/");
        }
    }
}