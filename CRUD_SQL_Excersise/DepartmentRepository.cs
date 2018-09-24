﻿
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CRUD_SQL_Excersise
{
    public class DepartmentRepository
    {
        // Connection string we use for the methods 
        private string connectionString;

        public DepartmentRepository(string _connectionstring)
        {
            connectionString = _connectionstring;
        }



        // GetDepartments method that returns a list of departments Department table 
        public List<Department> GetDepartments()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DepartmentID AS id, Name, GroupName as gname, ModifiedDate as date FROM department;";
                MySqlDataReader reader = cmd.ExecuteReader();

                List<Department> departments = new List<Department>();
                // While loop to iterate through each line in Department table then add to list
                while (reader.Read())
                {
                    Department dept = new Department();
                    dept.DepartmentID = (int)reader["id"];
                    dept.Name = reader["Name"].ToString();
                    dept.GroupName = reader["gname"].ToString();
                    dept.ModifiedDate = (DateTime)reader["date"];
                    departments.Add(dept);
                }

                return departments;
            }
        }

        // AddDepartment method that creates a new department to table
        public void AddDepartment(string name, string gname, DateTime date)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO department (Name, GroupName, ModifiedDate) VALUES (@name, @gName, @date)";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("gname", gname);
                cmd.Parameters.AddWithValue("date", date);
                cmd.ExecuteNonQuery();
            }


        }
    }
}
