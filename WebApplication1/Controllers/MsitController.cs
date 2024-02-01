using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    public class MsitController : Controller
    {
        public readonly MyDBContext _context;
        public readonly IWebHostEnvironment _environment;
        public MsitController(MyDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            Thread.Sleep(3000);
            //int x=10, y=0;
            //int z = x / y;
            //return View();
            return Content("Hellow World! 你好", "text/plain", Encoding.UTF8);
        }
        [HttpGet]
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }
        public IActionResult Avatar(int id = 1) //https://localhost:7203/msit/avatar?id=3
        {
            Member? member = _context.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                if (img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            return NotFound();
        }
        public IActionResult First(int id) { return View(); }

        //public IActionResult Register(string name,int age)
        //public IActionResult Register(Memember user)
        //public IActionResult Register(UserDTO user)
        public IActionResult Register(Member user, IFormFile photo)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                user.Name = "guest";
            }
            string fileName = "empty.jpg";
            if (photo != null)
            {
                fileName = user.FileName;
            }
            string uploadPath = Path.Combine(_environment.WebRootPath, "upload", fileName);

            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                photo?.CopyTo(fileStream);
            }


            user.FileName = fileName;
            //轉成二進位
            byte[]? imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                photo?.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            user.FileData = imgByte;


            _context.Members.Add(user);
            _context.SaveChanges();


            return Content(uploadPath);
            //return Content($"Hello{name}.You are {age}years old.!!");
            //return Content($"Hello {user.Name}, {user.Age}歲了", "text/plain", Encoding.UTF8);
            //return Content($"{user.photo.ContentType }-{user.photo.Length}-{user.photo.FileName}");
        }
        public IActionResult District(string city)
        {
            var districts = _context.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        [HttpPost]
        public IActionResult Sport([FromBody] SearchDTO _search) //加上FromBody 明確指示該方法應該從請求主體中讀取數據。
        {
            //讀取
            var spots = _search.CategoryId == 0 ? _context.SpotImagesSpots : _context.SpotImagesSpots.Where(s => s.CategoryId.Equals(_search.CategoryId));
            //關鍵字搜尋
            if (!string.IsNullOrEmpty(_search.Keyword))
            {
                spots = spots.Where(s => s.SpotTitle.Contains(_search.Keyword) || s.SpotDescription.Contains(_search.Keyword));
            }
            //排序
            switch (_search.SortBy)
            {
                case "spotId":
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.SpotId) : spots.OrderByDescending(s => s.SpotId);
                    break;
                case "spotTitle":
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.SpotTitle) : spots.OrderByDescending(s => s.SpotTitle);
                    break;
                default:
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.CategoryId) : spots.OrderByDescending(s => s.CategoryId);
                    break;
            }
            //分頁
            int TotalCount = spots.Count();//總共幾筆資料
            int pageSize = _search.PageSize ?? 9;//每頁顯示多少筆 ??預設值
            int page = _search.Page ?? 1; //目前要顯示第幾頁
            int TotalPages = (int)Math.Ceiling((decimal)TotalCount / pageSize); //計算總共有幾頁

            spots = spots.Skip((int)((page -1)*pageSize)).Take(pageSize);


            SpotsPagingDTO spotsPaging = new SpotsPagingDTO();
            spotsPaging.TotalPages = TotalPages;
            spotsPaging.TotalCount= TotalCount;
            spotsPaging.SpotsResult = spots.ToList();
            return Json(spotsPaging);
        }
        public IActionResult SpotTitle(string title)
        {
            var titles = _context.Spots.Where(s => s.SpotTitle.Contains(title)).Select(s => s.SpotTitle).Take(8);
            return Json(titles);
        }
    }
}
