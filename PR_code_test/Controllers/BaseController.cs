using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;


namespace PR_code_test.Controllers
{
    public class BaseController : SurfaceController
    {
        public IPublishedContent GetCurrentPage()
        {
            int? currentPageId = UmbracoContext.PageId;
            UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext);
            IPublishedContent currentPage = currentPageId != null ? umbracoHelper.TypedContent(currentPageId) : null;

            return currentPage;
        }
    }
}