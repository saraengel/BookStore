using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Model.Models
{
    [Table("Book")]
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        [Column(TypeName = "money")]
        public Decimal price { get; set; }
    }
}
