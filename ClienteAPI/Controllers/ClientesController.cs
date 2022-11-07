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
    public class ClientesController : Controller
    {
        string BaseUrl = "https://localhost:7197/api/Cliente";

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Read
        public async Task<ActionResult> Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llamar a todos los clientes usando HttpClient
                HttpResponseMessage Res = await client.GetAsync("https://localhost:7197/api/Cliente");
                if (Res.IsSuccessStatusCode)
                {
                    var CliResponse = Res.Content.ReadAsStringAsync().Result;
                    clientes = JsonConvert.DeserializeObject<List<Cliente>>(CliResponse);
                }

                //Mostrar lista de clientes
                return View(clientes);
            }
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //create
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Cliente cliente)
        {
            using (var client = new HttpClient())
            {

                var postTask = client.PostAsJsonAsync("https://localhost:7197/api/Cliente", cliente);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Update
        public ActionResult Edit(int id)
        {
            Cliente cliente = null;
            using (var client = new HttpClient())
            {
                //obtener cliente
                var responseTask = client.GetAsync("https://localhost:7197/api/Cliente/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTASKs = result.Content.ReadAsAsync<Cliente>();
                    readTASKs.Wait();
                    cliente = readTASKs.Result;
                }
            }
            return View(cliente);

        }
        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            using(var client = new HttpClient())
            {
                var putTask = client.PutAsJsonAsync($"https://localhost:7197/api/Cliente/{cliente.IdCliente}", cliente);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Delete
        public ActionResult Delete(int id)
        {
            Cliente cliente = null;
            using (var client = new HttpClient())
            {
                var respTask = client.GetAsync("https://localhost:7197/api/Cliente/" + id.ToString());
                respTask.Wait();
                var result = respTask.Result;
                if (result.IsSuccessStatusCode)

                {
                    var readTASK = result.Content.ReadAsAsync<Cliente>();
                    readTASK.Wait();
                    cliente = readTASK.Result;
                }
            }
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Delete(Cliente cliente, int id)
        {
            using (var client = new HttpClient())
            {

                var deleteTask = client.DeleteAsync($"https://localhost:7197/api/Cliente/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }

    }
}