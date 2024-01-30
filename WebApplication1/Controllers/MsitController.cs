using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MsitController : Controller
    {
        public readonly MyDBContext _context;
        public MsitController(MyDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Thread.Sleep(3000);
            //int x=10, y=0;
            //int z = x / y;
            //return View();
            return Content("Hellow World! 你好","text/plain",Encoding.UTF8);
        }
        [HttpGet]
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }
        public IActionResult Avatar(int id=1) //https://localhost:7203/msit/avatar?id=3
        {
            Member? member = _context.Members.Find(id);
            if(member != null)
            {
                byte[] img = member.FileData;
                if(img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            return NotFound();
        }
        public IActionResult First(int id) { return View(); }
        public IActionResult Register(string name,int age =20) {
            if(string.IsNullOrEmpty(name))
            {
                name = "guest";
            }
            //return Content($"Hello{name}.You are {age}years old.!!");
            return Content($"Hello {name}, {age}歲了", "text/plain", Encoding.UTF8);
        }

    }
}
