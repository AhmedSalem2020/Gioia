using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.ProjectOxford.Face;
using FaceAPI_MVC.Web.Models;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using FaceAPI_MVC.Web.Helper;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;
using System.Text;
using Microsoft.ProjectOxford.Common.Contract;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using Gioia.Models;

namespace WebcamMVC.Controllers
{
    public class webcamController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Session["val"] = "";
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Capture()
        {
            int emotion = 0;
            //string linkUrl = "";
            var stream = Request.InputStream;
            string dump;

            try
            {
                //int endOfUrl = 0;
                using (var reader = new StreamReader(stream))
                {
                    dump = reader.ReadToEnd();
                    DateTime nm = DateTime.Now;
                    string date = nm.ToString("yyyymmddMMss");
                    var x = "~/WebImages/" + date + "test.jpg";
                    var path = Server.MapPath(x);
                    System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));
                    Stream image = System.IO.File.OpenRead(path);

                    emotion = await GetAverageHappinessScoreAsync(image);
                    TempData["emotion"] = emotion;
                    //HttpResponseMessage response = await DbAPI.webApiClient.GetAsync("Features/" + emotion);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    linkUrl = response.Content.ReadAsStringAsync().Result;
                    //    if (Session["Member"] != null)
                    //    {
                    //        userMood user = new userMood
                    //        {
                    //            moodId = emotion,
                    //            userId = TempData["userId"].ToString(),
                    //            Date = DateTime.Now

                    //        };
                    //        HttpResponseMessage Moodresponse = await DbAPI.webApiClient.PostAsJsonAsync("userMode/", user);
                    //        if (Moodresponse.IsSuccessStatusCode)
                    //        {

                    //        }

                    //    }
                    //}
                    //endOfUrl = linkUrl.Length - 2;
                    //linkUrl = linkUrl.Substring(1, endOfUrl);
                    //TempData["Link"] = linkUrl;
                }
            }
            catch (Exception ex)
            {
                //throw exception
            }
            finally
            {

            }
             return new EmptyResult();
            //return View();

        }
        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];
            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }
            return bytes;
        }




        private static async Task<Face[]> GetHappinessAsync(Stream stream)
        {
            IEnumerable<FaceAttributeType> faceAttributes =
               new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Emotion, FaceAttributeType.Glasses, FaceAttributeType.Hair };

            var emotionClient = new FaceServiceClient("7136a9d9cc74496aab0fc863d9996199", "https://westeurope.api.cognitive.microsoft.com/face/v1.0");
            Face[] emotionResults = await emotionClient.DetectAsync(stream, returnFaceId: true, returnFaceLandmarks: false, returnFaceAttributes: faceAttributes);

            // var emotionResults = await emotionClient.RecognizeAsync(stream);

            if (emotionResults == null || emotionResults.Count() == 0)
            {
                throw new Exception("Can't detect face");
            }
            if (emotionResults.Count() > 1)
            {
                throw new Exception("please select Picture for one person");
            }

            return emotionResults;
        }

        //Average happiness calculation in case of multiple people
        public static async Task<int> GetAverageHappinessScoreAsync(Stream stream)
        {
            Face[] emotionResults = await GetHappinessAsync(stream);
            int fdesc = 0;

            foreach (Face face in emotionResults)
            {

                fdesc = FaceDescription(face);
            }

            return fdesc;

        }
        private static int FaceDescription(Face face)
        {
            EmotionScores emotionScores = face.FaceAttributes.Emotion;

            float max = 0;
            int moodID = 0;
            //anger
            if (emotionScores.Anger >= 0.1f)
            {

                if (max < emotionScores.Anger * 100)
                {
                    max = emotionScores.Anger * 100;
                    moodID = 4;
                }
            }


            //Contempt
            if (emotionScores.Contempt >= 0.1f)
            {

                if (max < emotionScores.Contempt * 100)
                {
                    max = emotionScores.Contempt * 100;
                    moodID = 3;
                }
            }



            //Disgust
            if (emotionScores.Disgust >= 0.1f)
            {

                if (max < emotionScores.Disgust * 100)
                {
                    max = emotionScores.Disgust * 100;
                    moodID = 4;
                }
            }


            //Fear
            if (emotionScores.Fear >= 0.1f)
            {


                if (max < emotionScores.Fear * 100)
                {
                    max = emotionScores.Fear * 100;
                    moodID = 2;
                }
            }


            //Happiness
            if (emotionScores.Happiness >= 0.1f)
            {

                if (max < emotionScores.Happiness * 100)
                {
                    max = emotionScores.Happiness * 100;
                    moodID = 1;
                }
            }


            //Neutral
            if (emotionScores.Neutral >= 0.1f)
            {

                if (max < emotionScores.Neutral * 100)
                {
                    max = emotionScores.Neutral * 100;
                    moodID = 3;
                }
            }


            //Sadness
            if (emotionScores.Sadness >= 0.1f)
            {

                if (max < emotionScores.Sadness * 100)
                {
                    max = emotionScores.Sadness * 100;
                    moodID = 2;
                }
            }


            //Surprise
            if (emotionScores.Surprise >= 0.1f)
            {

                if (max < emotionScores.Surprise * 100)
                {
                    max = emotionScores.Surprise * 100;
                    moodID = 1;
                }
            }

            return moodID;

        }

        public async Task<ActionResult> VideosAsync(int id)
        {
            int endOfUrl = 0;
            string linkUrl = "";
            int Mood = int.Parse(TempData["emotion"].ToString());
            TempData.Keep();
            if (id == 1)
            {

                HttpResponseMessage response = await DbAPI.webApiClient.GetAsync("Features/" + Mood);
                if (response.IsSuccessStatusCode)
                {
                    linkUrl = response.Content.ReadAsStringAsync().Result;
                    if (Session["Member"] != null)
                    {
                        userMood user = new userMood
                        {
                            moodId = Mood,
                            userId = TempData["userId"].ToString(),
                            Date = DateTime.Now

                        };
                        HttpResponseMessage Moodresponse = await DbAPI.webApiClient.PostAsJsonAsync("userMode/", user);
                        if (Moodresponse.IsSuccessStatusCode)
                        {

                        }

                    }
                }
            }
            else if (id == 2)
            {


                HttpResponseMessage response = await DbAPI.webApiClient.GetAsync("videoFeatures/" + Mood);
                if (response.IsSuccessStatusCode)
                {
                    linkUrl = response.Content.ReadAsStringAsync().Result;
                    if (Session["Member"] != null)
                    {
                        userMood user = new userMood
                        {
                            moodId = Mood,
                            userId = TempData["userId"].ToString(),
                            Date = DateTime.Now

                        };
                        HttpResponseMessage Moodresponse = await DbAPI.webApiClient.PostAsJsonAsync("userMode/", user);
                        if (Moodresponse.IsSuccessStatusCode)
                        {

                        }

                    }
                }


            }
            endOfUrl = linkUrl.Length - 2;
            linkUrl = linkUrl.Substring(1, endOfUrl);
            TempData["Link"] = linkUrl;
            return View();

        }

        public ActionResult options() {
            return View();

        }
    }
}

