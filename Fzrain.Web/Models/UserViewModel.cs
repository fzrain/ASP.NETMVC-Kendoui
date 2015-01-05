using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Fzrain.Core.Domain;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Web.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public DateTime Birthday { get; set; }
       
        public Gender Gender { get; set; }
    }
}