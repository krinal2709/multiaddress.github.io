using AddressBook_Replica.Areas.CON_ContactCategory.Models;
using AddressBook_Replica.Areas.LOC_City.Models;
using AddressBook_Replica.Areas.LOC_Country.Models;
using AddressBook_Replica.Areas.LOC_State.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBook_Replica.DAL
{
    public class LOC_DAL : LOC_DAL_Base
    {
        #region COUNTRY_DROPDOWN

        public List<LOC_Country_DropDownModel> LOC_Country_DropDown(string connectionString, int userID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_Country_SelectComboBox]");

                database.AddInParameter(command, "@UserID", SqlDbType.Int, userID);

                DataTable dt = new DataTable();
                List<LOC_Country_DropDownModel> country_list = new List<LOC_Country_DropDownModel>();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            LOC_Country_DropDownModel tuple = new LOC_Country_DropDownModel();
                            tuple.CountryID = Convert.ToInt32(dr["CountryID"]);
                            tuple.CountryName = Convert.ToString(dr["CountryName"]);
                            country_list.Add(tuple);
                        }
                    }
                }
                return country_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region STATE_DROPDOWN

        public List<LOC_State_DropDownModel> LOC_State_DropDown(string connectionString, int CountryID, int userID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_State_SelectComboBoxByCountryID]");

                database.AddInParameter(command, "@CountryID", DbType.Int32, CountryID);
                database.AddInParameter(command, "@UserID", SqlDbType.Int, userID);

                DataTable dt = new DataTable();
                List<LOC_State_DropDownModel> state_list = new List<LOC_State_DropDownModel>();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            LOC_State_DropDownModel tuple = new LOC_State_DropDownModel();
                            tuple.StateID = Convert.ToInt32(dr["StateID"]);
                            tuple.StateName = Convert.ToString(dr["StateName"]);
                            state_list.Add(tuple);
                        }
                    }
                }
                return state_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region CITY_DROPDOWN

        public List<LOC_City_DropDownModel> LOC_City_DropDown(string connectionString, int StateID, int userID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_City_SelectComboBoxByStateID]");

                database.AddInParameter(command, "@StateID", DbType.Int32, StateID);
                database.AddInParameter(command, "@UserID", SqlDbType.Int, userID);

                DataTable dt = new DataTable();
                List<LOC_City_DropDownModel> city_list = new List<LOC_City_DropDownModel>();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            LOC_City_DropDownModel tuple = new LOC_City_DropDownModel();
                            tuple.CityID = Convert.ToInt32(dr["CityID"]);
                            tuple.CityName = Convert.ToString(dr["CityName"]);
                            city_list.Add(tuple);
                        }
                    }
                }
                return city_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion
    }
}
