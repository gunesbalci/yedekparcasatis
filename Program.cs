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
        internal void arabaEkle(string dosya)
        {
            File.AppendAllText(dosya, this.marka+ " ");
            File.AppendAllText(dosya, this.model+ " ");
            for(int i=0; i<2; i++)
            {
                File.AppendAllText(dosya, this.donanim[i].isim + " ");
                for(int j=0; j<5; j++)
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
            Console.Write("\n1 veya 2'yi tuslayiniz.\n");
            Console.Write("1- Hesabim var: Giris yap\n2- Hesabim yok: Hesap Olustur\n");
            int girisYontemi = Convert.ToInt32(Console.ReadLine());
            if(girisYontemi!=1 && girisYontemi!=2)
            {
                Console.Write("HATA!: Hatali tuslama.\n");
                return GirisEkrani();
            }
            return (GirisYontemi)girisYontemi-1;
        }  
        internal static void musteriOlustur(string dosya)
        { 
            Console.Write("\nIstenen bilgileri sirasiyla giriniz.\n");
            string kullaniciAdi = kayitKontrol.KullaniciAdiOlustur(dosya);
            Console.WriteLine("\nBasarili kullanici adi girisi.");
            string sifre = kayitKontrol.KullaniciSifreOlustur();
            Console.WriteLine("\nBasarili sifre girisi.");
            string isim = kayitKontrol.KullaniciIsmiOlustur();
            Console.WriteLine("\nBasarili isim girisi.");
            string eposta = kayitKontrol.KullaniciEpostaOlustur();
            Console.WriteLine("\nBasarili e-posta girisi.");
            string telefon = kayitKontrol.KullaniciTelefonOlustur();
            Console.WriteLine("\nBasarili telefon girisi.");
            Musteri musteri = new Musteri(kullaniciAdi,sifre,isim,telefon,eposta);
            File.AppendAllText(dosya,"\n");
            musteri.KullaniciEkle(dosya);
            Console.WriteLine("\nKaydiniz basariyla tamamlanmistir.");
        } 
        internal static void defaultKullanicilariOlustur(string dosya)
        {
            Yonetici yonetici = new Yonetici("GunesBalci","Gunesbalc1*","Gunes Balci","511-111-2211","gunesbalci66@hotmail.com");
            Satici satici1 = new Satici("OsmanSu","Osmansu2001*","Osman Su","200-120-0120","osmn2001@gmail.com");
            Satici satici2 = new Satici("Birey","Bireyimsi1ey*","Birey Birey","200-120-0127","birey@gmail.com");
            Musteri musteri1 = new Musteri("DenizOzen","deniz2121*","Deniz Ozen","321-123-1242","denizozen@gmail.com");
            Musteri musteri2 = new Musteri("FatmaBaygin","Fatmabygn12*","Fatma Baygin","653-234-1235","fatos12@hotmail.com");
            if(!kayitKontrol.kullaniciAdiVar(dosya,yonetici.kullaniciAdi))
            {
                yonetici.KullaniciEkle(dosya);
            }
            if(!kayitKontrol.kullaniciAdiVar(dosya,satici1.kullaniciAdi))
            {
                File.AppendAllText(dosya,"\n");
                satici1.KullaniciEkle(dosya);
            }
            if(!kayitKontrol.kullaniciAdiVar(dosya,satici2.kullaniciAdi))
            {
                File.AppendAllText(dosya,"\n");
                satici2.KullaniciEkle(dosya);
            }
            if(!kayitKontrol.kullaniciAdiVar(dosya,musteri1.kullaniciAdi))
            {
                File.AppendAllText(dosya,"\n");
                musteri1.KullaniciEkle(dosya);
            }
            if(!kayitKontrol.kullaniciAdiVar(dosya,musteri2.kullaniciAdi))
            {
                File.AppendAllText(dosya,"\n");
                musteri2.KullaniciEkle(dosya);
            }
        }
        internal static void defaultArabaOlustur(string dosya)
        {
            YedekParca[] yedekParca = new YedekParca[5];
            yedekParca[0] = new YedekParca("tekerlek",123);
            yedekParca[1] = new YedekParca("yan_cam",13);
            yedekParca[2] = new YedekParca("dikiz_aynasi",756);
            yedekParca[3] = new YedekParca("motor",143);
            yedekParca[4] = new YedekParca("direksiyon",90);

            Donanim[] donanim1 = new Donanim[2];
            donanim1[0] = new Donanim("Vision",yedekParca);
            donanim1[1] = new Donanim("Dream",yedekParca);

            Donanim[] donanim2 = new Donanim[2];
            donanim2[0] = new Donanim("Easy",yedekParca);
            donanim2[1] = new Donanim("Urban Plus",yedekParca);

            Donanim[] donanim3 = new Donanim[2];
            donanim3[0] = new Donanim("Dinamik",yedekParca);
            donanim3[1] = new Donanim("Konfor",yedekParca);

            Donanim[] donanim4 = new Donanim[2];
            donanim4[0] = new Donanim("California",yedekParca);
            donanim4[1] = new Donanim("Winter",yedekParca);

            Donanim[] donanim5 = new Donanim[2];
            donanim5[0] = new Donanim("EX",yedekParca);
            donanim5[1] = new Donanim("LX",yedekParca);

            Donanim[] donanim6 = new Donanim[2];
            donanim6[0] = new Donanim("6",yedekParca);
            donanim6[1] = new Donanim("6",yedekParca);

            Donanim[] donanim7 = new Donanim[2];
            donanim7[0] = new Donanim("7",yedekParca);
            donanim7[1] = new Donanim("7",yedekParca);

            Donanim[] donanim8 = new Donanim[2];
            donanim8[0] = new Donanim("8",yedekParca);
            donanim8[1] = new Donanim("8",yedekParca);

            Donanim[] donanim9 = new Donanim[2];
            donanim9[0] = new Donanim("9",yedekParca);
            donanim9[1] = new Donanim("9",yedekParca);

            Donanim[] donanim10 = new Donanim[2];
            donanim10[0] = new Donanim("10",yedekParca);
            donanim10[1] = new Donanim("10",yedekParca);
            
            Araba araba1 = new Araba("Toyota","Corolla",donanim1);
            Araba araba2 = new Araba("Fiat","Egea",donanim2);
            Araba araba3 = new Araba("Audi","A3",donanim3);
            Araba araba4 = new Araba("BMW","E3",donanim4);
            Araba araba5 = new Araba("Honda","Civic",donanim5);
            Araba araba6 = new Araba("Hyundai","Staria",donanim6);
            Araba araba7 = new Araba("Jeep","model",donanim7);
            Araba araba8 = new Araba("Nissan","model",donanim8);
            Araba araba9 = new Araba("Volkswagen","model",donanim9);
            Araba araba10 = new Araba("Volva","model",donanim10);

            araba1.arabaEkle(dosya);
            araba2.arabaEkle(dosya);
            araba3.arabaEkle(dosya);
            araba4.arabaEkle(dosya);
            araba5.arabaEkle(dosya);
            araba6.arabaEkle(dosya);
            araba7.arabaEkle(dosya);
            araba8.arabaEkle(dosya);
            araba9.arabaEkle(dosya);
            araba10.arabaEkle(dosya);
        }
        internal static void ArabaOlustur(string dosya)
        {
            string cikis = "H";
            Console.Write("\nArabanin:\nMarkasi:");
            string marka = Convert.ToString(Console.ReadLine());
            Console.Write("\nModeli:");
            string model = Convert.ToString(Console.ReadLine());
            Console.Write("\nİlk donanimi:");
            string donanim1 = Convert.ToString(Console.ReadLine());
            Console.Write("\nİkinci donanimi:");
            string donanim2 = Convert.ToString(Console.ReadLine());
            Console.Write("\nİlk donanimin yedek parcalarini ve ardindan stoklarini girin.");
            string yedekparca;
            int stok;
            while(cikis=="H"||cikis=="h")
            {
                Console.Write("\nYedek parca:");
                yedekparca = Convert.ToString(Console.ReadLine());
                Console.Write("\nStok:");
                stok = Convert.ToInt32(Console.ReadLine());
                File.AppendAllText(dosya,yedekparca+" "+stok+"\n");
                Console.Write("\nEklemeye devam etmek istiyor musunuz?(E/H)");
                cikis = Convert.ToString(Console.ReadLine());
                while(cikis!="h"||cikis!="H"||cikis!="E"||cikis!="e")
                {
                    Console.Write("\nHATA!: Gecersiz giris. Tekrar deneyin.");
                    Console.Write("\nEklemeye devam etmek istiyor musunuz?(E/H)");
                    cikis = Convert.ToString(Console.ReadLine());
                }
            }
        }
        internal static Kullanici kullaniciGiris(string dosya)
        {
            string[] kullaniciDosya = File.ReadAllLines(dosya);
            int kullaniciDosya_boyut = kullaniciDosya.Length;
            bool kullaniciEslesdi = false;

            Console.Write("\nIstenen bilgileri sirasiyla giriniz.\n");
            Console.Write("\nKullanici adiniz:\n>");
            string kullaniciAdi = Convert.ToString(Console.ReadLine());

            for( int i=1 ; i<kullaniciDosya_boyut ; i+=6 )
            {
                if( kullaniciAdi == kullaniciDosya[i] )
                {
                    Console.Write("\nSifreniz:\n>");
                    string sifre = Convert.ToString(Console.ReadLine());
                    kullaniciEslesdi = true;

                    if( sifre == kullaniciDosya[i+1] )
                    {
                        Console.Write("\nGiris Basarili");
                        Kullanici kullanici = 
                        new Kullanici(kullaniciAdi,sifre,kullaniciDosya[i+2],kullaniciDosya[i+4],kullaniciDosya[i+3]);
                        kullanici.statu = kullaniciDosya[i-1];
                        return kullanici;
                    }
                    else
                    {
                        Console.Write("\nHATA!: Hatali sifre. Tekrar Deneyiniz.");
                        return kullaniciGiris(dosya);
                    }
                }
            }
            if(!kullaniciEslesdi)
            {
                Console.Write("\nHATA!: Hatali kullanici adi. Tekrar Deneyiniz.");
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
                defaultKullanicilariOlustur(kullaniciDosya);
            }
            if(!File.Exists(arabaDosya))
            {
                File.AppendAllText(arabaDosya,"");
                defaultArabaOlustur(arabaDosya);
            }
            
            GirisYontemi girisYontemi = GirisEkrani();

            if(girisYontemi==GirisYontemi.giris)
            {
                Console.Clear();
                if( kullaniciGiris(kullaniciDosya).statu == "musteri")
                {
                    
                }
                
            }
            if(girisYontemi==GirisYontemi.kayıt)
            {
                Console.Clear();
                musteriOlustur(kullaniciDosya);
            }
        }
    }
}

