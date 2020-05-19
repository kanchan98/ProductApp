using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductApp.Data;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly MongoDBContext _context;
        private readonly IOptions<MongoDBSettings> _settings;




        public ProductController(IOptions<MongoDBSettings> settings)
        {
            _context = new MongoDBContext(settings);
            _settings = settings;
          
        }


        // GET: Product
        public ActionResult Index()
       {
            
            var _ProductCount = _context.productsTable.Find(p => p.Status == "Active").ToList();
           
            return View(_ProductCount);
           
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products prod)
        {
            try
            {
                // TODO: Add insert logic here


                if (ModelState.IsValid)
                {
                    //Check same Product name already exists
                    var checkNameInList = _context.productsTable.Find(p => p.Status == "Active" &&p.Name.ToLower() == prod.Name.ToLower()).FirstOrDefault();
                    if (checkNameInList != null)
                    {
                        Notification notify = new Notification("Error", "Product Name is already present...!");
                        ViewBag.Notification = notify;
                        return View(prod);
                    }
                    _context.productsTable.InsertOne(prod);

                    return RedirectToAction(nameof(Index));
                }
                return View(prod);
            }
            catch
            {
                Notification notify = new Notification("Error", "Something went wrong...!");
                ViewBag.Notification = notify;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
                Products product = _context.productsTable.Find<Products>(p => p.Id == id).FirstOrDefault();

                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
           
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Products product)
        {
            try
            {
                // TODO: Add update logic here
                var updateProduct = Builders<Products>.Update.Set(p => p.Name, product.Name)
                                                               .Set(p => p.Price, product.Price)
                                                               .Set(p => p.Status, product.Status)
                                                               .Set(p => p.Quantity, product.Quantity)
                                                               .Set(p => p.IsGSTApplicable, product.IsGSTApplicable)
                                                               .Set(p => p.Purchase_Date, product.Purchase_Date)
                                                               .Set(p => p.Expiry_Date, product.Expiry_Date)
                                                                .Set(p => p.Color, product.Color)
                                                               .Set(p => p.Status, product.Status);

                _context.productsTable.FindOneAndUpdate(e => e.Id == id ,updateProduct);

                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                Notification notify = new Notification("Error", "Something went wrong...!");
                ViewBag.Notification = notify;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Products product = _context.productsTable.Find<Products>(p => p.Id == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Products product)
        {
            try
            {
              
                    _context.productsTable.FindOneAndDelete(p => p.Id == id);

                    return RedirectToAction(nameof(Index));
              
            }
            catch (Exception ex)
            {
                Notification notify = new Notification("Error", "Something went wrong...!");
                ViewBag.Notification = notify;
                return View();
            }
        }
    }
}