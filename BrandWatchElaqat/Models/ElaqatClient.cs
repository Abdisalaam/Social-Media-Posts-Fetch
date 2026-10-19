using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandWatchElaqat.ViewModel;

namespace BrandWatchElaqat.Models
{

    public partial class ElaqatClient
    {
        public static List<ElaqatClient> GetAllClient()
        {
            var db = new DBModelBrandWatchContainer();
            List<ElaqatClient> clients = db.ElaqatClients.ToList();

            return clients;

        }

       
        public static ElaqatClientVM PrepareElaqatClientToBeCreated()
        {
            ElaqatClientVM elaqatClientVM = new ElaqatClientVM();
            var allRoless = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).ToList();
            elaqatClientVM._RoleList = new SelectList(allRoless, "Id", "Name");
            elaqatClientVM.SelectedRole_ID = 2;
            return elaqatClientVM;
        }
        public static ElaqatClient AddNewElaqatClient(ElaqatClientVM elaqatClientVM)
        {               
                var db = new DBModelBrandWatchContainer();
                ElaqatClient elaqatClient = new ElaqatClient();
                elaqatClient.ClientName = elaqatClientVM.ClientName;
                elaqatClient.CompanyName = elaqatClientVM.CompanyName;
                elaqatClient.PhoneNumber = elaqatClientVM.PhoneNumber;
                elaqatClient.ContactPersonName = elaqatClientVM.ContactPersonName;
                elaqatClient.Adress = elaqatClientVM.Adress;
                elaqatClient.RegisterDate = DateTime.Now;
                elaqatClient.ClientEmail = elaqatClientVM.Email;
                elaqatClient.ClientRegisterID = elaqatClientVM.ClientRegisterID;
                elaqatClient.ClientRole = elaqatClientVM.ClientRole;
                elaqatClient.IsActive = true;

                db.ElaqatClients.Add(elaqatClient);
                db.SaveChanges();

            return elaqatClient;
               

           
        }

        public static ElaqatClient EditElaqatClient(ElaqatClientVM elaqatClientVM)
        {
            var db = new DBModelBrandWatchContainer();
            ElaqatClient elaqatClient = db.ElaqatClients.Find(elaqatClientVM.Id);
            elaqatClient.Adress = elaqatClientVM.Adress;
            elaqatClient.CompanyName = elaqatClientVM.CompanyName;
            elaqatClient.ClientName = elaqatClientVM.ClientName;
            elaqatClient.ContactPersonName = elaqatClientVM.ContactPersonName;
            elaqatClient.PhoneNumber = elaqatClientVM.PhoneNumber;
            elaqatClient.IsActive = elaqatClientVM.IsActive;
            db.Entry(elaqatClient).State = EntityState.Modified;
            db.SaveChanges();
            return elaqatClient;
        }

        public static ElaqatClientVM PrepareElaqatClientToBeEdit(int elaqatClientId)
        {
            var db = new DBModelBrandWatchContainer();
            ElaqatClientVM elaqatClientVM = new ElaqatClientVM();
            ElaqatClient elaqatClient = db.ElaqatClients.Find(elaqatClientId);
            elaqatClientVM.Id = elaqatClient.Id;
            elaqatClientVM.Adress = elaqatClient.Adress;
            elaqatClientVM.ClientName = elaqatClient.ClientName;
            elaqatClientVM.ContactPersonName = elaqatClient.ContactPersonName;
            elaqatClientVM.PhoneNumber = elaqatClient.PhoneNumber;
            elaqatClientVM.Email = elaqatClient.ClientEmail;
            elaqatClientVM.RegisterDate = elaqatClient.RegisterDate;
            elaqatClientVM.ClientRegisterID = elaqatClient.ClientRegisterID;
            elaqatClientVM.ClientRole = elaqatClient.ClientRole;
            elaqatClientVM.IsActive = elaqatClient.IsActive;
            return elaqatClientVM;
        }

        public static ElaqatClient GetElaqatClientDetails(int elaqatClientID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ElaqatClients.Find(elaqatClientID);
        }

        public static ElaqatClient GetElaqatClientByRegisterID(string elaqatClientRegisterID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ElaqatClients.Where(e=> e.ClientRegisterID == elaqatClientRegisterID).FirstOrDefault();
        }
        public static ElaqatClient GetElaqatClientByEmail(string email)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ElaqatClients.Where(e => e.ClientEmail == email).FirstOrDefault();
        }
        public static ElaqatClient GetElaqatClientByRegisterIdAndEmail(string email, string elaqatClientRegisterID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ElaqatClients.Where(e => e.ClientEmail == email && e.ClientRegisterID == elaqatClientRegisterID).FirstOrDefault();
        }

        public static bool UpdateElaqatClientPasswordISUpdated(int elaqatClientID)
        {
            try
            {
                var db = new DBModelBrandWatchContainer();
                var client = db.ElaqatClients.Find(elaqatClientID);
                client.IsPasswordUpdated = true;
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public static bool ElaqatClientPasswordISReseted(int elaqatClientID)
        {
            try
            {
                var db = new DBModelBrandWatchContainer();
                var client = db.ElaqatClients.Find(elaqatClientID);
                client.IsPasswordUpdated = false;
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}