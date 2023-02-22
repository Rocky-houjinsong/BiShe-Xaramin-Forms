using System;
using System.Collections.Generic;
using System.Text;
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
