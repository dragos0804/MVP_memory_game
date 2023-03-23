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
    public static class UserListSerializationActions
    {
        public static void SerializeUserList(UserList userList)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserList));
            using (FileStream file = new FileStream("users.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(file, userList);
            }
        }

        public static void DeserializeUserList(UserList userList)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserList));
            if (File.Exists("users.xml"))
            {
                using (FileStream file = new FileStream("users.xml", FileMode.Open))
                {
                    UserList deserializedUserList = xmlSerializer.Deserialize(file) as UserList;
                    userList.Users.Clear();
                    foreach (User user in deserializedUserList.Users)
                    {
                        userList.Users.Add(user);
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
    }
}