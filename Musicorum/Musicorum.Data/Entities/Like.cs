namespace Musicorum.Data.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public string UserId { get; set; }
    }
}
