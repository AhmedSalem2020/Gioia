using Gioia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Gioia.Controllers
{
    public class APIsController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        //[AllowCrossSiteIFrame]
        [HttpGet]
        public ActionResult youtube()

        {

            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> youtube(userMood mood)
        //{
        //    try
        //    {
        //        if (Session["Member"] != null)
        //        {

        //            userMood User = null;

        //            userMood u = new userMood()
        //            {



        //            };

        //            HttpResponseMessage response = await globalVariables.webApiClient.PutAsJsonAsync("ApplicationUsers/" + Session["Member"].ToString(), u);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                User = await response.Content.ReadAsAsync<userMood>();
        //            }

        //            return RedirectToAction("viewprofile");
        //        }
        //        else
        //        {
        //            return View("Login", "Account");
        //        }

        //        // return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        return View("ErrorPage");
        //    }
        //}
    }
}
