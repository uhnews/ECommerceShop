using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceShop.WebUI;
using ECommerceShop.WebUI.Controllers;
using ECommerceShop.Core.Contracts;
using ECommerceShop.Core.Models;
using ECommerceShop.Core.ViewModels;

namespace ECommerceShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        //[TestMethod]
        //public void Index()
        //{
        //    //// Arrange
        //    //HomeController controller = new HomeController();

        //    //// Act
        //    //ViewResult result = controller.Index() as ViewResult;

        //    //// Assert
        //    //Assert.IsNotNull(result);
        //}

        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();

            productContext.Insert(new Product());
            HomeController controller = new HomeController(productContext, productCategoryContext);
            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Products.Count());
        }
    }
}
