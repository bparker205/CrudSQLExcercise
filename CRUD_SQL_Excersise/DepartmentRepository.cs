
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
    }
}
