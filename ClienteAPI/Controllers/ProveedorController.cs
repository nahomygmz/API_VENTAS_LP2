using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClienteAPI.Models;
using Newtonsoft.Json;
using System.Runtime.Remoting.Messaging;

namespace ClienteAPI.Controllers
{
    public class ProveedorController : Controller
    {
        string BaseUrl = "https://localhost:7197/api/Proveedor";

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Read
        public async Task<ActionResult> Index()
        {
            List<Proveedor> proveedor = new List<Proveedor>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("https://localhost:7197/api/Proveedor");
                if (Res.IsSuccessStatusCode)
                {
                    var ProResponse = Res.Content.ReadAsStringAsync().Result;
                    proveedor = JsonConvert.DeserializeObject<List<Proveedor>>(ProResponse);
                }
                return View(proveedor);
            }
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //create
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Proveedor proveedor)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:7197/api/Proveedor");

                var postTask = client.PostAsJsonAsync("https://localhost:7197/api/Proveedor", proveedor);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(proveedor);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Update
        public ActionResult Edit(int id)
        {
            Proveedor proveedor = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7197/api/Proveedor");
                var responseTask = client.GetAsync("https://localhost:7197/api/Proveedor/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTASKs = result.Content.ReadAsAsync<Proveedor>();
                    readTASKs.Wait();
                    proveedor = readTASKs.Result;
                }
            }
            return View(proveedor);

        }
        [HttpPost]
        public ActionResult Edit(Proveedor proveedor)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:7197/api/Proveedor");
                var putTask = client.PutAsJsonAsync($"https://localhost:7197/api/Proveedor/{proveedor.IdProveedor}", proveedor);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(proveedor);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Delete
        public ActionResult Delete(int id)
        {
            Proveedor proveedor = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7197/api/Proveedor");

                var respTask = client.GetAsync("https://localhost:7197/api/Proveedor/" + id.ToString());
                respTask.Wait();
                var result = respTask.Result;
                if (result.IsSuccessStatusCode)

                {
                    var readTASK = result.Content.ReadAsAsync<Proveedor>();
                    readTASK.Wait();
                    proveedor = readTASK.Result;
                }
            }
            return View(proveedor);
        }

        [HttpPost]
        public ActionResult Delete(Proveedor proveedor, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7197/api/Proveedor/");

                var deleteTask = client.DeleteAsync($"https://localhost:7197/api/Proveedor/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(proveedor);
        }
    }
}