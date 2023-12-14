using System;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace Proje
{
    public class Araba
    {
        string marka;
        public string model;
        public string[] donanim = new string[2];
        public string[] yedekParca = new string[5];

        internal void arabaBilgi(string dosya)
        {
            if (File.Exists(dosya)) 
            {
                string[] arababilgi = File.ReadAllLines(dosya); 
                marka = arababilgi[0];
                model = arababilgi[1];
                Console.WriteLine($"{marka},{model}"); 
            } 
        }
    }
    public class Kullanici
    {
        public string kullaniciAdi;
        public string sifre;
        public string isim;
        public string telefon;
        public string eposta;

        /*internal Kullanici(string kullaniciAdi,string sifre,string isim,string telefon,string eposta)
        {
            this.kullaniciAdi = kullaniciAdi;
            this.sifre = sifre;
            this.isim = isim;
            this.telefon = telefon;
            this.eposta = eposta;
        }*/
    }

    public class Yonetici : Kullanici
    {
        /*internal Yonetici(string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(kullaniciAdi,sifre,isim,telefon,eposta)
        {}*/
        internal void yoneticiBilgi(string dosya)
        {
            if (File.Exists(dosya)) 
            {
                string[] yoneticibilgi = File.ReadAllLines(dosya); 
                kullaniciAdi = yoneticibilgi[0];
                sifre = yoneticibilgi[1];
                isim = yoneticibilgi[2];
                telefon = yoneticibilgi[3];
                eposta = yoneticibilgi[4]; 
            } 
        }
    }

    public class Program
    {
        internal string GirisEkrani()
        {
            Console.Write("\n1 veya 2'yi tuslayiniz.\n");
            Console.Write("1- Hesabim var: Giris yap\n2- Hesabim yok: Hesap Olustur\n");
            string girisYontemi = Console.ReadLine();
            if(girisYontemi!="1" && girisYontemi!="2")
            {
                Console.Write("HATA!: Hatali tuslama.\n");
                return GirisEkrani();
            }
            return girisYontemi;
        }
        public static void Main()
        {
            /*
            string dosya = "carList.txt";
            Araba araba = new Araba();
            araba.arabaBilgi(dosya);
            */

            Program program = new Program();
            string girisYontemi = program.GirisEkrani();
            Yonetici yonetici = new Yonetici();
            string yoneticiDosya = "kullanicilar.txt";
            yonetici.yoneticiBilgi(yoneticiDosya);
            Kullanici kullanici = new Kullanici();

            if(girisYontemi=="1")
            {
                Console.Write("\nIstenen bilgileri sirasiyla giriniz.\n");
                Console.Write("1-Kullanici adiniz: \n2-Sifreniz: \n");
                Console.Write("\n1- ");
                kullanici.kullaniciAdi = Convert.ToString(Console.ReadLine());
                Console.Write("2- ");
                kullanici.sifre = Convert.ToString(Console.ReadLine());
            }
            
        }
    }
}

