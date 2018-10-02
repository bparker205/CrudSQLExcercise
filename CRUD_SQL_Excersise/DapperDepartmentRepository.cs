using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;

namespace CRUD_SQL_Excercise
{
    public class DapperDepartmentRepository : IRepository
    {
        private string connectionString;

        public DapperDepartmentRepository(string _connectionstring)
        {
            connectionString = _connectionstring;
        }

        public List<Department> GetDepartments()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query<Department>("SELECT DepartmentID, Name, GroupName, ModifiedDate as date FROM department;").ToList();
            }
        }

        public void CreateDepartment(Department newdept)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                conn.Execute("INSERT INTO department(Name,GroupName,ModifiedDate) VALUES(@name,@gname,@date);", new { name = newdept.Name, gname = newdept.GroupName, date = newdept.ModifiedDate });
            }
        }

        public void UpdateDepartment(Department newdept)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                conn.Execute("UPDATE department SET Name = @newName, ModifiedDate = @date WHERE DepartmentId = @id;", new { newName = newdept.Name, date = newdept.ModifiedDate, id = newdept.DepartmentID });
            }
        }

        public void DeleteDepartment(int deptID)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                conn.Execute("DELETE FROM department WHERE DepartmentID = @id;", new { id = deptID });
            }
        }
    }
}
