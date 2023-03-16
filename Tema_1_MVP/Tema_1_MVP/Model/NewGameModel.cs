using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1_MVP.Model
{
    class NewGameModel
    {
        public List<int> dimensions { get; set; }
        public NewGameModel()
        {
            dimensions = new List<int>() { 2, 3, 4, 5, 6, 7 };
        }

    }
}
