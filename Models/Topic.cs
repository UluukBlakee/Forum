using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }
        public List<Answer>? Answers { get; set; }
    }
}
