using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using PR_code_test.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace PR_code_test.Controllers
{
    public class CallToActionController : BaseController
    {
        private readonly string _Message = "message";
        private readonly string _Target = "target";
        private readonly string _LinkText = "linkText";
        public ActionResult GetCallToAction()
        {
            IPublishedContent currentPage = GetCurrentPage();

            IPublishedContent callToActionView = currentPage.Children.FirstOrDefault(x => x.DocumentTypeAlias == "callToAction");

            if (callToActionView == null)
            {
                return null;
            }

            CallToActionModel viewModel = new CallToActionModel();

            viewModel.Message = callToActionView.GetPropertyValue<string>(_Message);

            IPublishedContent target = callToActionView.GetPropertyValue<IPublishedContent>(_Target);
            if (target != null)
            {
                viewModel.Link = target.Url;
                viewModel.LinkText = callToActionView.GetPropertyValue<string>(_LinkText);
            }

            return PartialView("CallToAction", viewModel);

        }
    }
}