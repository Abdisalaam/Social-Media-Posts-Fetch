using BrandWatchElaqat.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandWatchElaqat.Models
{
    public partial class ClientProjectCategory
    {
        public static List<ClientProjectCategory> GetAllClientProjectCategory()
        {
            var db = new DBModelBrandWatchContainer();

            return db.ClientProjectCategories.ToList(); ;

        }

        public static ClientProjectSubCategory GetClientProjectSubCategoryDetails(int clientProjectSubCategoryID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ClientProjectSubCategories.Find(clientProjectSubCategoryID);
        }

        public static ClientProjectCategory GetClientProjectCategoryDetails(int clientProjectCategoryID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ClientProjectCategories.Find(clientProjectCategoryID);
        }

        public static ClientProjectCategoryVM PrepareClientProjectCategoryToBeEdit(int clientProjectCategoryId)
        {
            var db = new DBModelBrandWatchContainer();
            ClientProjectCategory clientProjectCategory = db.ClientProjectCategories.Find(clientProjectCategoryId);
            ClientProjectCategoryVM clientProjectCategoryVM = new ClientProjectCategoryVM();

            clientProjectCategoryVM.Id = clientProjectCategory.Id;
            clientProjectCategoryVM.CategoryName = clientProjectCategory.CategoryName;
            clientProjectCategoryVM.BrandWatchCategoeryID = clientProjectCategory.BrandWatchCategoeryID;


            clientProjectCategoryVM.ClientProjectList = new SelectList(db.ClientProjects, "Id", "ProjectName");
            clientProjectCategoryVM.SelectedProjectId = clientProjectCategory.ClientProjectId;
            return clientProjectCategoryVM;
        }

        public static ClientProjectSubCategoryVM PrepareClientProjectSubCategoryVMToBeEdit(int ClientProjectSubCategoryId)
        {
            var db = new DBModelBrandWatchContainer();
            ClientProjectSubCategory clientProjectSubCategory = db.ClientProjectSubCategories.Find(ClientProjectSubCategoryId);
            ClientProjectSubCategoryVM subCategoryVM = new ClientProjectSubCategoryVM();

            subCategoryVM.Id = clientProjectSubCategory.Id;
            subCategoryVM.SubCategoryName = clientProjectSubCategory.SubCategoryName;
            subCategoryVM.SubCategoryOrderingNumber = clientProjectSubCategory.OrderingNumber;
            subCategoryVM.BrandWatchSubCategoeryID = clientProjectSubCategory.BrandWatchSubCategoryID;


            subCategoryVM.CategoryList = new SelectList(db.ClientProjectCategories, "Id", "CategoryName");
            subCategoryVM.SelectedCategoryId = clientProjectSubCategory.ClientProjectCategoryId;
            return subCategoryVM;
        }
        public static ClientProjectCategory EditClientProjectSubCategory(ClientProjectSubCategoryVM clientProjectSubCategoryVM)
        {
            var db = new DBModelBrandWatchContainer();
            ClientProjectCategory clientProjectCategory = db.ClientProjectCategories.Find(clientProjectSubCategoryVM.SelectedCategoryId);

            ClientProjectSubCategory clientProjectSubCategory = db.ClientProjectSubCategories.Find(clientProjectSubCategoryVM.Id);

            clientProjectSubCategory.SubCategoryName = clientProjectSubCategoryVM.SubCategoryName;
            clientProjectSubCategory.BrandWatchSubCategoryID = clientProjectSubCategoryVM.BrandWatchSubCategoeryID;
            clientProjectSubCategory.OrderingNumber = clientProjectSubCategoryVM.SubCategoryOrderingNumber;

            clientProjectSubCategory.ClientProjectCategory = clientProjectCategory;
            clientProjectSubCategory.ClientProjectCategoryId = clientProjectCategory.Id;

            db.Entry(clientProjectSubCategory).State = EntityState.Modified;
            db.SaveChanges();
            return clientProjectCategory;
        }


        public static ClientProjectCategory EditClientProjectCategory(ClientProjectCategoryVM clientProjectCategoryVM)
        {
            var db = new DBModelBrandWatchContainer();
            ClientProject clientProject = db.ClientProjects.Find(clientProjectCategoryVM.SelectedProjectId);

            ClientProjectCategory clientProjectCategory = db.ClientProjectCategories.Find(clientProjectCategoryVM.Id);
            clientProjectCategory.BrandWatchCategoeryID = clientProjectCategoryVM.BrandWatchCategoeryID;
            clientProjectCategory.CategoryName = clientProjectCategoryVM.CategoryName;
            clientProjectCategory.ClientProject = clientProject;
            clientProjectCategory.ClientProjectId = clientProject.Id;

            db.Entry(clientProjectCategory).State = EntityState.Modified;
            db.SaveChanges();
            return clientProjectCategory;
        }

        public static ClientProjectCategory DeleteClientProjectCategory(int clientProjectCategoryId)
        {
            var db = new DBModelBrandWatchContainer();

            ClientProjectCategory clientProjectCategory = db.ClientProjectCategories.Find(clientProjectCategoryId);

            if (clientProjectCategory.ClientProjectSubCategories.Any())
            {
               
                db.ClientProjectSubCategories.RemoveRange(clientProjectCategory.ClientProjectSubCategories);
            }
            if (clientProjectCategory.ClintProjectCtegoryQueryIDs.Any())
            {
                db.ClintProjectCtegoryQueryIDs.RemoveRange(clientProjectCategory.ClintProjectCtegoryQueryIDs);
            }
            db.ClientProjectCategories.Remove(clientProjectCategory);
            db.SaveChanges();
            return clientProjectCategory;
        }

        public static ClientProjectCategory DeleteClientProjectSubCategory(int clientProjectSubCategoryId)
        {
            var db = new DBModelBrandWatchContainer();

            ClientProjectSubCategory clientProjectSubCategory = db.ClientProjectSubCategories.Find(clientProjectSubCategoryId);
            ClientProjectCategory clientProjectCategory = clientProjectSubCategory.ClientProjectCategory;


            
            db.ClientProjectSubCategories.Remove(clientProjectSubCategory);
            db.SaveChanges();
            return clientProjectCategory;
        }


    }
}