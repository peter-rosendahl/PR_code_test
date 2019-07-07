using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR_code_test.Models
{
    public class ProductListModel : BaseModel
    {
        public string ListHeadline { get; set; }
        public string ListDescription { get; set; }
        public List<Product> ProductList { get; set; }
    }

    public class Product
    {
        public string Headline { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ClassName { get; set; }
    }
}