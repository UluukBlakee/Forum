using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private readonly ForumContext _context;
        public AnswersController(ForumContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(string text, int topicId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if (topicId != null && text != null && user != null)
            {
                Answer answer = new Answer
                {
                    Text = text,
                    TopicId = topicId,
                    DepartureDate = DateTime.UtcNow,
                    UserId = user.Id
                };
                await _context.Answers.AddAsync(answer);
                await _context.SaveChangesAsync();

                return PartialView("AnswerPartial", answer);
            }
            return NotFound();
        }

        public async Task<IActionResult> GetAnswers(int topicId, int page)
        {
            int pageSize = 2;

            List<Answer> answers = await _context.Answers
                .Include(a => a.User)
                .Where(a => a.TopicId == topicId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (answers != null)
                return PartialView("AnswersListPartial", answers);

            return NotFound();
        }

    }
}