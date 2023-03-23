using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MemoryTiles
{
    [Serializable]
    public class User
    {
        [XmlAttribute]
        public int UserId { get; set; }

        [XmlAttribute]
        public string Username { get; set; }

        [XmlAttribute]
        public string AvatarPath { get; set; }

        [XmlAttribute]
        public int GamesPlayed { get; set; }

        [XmlAttribute]
        public int GamesWon { get; set; }

        public User()
        {
            // for serialization purposes
        }

        public User(int userId, string username, string avatarPath)
        {
            UserId = userId;
            Username = username;
            AvatarPath = avatarPath;
            GamesPlayed = 0;
            GamesWon = 0;
        }
    }
}
