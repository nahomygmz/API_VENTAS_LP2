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
    public class ProductoController : Controller
    {
        string BaseUrl = "https://localhost:7197/api/Producto";

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Read
        public async Task<ActionResult> Index()
        {
            List<Producto> productos = new List<Producto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("https://localhost:7197/api/Producto");
                if (Res.IsSuccessStatusCode)
                {
                    var ProResponse = Res.Content.ReadAsStringAsync().Result;
                    productos = JsonConvert.DeserializeObject<List<Producto>>(ProResponse);
                }
                return View(productos);
            }
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //create
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Producto producto)
        {
            using (var client = new HttpClient())
            {
                var postTask = client.PostAsJsonAsync("https://localhost:7197/api/Producto", producto);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(producto);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Update
        public ActionResult Edit(int id)
        {
            Producto producto = null;
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("https://localhost:7197/api/Producto/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTASKs = result.Content.ReadAsAsync<Producto>();
                    readTASKs.Wait();
                    producto = readTASKs.Result;
                }
            }
            return View(producto);

        }
        [HttpPost]
        public ActionResult Edit(Producto producto)
        {
            using (var client = new HttpClient())
            {
                var putTask = client.PutAsJsonAsync($"https://localhost:7197/api/Producto/{producto.IdProducto}", producto);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(producto);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Delete
        public ActionResult Delete(int id)
        {
            Producto producto = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7197/api/Producto");

                var respTask = client.GetAsync("https://localhost:7197/api/Producto/" + id.ToString());
                respTask.Wait();
                var result = respTask.Result;
                if (result.IsSuccessStatusCode)

                {
                    var readTASK = result.Content.ReadAsAsync<Producto>();
                    readTASK.Wait();
                    producto = readTASK.Result;
                }
            }
            return View(producto);
        }

        [HttpPost]
        public ActionResult Delete(Producto producto, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7197/api/Produco/");

                var deleteTask = client.DeleteAsync($"https://localhost:7197/api/Producto/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(producto);
        }
    }
}