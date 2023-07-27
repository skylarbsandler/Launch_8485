using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.DataAccess;
using Tourism.Models;

namespace Tourism.Controllers
{
    public class StatesController : Controller
    {
        private readonly TourismContext _context;

        public StatesController(TourismContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? timezone)
        {
            var states = _context.States.AsEnumerable();
            if (timezone != null)
            {
                states = states.Where(state => state.TimeZone == timezone);
                ViewData["SearchTimeZone"] = timezone;
            }

            ViewData["AllTimeZones"] = _context.States.Select(states => states.TimeZone).Distinct().ToList();

            return View(states);
        }

		public IActionResult New()
		{
			return View();
		}

        [HttpPost]
        [Route("/states/")]
        public IActionResult Create(State state)
        {
            _context.Add(state);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        [Route("states/{stateId:int}")]
        public IActionResult Show(int stateId)
        {
            var state = _context.States.Find(stateId);
            return View(state);
        }

        // GET: /States/:id/edit
        [Route("/States/{id:int}/edit")]
        public IActionResult Edit(int id)
        {
            var state = _context.States.Find(id);

            return View(state);
        }

        // PUT: /States/:id
        [HttpPost]
        [Route("/States/{id:int}")]
        public IActionResult Update(int id, State state)
        {
            state.Id = id;
            _context.States.Update(state);
            _context.SaveChanges();

            return Redirect("/states");
        }
    }
}
