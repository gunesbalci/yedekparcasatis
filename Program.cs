using System;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

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
        public string statu;

        internal Kullanici(string kullaniciAdi,string sifre,string isim,string telefon,string eposta)
        {
            this.kullaniciAdi = kullaniciAdi;
            this.sifre = sifre;
            this.isim = isim;
            this.telefon = telefon;
            this.eposta = eposta;
        }
    }

    public class Yonetici : Kullanici
    {
        internal Yonetici(string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(kullaniciAdi,sifre,isim,telefon,eposta)
        {
            this.statu = "yonetici";
        }
        internal void yoneticiEkle(string dosya)
        {
            File.AppendAllText(dosya, this.statu);
            File.AppendAllText(dosya, "\n"+this.kullaniciAdi);
            File.AppendAllText(dosya, "\n"+this.sifre);
            File.AppendAllText(dosya, "\n"+this.isim);
            File.AppendAllText(dosya, "\n"+this.eposta);
            File.AppendAllText(dosya, "\n"+this.telefon);
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
        internal void saticiEkle(string dosya)
        {
            File.AppendAllText(dosya, "\n"+this.statu);
            File.AppendAllText(dosya, "\n"+this.kullaniciAdi);
            File.AppendAllText(dosya, "\n"+this.sifre);
            File.AppendAllText(dosya, "\n"+this.isim);
            File.AppendAllText(dosya, "\n"+this.eposta);
            File.AppendAllText(dosya, "\n"+this.telefon);
        }
    } 

    public class Program
    {
        internal static string GirisEkrani()
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
        internal static bool kullaniciAdiVar(string dosya,string kullaniciAdi)
        {
            string[] kullaniciDosya = File.ReadAllLines(dosya);
            int kullaniciDosya_boyut = kullaniciDosya.Length;
            for( int i=1 ; i<kullaniciDosya_boyut ; i+=6 )
            {
                if( kullaniciAdi == kullaniciDosya[i] )
                {
                    return true;
                }
            }
            return false;
        }
        internal static string KullaniciAdiOlustur(string dosya)
        {
            string AlfabetikveNumerik = "[a-zA-Z0-9ığĞüÜşŞİöÖçÇ]";
            string IlkKarakterHarf = "^[a-zA-Z]";
            Regex AlfabetikveNumerik_RGX = new Regex(AlfabetikveNumerik);
            Regex IlkKarakterHarf_RGX = new Regex(IlkKarakterHarf);

            Console.Write("Kullanici adiniz:\n>");
            string kullaniciAdi = Convert.ToString(Console.ReadLine());
            if(kullaniciAdi.Length<5 || kullaniciAdi.Length>20)
            {
                Console.Write("HATA!: Hatalı karakter uzunlugu. Tekrar deneyin.\n");
                return KullaniciAdiOlustur(dosya);
            }
            MatchCollection mc = AlfabetikveNumerik_RGX.Matches(kullaniciAdi);
            if(mc.Count != kullaniciAdi.Length)
            {
                Console.Write("HATA!: Alfabetik veya numarik olamayan karakter kullandiniz. Tekrar deneyin.\n");
                return KullaniciAdiOlustur(dosya);
            }
            mc = IlkKarakterHarf_RGX.Matches(kullaniciAdi);
            if(mc.Count == 0)
            {
                Console.Write("HATA!: Ilk karakter harf degil. Tekrar deneyin.\n");
                return KullaniciAdiOlustur(dosya);
            }
            if(kullaniciAdiVar(dosya,kullaniciAdi))
            {
                Console.Write("HATA!: Bu kullanici adi coktan alinmistir. Tekrar deneyin.\n");
                return KullaniciAdiOlustur(dosya);
            }
            return kullaniciAdi;
        }
        internal static string KullaniciSifreOlustur()
        {
            string Numerik = "[0-9]";
            string BuyukHarf = "[A-Z]";
            string KucukHarf = "[a-z]";
            string OzelKarakter = "[!@#$%&*-+]";
            string BoslukKarakter = "\\s+"; 
            Regex Numerik_RGX = new Regex(Numerik);
            Regex BuyukHarf_RGX = new Regex(BuyukHarf);
            Regex KucukHarf_RGX = new Regex(KucukHarf);
            Regex OzelKarakter_RGX = new Regex(OzelKarakter);
            Regex BoslukKarakter_RGX = new Regex(BoslukKarakter);

            Console.Write("Sifreniz:\n>");
            string sifre = Convert.ToString(Console.ReadLine());
            if(sifre.Length<8 || sifre.Length>20)
            {
                Console.Write("HATA!: Hatalı karakter uzunlugu. Tekrar deneyin.\n");
                return KullaniciSifreOlustur();
            }
            MatchCollection mc = Numerik_RGX.Matches(sifre);
            if(mc.Count == 0)
            {
                Console.Write("HATA!: En az bir nümerik karakteriniz olmalı. Tekrar deneyin.\n");
                return KullaniciSifreOlustur();
            }
            mc = BuyukHarf_RGX.Matches(sifre);
            if(mc.Count == 0)
            {
                Console.Write("HATA!: En az bir buyuk harf karakteriniz olmalı. Tekrar deneyin.\n");
                return KullaniciSifreOlustur();
            }
            mc = KucukHarf_RGX.Matches(sifre);
            if(mc.Count == 0)
            {
                Console.Write("HATA!: En az bir kucuk harf karakteriniz olmalı. Tekrar deneyin.\n");
                return KullaniciSifreOlustur();
            }
            mc = OzelKarakter_RGX.Matches(sifre);
            if(mc.Count == 0)
            {
                Console.Write("HATA!: En az bir ozel karakteriniz olmalı. Tekrar deneyin.\n");
                return KullaniciSifreOlustur();
            }
            mc = BoslukKarakter_RGX.Matches(sifre);
            if(mc.Count != 0)
            {
                Console.Write("HATA!: Bosluk karakteriniz olmamalı. Tekrar deneyin.\n");
                return KullaniciSifreOlustur();
            }
            return sifre;
        }
        internal static string KullaniciIsmiOlustur()
        {
            string IlkKarakterHarf = "^[a-zA-Z]";
            string OzelKarakter = "[!@#$%&*-+]";
            Regex IlkKarakterHarf_RGX = new Regex(IlkKarakterHarf);
            Regex OzelKarakter_RGX = new Regex(OzelKarakter);
            
            Console.Write("Isminiz:\n>");
            string isim = Convert.ToString(Console.ReadLine());

            MatchCollection mc = IlkKarakterHarf_RGX.Matches(isim);
            if(mc.Count == 0)
            {
                Console.Write("HATA!: Ilk karakter harf degil. Tekrar deneyin.\n");
                return KullaniciIsmiOlustur();
            }
            mc = OzelKarakter_RGX.Matches(isim);
            if(mc.Count != 0)
            {
                Console.Write("HATA!: Isminiz ozel karakter iceremez. Tekrar deneyin.\n");
                return KullaniciIsmiOlustur();
            }
            return isim;
        }
        internal static string KullaniciEpostaOlustur()
        {
            string IlkOzelKarakter = "^[!@#$%&*-+]";
            string karakterAT = "[@]";
            Regex IlkOzelKarakter_RGX = new Regex(IlkOzelKarakter);
            Regex karakterAT_RGX = new Regex(karakterAT);


            Console.Write("E-Postaniz:\n>");
            string eposta = Convert.ToString(Console.ReadLine());

            MatchCollection mc = IlkOzelKarakter_RGX.Matches(eposta);
            if(mc.Count != 0)
            {
                Console.Write("HATA!: Ilk karakter ozel karakter olamaz. Tekrar deneyin.\n");
                return KullaniciEpostaOlustur();
            }
            mc = karakterAT_RGX.Matches(eposta);
            if(mc.Count == 0)
            {
                Console.Write("HATA!: E-postaniz @ icermelidir. Tekrar deneyin.\n");
                return KullaniciEpostaOlustur();
            }
            return eposta;
        }
        internal static string KullaniciTelefonOlustur()
        {
            string Alfabetik = "[a-zA-Z]";
            string telefonFormat = ".{3,3}-.{3,3}-.{4,4}";
            Regex Alfabetik_RGX = new Regex(Alfabetik);
            Regex telefonFormat_RGX = new Regex(telefonFormat);

            Console.Write("Telefon Numaraniz:\n>");
            string telefon = Convert.ToString(Console.ReadLine());

            MatchCollection mc = Alfabetik_RGX.Matches(telefon);
            if(mc.Count != 0)
            {
                Console.Write("HATA!: Telefon numaraniz harf iceremez. Tekrar deneyin.\n");
                return KullaniciTelefonOlustur();
            }
            mc = telefonFormat_RGX.Matches(telefon);
            if(mc.Count == 0)
            {
                Console.Write("HATA!: Telefon numaraniz dogru formatta degildir. Tekrar deneyin.\n");
                return KullaniciTelefonOlustur();
            }
            return telefon;
        }
        internal static void musteriOlustur(string dosya)
        { 
            Console.Write("\nIstenen bilgileri sirasiyla giriniz.\n");
            string kullaniciAdi =  "\n" + Program.KullaniciAdiOlustur(dosya);
            Console.WriteLine("\nBasarili kullanici adi girisi.");
            string sifre = "\n" + Program.KullaniciSifreOlustur() + "\n";
            Console.WriteLine("\nBasarili sifre girisi.");
            string isim = "\n" + Program.KullaniciIsmiOlustur() + "\n";
            Console.WriteLine("\nBasarili isim girisi.");
            string eposta = "\n" + Program.KullaniciEpostaOlustur() + "\n";
            Console.WriteLine("\nBasarili e-posta girisi.");
            string telefon = "\n" + Program.KullaniciTelefonOlustur() + "\n";
            Console.WriteLine("\nBasarili telefon girisi.");
            Musteri musteri = new Musteri(kullaniciAdi,sifre,isim,eposta,telefon);
            File.AppendAllText(dosya, "\n"+musteri.statu);
            File.AppendAllText(dosya, "\n"+musteri.kullaniciAdi);
            File.AppendAllText(dosya, "\n"+musteri.sifre);
            File.AppendAllText(dosya, "\n"+musteri.isim);
            File.AppendAllText(dosya, "\n"+musteri.eposta);
            File.AppendAllText(dosya, "\n"+musteri.telefon);
            Console.WriteLine("\nKaydiniz basariyla tamamlanmistir.");
        } 
        internal static void defaultKullanicilariOlustur(string dosya)
        {
            Yonetici yonetici = new Yonetici("GunesBalci","Gunesbalc1*","Gunes Balci","511-111-2211","gunesbalci66@hotmail.com");
            Satici satici1 = new Satici("OsmanSu","Osmansu2001*","Osman Su","200-120-0120","osmn2001@gmail.com");
            Satici satici2 = new Satici("Birey","Bireyimsi1ey*","Birey Birey","200-120-0127","birey@gmail.com");
            if(!kullaniciAdiVar(dosya,yonetici.kullaniciAdi))
            {
                yonetici.yoneticiEkle(dosya);
            }
            if(!kullaniciAdiVar(dosya,satici1.kullaniciAdi))
            {
                satici1.saticiEkle(dosya);
            }
            if(!kullaniciAdiVar(dosya,satici2.kullaniciAdi))
            {
                satici2.saticiEkle(dosya);
            }
        }
        internal static int kullaniciGiris(string dosya)
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
                        string statu = kullaniciDosya[i-1];
                        switch(statu)
                        {
                            case "yonetici":
                            return 1;
                            case "satici":
                            return 2;
                            case "musteri":
                            return 3;
                        }
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
            return 0;
        }
        public static void Main()
        {
            string kullaniciDosya = "kullanicilar.txt";
            File.AppendAllText(kullaniciDosya,"");
            /*
            string dosya = "carList.txt";
            Araba araba = new Araba();
            araba.arabaBilgi(dosya);
            */
            defaultKullanicilariOlustur(kullaniciDosya);
            
            string girisYontemi = GirisEkrani();

            if(girisYontemi=="1")
            {
                Console.Clear();
                kullaniciGiris(kullaniciDosya);
            }
            if(girisYontemi=="2")
            {
                Console.Clear();
                musteriOlustur(kullaniciDosya);
            }
        }
    }
}

