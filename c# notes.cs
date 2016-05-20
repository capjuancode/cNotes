
// select query from data layer
public IEnumerable<KeyValuePair<Int32, string>> Namefunction()
        {
            AssetTrackingEntities db = new NameOfYourEntities();
            return (from u in db.NameOfYourTable select new {Key = u.NameColum, Name = u.NameColum}).OrderBy(u => u.Key).ToList()
                .Select(u => new KeyValuePair<Int32, string>(u.Key, u.Name));
        }

//method to call

[System.Web.Services.WebMethod]
     public static string GetDropdownRole()
     {
         var currentDataLayer = new NameOfYourDataLayer();
         var a = currentDataLayer.GetDropdownRole().ToList();

         return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(a);
     }

// ==========================================================================================================
// create a class encription
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    class Security
    {
        public string encryptPassword(string pass)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);
            string EncryptedPassword = Convert.ToBase64String(bytes);
            return EncryptedPassword;
        }

        private string decryptPassword(string Encryptedpass)
        {
            byte[] bytes = Convert.FromBase64String(Encryptedpass);
            string decryptedPassword = System.Text.Encoding.Unicode.GetString(bytes);
            return decryptedPassword;
        }
    }
}

// ==========class for calling StoredProcedure in difrent DB==========================================================================

// conetcion
<add name="connection_name" connectionString="Data Source=API-or-Name;Initial Catalog=Name_tabble;Persist Security Info=True;User ID=LoginID ;Password=LoginPass " providerName="System.Data.SqlClient" />

// class
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    class YourNameOfYourClass
    {
        public Int32 EmployeeStatus { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmpFirstThreeLastName { get; set; }

        public nameFunctionEmployee(string EmpNum)
        {
            getUserActive(EmpNum);
        }

        public void getUserActive(string EmpNum)
        {

            DataSet ds = new DataSet("AnyName");
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_name"].ConnectionString))
            {

                SqlCommand sqlComm = new SqlCommand("sp_EmpSecStatus", connection);
                sqlComm.Parameters.AddWithValue("@EmpNum", EmpNum);

                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

                dt = ds.Tables[0];


            }



               EmployeeStatus =        Convert.ToInt32( dt.Rows[0].ItemArray[0]); //number int( 0=active 1=termd, 2=not exits
               EmployeeFullName =      dt.Rows[0].ItemArray[1].ToString(); //full name string
               EmpFirstThreeLastName = dt.Rows[0].ItemArray[2].ToString(); //first 3 of last name string

        }
    }
}


// this is how you would call this class


         [System.Web.Services.WebMethod]
         public static string getUserActive(string EmpNum)
         {

            nameFunction ke = new nameFunction(EmpNum);

            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ke);

         }
