using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliophileApplication.ViewModels
{
    public class CheckoutViewModel
    {
        public Models.Book Book { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
