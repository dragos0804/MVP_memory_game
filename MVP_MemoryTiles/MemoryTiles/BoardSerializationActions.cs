using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MemoryTiles
{
    public static class BoardSerializationActions
    {
        public static void SerializeBoard(Board board)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Board));
            FileStream file = new FileStream($"board_{board.Username}.xml", FileMode.Create);
            xmlSerializer.Serialize(file, board);
            file.Dispose();
        }

        public static Board DeserializeBoard(string filename) // let's hope it works this way
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Board));
            FileStream file = new FileStream(filename, FileMode.Open);
            Board deserializedBoard = xmlSerializer.Deserialize(file) as Board;
            file.Dispose();
            return deserializedBoard;
        }
    }
}