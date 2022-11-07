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
    public class FacturaController : Controller
    {
        string BaseUrl = "https://localhost:7197/api/Factura";

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Read
        public async Task<ActionResult> Index()
        {
            List<Factura> facturas = new List<Factura>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("https://localhost:7197/api/Factura");
                if (Res.IsSuccessStatusCode)
                {
                    var FacResponse = Res.Content.ReadAsStringAsync().Result;
                    facturas = JsonConvert.DeserializeObject<List<Factura>>(FacResponse);
                }
                return View(facturas);
            }
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //create
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Factura factura)
        {
            using (var client = new HttpClient())
            {
                var postTask = client.PostAsJsonAsync("https://localhost:7197/api/Factura", factura);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(factura);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Update
        public ActionResult Edit(int id)
        {
            Factura factura = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7197/api/Factura");

                var responseTask = client.GetAsync("https://localhost:7197/api/Factura/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTASKs = result.Content.ReadAsAsync<Factura>();
                    readTASKs.Wait();
                    factura = readTASKs.Result;
                }
            }
            return View(factura);

        }
        [HttpPost]
        public ActionResult Edit(Factura factura)
        {
            using (var client = new HttpClient())
            {
                var putTask = client.PutAsJsonAsync($"https://localhost:7197/api/Factura/{factura.IdFactura}", factura);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(factura);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Delete
        public ActionResult Delete(int id)
        {
            Factura factura = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7197/api/Factura");

                var respTask = client.GetAsync("https://localhost:7197/api/Factura/" + id.ToString());
                respTask.Wait();
                var result = respTask.Result;
                if (result.IsSuccessStatusCode)

                {
                    var readTASK = result.Content.ReadAsAsync<Factura>();
                    readTASK.Wait();
                    factura = readTASK.Result;
                }
            }
            return View(factura);
        }

        [HttpPost]
        public ActionResult Delete(Factura factura, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7197/api/Factura/");

                var deleteTask = client.DeleteAsync($"https://localhost:7197/api/Factura/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(factura);
        }
    }
}