using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliophileApplication.ViewModels
{
    public class UserMainWindowViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<Models.Book> Books { get; set; }
    }
}
