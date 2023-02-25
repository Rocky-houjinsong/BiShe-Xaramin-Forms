using SQLite;

namespace Demo.Models
{
    public class Favorite
    {
        // Attribute
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsFavorite { get; set; }
    }
}