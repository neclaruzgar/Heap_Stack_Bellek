using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Heap_Stack_Bellek
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var console=new Product() { Name = "Ps5" })
            {
                Console.WriteLine($"Ürün Adı:{console.Name}");
            }

            Customer ali = new Customer(); //new default değer atar
            Console.WriteLine(ali.Id); //Örneğin şuan Id=0
            //Yani belleğe ilk değer atar

            ali.FirstName = "Ali";
            ali.LastName = "Ali Lastname";
            ali.Id = 1;
            ali.HomeAdress = new Adress() { City = "İstanbul", Street = "Ali Street", Country = "Türkiye" };

            Customer veli = ali.ShallowCopy() ;

            veli.FirstName = "Veli Yaptım";
            veli.HomeAdress.City = "Ankara";

            Console.WriteLine($"Sehir : {ali.HomeAdress.City}");
            Console.WriteLine($"Adı: {ali.FirstName}"); 
            Console.WriteLine($"Soyadı: {ali.LastName}"); 
            Console.WriteLine($"Id: {ali.Id}"); 
        }
    }
    [Serializable]
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Adress HomeAdress { get; set; }

        public Customer ShallowCopy()
        {
            return (Customer)this.MemberwiseClone(); //Bilinçli tür dönüşümü yapacağız
            //objecti Customera çevirdim. 
        }

        public Customer DeeepCopy()
        {
            using(var ms=new MemoryStream())
            {
                var formatter = new BinaryFormatter(); //Tüm sınıfı binary hale getiriyoruz
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return (Customer)formatter.Deserialize(ms); //Binaryi olması gereken formata dönüştürüyor 
            }
        }
    }
    [Serializable]
    public class Adress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
    public class Product:IDisposable
    {
        private bool _disposed = false;
        ~Product() => Dispose(false);

        public int Id { get; set; }
        public string Name { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
        }
    }
}
