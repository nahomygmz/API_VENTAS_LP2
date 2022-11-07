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
    public class VentaController : Controller
    {
        string BaseUrl = "https://localhost:7197/api/Venta";

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Read
        public async Task<ActionResult> Index()
        {
            List<Venta> ventas = new List<Venta>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("https://localhost:7197/api/Venta");
                if (Res.IsSuccessStatusCode)
                {
                    var VenResponse = Res.Content.ReadAsStringAsync().Result;
                    ventas = JsonConvert.DeserializeObject<List<Venta>>(VenResponse);
                }
                return View(ventas);
            }
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //create
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Venta venta)
        {
            using (var client = new HttpClient())
            {
                var postTask = client.PostAsJsonAsync("https://localhost:7197/api/Venta", venta);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(venta);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Update
        public ActionResult Edit(int id)
        {
            Venta venta = null;
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("https://localhost:7197/api/Venta/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTASKs = result.Content.ReadAsAsync<Venta>();
                    readTASKs.Wait();
                    venta = readTASKs.Result;
                }
            }
            return View(venta);

        }
        [HttpPost]
        public ActionResult Edit(Venta venta)
        {
            using (var client = new HttpClient())
            {
                var putTask = client.PutAsJsonAsync($"https://localhost:7197/api/Venta/{venta.IdVenta}", venta);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(venta);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Delete
        public ActionResult Delete(int id)
        {
            Venta venta = null;
            using (var client = new HttpClient())
            {
                var respTask = client.GetAsync("https://localhost:7197/api/Venta/" + id.ToString());
                respTask.Wait();
                var result = respTask.Result;
                if (result.IsSuccessStatusCode)

                {
                    var readTASK = result.Content.ReadAsAsync<Venta>();
                    readTASK.Wait();
                    venta = readTASK.Result;
                }
            }
            return View(venta);
        }

        [HttpPost]
        public ActionResult Delete(Venta venta, int id)
        {
            using (var client = new HttpClient())
            {
                var deleteTask = client.DeleteAsync($"https://localhost:7197/api/Venta/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(venta);
        }
    }
}