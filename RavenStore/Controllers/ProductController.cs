using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RavenStore.Infrastructure.Raven;
using RavenStore.Models;
using RavenStore.ViewModels;

namespace RavenStore.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            var products = DocumentSession.Advanced.DocumentQuery<Product, Products_ByCategory>().ToList();

            // Returning all the products as Json
            // return Json(products, JsonRequestBehavior.AllowGet);
            return View(products);
        }
        public ActionResult Create()
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                Product = new Product()
            };

            return View(viewModel.Product);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {

                if (file != null)
                {
                   Trace.WriteLine(file);
                }

                DocumentSession.Store(product);
                DocumentSession.SaveChanges();
                return RedirectToAction("Index");
            }

            
        }

       

        [HttpPost]
        public ActionResult Edit(string id, Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                Product product =  DocumentSession.Load<Product>(id);
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.Category = updatedProduct.Category;

                DocumentSession.Store(product);
                DocumentSession.SaveChanges();

                RedirectToAction("Index");
            }

            return View(updatedProduct);
        }

        public ActionResult Edit(string id)
        {
            Product product = DocumentSession.Load<Product>(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.Product = product;
                return View(viewModel.Product);
            }

           
        }

        public ActionResult Details(string id)
        {
            var product = DocumentSession.Load<Product>(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        //public ActionResult Edit(int id, string name, decimal price, string category)
        //{

        //    var product = DocumentSession.Load<Product>($"products/{id}");

        //    product.Name = name;
        //    product.Price = price;
        //    product.Category = category;

        //    DocumentSession.SaveChanges();

        //    return RedirectToAction("Index");

        //}

        public ActionResult BrowseProductsByCategory(string category)
        {
            var products = DocumentSession.Advanced.DocumentQuery<Product, Products_ByCategory>()
                .WhereEquals("category", category)
                .ToList();

            // Returning all the products as Json
            return Json(products, JsonRequestBehavior.AllowGet);

            //return View(products);
        }

        public ActionResult Delete(string id)
        {
            var productToDelete = DocumentSession.Load<Product>(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            var productToDelete = DocumentSession.Load<Product>(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
               DocumentSession.Delete(id);
               DocumentSession.SaveChanges();
               return RedirectToAction("Index");
            }
        }
    }
}