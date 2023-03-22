using AddressBook_Replica.Areas.LOC_State.Models;
using AddressBook_Replica.BAL;
using AddressBook_Replica.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Areas.LOC_State.Controllers
{
    [CheckAccess]
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {

        #region PRIVATE_VAR

        private IConfiguration Configuration;
        private LOC_DAL dal = new LOC_DAL();

        #endregion

        #region CONSTRUCTOR

        public LOC_StateController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region SELECT_ALL

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("Default");

            int userID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));

            return View("../Home/Index", dal.LOC_State_SelectAll(connectionString, userID));
        }

        #endregion

        #region DELETE

        public IActionResult Delete(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("Default");

            int userID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            if (dal.LOC_State_Delete(connectionString, StateID, userID))
            {
                TempData["LOC_State_Delete_Msg"] = "State Deleted Successfully.";
            }
            else
            {
                TempData["LOC_State_Delete_Msg"] = "Error in State Deletion.";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("Default");

            int userID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
        
            ViewBag.CountryList = dal.LOC_Country_DropDown(connectionString, userID);
            ViewBag.UserID = userID;

            if (StateID != null)
            {
                return View("../Home/LOC_StateAddEdit", dal.LOC_State_SelectByPk(connectionString, (int)StateID, userID));
            }
            return View("../Home/LOC_StateAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(LOC_StateModel stateModel)
        {
            string connectionString = this.Configuration.GetConnectionString("Default");

            if (stateModel.StateID == null)
            {
                if (dal.LOC_State_Insert(connectionString, stateModel))
                {
                    TempData["LOC_State_Insert_Msg"] = "State Inserted Successfully.";
                }
                else
                {
                    TempData["LOC_State_Insert_Msg"] = "Error in State Insertion.";
                }
            }
            else
            {
                if (dal.LOC_State_Update(connectionString, stateModel))
                {
                    TempData["LOC_State_Update_Msg"] = "State Updated Successfully.";
                }
                else
                {
                    TempData["LOC_State_Update_Msg"] = "Error in State Updation.";
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Add");
        }

        #endregion

        #region SEARCH_BOX

        public IActionResult Search()
        {
            string connectionString = this.Configuration.GetConnectionString("Default");

            LOC_State_SearchModel state_SearchModel = new LOC_State_SearchModel();

            state_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            state_SearchModel.StateName = HttpContext.Request.Form["StateName"].ToString();

            int userID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));

            ViewBag.CountryName = state_SearchModel.CountryName;
            ViewBag.StateName = state_SearchModel.StateName;

            return View("../Home/Index", dal.LOC_State_Search(connectionString, state_SearchModel, userID));
        }

        #endregion

    }
}
