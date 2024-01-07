//220229043_GüneşBalcı

using System;
using System.Text.RegularExpressions;

namespace Proje
{
    internal class kayitKontrol
    {
        internal static bool boslukVar(string deger) //string'de bosluk karakteri varsa true dondurur
        {
            string BoslukKarakter = "\\s+";
            Regex BoslukKarakter_RGX = new Regex(BoslukKarakter);
            MatchCollection mc = BoslukKarakter_RGX.Matches(deger);
            if(mc.Count == 0)
            {
                return false;
            }
            return true;
        }
        internal static bool kullaniciAdiVar(string dosyaYolu,string kullaniciAdi) //kullanici adi sistemde(dosyada) varsa true dondurur
        {
            Kullanici[] kullaniciListe = Dosya_Stok.DosyadanKullaniciya(dosyaYolu);
            for( int i=0 ; i<kullaniciListe.Length; i++ )
            {
                if( kullaniciAdi == kullaniciListe[i].kullaniciAdi )
                {
                    return true;
                }
            }
            return false;
        }
        internal static string KullaniciAdiOlustur(string dosyaYolu)     //Kullanici adi giris icin ekran yazdirir,
        {                                                                //kullanici adinin alfanumarik oldugunu kontrol eder,
            string AlfabetikveNumerik = "[a-zA-Z0-9ığĞüÜşŞİöÖçÇ]";       //kullanici adinin ilk karakterinin karf oldugunu kontrol eder ve
            string IlkKarakterHarf = "^[a-zA-Z]";                        //kullanici adina girilen karakter sayisini kontrol eder.
            Regex AlfabetikveNumerik_RGX = new Regex(AlfabetikveNumerik);
            Regex IlkKarakterHarf_RGX = new Regex(IlkKarakterHarf);

            Console.Write("User ID:\n>");
            string kullaniciAdi = Convert.ToString(Console.ReadLine());
            if(kullaniciAdi.Length<5 || kullaniciAdi.Length>20)     //karakter sayisinin kontrolu
            {
                Console.Write
                ("ERROR!: Invalid number of characters. Character number cant be less than 5 or more than 20. Try again.\n");
                return KullaniciAdiOlustur(dosyaYolu);
            }
            MatchCollection mc = AlfabetikveNumerik_RGX.Matches(kullaniciAdi);
            if(mc.Count != kullaniciAdi.Length)                     //alfanumarik kontrolu
            {
                Console.Write("ERROR!: You cant use a non-alphanumeric character. Try again.\n");
                return KullaniciAdiOlustur(dosyaYolu);
            }
            mc = IlkKarakterHarf_RGX.Matches(kullaniciAdi);
            if(mc.Count == 0)                                       //ilk karakterin kontrolu
            {
                Console.Write("ERROR!: First character is non-alphabetic character. Try again.\n");
                return KullaniciAdiOlustur(dosyaYolu);
            }
            if(kullaniciAdiVar(dosyaYolu,kullaniciAdi))             //kullanici adinin daha once alinip alinmadiginin kontrolu
            {
                Console.Write("ERROR!: This user id is already taken. Try again.\n");
                return KullaniciAdiOlustur(dosyaYolu);
            }
            return kullaniciAdi;
        }
        internal static string KullaniciSifreOlustur()      //sifre girisi icin ekran yazdirir,
        {                                                   //sifrenin en az bir buyuk harf icerdigini kontrol eder,
            string Numerik = "[0-9]";                       //sifrenin en az bir kucuk harf icerdigini kontrol eder,
            string BuyukHarf = "[A-Z]";                     //sifrenin en az bir ozel karakter icerdigini kontrol eder,
            string KucukHarf = "[a-z]";                     //sifrenin en az bir rakam icerdigini kontrol eder,
            string OzelKarakter = "[!@#$%&*-+]";            //sifrenin bosluk icermedigini kontrol eder ve sifrenin karakter sayisini kontrol eder.
            string BoslukKarakter = "\\s+"; 
            Regex Numerik_RGX = new Regex(Numerik);
            Regex BuyukHarf_RGX = new Regex(BuyukHarf);
            Regex KucukHarf_RGX = new Regex(KucukHarf);
            Regex OzelKarakter_RGX = new Regex(OzelKarakter);
            Regex BoslukKarakter_RGX = new Regex(BoslukKarakter);

            Console.Write("Password:\n>");
            string sifre = Convert.ToString(Console.ReadLine());
            if(sifre.Length<8 || sifre.Length>20)           //sifre karakter sayisi kontrolu
            {
                Console.Write("ERROR!: Invalid number of characters. Character number can't be less than 8 or more than 20. Try again.\n");
                return KullaniciSifreOlustur();
            }
            MatchCollection mc = Numerik_RGX.Matches(sifre);
            if(mc.Count == 0)                               //sifrenin en az bir rakam icerdiginin kontrolu
            {
                Console.Write("ERROR!: You must have at least one numaric character. Try again.\n");
                return KullaniciSifreOlustur();
            }
            mc = BuyukHarf_RGX.Matches(sifre);
            if(mc.Count == 0)                               //sifrenin en az bir buyuk harf icerdiginin kontrolu
            {
                Console.Write("ERROR!: You must have at least one capital alphabetic character. Try again.\n");
                return KullaniciSifreOlustur();
            }
            mc = KucukHarf_RGX.Matches(sifre);
            if(mc.Count == 0)                              //sifrenin en az bir kucuk harf icerdiginin kontrolu 
            {
                Console.Write("ERROR!: You must have at least one non-capital alphabetic character. Try again.\n");
                return KullaniciSifreOlustur();
            }
            mc = OzelKarakter_RGX.Matches(sifre);
            if(mc.Count == 0)                                 //sifrenin en az bir ozel karakter icerdiginin kontrolu 
            {
                Console.Write("ERROR!: You must have at least one special character. Try again.\n");
                return KullaniciSifreOlustur();
            }
            mc = BoslukKarakter_RGX.Matches(sifre);
            if(mc.Count != 0)                               //sifrenin bosluk karakteri icermediginin kontrolu
            {
                Console.Write("ERROR!: You shouldn't have space character. Try again.\n");
                return KullaniciSifreOlustur();
            }
            return sifre;
        }
        internal static string KullaniciIsmiOlustur()               //kullanici isim girisi icin ekran yazdirir,
        {                                                           //kullanici isminin ilk karakterinin harf oldugunu kontrol eder ve
            string IlkKarakterHarf = "^[a-zA-Z]";                   //kullanci isminin ozel karakter icermedigini kontrol eder.
            string OzelKarakter = "[!@#$%&*-+]";
            Regex IlkKarakterHarf_RGX = new Regex(IlkKarakterHarf);
            Regex OzelKarakter_RGX = new Regex(OzelKarakter);
            
            Console.Write("Name:\n>");
            string isim = Convert.ToString(Console.ReadLine());

            MatchCollection mc = IlkKarakterHarf_RGX.Matches(isim);
            if(mc.Count == 0)               //kullanici isminin ilk karakterinin harf oldugu kontrolu
            {
                Console.Write("ERROR!: First character isn't alphabetic character. Try again.\n");
                return KullaniciIsmiOlustur();
            }
            mc = OzelKarakter_RGX.Matches(isim);
            if(mc.Count != 0)                //kullanici isminin ozel karakter icermediginin kontrolu
            {
                Console.Write("ERROR!: Name can't involve special character. Try again.\n");
                return KullaniciIsmiOlustur();
            }
            return isim;
        }
        internal static string KullaniciEpostaOlustur()         //kullanici e-posta girisi icin ekran yazdirir,
        {                                                       //e-postanin ozel karakterle baslamadigini kontrol eder ve 
            string IlkOzelKarakter = "^[!@#$%&*-+]";            //e-postanin @ icerdigini kontrol eder.
            string karakterAT = "[@]";
            Regex IlkOzelKarakter_RGX = new Regex(IlkOzelKarakter);
            Regex karakterAT_RGX = new Regex(karakterAT);


            Console.Write("E-Mail:\n>");
            string eposta = Convert.ToString(Console.ReadLine());

            MatchCollection mc = IlkOzelKarakter_RGX.Matches(eposta);
            if(mc.Count != 0)               //ilk karakterin ozel karakter olmadiginin kontrolu
            {
                Console.Write("ERROR!: First character can't be special character. Try again.\n");
                return KullaniciEpostaOlustur();
            }
            mc = karakterAT_RGX.Matches(eposta);
            if(mc.Count == 0)               //@ karakterini icerdiginin kontrolu
            {
                Console.Write("ERROR!: E-mail must have \"@\" character. Try again.\n");
                return KullaniciEpostaOlustur();
            }
            return eposta;
        }
        internal static string KullaniciTelefonOlustur()            //kullanici telefon girisi icin ekran yazdirir,
        {                                                           //telefonun harf icermedigini kontrol eder ve 
            string Alfabetik = "[a-zA-Z]";                          //telefonun xxx-xxx-xxxx formatinda girildigini kontrol eder.
            string telefonFormat = "^.{3,3}-.{3,3}-.{4,4}$";
            Regex Alfabetik_RGX = new Regex(Alfabetik);
            Regex telefonFormat_RGX = new Regex(telefonFormat);

            Console.Write("Phone number:\n>");
            string telefon = Convert.ToString(Console.ReadLine());

            MatchCollection mc = Alfabetik_RGX.Matches(telefon);
            if(mc.Count != 0)                   //harf icermediginin kontrolu
            {
                Console.Write("ERROR!: Phone number can't include alphabetic character. Try again.\n");
                return KullaniciTelefonOlustur();
            }
            mc = telefonFormat_RGX.Matches(telefon);
            if(mc.Count == 0)                   //telefon formatinin kontrolu
            {
                Console.Write("ERROR!: Phone number is entered in wrong shape. It must be entered like \"xxx-xxx-xxxx\" this. Try again.\n");
                return KullaniciTelefonOlustur();
            }
            return telefon;
        }
    }
}