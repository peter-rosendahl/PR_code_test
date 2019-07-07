using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PR_code_test.Models
{
    public class MemberModel : BaseModel
    {
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email address")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; } = string.Empty;

        [DisplayName("Repeat Password")]
        public string RepeatPassword { get; set; } = string.Empty;


        [DisplayName("Remember me")]
        public bool RememberMe { get; set; } = false;
    }
}