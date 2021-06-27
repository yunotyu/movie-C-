using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using movies.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Threading;

namespace movies.Controllers
{
    //这里的策略名要和上面对上
    [EnableCors("AllowSpecificOrigin")] //设置跨域处理的 代理
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MoviesContext moviesContext;

        public HomeController(ILogger<HomeController> logger, MoviesContext dbContext)
        {
            _logger = logger;
            moviesContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IEnumerable<Movei> GetMoveis()
        {
            var list= moviesContext.Moveis.Where(m => true).ToList();
            Thread.Sleep(1000);
            //var address="http://" +Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + Request.HttpContext.Connection.LocalPort;
            var address="http://localhost:" + Request.HttpContext.Connection.LocalPort;
            foreach (var item in list)
            {
                item.SwiperImgPath = address + item.SwiperImgPath;
                item.PriviewImgPath = address + item.PriviewImgPath;
                item.VedioPath = address + item.VedioPath;
            }
            return list;
        }

        public IEnumerable<Recommend> GetRecommends()
        {
            var list = moviesContext.Recommends.Where(m => true).ToList();
            Thread.Sleep(1000);
            var address = "http://localhost:" + Request.HttpContext.Connection.LocalPort;
            foreach (var item in list)
            {
                item.Pic = address + item.Pic;
            }
            return list;
        }

        public IEnumerable<Like> GetLikes()
        {
            var list = moviesContext.Likes.Where(m => true).ToList();
            Thread.Sleep(1000);
            var address = "http://localhost:" + Request.HttpContext.Connection.LocalPort;
            var rm = new Random();
            List<int> numList=new List<int>();
            for (int i = 0; i < 99; i++)
            {
                if (numList.Count == 4)
                {
                    break;
                }
                var index = rm.Next(0, 8);
                if (!numList.Contains(index))
                {
                    numList.Add(index);
                }
            }

            var newList = new List<Like>();

            foreach (var item in numList)
            {
                list[item].MainImg = address + list[item].MainImg;
                newList.Add(list[item]);
            }
            return newList;
        }
    }
}
