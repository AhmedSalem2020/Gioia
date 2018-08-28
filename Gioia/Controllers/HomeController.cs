using Gioia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Gioia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if (Request.Cookies["_rememberme"] != null)
                {
                    HttpCookie _rememberme = Request.Cookies["_rememberme"];
                    TempData["userId"] = _rememberme["Id"].ToString();

                    //var keyValues = new List<KeyValuePair<string, string>>
                    //{
                    //    new KeyValuePair<string, string>("grant_type", "password"),
                    //    new KeyValuePair<string, string>("username",_rememberme["UserName"]),
                    //    new KeyValuePair<string, string>("password",_rememberme["Password"])
                    //};

                    //var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:29724/token");
                    //request.Content = new FormUrlEncodedContent(keyValues);
                    //var client = new HttpClient();
                    //var response = await client.SendAsync(request);
                    //var r = await response.Content.ReadAsStringAsync();
                    //tokenUser tkuser = JsonConvert.DeserializeObject<tokenUser>(r);
                    //string username = tkuser.userID;
                    //TempData["userId"] = tkuser.userID;
                    Session["userName"] = _rememberme["UserName"].ToString();
                    //TempData.Keep();
                    Session["Member"] = _rememberme["Id"].ToString();

                    // return RedirectToAction("viewprofile", "Member");
                    return View();
                }

                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("ErrorPage","Member");
            }
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Start()
        {
            return View();
        }
    }
}