using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using Tema_1_MVP.View;

namespace Tema_1_MVP.Logic
{
    internal class Game
    {
        private List<string> m_images;
        private int m_height { get; set; }
        private int m_width { get; set; }
        private List<MyToggleButton> m_ButtonGrid;
        public Game(int width, int height)
        {
            // Keeping this data in this class for serialization
            m_height = height;
            m_width = width;

            m_images = new List<string>(m_width * m_height / 2);

            Random rnd = new Random();

            List<int> randomImagesIndexes = new List<int>();
            for (int i = 0; i < 30; i++)
                randomImagesIndexes.Add(i);
            randomImagesIndexes = randomImagesIndexes.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < width * height / 2; i++)
            {
                m_images.Add("/Assets/MemoryCards/Cards/" + randomImagesIndexes[i].ToString() + ".png");
                m_images.Add("/Assets/MemoryCards/Cards/" + randomImagesIndexes[i].ToString() + ".png");
            }

            m_images = m_images.OrderBy(x => rnd.Next()).ToList();

            
            m_ButtonGrid = new List<MyToggleButton>();
            for (int i = 0; i < width * height; i++)
                m_ButtonGrid.Add(new MyToggleButton("/Assets/MemoryCards/backCard.png"));
           
            for (int i = 0; i < m_height * m_width; i++)
            {
                m_ButtonGrid[i].SetImages(m_images[i]);
                m_ButtonGrid[i].Width = 100;
                m_ButtonGrid[i].Height = 100;
            }
        }
        public List<string> GetImages(int n)
        {
            // Return an empty list if n is 0
            if (n == 0)
            {
                return new List<string>();
            }

            // Return the entire list if n is greater than or equal to the list count
            if (n >= m_images.Count)
            {
                return m_images;
            }

            // Otherwise, return a new list containing the first n elements
            return m_images.GetRange(0, n);
        }
        public List<MyToggleButton> GetButtonGrid()
        {
            return m_ButtonGrid;
        }

    }
}
