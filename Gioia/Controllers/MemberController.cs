using Gioia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace Gioia.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        static HttpClient client = new HttpClient();
        
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult> viewprofile( HttpPostedFileBase img)
        {
            try
            {

                if (Session["Member"] != null)
                {
                    //string st = TempData["userId"].ToString();

                    ApplicationUser loginUser = null;

                    // IEnumerable<ApplicationUser> userProfile;

                    //var res = globalVariables.webApiClient.PostAsJsonAsync("GetApplicationUser/", st).Result;

                    HttpResponseMessage response = globalVariables.webApiClient.GetAsync("ApplicationUsers/" + Session["Member"].ToString()).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        loginUser = await response.Content.ReadAsAsync<ApplicationUser>();
                    }

                    // userProfile = response.Content.ReadAsAsync<IEnumerable<ApplicationUser>>().Result;

                    // TempData["userId"] = st;

                    //return JsonConvert.DeserializeObjectAsync<Int32>(userProfile,null);

                    return View(loginUser);
                    //return View(userProfile);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                //return View();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
            

        }


        //[Authorize]
        [HttpGet]
        public async Task<ActionResult> edit()
        {
            try
            {
                if (Session["Member"] != null)
                {
                    //string st = TempData["userId"].ToString();

                    ApplicationUser loginUser = null;
                    HttpResponseMessage response = globalVariables.webApiClient.GetAsync("ApplicationUsers/" + Session["Member"].ToString()).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        loginUser = await response.Content.ReadAsAsync<ApplicationUser>();
                    }

                    return View(loginUser);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult> edit(ApplicationUser newUser ,HttpPostedFileBase img)
        {
            try
            {
                if (Session["Member"] != null)
                {
                    ApplicationUser loginUser = null;
                    ApplicationUser u = new ApplicationUser()
                    {
                        Id = newUser.Id,
                        Fname = newUser.Fname,
                        Lname = newUser.Lname,
                        Email = newUser.Email,
                        UserName = newUser.UserName,
                        Image = newUser.Image,
                        BirthDate = newUser.BirthDate,
                        PhoneNumber = newUser.PhoneNumber,
                    };
                    if (img != null && img.ContentLength > 0)
                    {
                        using (var binaryReader = new BinaryReader(img.InputStream))
                            u.Image = binaryReader.ReadBytes(img.ContentLength);
                    }
                    HttpResponseMessage response = await globalVariables.webApiClient.PutAsJsonAsync("ApplicationUsers/" + Session["Member"].ToString(), u);

                    if (response.IsSuccessStatusCode)
                    {
                        loginUser = await response.Content.ReadAsAsync<ApplicationUser>();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        Error error = JsonConvert.DeserializeObject<Error>(data);
                        ViewBag.error = error.Message;
                        return View(u);
                    }
                    return RedirectToAction("viewprofile");
                }
                else
                {
                    return View("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }


        //search users
        public async Task<ActionResult> searchAsync(string name)
        {
            try
            {
                if (Session["Member"] != null)
                {
                    TempData["search"] = name;
                    TempData.Keep();
                    List<ApplicationUser> users = null;
                    HttpResponseMessage response = globalVariables.webApiClient.GetAsync("ApplicationUsers/?name=" + TempData["search"]).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        users = await response.Content.ReadAsAsync<List<ApplicationUser>>();
                    }



                    return View(users);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }

        //display user profile
        public async Task<ActionResult> userProfile(string submit = null, string id = null)
        {
            try
            {
                if (Session["Member"] != null)
                {
                    string sender = TempData["userId"].ToString();

                    //opening profile 
                    if (submit != null)
                    {
                        TempData["search"] = submit;
                    }

                    //reload profile after sending friend request
                    else if (id != null)
                    {
                        TempData["search"] = id;
                    }

                    TempData.Keep();
                    //getting friend state
                    //  Friend friendState = null;
                    string state = "";
                    HttpResponseMessage stateResponse = globalVariables.webApiClient.GetAsync("/api/Friend?senderID=" + sender + "&recieverId=" + TempData["search"]).Result;

                    if (stateResponse.IsSuccessStatusCode)
                    {
                        state = await stateResponse.Content.ReadAsAsync<string>();
                        //state = stateResponse.Content.ToString();
                        // friendState.state = friendState.state;
                        TempData["state"] = state;

                    }
                    TempData.Keep();
                    //ApplicationUser user = null;
                    //HttpResponseMessage profileResponse = globalVariables.webApiClient.GetAsync("ApplicationUsers/" + TempData["search"]).Result;

                    //if (profileResponse.IsSuccessStatusCode)
                    //{
                    //    user = await profileResponse.Content.ReadAsAsync<ApplicationUser>();
                    //}
                    List<Posts> posts = null;
                    
                    HttpClient client = new HttpClient();
                    HttpResponseMessage Msg = client.GetAsync("http://192.168.43.205:3050/api/Posts/" + TempData["search"]).Result;
                    if (Msg.IsSuccessStatusCode)
                    {
                        var response = Msg.Content.ReadAsStringAsync().Result;
                         posts = JsonConvert.DeserializeObject<List<Posts>>(response);
                        if (posts.Count == 0)
                        {
                            ApplicationUser user = null;
                            HttpResponseMessage profileResponse = globalVariables.webApiClient.GetAsync("ApplicationUsers/" + TempData["search"]).Result;

                            if (profileResponse.IsSuccessStatusCode)
                            {
                                user = await profileResponse.Content.ReadAsAsync<ApplicationUser>();
                                posts = new List<Posts> { new Posts { ApplicationUser=user} };
                            }
                        }

                        // posts.Select
                    }

                    return View(posts);
                    //return HttpNotFound();
                    //return View(user);

                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }

        //add friend
        public ActionResult AddFriend(string submit)
        {
            try
            {
                if (Session["Member"] != null)
                {
                    TempData["search"] = submit;
                    string sender = TempData["userId"].ToString();
                    TempData.Keep();
                    Friend friend = new Friend()
                    {
                        SenderId = sender,
                        ReceiverId = submit,
                        state = "Pending"

                    };
                    HttpResponseMessage response = globalVariables.webApiClient.PostAsJsonAsync("/api/Friend", friend).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("userProfile", "Member", new { id = submit });
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                return View();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }


        //cancel request
        public ActionResult CancelRequest(string submit)
        {
            try
            {
                if (Session["Member"] != null)
                {
                    TempData["search"] = submit;
                    string sender = TempData["userId"].ToString();
                    TempData.Keep();
                    HttpResponseMessage response = globalVariables.webApiClient.DeleteAsync("/api/Friend?senderID=" + sender + "&recieverId=" + TempData["search"]).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("userProfile", "Member", new { id = submit });
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                return View();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }

        //FriendRequests
        public async Task<ActionResult> FriendRequestsAsync()
        {
            try
            {
                if (Session["Member"] != null)
                {
                    List<Friend> users = null;
                    string currentUser = TempData["userId"].ToString();
                    TempData.Keep();
                    HttpResponseMessage response = globalVariables.webApiClient.GetAsync("/api/Friend/" + currentUser + "?State=Pending").Result;


                    if (response.IsSuccessStatusCode)
                    {
                        users = await response.Content.ReadAsAsync<List<Friend>>();
                    }

                    return View(users);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                //return View();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }


        //friends
        public async Task<ActionResult> FriendsAsync()
        {
            try
            {
                if (Session["Member"] != null)
                {
                    List<Friend> users = null;
                    string currentUser = TempData["userId"].ToString();
                    TempData.Keep();
                    HttpResponseMessage response = globalVariables.webApiClient.GetAsync("/api/Friend/" + currentUser + "?State=Accepted").Result;


                    if (response.IsSuccessStatusCode)
                    {
                        users = await response.Content.ReadAsAsync<List<Friend>>();
                    }

                    return View(users);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }
        
        //accept request
        public ActionResult Accept(string submit)
        {
            try
            {


                if (Session["Member"] != null)
                {
                    string reciever = TempData["userId"].ToString();
                    TempData.Keep();
                    Friend friend = new Friend()
                    {
                        SenderId = submit,
                        ReceiverId = reciever,
                        state = "Accepted"

                    };
                    HttpResponseMessage response = globalVariables.webApiClient.PutAsJsonAsync("/api/Friend", friend).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("FriendsAsync", "Member");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                return View();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }

        public ActionResult Block(string submit)
        {
            try
            {
                if (Session["Member"] != null)
                {
                    string reciever = TempData["userId"].ToString();
                    TempData.Keep();
                    Friend friend = new Friend()
                    {
                        SenderId = submit,
                        ReceiverId = reciever,
                        state = "Blocked"

                    };
                    HttpResponseMessage response = globalVariables.webApiClient.PutAsJsonAsync("/api/Friend", friend).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        HttpResponseMessage responseBlock = globalVariables.webApiClient.PostAsJsonAsync("/api/Friend", friend).Result;
                        if (responseBlock.IsSuccessStatusCode)
                        {
                            return RedirectToAction("userProfile", "Member", new { id = submit });
                        }
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("userProfile", "Member", new { id = submit });
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

                return View();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }



        [HttpGet]
        public async Task<ActionResult> Dashboard()
        {
            try
            {
                if (Session["Member"] != null)
                {
                    //ApplicationUser loginUser = null;
                    moodChart mood = null;
                    HttpResponseMessage response = globalVariables.webApiClient.GetAsync("ApplicationUsers?userId=" + Session["Member"].ToString()).Result;

                    if (response.IsSuccessStatusCode)
                    {

                        mood = await response.Content.ReadAsAsync<moodChart>();
                    }
                    TempData.Keep();
                    return View(mood);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

                //return View();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return View("ErrorPage");
            }
        }

        [HttpGet]
        public ActionResult ErrorPage()
        {
            return View();
        }


    }
}