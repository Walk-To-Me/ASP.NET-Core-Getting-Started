using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class IBook
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "书名不能为空")]
        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public decimal Value { get; set; }
    }
}
