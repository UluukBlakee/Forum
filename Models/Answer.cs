namespace Forum.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
