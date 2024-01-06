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
        internal void arabaEkle(string dosyaYolu,int yedekParcaSize)
        {
            File.AppendAllText(dosyaYolu, this.marka+ " ");
            File.AppendAllText(dosyaYolu, this.model+ " ");
            for(int i=0; i<2; i++)
            {
                File.AppendAllText(dosyaYolu, this.donanim[i].isim + " ");
                for(int j=0; j<yedekParcaSize; j++)
                {
                    if(i==1&&j==yedekParcaSize-1)
                    {
                        File.AppendAllText(dosyaYolu, this.donanim[i].yedekParca[j].parca+ " ");
                        File.AppendAllText(dosyaYolu, Convert.ToString(this.donanim[i].yedekParca[j].stok));
                    }
                    else
                    {
                        File.AppendAllText(dosyaYolu, this.donanim[i].yedekParca[j].parca+ " ");
                        File.AppendAllText(dosyaYolu, Convert.ToString(this.donanim[i].yedekParca[j].stok)+ " ");
                    }
                }
            }
            File.AppendAllText(dosyaYolu, "\n");
        }
        internal void arabaBilgi()
        {
            Console.Write($"{this.marka} {this.model}\n\n>{this.donanim[0].isim} -> Spare Part List:"); 
            for(int i=0; i<this.donanim[0].yedekParca.Length; i++)
            {
                Console.Write($"\n{this.donanim[0].yedekParca[i].parca}: {this.donanim[0].yedekParca[i].stok}"); 
            }
            Console.Write($"\n\n>{this.donanim[1].isim} -> Spare Part List:");
            for(int i=0; i<this.donanim[1].yedekParca.Length; i++)
            {
                Console.Write($"\n{this.donanim[1].yedekParca[i].parca}: {this.donanim[1].yedekParca[i].stok}"); 
            }
            Console.Write("\n");
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

        internal Kullanici(string statu,string kullaniciAdi,string sifre,string isim,string telefon,string eposta)
        {
            this.statu = statu;
            this.kullaniciAdi = kullaniciAdi;
            this.sifre = sifre;
            this.isim = isim;
            this.telefon = telefon;
            this.eposta = eposta;
        }
        internal void KullaniciEkle(string dosyaYolu)
        {
            File.AppendAllText(dosyaYolu, this.statu);
            File.AppendAllText(dosyaYolu, "\n"+this.kullaniciAdi);
            File.AppendAllText(dosyaYolu, "\n"+this.sifre);
            File.AppendAllText(dosyaYolu, "\n"+this.isim);
            File.AppendAllText(dosyaYolu, "\n"+this.eposta);
            File.AppendAllText(dosyaYolu, "\n"+this.telefon);
        }
        internal void KullaniciSil(string dosyaYolu)
        {
            Kullanici[] kullaniciListeEski = Program.DosyadanKullaniciya(dosyaYolu);
            int silinenIndex = 0;
            
            for(int i=0; i<kullaniciListeEski.Length; i++)
            {
                if(kullaniciListeEski[i].kullaniciAdi == this.kullaniciAdi)
                {
                    silinenIndex = i;   
                }
            }
            for(int j=silinenIndex; j<kullaniciListeEski.Length-1; j++)
            {
                kullaniciListeEski[j] = kullaniciListeEski[j+1];
            }
            Kullanici[] kullaniciListeYeni = new Kullanici[kullaniciListeEski.Length-1];
            for(int i=0; i<kullaniciListeEski.Length-1; i++)
            {
                kullaniciListeYeni[i] = new Kullanici(kullaniciListeEski[i].statu,kullaniciListeEski[i].kullaniciAdi,
                kullaniciListeEski[i].sifre,kullaniciListeEski[i].isim,kullaniciListeEski[i].telefon,kullaniciListeEski[i].eposta);
            }
            Program.KullanicidanDosyaya(dosyaYolu,kullaniciListeYeni);
        }
    }
    public class Yonetici : Kullanici
    {
        internal Yonetici(string statu,string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(statu,kullaniciAdi,sifre,isim,telefon,eposta)
        {}
        internal Yonetici(Kullanici kullanici) 
        : base(kullanici.statu,kullanici.kullaniciAdi,kullanici.sifre,kullanici.isim,kullanici.telefon,kullanici.eposta)
        {
            this.statu = "yonetici";
        }
        internal void yoneticiBilgi(string dosyaYolu)
        {
            if (File.Exists(dosyaYolu)) 
            {
                string[] yoneticibilgi = File.ReadAllLines(dosyaYolu); 
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
        internal Musteri(string statu,string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(statu,kullaniciAdi,sifre,isim,telefon,eposta)
        {
            this.statu = "musteri";
        }
        internal Musteri(Kullanici kullanici)
        : base(kullanici.statu,kullanici.kullaniciAdi,kullanici.sifre,kullanici.isim,kullanici.telefon,kullanici.eposta)
        {
            this.statu = "musteri";
        }
        internal void musteriBilgi(string dosyaYolu)
        {
            if (File.Exists(dosyaYolu)) 
            {
                string[] musteribilgi = File.ReadAllLines(dosyaYolu); 
                kullaniciAdi = musteribilgi[0];
                sifre = musteribilgi[1];
                isim = musteribilgi[2];
                telefon = musteribilgi[3];
                eposta = musteribilgi[4]; 
            } 
        }
        internal void ArabaListele_M(string dosyaYoluaraba,string dosyayoluKullanici,string dosyaYoluSepet,Musteri musteri)
        {
            int secilenAraba;
            int secilenDonanim;
            int secilenYedekParca;
            int quantity;
            string cancel_purchaseALL;

            Console.Write("\n- LIST OF CARS -\n");
            Araba[] arabaListe = Program.DosyadanArabaya(dosyaYoluaraba);
            Console.Write($"\n{arabaListe[0].marka}");
            for(int i=0; i<arabaListe.Length; i++)
            {
                Console.Write($"\n{i+1}- {arabaListe[i].marka} {arabaListe[i].model}");
            }
            Console.Write("\nSelect a car.\n>");
            secilenAraba = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nPackage List:");
            Console.Write($"\n1- {arabaListe[secilenAraba-1].donanim[0].isim}\n2- {arabaListe[secilenAraba-1].donanim[1].isim}");
            Console.Write("\nSelect a package of this car.\n>");
            secilenDonanim = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nSpare Part List:");
            for(int i=0; i<arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca.Length; i++)
            {
                Console.Write
                ($"\n{i+1}- {arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[i].parca} ");
                Console.Write($"- Stok: {arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[i].stok}");
            }
            Console.Write("\nSelect a spare part to purchase.\n>");
            secilenYedekParca = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nEnter quantity of your purchase: ");
            quantity = Convert.ToInt32(Console.ReadLine());
            if(quantity > arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok)
            {
                Console.Write("\nERROR!: Entered quantity is above current stock. ");
                Console.Write("Would you like to purchase all of the stock of this item or cancel your purchase?(Purchase/Cancel)\n>");
                cancel_purchaseALL = Convert.ToString(Console.ReadLine());
                while((cancel_purchaseALL != "Purchase" ||  cancel_purchaseALL != "purchase")&&
                ( cancel_purchaseALL != "Cancel" || cancel_purchaseALL != "cancel"))
                {
                    Console.Write("\nERROR!: Invalid enter. Try again.");
                    Console.Write("Would you like to purchase all of the stock of this item or cancel your purchase?(Purchase/Cancel)\n>");
                    cancel_purchaseALL = Convert.ToString(Console.ReadLine());
                }
                if(cancel_purchaseALL == "Purchase" ||  cancel_purchaseALL == "purchase")
                {
                    File.AppendAllText(dosyaYoluSepet,musteri.kullaniciAdi+"\n");
                    File.AppendAllText(dosyaYoluSepet,arabaListe[secilenAraba-1].marka+" ");
                    File.AppendAllText(dosyaYoluSepet,arabaListe[secilenAraba-1].model+" ");
                    File.AppendAllText(dosyaYoluSepet,arabaListe[secilenAraba-1].donanim[secilenDonanim-1].isim+" ");
                    File.AppendAllText
                    (dosyaYoluSepet,arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].parca+" ");
                    File.AppendAllText
                    (dosyaYoluSepet,arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok+"\n");
                }
                else if(cancel_purchaseALL == "Cancel" || cancel_purchaseALL == "cancel")
                {
                    Program.MusteriEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,musteri);
                }
            }
            else
            {
                arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok = quantity;
                File.AppendAllText(dosyaYoluSepet,musteri.kullaniciAdi+"\n");
                File.AppendAllText(dosyaYoluSepet,arabaListe[secilenAraba-1].marka+" ");
                File.AppendAllText(dosyaYoluSepet,arabaListe[secilenAraba-1].model+" ");
                File.AppendAllText(dosyaYoluSepet,arabaListe[secilenAraba-1].donanim[secilenDonanim-1].isim+" ");
                File.AppendAllText
                (dosyaYoluSepet,arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].parca+" ");
                File.AppendAllText
                (dosyaYoluSepet,arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok+"\n");
            } 
        }
    }
    public class Satici : Kullanici
    {
        internal Satici(string statu, string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(statu,kullaniciAdi,sifre,isim,telefon,eposta)
        {
            this.statu = "satici";
        }
        internal Satici(Kullanici kullanici)
        : base(kullanici.statu,kullanici.kullaniciAdi,kullanici.sifre,kullanici.isim,kullanici.telefon,kullanici.eposta)
        {
            this.statu = "satici";
        }
        internal void ArabaListele(string dosyaYolu)
        {
            Console.Write($"\n- LIST OF CARS -\n");
            Araba[] arabaListesi = Program.DosyadanArabaya(dosyaYolu);
            for(int i=0; i<arabaListesi.Length; i++)
            {
                Console.Write($"\n{i+1}-");
                arabaListesi[i].arabaBilgi();
            }
        }
        internal void ArabaSil(string dosyaYolu)
        {
            Araba[] arabaListeeski = Program.DosyadanArabaya(dosyaYolu);
            int secilenAraba;
            for(int i=0; i<arabaListeeski.Length; i++)
            {
                Console.Write($"\n{i+1}- {arabaListeeski[i].marka} {arabaListeeski[i].model}");
            }
            Console.Write("\nSelect a car to delete.\n>");
            secilenAraba = Convert.ToInt32(Console.ReadLine());
            for(int i=secilenAraba-1; i<arabaListeeski.Length-1; i++)
            {
                arabaListeeski[i] = arabaListeeski[i+1];
            }
            Araba[] arabaListeYeni = new Araba[arabaListeeski.Length-1];
            for(int i=0; i<arabaListeeski.Length-1; i++)
            {
                arabaListeYeni[i] = arabaListeeski[i];
            }
            Program.ArabadanDosyaya(dosyaYolu,arabaListeYeni);
        }
        internal void yedekparcaGüncelle(string dosyaYolu)
        {
            int secilenAraba = 0;
            int secilenDonanim = 0;
            int secilenYedekParca = 0;
            int stok = 0;
            Console.Write("\nCar list:");
            Araba[] arabaListe = Program.DosyadanArabaya(dosyaYolu);
            for(int i=0; i<arabaListe.Length; i++)
            {
                Console.Write($"\n{i+1}- {arabaListe[i].marka} {arabaListe[i].model}");
            }
            Console.Write("\nSelect a car to update its stock information.\n>");
            secilenAraba = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nPackage List:");
            Console.Write($"\n1- {arabaListe[secilenAraba-1].donanim[0].isim}\n2- {arabaListe[secilenAraba-1].donanim[1].isim}");
            Console.Write("\nSelect a package of this car.\n>");
            secilenDonanim = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nSpare Part List:");
            for(int i=0; i<arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca.Length; i++)
            {
                Console.Write
                ($"\n{i+1}- {arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[i].parca} ");
                Console.Write($"- Stok: {arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[i].stok}");
            }
            Console.Write("\nSelect a spare part.\n>");
            secilenYedekParca = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nEnter updated stock: ");
            stok = Convert.ToInt32(Console.ReadLine());
            arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok = stok;
            Program.ArabadanDosyaya(dosyaYolu,arabaListe);
            Console.Write("\nUpdate is successful.1");
        }
        internal void ArabaOlustur(string dosyaYolu)
        {
            int YedekParcaSayisi = 0;
            
            Console.Write("\nYour cars:\nBrand:");
            string marka = Convert.ToString(Console.ReadLine());
            Console.Write("\nModel:");
            string model = Convert.ToString(Console.ReadLine());
            Console.Write("\nFirst package:");
            string donanim1 = Convert.ToString(Console.ReadLine());
            Console.Write("\nSecond package:");
            string donanim2 = Convert.ToString(Console.ReadLine());
            Console.Write("\nHow many spare part you would like to enter?\n");
            YedekParcaSayisi = Convert.ToInt32(Console.ReadLine());

            YedekParca[] yedekparca = new YedekParca[YedekParcaSayisi];
            for(int i=0; i<YedekParcaSayisi; i++)
            {
                yedekparca[i] = new YedekParca("",0);
            }
            Donanim[] donanim = new Donanim[2];
            donanim[0] = new Donanim("",yedekparca); 
            donanim[1] = new Donanim("",yedekparca);
            Araba araba = new Araba("","",donanim);

            araba.marka = marka;
            araba.model = model;
            araba.donanim[0].isim = donanim1;
            araba.donanim[1].isim = donanim2;

            while(YedekParcaSayisi<5||YedekParcaSayisi>15)
            {
                Console.Write("\nERROR!: Invalid enter. You cant enter spare parts less than 5 or more than 15.");
                Console.Write("\nHow many spare part you would like to enter?\n");
                YedekParcaSayisi = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nEnter spare parts of your first package.");
            for(int i=0; i<YedekParcaSayisi; i++)
            {   
                Console.Write("\nSpare part:");
                araba.donanim[0].yedekParca[i].parca = Convert.ToString(Console.ReadLine());
                Console.Write("\nStock:");
                araba.donanim[0].yedekParca[i].stok = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nEnter spare parts of your second package");
            for(int i=0; i<YedekParcaSayisi; i++)
            {
                Console.Write("\nSpare part:");
                araba.donanim[1].yedekParca[i].parca = Convert.ToString(Console.ReadLine());
                Console.Write("\nStock:");
                araba.donanim[1].yedekParca[i].stok = Convert.ToInt32(Console.ReadLine());
            }
            araba.arabaEkle(dosyaYolu,YedekParcaSayisi);
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
        internal static void SaticiEkrani(string dosyaYoluaraba,string dosyayoluKullanici,Satici satici)
        {
            Console.Clear();
            Console.Write("\nSelect an operation");
            Console.Write("\n1- List cars and their features\n2- Update spare part stock information");
            Console.Write("\n3- Add a car to the system\n4- Delete a car from the system");
            Console.Write("\n5- Delete my account\n6- Exit my account\n>");
            int islem = Convert.ToInt32(Console.ReadLine());
            switch(islem)
            {
                case 1:
                    Console.Clear();
                    satici.ArabaListele(dosyaYoluaraba);
                    break;
                case 2:
                    Console.Clear();
                    satici.yedekparcaGüncelle(dosyaYoluaraba);
                    break;
                case 3:
                    Console.Clear();
                    satici.ArabaOlustur(dosyaYoluaraba);
                    break;
                case 4:
                    Console.Clear();
                    satici.ArabaSil(dosyaYoluaraba);
                    break;
                case 5:
                    Console.Clear();
                    satici.KullaniciSil(dosyayoluKullanici);
                    break;
            }
        }
        internal static void MusteriEkrani(string dosyaYoluaraba,string dosyayoluKullanici,string dosyaYoluSepet,Musteri musteri)
        {
            Console.Clear();
            Console.Write("\nSelect an operation");
            Console.Write("\n1- List cars and their features\n2- List my cart");
            Console.Write("\n3- Delete my account\n>");
            int islem = Convert.ToInt32(Console.ReadLine());
            switch(islem)
            {
                case 1:
                    Console.Clear();
                    musteri.ArabaListele_M(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,musteri);
                    break;
                case 2:
                    Console.Clear();
                    
                    break;
                case 3:
                    Console.Clear();
                    musteri.KullaniciSil(dosyayoluKullanici);
                    break;
            }
        }
        internal static Kullanici[] DosyadanKullaniciya(string dosyaYolu)
        {
            string[] stringDosya = File.ReadAllLines(dosyaYolu);
            Kullanici[] kullaniciListe = new Kullanici[stringDosya.Length/6];
            for(int i=0; i<stringDosya.Length; i+=6)
            {
                kullaniciListe[i/6] = 
                new Kullanici(stringDosya[i],stringDosya[i+1],stringDosya[i+2],stringDosya[i+3],stringDosya[i+5],stringDosya[i+4]);
            }
            return kullaniciListe;
        }
        internal static void KullanicidanDosyaya(string dosyaYolu, Kullanici[] kullaniciListe)
        {
            File.WriteAllText(dosyaYolu,"");
            for(int i=0; i<kullaniciListe.Length; i++)
            {
                kullaniciListe[i].KullaniciEkle(dosyaYolu);
                File.AppendAllText(dosyaYolu,"\n");
            }
        }
        
        internal static Araba[] DosyadanArabaya(string dosyaYolu)
        {
            string[] stringDosya = File.ReadAllLines(dosyaYolu);
            Araba[] arabaListe = new Araba[stringDosya.Length];
            for(int i=0; i<stringDosya.Length; i++)
            {
                int satirBoyut = stringDosya[i].Split(" ").Length;
                Console.Clear();
                Console.Write($"{satirBoyut}");
                string[] arabaDosya = new string[satirBoyut];
                arabaDosya = stringDosya[i].Split(" ");
                YedekParca[] yedekParca = new YedekParca[(satirBoyut-4)/4];
                int parcaIndex = 3;
                for(int j=0; j<(satirBoyut-4)/4; j++)
                {
                    yedekParca[j] = new YedekParca(arabaDosya[parcaIndex],Convert.ToInt32(arabaDosya[parcaIndex+1]));
                    parcaIndex +=2;
                }
                YedekParca[] yedekParca2 = new YedekParca[(satirBoyut-4)/4];
                parcaIndex = (int)(satirBoyut/2)+2;
                for(int j=0; j<(satirBoyut-4)/4; j++)
                {
                    yedekParca2[j] = new YedekParca(arabaDosya[parcaIndex],Convert.ToInt32(arabaDosya[parcaIndex+1]));
                    parcaIndex +=2;
                }
                Donanim[] donanim = new Donanim[2];
                donanim[0] = new Donanim(arabaDosya[2],yedekParca);
                donanim[1] = new Donanim(arabaDosya[(satirBoyut/2)+1],yedekParca2);
                arabaListe[i] = new Araba(arabaDosya[0],arabaDosya[1],donanim);
            }
            return arabaListe;
        }
        internal static void ArabadanDosyaya(string dosyaYolu, Araba[] arabaListe)
        {
            File.WriteAllText(dosyaYolu,"");
            int yedekParcaSize;
            for(int i=0; i<arabaListe.Length; i++)
            {
                yedekParcaSize = arabaListe[i].donanim[0].yedekParca.Length;
                arabaListe[i].arabaEkle(dosyaYolu,yedekParcaSize); 
            }
        }
        internal static Kullanici kullaniciGiris(string dosyaYolu)
        {
            Kullanici[] kullaniciListe = DosyadanKullaniciya(dosyaYolu);

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
        public static void Main()
        {
            string kullaniciDosya = "kullanicilar.txt";
            string arabaDosya = "arabalar.txt";
            string sepetDosya = "sepet.txt";
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
            if(!File.Exists(sepetDosya))
            {
                File.AppendAllText(sepetDosya,"");
            }
            
            GirisYontemi girisYontemi = GirisEkrani();

            if(girisYontemi==GirisYontemi.giris)
            {
                Kullanici kullanici = kullaniciGiris(kullaniciDosya);
                Console.Clear();
                switch(kullanici.statu)
                {
                    case "satici":
                        Satici satici = new Satici(kullanici);
                        SaticiEkrani(arabaDosya,kullaniciDosya,satici);
                        break;
                    case "musteri":
                        Musteri musteri = new Musteri(kullanici);
                        MusteriEkrani(arabaDosya,kullaniciDosya,sepetDosya,musteri);
                        break;
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

