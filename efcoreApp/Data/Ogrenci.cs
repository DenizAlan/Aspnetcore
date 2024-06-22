using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        // id => primary key
        [Key]
        public int OgrenciId { get; set; }
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyad { get; set; }

        public string AdSoyad 
        { 
            get
            {
                return this.OgrenciAd+ " "+ this.OgrenciSoyad;
            } 
            
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlari {get;set;} =new List<KursKayit>{} ;


    }
}

        //ICollection<KursKayit>: KursKayit türündeki öğeleri içeren bir koleksiyonu temsil eder. ICollection,(bir liste tipi) koleksiyon işlemlerini destekleyen bir temel arabirimdir. Boyutunu almak, yeni öğeler eklemek gibi temel işlemleri içerir.