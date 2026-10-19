using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandWatchElaqat.ViewModel
{
    public class ClientProjectCategoryVM
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string BrandWatchCategoeryID { get; set; }

        [Required(ErrorMessage = "Client is required. Please Select a Client")]
        public int SelectedProjectId { get; set; }
        public SelectList ClientProjectList { get; set; }
        public bool IsActive { get; set; }

    }
}