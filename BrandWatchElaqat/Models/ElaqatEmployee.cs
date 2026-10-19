using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandWatchElaqat.ViewModel;

namespace BrandWatchElaqat.Models
{
    public partial class ElaqatEmployee
    {
        public static List<ElaqatEmployee> GetAllEmployees()
        {
            var db = new DBModelBrandWatchContainer();
            List<ElaqatEmployee> employees = db.ElaqatEmployees.ToList();

            return employees;

        }


        public static EmployeeVM PrepareElaqatEmployeeToBeCreated()
        {
            EmployeeVM employeeVM = new EmployeeVM();
            var allRoless = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).ToList();
            employeeVM._RoleList = new SelectList(allRoless, "Id", "Name");
            employeeVM.SelectedRole_ID = 3;
            return employeeVM;
        }

        public static EmployeeVM PrepareElaqatEmployeeToBeEdited(int employeeId)
        {
           
            var db = new DBModelBrandWatchContainer();
            var employee = db.ElaqatEmployees.Find(employeeId);
            EmployeeVM employeeVM = new EmployeeVM();
            var allRoless = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).ToList();
            employeeVM._RoleList = new SelectList(allRoless, "Id", "Name");
            employeeVM.SelectedRole_ID = 3;
            if (employee.EmployeeRole == "Admin")
            {
                employeeVM.SelectedRole_ID = 1;
            }
            
            employeeVM.Email = employee.EmployeeEmail;
            employeeVM.EmployeeName = employee.EmployeeName;
            employeeVM.EmployeeRole = employee.EmployeeRole;
            employeeVM.IsActive = employee.IsActive;
            employeeVM.PhoneNumber = employee.PhoneNumber;
            employeeVM.Id = employee.Id;
            employeeVM.EmployeeRegisterID = employee.EmployeeRegisterID;
            employeeVM.RegisterDate = employee.RegisterDate;
            return employeeVM;
        }
        public static ElaqatEmployee AddNewElaqatEmployee(EmployeeVM elaqatElaqatVM)
        {
            var db = new DBModelBrandWatchContainer();
            ElaqatEmployee elaqatEmployee = new ElaqatEmployee();
            elaqatEmployee.EmployeeName = elaqatElaqatVM.EmployeeName;
            elaqatEmployee.PhoneNumber = elaqatElaqatVM.PhoneNumber;
            elaqatEmployee.RegisterDate = DateTime.Now;
            elaqatEmployee.EmployeeEmail = elaqatElaqatVM.Email;
            elaqatEmployee.EmployeeRegisterID = elaqatElaqatVM.EmployeeRegisterID;
            elaqatEmployee.EmployeeRole = elaqatElaqatVM.EmployeeRole;
            elaqatEmployee.IsActive = true;
            db.ElaqatEmployees.Add(elaqatEmployee);
            db.SaveChanges();
            return elaqatEmployee;
        }

        public static ElaqatEmployee GetElaqatEmployeeByRegisterID(string elaqatEmployeeRegisterID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ElaqatEmployees.Where(e => e.EmployeeRegisterID == elaqatEmployeeRegisterID).FirstOrDefault();
        }
        public static ElaqatEmployee UpdateElaqatEmployee(EmployeeVM elaqatElaqatVM)
        {
            var db = new DBModelBrandWatchContainer();
            ElaqatEmployee elaqatEmployee = db.ElaqatEmployees.Find(elaqatElaqatVM.Id);
            elaqatEmployee.EmployeeName = elaqatElaqatVM.EmployeeName;
            elaqatEmployee.PhoneNumber = elaqatElaqatVM.PhoneNumber;
            elaqatEmployee.EmployeeRole = elaqatElaqatVM.EmployeeRole;
            elaqatEmployee.IsActive = elaqatElaqatVM.IsActive;
            db.Entry(elaqatEmployee).State = EntityState.Modified;
            db.SaveChanges();
            return elaqatEmployee;
        }

        public static ElaqatEmployee GetElaqatEmployeeDetails(int elaqatEmployeeID)
        {
            var db = new DBModelBrandWatchContainer();
            return db.ElaqatEmployees.Find(elaqatEmployeeID);
        }
        public static bool UpdateElaqatEmployeePasswordISUpdated(int elaqatEmployeeID)
        {
            try
            {
                var db = new DBModelBrandWatchContainer();
                var employee = db.ElaqatEmployees.Find(elaqatEmployeeID);
                employee.IsPasswordUpdated = true;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static bool ElaqatEmployeePasswordISReseted(int elaqatEmployeeID)
        {
            try
            {
                var db = new DBModelBrandWatchContainer();
                var employee = db.ElaqatEmployees.Find(elaqatEmployeeID);
                employee.IsPasswordUpdated = false;
                db.Entry(employee).State = EntityState.Modified;
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