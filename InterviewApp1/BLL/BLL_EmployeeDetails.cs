using InterviewApp1.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp1.BLL
{
    class BLL_EmployeeDetails
    {
        DAL_DS_EmployeeDetails emp = new DAL_DS_EmployeeDetails();
        public DataTable LoadEmployeeDetails()
        {
            return emp.LoadEmployeeDetails();
        }

        internal bool AddEmployeeDetails(string employeeID, string name, string age, string education, string gender)
        {
           
            string updateGender;

            if (gender == "Male")
            {
                updateGender = "M";
            }

            else
            {
                updateGender = "F";
            }



            return emp.AddEmployeeDetails(employeeID, name, age, education, updateGender);
        }

        internal bool DeleteEmployeeDetails(string employeeID)
        {
            return emp.DeleteEmployeeDetails(employeeID);
        }

        public DataTable LoadEducationDetails()
        {
            return emp.LoadEducationDetails();
        }

    }
}
