using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR_code_test.Models
{
    public class CallToActionModel : BaseModel
    {
        public string Message { get; set; }
        public string Link { get; set; }
        public string LinkText { get; set; }
    }
}