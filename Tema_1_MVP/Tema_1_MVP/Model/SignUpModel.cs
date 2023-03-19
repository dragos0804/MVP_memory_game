using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tema_1_MVP.Model
{
    class SignUpModel
    {
        private List<string> imagePaths;
        private int currentIndex;

        public SignUpModel(List<string> imagePaths)
        {
            this.imagePaths = imagePaths;
            this.currentIndex = 0;
        }

        public string CurrentImagePath
        {
            get { return imagePaths[currentIndex]; }
        }

        public void NextImage()
        {
            currentIndex = (currentIndex + 1) % imagePaths.Count;
        }

        public void PrevImage()
        {
            currentIndex = (currentIndex + imagePaths.Count - 1) % imagePaths.Count;
        }
    }
}