using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ConsumeWebAPI.Controllers
{
    public class ProductController : Controller
    {
        //Uri baseAddress = new Uri("https://localhost:44396/api");
        Uri baseAddress = new Uri("https://localhost:7067/api");//swager portnumarası:7067
        //
        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Product").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
                //bu işlemi tek satırla da yapabilirdim:json ı deserilize ediyor.
                //products = response.Content.ReadFromJsonAsync<List<ProductViewModel>>().Result;
            }
            return View(products);
        }
        

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            try
            {
                //product serilize etcez. json formata dönüştür.
                string data = JsonConvert.SerializeObject(product);
                //var data2 = JsonSerializer.Serialize(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Product", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product created.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            try
            {
                ProductViewModel product = new ProductViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<ProductViewModel>(data);                    
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
                
            }
            
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel model) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Product", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product details updated";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }
        }
        [HttpGet]
        public IActionResult Delete(int id) 
        {
            try
            {

                ProductViewModel product = new ProductViewModel();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    //jsondatayı bizim viewmodel ımıza deserilize edecez.
                    product = JsonConvert.DeserializeObject<ProductViewModel>(data);
                }
                return View(product);
                //HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Product?id=" + id).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    TempData["deleteMessage"] = "Product details deleted";
                //    return RedirectToAction("Index");
                //}
                //return View();
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Product/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["deleteMessage"] = "Product details deleted";
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}
