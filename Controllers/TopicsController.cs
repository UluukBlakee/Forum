using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Forum.Controllers
{
    [Authorize]
    public class TopicsController : Controller
    {
        private readonly ForumContext _context;
        public TopicsController(ForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            List<Topic> topics = await _context.Topics.Include(t => t.User).Include(t => t.Answers).OrderByDescending(t => t.DateCreated).ToListAsync();
            return View(topics.ToPagedList(pageNumber, pageSize));
        }


        public async Task<IActionResult> Details(int id)
        {
            Topic topic = await _context.Topics.Include(t => t.User).Include(t => t.Answers).ThenInclude(a => a.User).FirstOrDefaultAsync(u => u.Id == id);
            if (topic == null)
                return NotFound();
            return View(topic);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Topic topic)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if (topic != null)
            {
                topic.UserId = user.Id;
                topic.DateCreated = DateTime.UtcNow;
                await _context.Topics.AddAsync(topic);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
