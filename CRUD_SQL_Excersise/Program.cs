using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace CRUD_SQL_Excercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
#if DEBUG
                .AddJsonFile("appsettings.Debug.json")
#else
                .AddJsonFile("appsettings.Release.json")
#endif
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            var departmentRepo = new DepartmentRepository(connString);

            //// Create Department row in department table
            Console.WriteLine("Creating new department....");
            departmentRepo.AddDepartment("New Department", "New Group Name", DateTime.Now);
            Console.WriteLine("Department Created.....");


            //// Converts table to a List then prints table rows to console
            List<Department> departments = departmentRepo.GetDepartments();

            foreach (Department department in departments)
            {
                Console.WriteLine($"{department.DepartmentID} | " +
                    $"{ department.Name} | " +
                    $"{department.GroupName} | " +
                    $"{department.ModifiedDate}\n");
            }


            //Updates department name to a new name
            Console.WriteLine("Updating department name.....");
            departmentRepo.UpdateDepartment("New Department", "New Updated Department");
            Console.WriteLine("Department Updated......");


            //Deletes row from department table based on DepartmentID
            Console.WriteLine("Deleting department ID.......");
            departmentRepo.DeleteDepartment(21);
            Console.WriteLine("Department ID deleted.....");

            // **************************************************************
            // **************   Begin Dapper Implementation   ***************
            // **************************************************************

            var dapperdepartmentRepo = new DapperDepartmentRepository(connString);

            var dept = new Department
            {
                DepartmentID = 999,
                Name = "New Dapper Department",
                GroupName = "Dapper Group",
                ModifiedDate = DateTime.Now,

            };

            // Converts all contents of the department table to a <List> then displays contents to console
            List<Department> departments = dapperdepartmentRepo.GetDepartments();
            foreach (Department department in departments)
            {
                Console.WriteLine($"{department.DepartmentID}  |  " +
                    $"{department.Name}  |  " +
                    $"{department.GroupName}  |  " +
                    $"{department.ModifiedDate}");
            }

            // Inserts new row into department table
            Console.WriteLine("Creating new department:........");
            dapperdepartmentRepo.CreateDepartment(dept);
            Console.WriteLine("Department created!........");

            // Updates existing table row 
            Console.WriteLine("Updating department:......");
            dapperdepartmentRepo.UpdateDepartment(dept);
            Console.WriteLine("Department updated:.......");

            int id;

            while (true)
            {
                Console.WriteLine("What department would you like to delete:");
                bool pass = int.TryParse(Console.ReadLine(), out id);
                if (!pass)
                {
                    Console.WriteLine("Please enter a number!");
                    continue;
                }
                else
                {
                    break;
                }
            }

            // Deletes table row based on DepartmentID
            Console.WriteLine("Deleting department:......");
            dapperdepartmentRepo.DeleteDepartment(id);
            Console.WriteLine("Department deleted:.......");

            //Console.ReadLine();
        }
    }
}