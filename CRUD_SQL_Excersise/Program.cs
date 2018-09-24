﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CRUD_SQL_Excersise
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
            //Console.WriteLine("Creating new department....");
            //departmentRepo.AddDepartment("New Department", "New Group Name", DateTime.Now);
            //Console.WriteLine("Department Created.....");


            //// Converts table to a List then prints table rows to console
            //List<Department> departments = departmentRepo.GetDepartments();

            //foreach (Department department in departments)
            //{
            //    Console.WriteLine($"{department.DepartmentID} | " +
            //        $"{ department.Name} | " +
            //        $"{department.GroupName} | " +
            //        $"{department.ModifiedDate}\n");
            //}


            //Updates department name to a new name
            //Console.WriteLine("Updating department name.....");
            //departmentRepo.UpdateDepartment("New Department", "New Updated Department");
            //Console.WriteLine("Department Updated......");


            // Deletes row from department table based on DepartmentID
            Console.WriteLine("Deleting department ID.......");
            departmentRepo.DeleteDepartment(19);
            Console.WriteLine("Department ID deleted.....");



        }
    }
}