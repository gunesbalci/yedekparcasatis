using System;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Proje
{
    public enum GirisYontemi{giris,kayıt}
    public class Araba
    {
        internal string marka;
        internal string model;
        internal Donanim[] donanim;
        internal Araba(string marka, string model, Donanim[] donanim)
        {
            this.marka = marka;
            this.model  = model;
            this.donanim = donanim;
        }
        internal void arabaEkle(string dosya,int yedekParcaSize)
        {
            File.AppendAllText(dosya, this.marka+ " ");
            File.AppendAllText(dosya, this.model+ " ");
            for(int i=0; i<2; i++)
            {
                File.AppendAllText(dosya, this.donanim[i].isim + " ");
                for(int j=0; j<yedekParcaSize; j++)
                {
                    File.AppendAllText(dosya, this.donanim[i].yedekParca[j].parca+ " ");
                    File.AppendAllText(dosya, Convert.ToString(this.donanim[i].yedekParca[j].stok)+ " ");
                }
            }
            File.AppendAllText(dosya, "\n");
        }
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

    public class YedekParca
    {
        internal string parca;
        internal int stok;
        internal YedekParca(string parca,int stok)
        {
            this.parca = parca;
            this.stok = stok;
        }
    }
    public class Donanim
    {
        internal string isim;
        internal YedekParca[] yedekParca;
        internal Donanim(string isim,YedekParca[] yedekParca)
        {
            this.isim = isim;
            this.yedekParca = yedekParca;
        }
    }
    public class Kullanici
    {
        public string kullaniciAdi;
        public string sifre;
        public string isim;
        public string telefon;
        public string eposta;
        public string statu;

        internal Kullanici(string kullaniciAdi,string sifre,string isim,string telefon,string eposta)
        {
            this.kullaniciAdi = kullaniciAdi;
            this.sifre = sifre;
            this.isim = isim;
            this.telefon = telefon;
            this.eposta = eposta;
        }
        internal void KullaniciEkle(string dosya)
        {
            File.AppendAllText(dosya, this.statu);
            File.AppendAllText(dosya, "\n"+this.kullaniciAdi);
            File.AppendAllText(dosya, "\n"+this.sifre);
            File.AppendAllText(dosya, "\n"+this.isim);
            File.AppendAllText(dosya, "\n"+this.eposta);
            File.AppendAllText(dosya, "\n"+this.telefon);
        }
    }
    public class Yonetici : Kullanici
    {
        internal Yonetici(string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(kullaniciAdi,sifre,isim,telefon,eposta)
        {
            this.statu = "yonetici";
        }
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
    public class Musteri : Kullanici
    {
        internal Musteri(string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(kullaniciAdi,sifre,isim,telefon,eposta)
        {
            this.statu = "musteri";
        }
        internal void musteriBilgi(string dosya)
        {
            if (File.Exists(dosya)) 
            {
                string[] musteribilgi = File.ReadAllLines(dosya); 
                kullaniciAdi = musteribilgi[0];
                sifre = musteribilgi[1];
                isim = musteribilgi[2];
                telefon = musteribilgi[3];
                eposta = musteribilgi[4]; 
            } 
        }
    }
    public class Satici : Kullanici
    {
        internal Satici(string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(kullaniciAdi,sifre,isim,telefon,eposta)
        {
            this.statu = "satici";
        }
    } 

    public class Program
    {
        internal static GirisYontemi GirisEkrani()
        {
            Console.Write("\nEnter 1 or 2.\n");
            Console.Write("1- I already have an account: Log in\n2- I dont have an account: Sign Up\n");
            int girisYontemi = Convert.ToInt32(Console.ReadLine());
            if(girisYontemi!=1 && girisYontemi!=2)
            {
                Console.Write("ERROR!: Invalid enter.\n");
                return GirisEkrani();
            }
            return (GirisYontemi)girisYontemi-1;
        }  
        internal static void SaticiEkrani(string dosya)
        {
            Console.Clear();
            Console.Write("\nSelect an operation");
            Console.Write("\n1- List cars and their features\n2- Update spare part informations");
            Console.Write("\n3- Add a car to the system\n4- Delete a car from the system");
            int islem = Convert.ToInt32(Console.ReadLine());
            switch(islem)
            {
                case 1:
                    break;
                case 2:
                    Console.Clear();
                    yedekparcaGüncelle(dosya);
                    break;
                case 3:
                    Console.Clear();
                    Olustur.ArabaOlustur(dosya);
                    break;
                case 4:
                    break;
            }
        }
        internal static void yedekparcaGüncelle(string dosya)
        {
            string[] stringDosya = File.ReadAllLines(dosya);
            Araba[] arabaListe = new Araba[stringDosya.Length];
            for(int i=0; i<stringDosya.Length; i++)
            {
                int satirBoyut = stringDosya[i].Split(" ").Length;
                string[] arabaDosya = new string[satirBoyut];
                arabaDosya = stringDosya[i].Split(" ");
                YedekParca[] yedekParca = new YedekParca[(satirBoyut-4)/4];
                int parcaIndex = 4;
                for(int j=0; j<(satirBoyut-4)/4; j++)
                {
                    yedekParca[j] = new YedekParca(arabaDosya[parcaIndex],Convert.ToInt32(arabaDosya[parcaIndex+1]));
                    parcaIndex +=2;
                }
                YedekParca[] yedekParca2 = new YedekParca[(satirBoyut-4)/4];
                parcaIndex = (satirBoyut/2)+3;
                for(int j=0; j<(satirBoyut-4)/4; j++)
                {
                    yedekParca2[j] = new YedekParca(arabaDosya[parcaIndex],Convert.ToInt32(arabaDosya[parcaIndex+1]));
                    parcaIndex +=2;
                }
                Donanim[] donanim = new Donanim[2];
                donanim[0] = new Donanim(arabaDosya[3],yedekParca);
                donanim[1] = new Donanim(arabaDosya[(satirBoyut/2)+2],yedekParca2);
                arabaListe[i] = new Araba(arabaDosya[0],arabaDosya[1],donanim);
            }
            Console.Write($"{arabaListe[2].marka}");
        }
        internal static Kullanici kullaniciGiris(string dosya)
        {
            string[] kullaniciDosya = File.ReadAllLines(dosya);
            int kullaniciDosya_boyut = kullaniciDosya.Length;
            bool kullaniciEslesdi = false;

            Console.Write("\nEnter the requested information respectively.\n");
            Console.Write("\nUser ID:\n>");
            string kullaniciAdi = Convert.ToString(Console.ReadLine());

            for( int i=1 ; i<kullaniciDosya_boyut ; i+=6 )
            {
                if( kullaniciAdi == kullaniciDosya[i] )
                {
                    Console.Write("\nPassword:\n>");
                    string sifre = Convert.ToString(Console.ReadLine());
                    kullaniciEslesdi = true;

                    if( sifre == kullaniciDosya[i+1] )
                    {
                        Console.Write("\nLog In successful.");
                        Kullanici kullanici = 
                        new Kullanici(kullaniciAdi,sifre,kullaniciDosya[i+2],kullaniciDosya[i+4],kullaniciDosya[i+3]);
                        kullanici.statu = kullaniciDosya[i-1];
                        return kullanici;
                    }
                    else
                    {
                        Console.Write("\nERROR!: Invalid password. Try again.");
                        return kullaniciGiris(dosya);
                    }
                }
            }
            if(!kullaniciEslesdi)
            {
                Console.Write("\nERROR!: Invalid user id. Try again.");
                return kullaniciGiris(dosya);
            }
            return kullaniciGiris(dosya);
        }
        public static void Main()
        {
            string kullaniciDosya = "kullanicilar.txt";
            string arabaDosya = "arabalar.txt";
            if(!File.Exists(kullaniciDosya))
            {
                File.AppendAllText(kullaniciDosya,"");
                Olustur.defaultKullanicilariOlustur(kullaniciDosya);
            }
            if(!File.Exists(arabaDosya))
            {
                File.AppendAllText(arabaDosya,"");
                Olustur.defaultArabaOlustur(arabaDosya);
            }
            
            GirisYontemi girisYontemi = GirisEkrani();

            if(girisYontemi==GirisYontemi.giris)
            {
                Console.Clear();
                if( kullaniciGiris(kullaniciDosya).statu == "satici")
                {
                    SaticiEkrani(arabaDosya);
                }
                
            }
            if(girisYontemi==GirisYontemi.kayıt)
            {
                Console.Clear();
                Olustur.musteriOlustur(kullaniciDosya);
            }
        }
    }
}

