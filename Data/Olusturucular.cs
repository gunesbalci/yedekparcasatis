//220229043_GüneşBalcı

using System;

namespace Proje
{
    internal class Olustur
    {
         internal static Musteri musteriOlustur(string dosyaYolu) //musteri turunde kullanici olusturup sisteme(dosyaya) ekler
        { 
            Console.Write("\nEnter the requested information respectively.\n");
            string kullaniciAdi = kayitKontrol.KullaniciAdiOlustur(dosyaYolu);
            Console.WriteLine("\nSuccessful user id.");
            string sifre = kayitKontrol.KullaniciSifreOlustur();
            Console.WriteLine("\nSuccessful password.");
            string isim = kayitKontrol.KullaniciIsmiOlustur();
            Console.WriteLine("\nSuccessful user name.");
            string eposta = kayitKontrol.KullaniciEpostaOlustur();
            Console.WriteLine("\nSuccessful e-mail.");
            string telefon = kayitKontrol.KullaniciTelefonOlustur();
            Console.WriteLine("\nSuccessful phone number.");
            Musteri musteri = new Musteri("musteri",kullaniciAdi,sifre,isim,telefon,eposta);     
            musteri.KullaniciEkle(dosyaYolu);
            Console.WriteLine("\nYour register is completed successfuly.");
            return musteri;
        } 
        internal static void defaultKullanicilariOlustur(string dosyaYolu) //programa onceden ekli kullanicilari olusturup ekler
        {
            Yonetici yonetici = new Yonetici("yonetici","GunesBalci","Gunesbalc1*","Gunes Balci","511-111-2211","gunesbalci66@hotmail.com");
            Satici satici1 = new Satici("satici","OsmanSu","Osmansu2001*","Osman Su","200-120-0120","osmn2001@gmail.com");
            Satici satici2 = new Satici("satici","Birey","Bireyimsi1ey*","Birey Birey","200-120-0127","birey@gmail.com");
            Musteri musteri1 = new Musteri("musteri","DenizOzen","deniz2121*","Deniz Ozen","321-123-1242","denizozen@gmail.com");
            Musteri musteri2 = new Musteri("musteri","FatmaBaygin","Fatmabygn12*","Fatma Baygin","653-234-1235","fatos12@hotmail.com");
            if(!kayitKontrol.kullaniciAdiVar(dosyaYolu,yonetici.kullaniciAdi))
            {
                yonetici.KullaniciEkle(dosyaYolu);
            }
            if(!kayitKontrol.kullaniciAdiVar(dosyaYolu,satici1.kullaniciAdi))
            {
                satici1.KullaniciEkle(dosyaYolu);
            }
            if(!kayitKontrol.kullaniciAdiVar(dosyaYolu,satici2.kullaniciAdi))
            {
                satici2.KullaniciEkle(dosyaYolu);
            }
            if(!kayitKontrol.kullaniciAdiVar(dosyaYolu,musteri1.kullaniciAdi))
            {
                musteri1.KullaniciEkle(dosyaYolu);
            }
            if(!kayitKontrol.kullaniciAdiVar(dosyaYolu,musteri2.kullaniciAdi))
            {
                musteri2.KullaniciEkle(dosyaYolu);
            }
        }
        internal static void defaultArabaOlustur(string dosyaYolu) //programa onceden ekli arabalari olusturup ekler
        {
            YedekParca[] yedekParca = new YedekParca[5];
            yedekParca[0] = new YedekParca("tire",123);
            yedekParca[1] = new YedekParca("side_window",13);
            yedekParca[2] = new YedekParca("rearview_mirror",756);
            yedekParca[3] = new YedekParca("engine",143);
            yedekParca[4] = new YedekParca("steering_wheel",90);

            Donanim[] donanim1 = new Donanim[2];
            donanim1[0] = new Donanim("Vision",yedekParca);
            donanim1[1] = new Donanim("Dream",yedekParca);

            Donanim[] donanim2 = new Donanim[2];
            donanim2[0] = new Donanim("Easy",yedekParca);
            donanim2[1] = new Donanim("Urban_Plus",yedekParca);

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
            donanim6[0] = new Donanim("Prime",yedekParca);
            donanim6[1] = new Donanim("Elite",yedekParca);

            Donanim[] donanim7 = new Donanim[2];
            donanim7[0] = new Donanim("Longitude",yedekParca);
            donanim7[1] = new Donanim("Limited",yedekParca);

            Donanim[] donanim8 = new Donanim[2];
            donanim8[0] = new Donanim("Skypack",yedekParca);
            donanim8[1] = new Donanim("Platinum_Premium",yedekParca);

            Donanim[] donanim9 = new Donanim[2];
            donanim9[0] = new Donanim("S",yedekParca);
            donanim9[1] = new Donanim("SE",yedekParca);

            Donanim[] donanim10 = new Donanim[2];
            donanim10[0] = new Donanim("Plus",yedekParca);
            donanim10[1] = new Donanim("Ultimate",yedekParca);
            
            Araba araba1 = new Araba("Toyota","Corolla",donanim1);
            Araba araba2 = new Araba("Fiat","Egea",donanim2);
            Araba araba3 = new Araba("Audi","A3",donanim3);
            Araba araba4 = new Araba("BMW","E3",donanim4);
            Araba araba5 = new Araba("Honda","Civic",donanim5);
            Araba araba6 = new Araba("Hyundai","Staria",donanim6);
            Araba araba7 = new Araba("Jeep","Renegade",donanim7);
            Araba araba8 = new Araba("Nissan","X-Trail",donanim8);
            Araba araba9 = new Araba("Volkswagen","Beetle",donanim9);
            Araba araba10 = new Araba("Volva","S60",donanim10);

            araba1.arabaEkle(dosyaYolu,5);
            araba2.arabaEkle(dosyaYolu,5);
            araba3.arabaEkle(dosyaYolu,5);
            araba4.arabaEkle(dosyaYolu,5);
            araba5.arabaEkle(dosyaYolu,5);
            araba6.arabaEkle(dosyaYolu,5);
            araba7.arabaEkle(dosyaYolu,5);
            araba8.arabaEkle(dosyaYolu,5);
            araba9.arabaEkle(dosyaYolu,5);
            araba10.arabaEkle(dosyaYolu,5);
        }
    }
}