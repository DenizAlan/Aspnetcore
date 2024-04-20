using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Models
{
    public static class Repository
    {
        private static List<UserInfo> _users=new();

        static Repository(){
        _users.Add(new UserInfo(){Id=1, Name="Deniz",Phone="1234567", Email="dbc@gmail.com" ,WillAttend=true});
        _users.Add(new UserInfo(){Id=2,Name="Hacer",Phone="1234567", Email="ahc@gmail.com" ,WillAttend=true});
        _users.Add(new UserInfo(){Id=3,Name="Gizem",Phone="1234567", Email="gbc@gmail.com" ,WillAttend=true});
        _users.Add(new UserInfo(){Id=4,Name="Merve",Phone="1234567", Email="gbc@gmail.com" ,WillAttend=false});
        }
        public static List<UserInfo> Users {
            get{
                return _users;
            }
        }

        public static void CreateUser(UserInfo user)
        {
            user.Id=Users.Count+1;
            _users.Add(user);
        }

        public static UserInfo? GetById(int id){
            return _users.FirstOrDefault(user=>user.Id==id);
        }
    }
}