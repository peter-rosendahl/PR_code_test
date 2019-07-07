using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using PR_code_test.Models;
using Umbraco.Web;
using Umbraco.Core.Models;

namespace PR_code_test.Controllers
{
    public class ProductController : BaseController
    {
        private const string _productListHeadline = "productListHeadline";
        private const string _productListDescription = "productListDescription";
        private const string _productHeadline = "productHeadline";
        private const string _productDescription = "productDescription";
        private const string _productPrice = "productPricePerMonth";
        private const string _productClassName = "productStyleClassName";

        public ActionResult GetProducts()
        {
            IPublishedContent currentPage = GetCurrentPage();

            IPublishedContent productListView = currentPage.Children.FirstOrDefault(x => x.DocumentTypeAlias == "productList");

            if (productListView == null)
            {
                return null;
            }


            ProductListModel viewModel = new ProductListModel();
            viewModel.ProductList = new List<Product>();
            viewModel.ListHeadline = productListView.GetPropertyValue<string>(_productListHeadline);
            viewModel.ListDescription = productListView.GetPropertyValue<string>(_productListDescription);


            if (!productListView.Children.Any())
            {
                return PartialView("ProductList", viewModel);
            }

            foreach (IPublishedContent productView in productListView.Children)
            {
                Product product = new Product();
                product.Headline = productView.GetPropertyValue<string>(_productHeadline);
                product.Description = productView.GetPropertyValue<string>(_productDescription);
                product.Price = productView.GetPropertyValue<string>(_productPrice);
                product.ClassName = productView.GetPropertyValue<string>(_productClassName);

                viewModel.ProductList.Add(product);
            }


            return PartialView("ProductList", viewModel);
        }
    }
}