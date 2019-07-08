using MvcForum.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace MvcForum.Web.Helper
{
    public class LDAPHelper
    {
        static string UserId = "haaldapquery";
        static string Password = "rip-comet";

        public static List<StaffModel> getAllStaff()
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://haa.hafele.corp", UserId, Password);
            DirectorySearcher dSearch = new DirectorySearcher(entry);
            dSearch.Filter = "(&(objectCategory=person)(objectClass=user))";
            List<StaffModel> staffList = new List<StaffModel>();
            foreach (SearchResult result in dSearch.FindAll())
            {
                StaffModel staff = searchResult(result);
                staffList.Add(staff);
            }

            return staffList;
        }

        public static List<StaffModel> getStaffByDept(string department, string company)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://haa.hafele.corp", UserId, Password);
            DirectorySearcher dSearch = new DirectorySearcher(entry);
            dSearch.Filter = "(&(objectCategory=person)(objectClass=user)(company="+ company + ")(department=" + department + "))";
            List<StaffModel> staffList = new List<StaffModel>();
            foreach (SearchResult result in dSearch.FindAll())
            {
                StaffModel staff = searchResult(result);
                staffList.Add(staff);
            }

            return staffList;
        }

        //staffID = 'haaXXXX'
        public static StaffModel getStaffByID(string staffID)
        {
            StaffModel staff = new StaffModel();
            DirectoryEntry entry = new DirectoryEntry("LDAP://haa.hafele.corp", UserId, Password);
            DirectorySearcher dSearch = new DirectorySearcher(entry);
            dSearch.Filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + staffID + "))";
            foreach (SearchResult result in dSearch.FindAll())
            {
                staff = searchResult(result);
            }

            return staff;
        }

        private static StaffModel searchResult(SearchResult result)
        {
            StaffModel staff = new StaffModel();
            if (result.Properties["mailnickname"].Count > 0)
            {
                staff.ID = result.Properties["mailnickname"][0].ToString();
            }
            if (result.Properties["sAMAccountName"].Count > 0)
            {
                staff.ID = result.Properties["sAMAccountName"][0].ToString();
            }
            if (result.Properties["physicalDeliveryOfficeName"].Count > 0)
            {
                staff.office = result.Properties["physicalDeliveryOfficeName"][0].ToString();
            }
            if (result.Properties["company"].Count > 0)
            {
                staff.company = result.Properties["company"][0].ToString();
            }
            if (result.Properties["givenName"].Count > 0)
            {
                staff.firstName = result.Properties["givenName"][0].ToString();
            }
            if (result.Properties["sn"].Count > 0)
            {
                staff.lastName = result.Properties["sn"][0].ToString();
            }
            if (result.Properties["displayName"].Count > 0)
            {
                staff.displayName = result.Properties["displayName"][0].ToString();
            }
            if (result.Properties["title"].Count > 0)
            {
                staff.position = result.Properties["title"][0].ToString();
            }
            if (result.Properties["facsimileTelephoneNumber"].Count > 0)
            {
                staff.workFax = result.Properties["facsimileTelephoneNumber"][0].ToString();
            }
            if (result.Properties["mobile"].Count > 0)
            {
                staff.mobile = result.Properties["mobile"][0].ToString();
            }
            if (result.Properties["telephoneNumber"].Count > 0)
            {
                staff.workPhone = result.Properties["telephoneNumber"][0].ToString();
            }
            if (result.Properties["mail"].Count > 0)
            {
                staff.email = result.Properties["mail"][0].ToString();
            }
            if (result.Properties["department"].Count > 0)
            {
                staff.department = result.Properties["department"][0].ToString();
            }
            if (result.Properties["st"].Count > 0)
            {
                staff.state = result.Properties["st"][0].ToString();
            }
            try
            {
                if (result.Properties["thumbnailPhoto"].Count > 0)
                {
                    byte[] staffPhoto = (byte[])result.Properties["thumbnailPhoto"][0];
                    string imreBase64Data = Convert.ToBase64String(staffPhoto);
                    staff.staffPhotoURL = String.Format("data:image/png;base64,{0}", imreBase64Data);
                }
                else
                {
                    staff.staffPhotoURL = null;
                }
            }
            catch (Exception e )
            {

                staff.staffPhotoURL = null;
            }
            return staff;
        }

        //public static string getEmailListByDept(string department)
        //{
        //    string emailString = "";
        //    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MitrefinchConnectionString"].ConnectionString);
        //    con.Open();
        //    string strSQL = "select emp.EMPREF, t.KNOWNBY, emp.FIRSTNAMES, emp.SURNAME, o.DEPARTMENT, j.JOBCLASS, o.SECTION, o.LOCATION, emp.EMAIL, emp.PEREMAIL, emp.HOMETEL, emp.MOBILETEL, emp.dob, emp.ADDR1, emp.ADDR2, emp.ADDR3, emp.POSTCODE, t.REPORTSTO, t.WPHONE, t.WEXT, t.WMOBILE, t.WFAX, t.SITE, c.STARTDATE from MFDATA.mf.TMSEMP emp, MFDATA.MF.OD o, MFDATA.MF.JD j, MFDATA.MF.CE c, MFDATA.mf.TMSTMS t where emp.EMPREF = o.EMPREF and emp.EMPREF = j.EMPREF and emp.EMPREF = c.EMPREF and emp.EMPREF = t.EMPREF and c.LEAVEDATE is null and o.DEPARTMENT = '" + department + "'";
        //    SqlCommand cmd = new SqlCommand(strSQL, con);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        emailString = emailString + reader["EMAIL"].ToString() + ";";
        //    }
        //    con.Close();
        //    return emailString;
        //}
    }
}