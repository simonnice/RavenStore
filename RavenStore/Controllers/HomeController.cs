using System.Linq;
using System.Web.Mvc;
using Raven.Client.Linq;
using RavenStore.Infrastructure.Raven;
using RavenStore.Models;

namespace RavenStore.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ShowProduct(int id)
        {
            var product = DocumentSession.Load<Product>($"products/{id}");
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Create(string name, decimal price, string category)
        //{
        //    var product = new Product
        //    {
        //        Name = name,
        //        Price = price,
        //        Category = category
        //    };

        //    DocumentSession.Store(product);
        //    DocumentSession.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}