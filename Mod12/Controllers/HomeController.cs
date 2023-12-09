using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mod12.Data;
using Mod12.Models;
using System.Diagnostics;

namespace Mod12.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        private IMemoryCache _memoryCache;

        //object injected with the service configured in program.cs builder.Services.AddMemoryCache();
        private const string PRODUCT_KEY = "test";
       
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _logger = logger;
            _context = context;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {

            return View(_context.menuItems.ToList());
        }

        public IActionResult Privacy()
        {
            
            DateTime currentTime;

            if (!_memoryCache.TryGetValue(PRODUCT_KEY, out currentTime))

            {
            

                currentTime= DateTime.Now;

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();

                cacheOptions.SetPriority(CacheItemPriority.Low);

                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(120));
                

                _memoryCache.Set(PRODUCT_KEY, currentTime, cacheOptions);

            }

            return View(currentTime);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}