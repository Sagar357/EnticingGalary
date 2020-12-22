using EnticingGalary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnticingGalary.ViewModels
{
    public class RedirectViewModel
    {
        public string CategoryName { get; set; }
        public string alias { get; set; }
    }

    public class Redirect_List
    {
        public Redirect_List()
        {
            this.List = new List<Category>();
        }
        public List<Category> List { get; set; }
     
    }
}