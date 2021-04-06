using AbbyWakeAss10.Models;
using AbbyWakeAss10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AbbyWakeAss10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? team,string teamName,  int pageNum = 0)
        {
            int pageSize = 5;

            return View(new IndexViewModel
            {
                Bowler = (context.Bowlers
                .Where(m => m.BowlerId == team || team == null)
                .OrderBy(m => m.BowlerFirstName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                PageNumberInfo = new PageNumberInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    //if no team has been selected, then get the full count. 
                    //Otherwise only count the number from the amount that has been selected. 
                    TotalNumItems = (team == null ? context.Bowlers.Count() :
                    context.Bowlers.Where(x => x.TeamId == team).Count())

                },

                TeamCategory = teamName

            }) ; 
            //.FromSqlRaw("SELECT * FROM Bowlers WHERE etc. \"%momomo%\" ") - 
            //.FromSqlInterpolated($}
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
    }
}
