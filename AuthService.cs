using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WarehouseAccountingJewelryStoreService.Objects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WarehouseAccountingJewelryStoreService
{
    public class AuthService : IAuthService
    {
        public int Auth(string username, string password)
        {
            password = Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.Default.GetBytes(password)));
            using (var db = new ApplicationContext())
            {
                if (db.Users.ToArray().Where(x => x.Login == username && x.Password == password).Count() == 0)
                    return -1;
                else return db.Users.ToArray().Where(x => x.Login == username && x.Password == password).First().AccessLevel;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var db = new ApplicationContext())
                return db.Users.ToList();
        }

        public bool IsAdminCreated()
        {
            using (var db = new ApplicationContext())
            {
                return db.Users.ToArray().Where(x => x.AccessLevel == 2).Count() > 0;
            }
        }

        public bool Register(string login, string password, int level)
        {
            password = Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.Default.GetBytes(password)));
            using (var db = new ApplicationContext())
            {
                if (db.Users.ToArray().Where(x => x.Login == login && x.Password == password).Count() > 0)
                    return false;
                else
                {
                    db.Users.Add(new Objects.User()
                    {
                        Login = login,
                        Password = password,
                        AccessLevel = level
                    });
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public void Remove(string login)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Users.ToArray().Where(x => x.Login == login).Count() > 0)
                {
                    db.Users.RemoveRange(db.Users.ToArray().Where(x => x.Login == login));
                    db.SaveChanges();
                }
            }
        }
    }
}
