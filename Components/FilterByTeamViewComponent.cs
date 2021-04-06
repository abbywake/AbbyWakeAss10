using AbbyWakeAss10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbbyWakeAss10.Components
{
    public class FilterByTeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context { get; set; }
        public FilterByTeamViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["TeamName"];

            return View(context.Teams
                .Distinct()
                .OrderBy(x => x)
                );
        }
    }
}
