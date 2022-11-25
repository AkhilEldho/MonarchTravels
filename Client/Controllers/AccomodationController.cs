using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models.DB;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class AccomodationController : Controller
    {
        private readonly CC22_Team2_sem2Context _context;

        public AccomodationController(CC22_Team2_sem2Context context)
        {
            _context = context;
        }


        //Searching Accomodation based on the user input
        public string SearchAccomodation(string cost)
        {
            //URL of the Azure function to do the checking
            string url = "http://localhost:7000/api/SearchAccomodation";
            string parameters = $"?Cost={cost}";

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = client.GetAsync(url + parameters).Result;
            HttpContent hContent = responseMessage.Content;

            string output = "";
            if (hContent != null)
            {
                output = hContent.ReadAsStringAsync().Result;
            }
            else
            {
                output = "Error";
            }

            return output;
        }

        
        [HttpPost]
        //Display DisplayAccomodation to a view
        public async Task<IActionResult> DisplayAccomodation(string cost)
        {
            string jsonInput = SearchAccomodation(cost);
            return View(JsonConvert.DeserializeObject<List<Accomodation>>(jsonInput));
        }


        //--------------------------
        // GET: Accomodation
        public async Task<IActionResult> Index()
        {
              return View(await _context.Accomodations.ToListAsync());
        }

        // GET: Accomodation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accomodations == null)
            {
                return NotFound();
            }

            var accomodation = await _context.Accomodations
                .FirstOrDefaultAsync(m => m.AccomodationId == id);
            if (accomodation == null)
            {
                return NotFound();
            }

            return View(accomodation);
        }

        // GET: Accomodation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accomodation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccomodationId,AccomodationName,AccomodationType,AccomodationAddress,AccomodationCost")] Accomodation accomodation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accomodation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accomodation);
        }

        // GET: Accomodation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accomodations == null)
            {
                return NotFound();
            }

            var accomodation = await _context.Accomodations.FindAsync(id);
            if (accomodation == null)
            {
                return NotFound();
            }
            return View(accomodation);
        }

        // POST: Accomodation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccomodationId,AccomodationName,AccomodationType,AccomodationAddress,AccomodationCost")] Accomodation accomodation)
        {
            if (id != accomodation.AccomodationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accomodation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccomodationExists(accomodation.AccomodationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accomodation);
        }

        // GET: Accomodation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accomodations == null)
            {
                return NotFound();
            }

            var accomodation = await _context.Accomodations
                .FirstOrDefaultAsync(m => m.AccomodationId == id);
            if (accomodation == null)
            {
                return NotFound();
            }

            return View(accomodation);
        }

        // POST: Accomodation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accomodations == null)
            {
                return Problem("Entity set 'CC22_Team2_sem2Context.Accomodations'  is null.");
            }
            var accomodation = await _context.Accomodations.FindAsync(id);
            if (accomodation != null)
            {
                _context.Accomodations.Remove(accomodation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccomodationExists(int id)
        {
          return _context.Accomodations.Any(e => e.AccomodationId == id);
        }
    }
}
