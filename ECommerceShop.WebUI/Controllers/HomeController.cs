﻿using ECommerceShop.Core.Contracts;
using ECommerceShop.Core.Models;
using ECommerceShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }

        public ActionResult Index(string category = null)
        {
            //List<Product> products = context.Collection().ToList();
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();

            if (category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            //return View(products);
            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}