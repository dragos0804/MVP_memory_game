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
    [Serializable]
    public class UserList
    {
        [XmlArray]
        public ObservableCollection<User> Users { get; set; }

        public UserList()
        {
            Users = new ObservableCollection<User>();
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _ = Users.Remove(Users.Single(u => u.UserId == user.UserId));
        }

        public bool FindUsername(string username)
        {
            return Users.Any(u => u.Username == username);
        }
    }
}
