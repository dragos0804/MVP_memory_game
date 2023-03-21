using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1_MVP.Logic
{
    internal class Game
    {
        private List<int> m_images = new List<int>();
        private int m_height { get; set; }
        private int m_width { get; set; }
        private void InitializeImages()
        {
            // Add numbers from 0 to 29 to the list
            for (int i = 0; i < 30; i++)
            {
                m_images.Add(i);
            }

            // Shuffle the list randomly
            Random rand = new Random();
            for (int i = 0; i < m_images.Count; i++)
            {
                int j = rand.Next(i, m_images.Count);
                int temp = m_images[i];
                m_images[i] = m_images[j];
                m_images[j] = temp;
            }
        }

        public List<int> GetImages(int n)
        {
            // Return an empty list if n is 0
            if (n == 0)
            {
                return new List<int>();
            }

            // Return the entire list if n is greater than or equal to the list count
            if (n >= m_images.Count)
            {
                return m_images;
            }

            // Otherwise, return a new list containing the first n elements
            return m_images.GetRange(0, n);
        }
        public Game(int width, int height)
        {
            InitializeImages();
            // Keeping this data in this class for serialization
            height = this.m_height;
            width = this.m_width;

            m_images = GetImages(width * height / 2);
        }
    }
}
