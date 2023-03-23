using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MemoryTiles
{
    [Serializable]
    public class Tile
    {
        [XmlAttribute]
        public int PairId { get; set; }
        [XmlAttribute]
        public string ImagePath { get; set; }
        [XmlAttribute]
        public bool IsGuessed { get; set; }

        public Tile()
        {
            // for serialization purposes
        }

        public Tile(int pairId, string imagePath)
        {
            PairId = pairId;
            ImagePath = imagePath;
            IsGuessed = false;
        }
    }
}
