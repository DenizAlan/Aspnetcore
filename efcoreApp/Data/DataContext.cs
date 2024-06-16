using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>();

         //1.yol   
        // public DbSet<Ogrenci> Ogrenciler { get; set; }=null!;
        //2.yol
        public DbSet<Ogrenci> Ogrenciler=>Set<Ogrenci>();
        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();

    }
}