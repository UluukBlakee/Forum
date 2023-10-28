using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ForumContext _context;
        public UsersController(ForumContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id, string? name)
        {
            User user = await _context.Users.Include(u => u.Answers).FirstOrDefaultAsync(u => u.UserName == name);
            if (user == null)
            {
                user = await _context.Users.Include(u => u.Answers).FirstOrDefaultAsync(u => u.Id == id);
            }
            return View(user);
        }
    }
}
