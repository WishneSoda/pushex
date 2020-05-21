using EntityLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApplication.Models
{
    public static class WebApiServiceClass
    {
        static Uri baseUri = new Uri("http://localhost:47396/");

        private static string CallAPI(string method, dynamic input)
        {
            string Result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = baseUri;
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Current.Session["Token"]);
                    string inputJson = (new JavaScriptSerializer()).Serialize(input);
                    HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync("api/LoginAPI/" + method, inputContent);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        Result = readTask.Result;
                    }
                    else
                    {
                        // yetkisiz giris uyarısı gibi uyarılar donucek
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return Result;
        }
        public static string GetAPIToken(string username, string Password)
        {
            APIToken Token = new APIToken();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>( "grant_type", "password" ),
                                new KeyValuePair<string, string>( "username", username ),
                                new KeyValuePair<string, string> ( "Password", Password )
                            };
                    var requestToken = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(baseUri, "SetToken"),
                        Content = new FormUrlEncodedContent(pairs)
                    };
                    requestToken.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };
                    var bearerResult = client.SendAsync(requestToken);
                    bearerResult.Wait();
                    var result = bearerResult.Result;
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    Token.IsSuccessStatusCode = result.IsSuccessStatusCode;
                    if (result.IsSuccessStatusCode)
                    {
                        Token.Token = JObject.Parse(readTask.Result)["access_token"].ToString();
                        HttpContext.Current.Session["Token"] = Token.Token;
                    }
                    else
                    {
                        Token.Error = JObject.Parse(readTask.Result)["error_description"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                Token.IsSuccessStatusCode = false;
                Token.Error = ex.Message;
            }
            return JsonConvert.SerializeObject(Token);
        }



        public static string GetLoginUser(string UserName, string UserPassword)
        {
            string Result = "";
            try
            {
                UserLogin sul = new UserLogin();
                sul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", sul == null ? 0 : sul.ID);

                _FilterParameter.Add("UserName", UserName);
                _FilterParameter.Add("UserPassword", UserPassword);

                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("GetLoginUser", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }



        public static string NewUser(UserLogin ul)
        {
            string Result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = baseUri;
                    UserLogin sul = new UserLogin();
                    sul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                    Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                    _FilterParameter.Add("UserLoginID", sul == null ? 0 : sul.ID);
                    _FilterParameter.Add("UserName", ul.UserName);
                    _FilterParameter.Add("UserPassword", ul.UserPassword);

                    var input = new
                    {
                        FilterParameter = _FilterParameter
                    };
                    string inputJson = (new JavaScriptSerializer()).Serialize(input);
                    HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync("api/LoginAPI/" + "AddUser", inputContent);

                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        Result = readTask.Result;
                    }
                    else
                    {
                        // yetkisiz giris uyarısı gibi uyarılar donucek
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Result;
        }


        public static string UsersList()
        {
            string Result = "";
            try
            {
                UserLogin sul = new UserLogin();
                sul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", sul.ID);

                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("GetUsers", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }



        public static string DeleteUserByID(int ID)
        {
            string Result = "";
            try
            {
                UserLogin sul = new UserLogin();
                sul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", sul.ID);
                _FilterParameter.Add("ID", ID);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("DeleteUserByID", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }


        public static string UpdateUser(UserLogin ul)
        {
            string Result = "";
            try
            {
                UserLogin sul = new UserLogin();
                sul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", sul.ID);
                _FilterParameter.Add("ID", ul.ID);
                _FilterParameter.Add("UserName", ul.UserName);
                _FilterParameter.Add("UserPassword", ul.UserPassword);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("UpdateUser", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }


        public static string GetTargetAppList()
        {
            string Result = "";
            try
            {
                UserLogin ul = new UserLogin();
                ul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", ul.ID);

                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("GetTargetAppList", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }


        public static string NewTargetApps(TargetApps ul)
        {
            string Result = "";
            try
            {
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("Name", ul.Name);
                _FilterParameter.Add("IntervalType", ul.IntervalType);
                _FilterParameter.Add("TargetUrl", ul.TargetUrl);
                _FilterParameter.Add("TimeInterval", ul.TimeInterval);
                _FilterParameter.Add("UserLoginID", ul.UserLoginID);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("AddTargetApps", input);
            }
            catch (Exception ex)
            {
            }
            return Result;
        }

        public static string UpdateTargetApps(TargetApps ul)
        {
            string Result = "";
            try
            {
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("ID", ul.ID);
                _FilterParameter.Add("Name", ul.Name);
                _FilterParameter.Add("IntervalType", ul.IntervalType);
                _FilterParameter.Add("TargetUrl", ul.TargetUrl);
                _FilterParameter.Add("TimeInterval", ul.TimeInterval);
                _FilterParameter.Add("UserLoginID", ul.UserLoginID);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("UpdateTargetApps", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }

        public static string DeleteTargetAppsByID(int ID)
        {
            string Result = "";
            try
            {
                UserLogin ul = new UserLogin();
                ul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", ul.ID);
                _FilterParameter.Add("ID", ID);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("DeleteTargetAppsByID", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }

        public static string GetEmailSettingsList()
        {
            string Result = "";
            try
            {
                UserLogin ul = new UserLogin();
                ul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", ul.ID);

                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("GetEmailSettingsList", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }


        public static string NewEmailSettings(EmailSettings ul)
        {
            string Result = "";
            try
            {
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("FromMail", ul.FromMail);
                _FilterParameter.Add("FromMailPassword", ul.FromMailPassword);

                _FilterParameter.Add("ID", ul.ID);
                _FilterParameter.Add("MailSubject", ul.MailSubject);
                _FilterParameter.Add("Port", ul.Port);
                _FilterParameter.Add("SmtpServer", ul.SmtpServer);
                _FilterParameter.Add("ToMail", ul.ToMail);
                _FilterParameter.Add("UserLoginID", ul.UserLoginID);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("AddEmailSettings", input);
            }
            catch (Exception ex)
            {
            }
            return Result;
        }


        public static string UpdateEmailSettings(EmailSettings ul)
        {
            string Result = "";
            try
            {
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("FromMail", ul.FromMail);
                _FilterParameter.Add("FromMailPassword", ul.FromMailPassword);

                _FilterParameter.Add("ID", ul.ID);
                _FilterParameter.Add("MailSubject", ul.MailSubject);
                _FilterParameter.Add("Port", ul.Port);
                _FilterParameter.Add("SmtpServer", ul.SmtpServer);
                _FilterParameter.Add("ToMail", ul.ToMail);
                _FilterParameter.Add("UserLoginID", ul.UserLoginID);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("UpdateEmailSettings", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }


        public static string DeleteEmailSettingsByID(int ID)
        {
            string Result = "";
            try
            {
                UserLogin ul = new UserLogin();
                ul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", ul.ID);
                _FilterParameter.Add("ID", ID);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("DeleteEmailSettingsByID", input);

            }
            catch (Exception ex)
            {
            }

            return Result;
        }



        public static string GetErrorLogsList()
        {
            string Result = "";

            try
            {
                UserLogin ul = new UserLogin();
                ul = (UserLogin)System.Web.HttpContext.Current.Session["User"];
                Dictionary<string, object> _FilterParameter = new Dictionary<string, object>();
                _FilterParameter.Add("UserLoginID", ul.ID);
                var input = new
                {
                    FilterParameter = _FilterParameter
                };
                Result = CallAPI("GetErrorLogsList", input);

            }
            catch (Exception ex)
            {

            }

            return Result;
        }

    }
}