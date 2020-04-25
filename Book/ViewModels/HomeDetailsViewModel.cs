using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Models;

namespace Book.ViewModels
{
    public class HomeDetailsViewModel
    {
        public IBook IBook { get; set; }
        public string PageTitle { get; set; }
    }
}
