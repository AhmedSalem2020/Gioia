using Gioia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Razor.Tokenizer;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace Gioia.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
           

            HttpClient client = new HttpClient();
            //var result=await client.PostAsJsonAsync("http://192.168.43.10:3050/api/account/register", user);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("http://192.168.43.10:3050/api/Account/Register", content);
            
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = await result.Content.ReadAsStringAsync();
                Error error = JsonConvert.DeserializeObject<Error>(data);
                foreach (var item in error.ModelState)
                {
                    foreach (var itm in item.Value)
                    {

                        ViewBag.error += itm + '\n';
                    }

                }
                return View();
            }
            LoginViewModel lvm = new LoginViewModel()
            {
                Email = user.Email,
                Password = user.Password,
                RememberMe = false
            };

            return await Login(lvm);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel user, string returnUrl = null)
        {
            if (Session["Member"] != null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return RedirectToAction("viewprofile", "Member");
            }

            if (Request.Cookies["_rememberme"] != null)
            {
                HttpCookie _rememberme = Request.Cookies["_rememberme"];
                TempData["userId"] = _rememberme["Id"];
                Session["userName"] = _rememberme["UserName"];
                TempData.Keep();
                Session["Member"] = _rememberme["Id"];
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View();
            }

        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel user)
{
            if (!ModelState.IsValid)
            {
                return View();
            }
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username",user.Email),
                new KeyValuePair<string, string>("password", user.Password)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.43.205:3050/token");
            request.Content = new FormUrlEncodedContent(keyValues);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            if (response.ReasonPhrase != "OK")
            {
                ModelState.AddModelError("", "invalid username or password");
                return View();

            }
            else
            {
                // string userid = HttpContext.User.Identity.Name; // use this to get the client userid
                var r = await response.Content.ReadAsStringAsync();
                tokenUser tkuser = JsonConvert.DeserializeObject<tokenUser>(r);
                string username = tkuser.userID;
                TempData["userId"] = tkuser.userID;
                Session["userName"] = tkuser.userName;
                TempData.Keep();
                Session["Member"] = tkuser.userID;

                TempData.Keep();

                if (user.RememberMe == true)
                {
                    HttpCookie _rememberme = new HttpCookie("_rememberme");
                    _rememberme.Expires = DateTime.Now.AddDays(30);
                    _rememberme["UserName"] = tkuser.userName;
                    TempData["userId"] = tkuser.userID;
                    _rememberme["Id"] = tkuser.userID;
                    _rememberme.Secure = false;
                    Response.Cookies.Add(_rememberme);
                }
                // return RedirectToAction("Index", "Home", new { id = tkuser.userID });
                return RedirectToAction("Index", "Home");

            }


        }

        [HttpGet]
        //[Authorize]
        public ActionResult changePassword()
        {
            if (Session["Member"] != null)
            {
                return View();
            }

            else
            {
                return View("Login");
            }

        }


        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> changePassword(ChangePasswordBindingModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (Session["Member"] != null)
            {
                ApplicationUser loginUser = null;

                HttpResponseMessage response = await globalVariables.webApiClient.PostAsJsonAsync("Account/ChangePassword", user);


                if (response.IsSuccessStatusCode)
                {
                    loginUser = await response.Content.ReadAsAsync<ApplicationUser>();
                }


                return RedirectToAction("viewProfile", "Member");
            }
            return View("Login");
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgetPassword(ForgotPasswordViewModel user)
        {
            ApplicationUser loginUser = null;

            HttpResponseMessage response = await globalVariables.webApiClient.PostAsJsonAsync("Account/ForgotPassword", user);


            if (response.IsSuccessStatusCode)
            {
                loginUser = await response.Content.ReadAsAsync<ApplicationUser>();
            }
            return View();
        }
        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            //string usId = User.Identity.GetUserId();

            //ApplicationUser OUser = lm.Users.FirstOrDefault(a => a.Id == usId);

            return code == null ? View("Error") : View();
        }

  

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //private string email = "20131226@hintdesk.com";

        //public override void Run()
        //{
        //    InitializeHttpClient();
        //    Register().Wait();
        //    ForgotPasswordToGetCode().Wait();
        //    ReadCodeFromEmailAndResetPassword().Wait();
        //    GetToken(newSecurePassword).Wait();
        //}

        //private async Task ForgotPasswordToGetCode()
        //{
        //    Console.WriteLine("--- Forgot password to get code ---");
        //    Console.WriteLine();

        //    Console.WriteLine("Step 2: Get code...");
        //    responseMessage =
        //        await
        //            httpClient.PostAsJsonAsync("api/Account/ForgotPassword",
        //                new ForgotPasswordViewModel()
        //                {
        //                    Email = email
        //                });
        //    responseContent = await responseMessage.Content.ReadAsStringAsync();
        //    Console.WriteLine(responseMessage.IsSuccessStatusCode ? "Step 2: Get code...Sucessfully" : responseContent);
        //}

        //private async Task GetToken(string password)
        //{
        //    Console.WriteLine("Step 3: Get Token...");
        //    responseMessage = await httpClient.PostAsync("Token", new FormUrlEncodedContent(
        //        new[]
        //        {
        //    new KeyValuePair<string, string>("grant_type", "password"),
        //    new KeyValuePair<string, string>("username", email),
        //    new KeyValuePair<string, string>("password", password),
        //        }
        //        ));
        //    tokenModel = await responseMessage.Content.ReadAsAsync<TokenModel>();

        //    if (tokenModel != null && tokenModel.AccessToken != null)
        //    {
        //        Console.WriteLine("Step 3: Get Token...Sucessfully");
        //    }
        //    else
        //        Console.WriteLine("Step 3: Get Token...Failed");
        //}

        //private async Task ReadCodeFromEmailAndResetPassword()
        //{
        //    Console.WriteLine("--- Read code from email and reset password ---");
        //    Console.WriteLine();

        //    Console.WriteLine("Step 4: Enter code from your email:");
        //    string code = Console.ReadLine();

        //    Console.WriteLine("Step 5: Reset password...");

        //    responseMessage =
        //        await
        //            httpClient.PostAsJsonAsync("api/Account/ResetPassword",
        //                new ResetPasswordViewModel()
        //                {
        //                    Code = code,
        //                    ConfirmPassword = newSecurePassword,
        //                    Email = email,
        //                    Password = newSecurePassword
        //                });
        //    responseContent = await responseMessage.Content.ReadAsStringAsync();
        //    Console.WriteLine(responseMessage.IsSuccessStatusCode
        //        ? "Step 5: Reset password...Sucessfully"
        //        : responseContent);
        //}

        //private async Task Register()
        //{
        //    Console.WriteLine("--- Register ---");
        //    Console.WriteLine();

        //    Console.WriteLine("Step 1: Register...");
        //    responseMessage =
        //        await
        //            httpClient.PostAsJsonAsync("api/Account/Register",
        //                new RegisterBindingModel()
        //                {
        //                    Email = email,
        //                    Password = securePassword,
        //                    ConfirmPassword = securePassword
        //                });
        //    responseContent = await responseMessage.Content.ReadAsStringAsync();
        //    Console.WriteLine(responseMessage.IsSuccessStatusCode ? "Step 1: Register...Sucessfully" : responseContent);
        //}

        public ActionResult Logout()
        {
            Session["Member"] = null;

            if (Request.Cookies["_rememberme"] != null)
            {
                var c = new HttpCookie("_rememberme");
                c.Expires = DateTime.Now.AddDays(-8);
                Response.Cookies.Add(c);
            }

            return View("Login");

        }

        
    }
}