using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_SQL_Excersise
{
    interface IRepository
    {
        List<Department> GetDepartments();
        void CreateDepartment(Department newdept);
        void UpdateDepartment(Department newdept);
        void DeleteDepartment(int deptID);
    }
}
