using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandWatchElaqat.ViewModel
{
    public class ClientProjectVM
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectBrandWatchID { get; set; }
        public string ProjectCreationDate { get; set; }
        [Required(ErrorMessage = "Client is required. Please Select a Client")]
        public int SelectedElaqatClientId { get; set; }
        public int ShowMentionsStartingFrom { get; set; }
        public SelectList ClientList { get; set; }

        [Required(ErrorMessage = "Project is required. Please Select a Project")]
        public int SelectedClientProjectId { get; set; }
        public SelectList ClientProjectList { get; set; }
        public string ElaqatClientName { get; set; }
        public string Description { get; set; }
        public string BrandWatchClientId { get; set; }
        public string BrandWatchClientName { get; set; }
        public bool IsActive { get; set; }


    }
}