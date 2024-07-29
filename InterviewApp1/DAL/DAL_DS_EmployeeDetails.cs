using InterviewApp1.DAL.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using InterviewApp1.DAL.DatabaseConnectivity;

namespace InterviewApp1.DAL
{
    class DAL_DS_EmployeeDetails
    {

        public DataTable LoadEmployeeDetails()
        {
            string query = @"SELECT a.Emp_ID,
                                    a.Name,
                                    a.Age,
									(select E_Category from Education where E_ID =Edu_ID  ) AS Education 
                                        From Employee_Details a
                                        ORDER BY Emp_ID";

            DataSet1 ds = new DataSet1();
            DataTable dt = ds.tbl_Employee;
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader dr = DBconnect.ReadTable(query))
                {
                    while (dr.Read())
                    {
                        DataRow r = dt.NewRow();
                        r["EmployeeID"] = dr["Emp_ID"].ToString();
                        r["Name"] = dr["Name"].ToString();
                        r["Age"] = dr["Age"].ToString();
                        r["Education"] = dr["Education"].ToString();
                        dt.Rows.Add(r);
                    }
                }
            }
            
            return dt;
        }

        public DataTable LoadEducationDetails()
        {
            string query = @"SELECT E_ID,
                                    E_Category                               
									from Education";

            DataSet1 ds = new DataSet1();
            DataTable dt = ds.EduTable;
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader dr = DBconnect.ReadTable(query))
                {
                    while (dr.Read())
                    {
                        DataRow r = dt.NewRow();
                        r["eID"] = dr["E_ID"].ToString();
                        r["EduType"] = dr["E_Category"].ToString();
                        dt.Rows.Add(r);
                    }
                }
            }

            return dt;
        }

        internal bool AddEmployeeDetails(string employeeID, string name, string age, string education, string updateGender)
        {
            bool res = false;

            string Query = "";

            if(employeeID != "")
            {
                Query = @"UPDATE Employee_Details
                        SET Name = '" + name + "', " +
                        "Age = '" + age + "', " +
                        "Edu_ID = '" + education + "' " +
                        "WHERE Emp_ID = '" + employeeID + "' ";

                using (var DBconnect = new DBconnect())
                {
                        if (DBconnect.AddEditDel(Query))
                            res = true;

                }

            }      
            else
            {
                Query = "INSERT INTO Employee_Dtails(EMPLOYEEID, NAME, AGE, GENDER, EDUCATION) " +
                                    "VALUES(CONCAT('EMP', LPAD((SELECT NVL(MAX(TO_NUMBER(SUBSTR(EMPLOYEEID, 4))) + 1, 1) FROM Employee_Dtails), 3, '0'))," +
                                    "'" + name + "'," +
                                    "'" + age + "'," +
                                    "'" + education + "'," +
                                    "'" + updateGender + "')";

                using (var DBconnect = new DBconnect())
                {
                    if (DBconnect.AddEditDel(Query))
                        res = true;

                }
            }
            
            return res;
        }

        internal bool DeleteEmployeeDetails(string employeeID)
        {
            bool res = false;

            string Query = "";

            if (employeeID != "")
            {
                Query = @"DELETE FROM Employee_Details
                               WHERE Emp_ID = '" + employeeID + "' ";

                using (var DBconnect = new DBconnect())
                {
                    if (DBconnect.AddEditDel(Query))
                        res = true;

                }

            }

            return res;
        }

    }
}
    