using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandWatchElaqat.ViewModel
{
    public class ClientProjectSubCategoryVM
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandWatchSubCategoeryID { get; set; }
        public string SubCategoryOrderingNumber { get; set; }

        [Required(ErrorMessage = "Client is required. Please Select a Client")]
        public int SelectedCategoryId { get; set; }
        public SelectList CategoryList { get; set; }
        public bool IsActive { get; set; }
        public int TotalCategoryCount { get; set; }

        public double TotalCategoryPrecentage { get; set; }


    }
}