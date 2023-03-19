using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1_MVP.Model
{
    class NewGameModel
    {
        public List<int> dimensions1 { get; set; }
        public List<int> dimensions2 { get; set; }
        public NewGameModel()
        {
            dimensions1 = new List<int>() { 2, 3, 4, 5 };
            dimensions2 = new List<int>() { 2, 4, 6 };
        }

    }
}
