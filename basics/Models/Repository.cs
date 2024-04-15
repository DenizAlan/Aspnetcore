using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace basics.Models
{
    public class Repository
    {
        private static readonly List<Course> _courses=new();

        static Repository (){

            _courses=new List<Course>(){
                 new Course() {Id = 1, Title = "Aspnet Core", Description = "Güzel bir kurs", image="1aspnetcore.png" ,
                 Tags=new string[]{"AspNet","Web Geliştirme"},
                 isActive=true,
                 isHome=true},
                new Course() {Id = 2, Title = "C#", Description = "Güzel bir kurs", image="2c.png" ,
                 Tags=new string[]{"C#","Web Geliştirme"},
                 isActive=true,
                 isHome=true},
                new Course() {Id = 3, Title = "Javascript", Description = "Güzel bir kurs", image="3javascript.jpg" ,
                isActive=true,
                 isHome=true},
                 new Course() {Id = 4, Title = "Php", Description = "Güzel bir kurs", image="4php.jpg" ,
                 Tags=new string[]{"Php","Web Geliştirme"},
                 isActive=true,
                 isHome=false}
            };
        }

        public static List<Course> Courses {
            get {
                return _courses;
            }
        }

        public static Course? GetById (int? id){

            return _courses.FirstOrDefault(c=> c.Id==id);
        }
    }
}