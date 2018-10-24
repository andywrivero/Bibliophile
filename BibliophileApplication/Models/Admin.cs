using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliophileApplication.Models
{
    public class Admin : User
    {
        private string _userName;
        private string _passWord;
        private DateTime _hireDate;

        public string UserName
        {
            get => _userName;

            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }
        public string PassWord
        {
            get => _passWord;

            set
            {
                _passWord = value;
                NotifyPropertyChanged("PassWord");
            }
        }
        public DateTime HireDate
        {
            get => _hireDate;

            set
            {
                _hireDate = value;
                NotifyPropertyChanged("HireDate");
            }
        }

        public Admin()
        {
            HireDate = DateTime.Now;
        }
    }
}
