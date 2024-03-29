﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceShop.Core.Contracts;
using ECommerceShop.Core.Models;
using ECommerceShop.Core.ViewModels;
using ECommerceShop.DataAccess.InMemory;

namespace ECommerceShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        //ProductRepository context;
        //ProductCategoryRepository productCategories;

        //InMemoryRepository<Product> context;
        //InMemoryRepository<ProductCategory> productCategories;

        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        //public ProductManagerController()
        //{
        //    //context = new ProductRepository();
        //    //productCategories = new ProductCategoryRepository();
        //    context = new InMemoryRepository<Product>();
        //    productCategories = new InMemoryRepository<ProductCategory>();
        //}

        //Refactor with dependency injection
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryConext)
        {
            context = productContext;
            productCategories = productCategoryConext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();

            //Product product = new Product();
            //return View(product);

            return View(viewModel);
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
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//" + product.Image));
                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                //return View(product);
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//" + productToEdit.Image));
                }

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
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
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}