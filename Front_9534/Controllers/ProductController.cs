using Front_9534.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Front_9534.Controllers
{
    public class ProductController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:44359/";
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<Product> ProdInfo = new List<Product>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Product");

                //Checking the response is successful or not which is sent HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing the Product list
                    ProdInfo = JsonConvert.DeserializeObject<List<Product>>(PrResponse);
                }

                //returning the Product list to view
                return View(ProdInfo);
            }
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                Product ProdInfo = null;

                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Product/" + id);

                //Checking the response is successful or not which is sent HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing the Product
                    ProdInfo = JsonConvert.DeserializeObject<Product>(PrResponse);
                }

                //returning the Product to view
                return View(ProdInfo);
            }
        }

        // GET: Product/Create
        public async Task<ActionResult> Create()
        {
            List<Category> CatInfo = new List<Category>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Category");

                //Checking the response is successful or not which is sent HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing the Product list
                    CatInfo = JsonConvert.DeserializeObject<List<Category>>(PrResponse);
                }

                ViewBag.ProductCategoryId = new SelectList(CatInfo, "Id", "Name");
                return View();
            }
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Product ProdInfo = null;

                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();

                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));

                    // Serialize data
                    Product product = new Product(collection["name"], collection["description"], decimal.Parse(collection["price"]), 1);
                    var productContent = JsonConvert.SerializeObject(product);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(productContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource using HttpClient
                    HttpResponseMessage Res = await client.PostAsync("api/Product/", byteContent);

                    //Checking the response is successful or not which is sent HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var PrResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing the Product
                        ProdInfo = JsonConvert.DeserializeObject<Product>(PrResponse);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Product ProdInfo = null;
                    List<Category> CatInfo = new List<Category>();

                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();

                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource using HttpClient
                    HttpResponseMessage Res = await client.GetAsync("api/Product/" + id);
                    HttpResponseMessage ResCat = await client.GetAsync("api/Category");


                    //Checking the response is successful or not which is sent HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var PrResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing the Product
                        ProdInfo = JsonConvert.DeserializeObject<Product>(PrResponse);

                    }
                    //Checking the response is successful or not which is sent HttpClient
                    if (ResCat.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var CatResponse = ResCat.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing the Product
                        CatInfo = JsonConvert.DeserializeObject<List<Category>>(CatResponse);
                    }


                    ViewBag.ProductCategoryId = new SelectList(CatInfo, "Id", "Name");

                    return View(ProdInfo);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Product ProdInfo = null;

                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();

                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serialize data
                    Product product = new Product(collection["name"], collection["description"], decimal.Parse(collection["price"]), int.Parse(collection["ProductCategoryId"]));
                    product.Id = int.Parse(collection["Id"]);
                    var productContent = JsonConvert.SerializeObject(product);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(productContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource using HttpClient
                    HttpResponseMessage Res = await client.PutAsync("api/Product/" + id, byteContent);

                    //Checking the response is successful or not which is sent HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var PrResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing the Product
                        ProdInfo = JsonConvert.DeserializeObject<Product>(PrResponse);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                Product ProdInfo = null;

                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Product/" + id);

                //Checking the response is successful or not which is sent HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing the Product
                    ProdInfo = JsonConvert.DeserializeObject<Product>(PrResponse);
                }

                //returning the Product to view
                return View(ProdInfo);
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Product ProdInfo = null;

                    //Passing service base url
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();

                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));

                    // Serialize data

                    //Sending request to find web api REST service resource using HttpClient
                    HttpResponseMessage Res = await client.DeleteAsync("api/Product/" + id);

                    //Checking the response is successful or not which is sent HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var PrResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing the Product
                        ProdInfo = JsonConvert.DeserializeObject<Product>(PrResponse);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
