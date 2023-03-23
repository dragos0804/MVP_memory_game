using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MemoryTiles
{
    [Serializable]
    public class Board
    {
        [XmlAttribute]
        public int Height { get; set; }
        [XmlAttribute]
        public int Width { get; set; }
        [XmlAttribute]
        public int TilePairsGuessed { get; set; }
        [XmlAttribute]
        public int Level { get; set; }
        [XmlArray]
        public List<List<Tile>> Tiles { get; set; }
        [XmlIgnore]
        public string Username { get; set; }
        [XmlIgnore]
        public bool IsSaved { get; set; }
        [XmlIgnore]
        public bool InProgress { get; set; }

        public Board()
        {
            // for serialization purposes
        }

        public Board(string username, int height = 5, int width = 5)
        {
            Username = username;
            Height = height;
            Width = width;
            TilePairsGuessed = 0;
            Level = 0;
            IsSaved = false;
            InProgress = false;
            Tiles = new List<List<Tile>>();
            for (int rowIndex = 0; rowIndex < Height; rowIndex++)
            {
                List<Tile> row = new List<Tile>();
                for (int columnIndex = 0; columnIndex < Width; columnIndex++)
                {
                    row.Add(new Tile());
                }
                Tiles.Add(row);
            }
        }

        public void Initialize()
        {
            TilePairsGuessed = 0;
            List<Tuple<int, int>> positions = new List<Tuple<int, int>>();
            for (int rowIndex = 0; rowIndex < Height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Width; columnIndex++)
                {
                    positions.Add(new Tuple<int, int>(rowIndex, columnIndex));
                }
            }
            Random random = new Random();
            for (int imageId = 0; imageId < Height * Width / 2; imageId++)
            {
                for (int i = 0; i < 2; i++) // we need two randomly selected tiles for each image
                {
                    int positionIndex = random.Next(positions.Count);
                    Tiles[positions[positionIndex].Item1][positions[positionIndex].Item2] = new Tile(imageId, $"assets/image{imageId}.png");
                    positions.RemoveAt(positionIndex);
                }
            }
            if (positions.Count == 1)
            {
                int imageId = random.Next(Height * Width / 2);
                Tiles[positions[0].Item1][positions[0].Item2] = new Tile(imageId, $"assets/image{imageId}.png");
            }
        }

        public void IncreaseLevel()
        {
            Level++;
        }
    }
}
