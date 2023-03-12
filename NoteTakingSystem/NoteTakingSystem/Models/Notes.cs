namespace NoteTakingSystem.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int IsVisible { get; set; }
        public int UserId { get; set; }
    }
}
