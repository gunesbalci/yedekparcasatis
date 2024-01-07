//220229043_GüneşBalcı

using System;

namespace Proje
{
    class Ekran
    {
        internal static GirisYontemi GirisEkrani() //giris ekranini yazdirir ve enum tipinde degisken dondurur
        {
            Console.Write("\nEnter 1 or 2.\n");
            Console.Write("1- I already have an account: Log in\n2- I dont have an account: Sign Up\n>");
            int girisYontemi = Convert.ToInt32(Console.ReadLine());
            if(girisYontemi!=1 && girisYontemi!=2)
            {
                Console.Write("ERROR!: Invalid enter.\n");
                return GirisEkrani();
            }
            return (GirisYontemi)girisYontemi-1;
        }  
        internal static void SaticiEkrani(string dosyaYoluaraba,string dosyayoluKullanici,string dosyaYoluSepet,Satici satici) //satici ekranini yazdirir
        {
            Console.Clear();
            Console.Write("\nSelect an operation");
            Console.Write("\n1- List cars and their features\n2- Update spare part stock information");
            Console.Write("\n3- Add a car to the system\n4- Delete a car from the system");
            Console.Write("\n5- Show purchase requests");
            Console.Write("\n6- Delete my account\n7- Exit my account\n>");
            int islem = Convert.ToInt32(Console.ReadLine());
            while(!(islem>0&&islem<8))
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect an operation\n>");
                islem = Convert.ToInt32(Console.ReadLine());
            }
            switch(islem)
            {
                case 1:
                    Console.Clear();
                    satici.ArabaListele(dosyaYoluaraba);
                    Console.Write("\nPress 1 to return to menu\n>");
                    int menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        SaticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,satici);
                    }
                    break;
                case 2:
                    Console.Clear();
                    satici.yedekparcaGüncelle(dosyaYoluaraba);
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        SaticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,satici);
                    }
                    break;
                case 3:
                    Console.Clear();
                    satici.ArabaOlustur(dosyaYoluaraba);
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        SaticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,satici);
                    }
                    break;
                case 4:
                    Console.Clear();
                    satici.ArabaSil(dosyaYoluaraba,dosyaYoluSepet);
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        SaticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,satici);
                    }
                    break;
                case 5:
                    Console.Clear();
                    satici.SatisTalepleriGoster(dosyaYoluSepet,dosyayoluKullanici,dosyaYoluaraba);
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        SaticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,satici);
                    }
                    break;
                case 6:
                    Console.Clear();
                    satici.KullaniciSil(dosyayoluKullanici);
                    Console.Write("\nPress 1 to return login page\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return login page\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        Console.Clear(); 
                        AnaMenu(dosyayoluKullanici,dosyaYoluaraba,dosyaYoluSepet);
                    }
                    break;
                case 7:
                    Console.Clear();
                    AnaMenu(dosyayoluKullanici,dosyaYoluaraba,dosyaYoluSepet);
                    break;
            }
        }
        internal static void MusteriEkrani(string dosyaYoluaraba,string dosyayoluKullanici,string dosyaYoluSepet,Musteri musteri) //musteri ekranini yazdirir
        {
            Console.Clear();
            Console.Write("\nSelect an operation");
            Console.Write("\n1- List cars and their features\n2- List my cart");
            Console.Write("\n3- Delete my account\n4- Exit my account\n>");
            int islem = Convert.ToInt32(Console.ReadLine());
            while(!(islem>0&&islem<5))
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect an operation\n>");
                islem = Convert.ToInt32(Console.ReadLine());
            }
            switch(islem)
            {
                case 1:
                    Console.Clear();
                    musteri.ArabaListele_M(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,musteri);
                    Console.Write("\nPress 1 to return to menu\n>");
                    int menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        MusteriEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,musteri);
                    }
                    break;
                case 2:
                    Console.Clear();
                    musteri.SepetListele(dosyaYoluSepet,musteri);
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        MusteriEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,musteri);
                    }
                    break;
                case 3:
                    Console.Clear();
                    musteri.KullaniciSil(dosyayoluKullanici);
                    string[] sepetDosya = File.ReadAllLines(dosyaYoluSepet);
                    File.WriteAllText(dosyaYoluSepet,"");
                    for(int i=0; i<sepetDosya.Length; i+=2)
                    {
                        if(musteri.kullaniciAdi==sepetDosya[i])
                        {
                            sepetDosya[i] = "";
                            sepetDosya[i+1] = "";
                        }
                    }
                    for(int i=0; i<sepetDosya.Length; i++)
                    {
                        if(sepetDosya[i]!="")
                        {
                            File.AppendAllLines(dosyaYoluSepet,sepetDosya);
                        }
                    }
                    Console.Write("\nPress 1 to return to login page\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to login\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        Console.Clear();
                        AnaMenu(dosyayoluKullanici,dosyaYoluaraba,dosyaYoluSepet);
                    }
                    break;
                case 4:
                    Console.Clear();
                    AnaMenu(dosyayoluKullanici,dosyaYoluaraba,dosyaYoluSepet);
                    break;
            }
        }
        internal static void YoneticiEkrani(string dosyaYoluaraba,string dosyayoluKullanici,string dosyaYoluSepet,Yonetici yonetici) //yonetici ekranini yazdrir
        {
            Console.Clear();
            Console.Write("\nSelect an operation");
            Console.Write("\n1- List cars\n2- Delete a customer");
            Console.Write("\n3- Delete a dealer\n4- Delete a car");
            Console.Write("\n5- Exit my account\n>");
            int islem = Convert.ToInt32(Console.ReadLine());
            while(!(islem>0&&islem<6))
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect an operation\n>");
                islem = Convert.ToInt32(Console.ReadLine());
            }
            switch(islem)
            {
                case 1:
                    Console.Clear();
                    Dosya_Stok.ArabaListe(dosyaYoluaraba);
                    int menu;
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        YoneticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,yonetici);
                    }
                    break;
                case 2:
                    Console.Clear();
                    yonetici.MusteriSil(dosyayoluKullanici,dosyaYoluSepet);
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        YoneticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,yonetici);
                    }
                    break;
                case 3:
                    Console.Clear();
                    yonetici.SaticiSil(dosyayoluKullanici);
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        YoneticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,yonetici);
                    }
                    break;
                case 4:
                    Console.Clear();
                    yonetici.ArabaSil(dosyaYoluaraba,dosyaYoluSepet);
                    Console.Write("\nPress 1 to return to menu\n>");
                    menu = Convert.ToInt32(Console.ReadLine());
                    while(menu!=1)
                    {
                        Console.Write("\nERROR!: Invalid enter. Try again.");
                        Console.Write("\nPress 1 to return to menu\n>");
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    if(menu == 1)
                    {
                        YoneticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,yonetici);
                    }
                    break;
                case 5:
                    Console.Clear();
                    AnaMenu(dosyayoluKullanici,dosyaYoluaraba,dosyaYoluSepet);
                    break;
            }
        }
        internal static void AnaMenu(string dosyayoluKullanici,string dosyaYoluaraba,string dosyaYoluSepet) //ana menuyu yazdirir
        {
            GirisYontemi girisYontemi = GirisEkrani();

            if(girisYontemi==GirisYontemi.giris)//log in bolumu
            {
                Kullanici kullanici = kullaniciGiris(dosyayoluKullanici);
                Console.Clear();
                switch(kullanici.statu)//giren kullanicinin sinifina gore ekran yazdirir
                {
                    case "satici":
                        Satici satici = new Satici(kullanici);
                        SaticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,satici);
                        break;
                    case "musteri":
                        Musteri musteri = new Musteri(kullanici);
                        MusteriEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,musteri);
                        break;
                    case "yonetici":
                        Yonetici yonetici = new Yonetici(kullanici);
                        YoneticiEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,yonetici);
                        break;
                }
            }
            if(girisYontemi==GirisYontemi.kayıt)//sign up bolumu
            {
                Console.Clear();
                Musteri musteri = Olustur.musteriOlustur(dosyayoluKullanici);  //kaydedilen kullanicinin tipi otomatik musteri olarak atanir
                MusteriEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,musteri); //kayit tamamlandiktan sonra giris de otomatik tamamlamis olur
            }
        }
        internal static Kullanici kullaniciGiris(string dosyaYolu)  //log in sayfasini yazdirir ve kullanici turunde deger dondurur
        {
            Console.Clear();
            Kullanici[] kullaniciListe = Dosya_Stok.DosyadanKullaniciya(dosyaYolu);

            Console.Write("\nEnter the requested information respectively.\n");
            Console.Write("\nUser ID:\n>");
            string kullaniciAdi = Convert.ToString(Console.ReadLine());

            for( int i=0 ; i<kullaniciListe.Length ; i++ )
            {
                if( kullaniciAdi == kullaniciListe[i].kullaniciAdi )
                {
                    Console.Write("\nPassword:\n>");
                    string sifre = Convert.ToString(Console.ReadLine());

                    if( sifre == kullaniciListe[i].sifre )
                    {
                        Console.Write("\nLog In successful.");
                        return kullaniciListe[i];
                    }
                    else
                    {
                        Console.Write("\nERROR!: Invalid password. Try again.");
                        return kullaniciGiris(dosyaYolu);
                    }
                }
            }
            Console.Write("\nERROR!: Invalid user id. Try again.");
            return kullaniciGiris(dosyaYolu);
        }
    }
}