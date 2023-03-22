using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1_MVP.Logic
{
    internal class User
    {
        private string username { get; set; }
        
        private string image { get; set; }

        User(string username, string image)
        {
            this.username = username;
            this.image = image;
        }
    }
}
