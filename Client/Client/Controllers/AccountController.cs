using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        string BaseUrl = "https://localhost:44313/api/";
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Auth(Login login)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    var postTask = client.PostAsJsonAsync<Login>("Account/login", login);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync().Result;
                        var parseObject = JObject.Parse(readTask);
                        var dataOnly = parseObject["data"].ToString();
                        var tokenOnly = parseObject["token"].ToString();

                        var data = JsonConvert.DeserializeObject<Login>(dataOnly);
                        HttpContext.Session.SetString("Email", data.Email);
                        HttpContext.Session.SetString("JWToken", tokenOnly);

                        return RedirectToAction("Index", "Dashboard");
                    }
                    return RedirectToAction("Indext");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
