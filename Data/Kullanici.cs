//220229043_GüneşBalcı

using System;

namespace Proje
{
    public class Kullanici //kullanici sinifi
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
        internal void KullaniciEkle(string dosyaYolu) //dosyaya kullanici turundeki degiskeni ekler
        {
            File.AppendAllText(dosyaYolu, this.statu);
            File.AppendAllText(dosyaYolu, "\n"+this.kullaniciAdi);
            File.AppendAllText(dosyaYolu, "\n"+this.sifre);
            File.AppendAllText(dosyaYolu, "\n"+this.isim);
            File.AppendAllText(dosyaYolu, "\n"+this.eposta);
            File.AppendAllText(dosyaYolu, "\n"+this.telefon+"\n");
        }
        internal void KullaniciSil(string dosyaYolu) //dosyadan kullaniciyi siler
        {
            Kullanici[] kullaniciListeEski = Dosya_Stok.DosyadanKullaniciya(dosyaYolu);
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
            Dosya_Stok.KullanicidanDosyaya(dosyaYolu,kullaniciListeYeni);
        }
    }
    public class Yonetici : Kullanici //kullanicinin alt sinifi yonetici
    {
        internal Yonetici(string statu,string kullaniciAdi,string sifre,string isim,string telefon,string eposta) 
        : base(statu,kullaniciAdi,sifre,isim,telefon,eposta)
        {}
        internal Yonetici(Kullanici kullanici) 
        : base(kullanici.statu,kullanici.kullaniciAdi,kullanici.sifre,kullanici.isim,kullanici.telefon,kullanici.eposta)
        {
            this.statu = "yonetici";
        }
        internal void MusteriSil(string dosyayoluKullanici,string dosyaYoluSepet) //musteri turundeki kullaniciyi sistemden(dosyadan) siler
        {
            int secilenMusteri;
            Kullanici[] kullaniciListe = Dosya_Stok.DosyadanKullaniciya(dosyayoluKullanici);
            string[] sepetDosya = File.ReadAllLines(dosyaYoluSepet);
            Console.Write("\nCustomer List");
            int j=1;
            int[] i_denksayi = new int[20];
            for(int i=0; i<kullaniciListe.Length; i++)
            {
                if(kullaniciListe[i].statu == "musteri")
                {
                    Console.Write($"\n{j}- {kullaniciListe[i].kullaniciAdi}");
                    i_denksayi[j]=i;
                    j++;
                }
            }
            Console.Write("\nSelect a customer to delete\n>");
            secilenMusteri = Convert.ToInt32(Console.ReadLine());
            while(secilenMusteri<1||secilenMusteri>j)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a customer to delete\n>");
                secilenMusteri = Convert.ToInt32(Console.ReadLine());
            }
            File.WriteAllText(dosyaYoluSepet,"");
            for(int i=0; i<sepetDosya.Length; i+=2)
            {
                if(kullaniciListe[i_denksayi[secilenMusteri]].kullaniciAdi==sepetDosya[i])
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
            kullaniciListe[i_denksayi[secilenMusteri]].KullaniciSil(dosyayoluKullanici);
            Console.Write("\nDelete is successful.");
        }
        internal void SaticiSil(string dosyayoluKullanici) //satici turundeki kullaniciyi sistemden(dosyadan) siler
        {
            int secilenSatici;
            Kullanici[] kullaniciListe = Dosya_Stok.DosyadanKullaniciya(dosyayoluKullanici);
            Console.Write("\nDealer List");
            int j=1;
            int[] i_denksayi = new int[20];
            for(int i=0; i<kullaniciListe.Length; i++)
            {
                if(kullaniciListe[i].statu == "satici")
                {
                    Console.Write($"\n{j}- {kullaniciListe[i].kullaniciAdi}");
                    i_denksayi[j]=i;
                    j++;
                }
            }
            Console.Write("\nSelect a dealer to delete\n>");
            secilenSatici = Convert.ToInt32(Console.ReadLine());
            while(secilenSatici<1||secilenSatici>j)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a dealer to delete\n>");
                secilenSatici = Convert.ToInt32(Console.ReadLine());
            }
            kullaniciListe[i_denksayi[secilenSatici]].KullaniciSil(dosyayoluKullanici);
            Console.Write("\nDelete is successful.");
        }
        internal void ArabaSil(string dosyaYoluaraba,string dosyaYoluSepet) //araba turundeki degeri sistemden(dosyadan) ve sepetten siler
        {
            int secilenAraba;
            Araba[] arabaListe = Dosya_Stok.DosyadanArabaya(dosyaYoluaraba);
            string[] sepetDosya = File.ReadAllLines(dosyaYoluSepet);
            Console.Write("\nCar List");
            for(int i=0; i<arabaListe.Length; i++)
            {
                Console.Write($"\n{i+1}- {arabaListe[i].marka} {arabaListe[i].model}");
            }
            Console.Write("\nSelect a car to delete\n>");
            secilenAraba = Convert.ToInt32(Console.ReadLine());
            while(secilenAraba<1||secilenAraba>arabaListe.Length)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a car to delete\n>");
                secilenAraba = Convert.ToInt32(Console.ReadLine());
            }
            File.WriteAllText(dosyaYoluSepet,"");
            for(int i=1; i<sepetDosya.Length; i+=2)
            {   
                string[] sepet = sepetDosya[i].Split(" ");
                if(arabaListe[secilenAraba-1].marka==sepet[0])
                {
                    sepetDosya[i] = "";
                    sepetDosya[i-1] = "";
                }
            }
            for(int i=0; i<sepetDosya.Length; i++)
            {
                if(sepetDosya[i]!="")
                {
                    File.AppendAllLines(dosyaYoluSepet,sepetDosya);
                }
            }
            for(int i=secilenAraba-1; i<arabaListe.Length-1; i++)
            {
                arabaListe[i] = arabaListe[i+1];
            }
            Araba[] arabaListeYeni = new Araba[arabaListe.Length-1];
            for(int i=0; i<arabaListe.Length-1; i++)
            {
                arabaListeYeni[i] = arabaListe[i];
            }
            Dosya_Stok.ArabadanDosyaya(dosyaYoluaraba,arabaListeYeni);
            Console.Write("\nDelete is successful.");
        }
    }
    public class Musteri : Kullanici //kullanicinin alt sinifi musteri
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
        internal void ArabaListele_M(string dosyaYoluaraba,string dosyayoluKullanici,string dosyaYoluSepet,Musteri musteri) //dosyadan okuyarak araba listesi yazdirir 
        {                                                                                                                   //ve secilen parcayi ozellikleriyle beraber sepete ekler
            int secilenAraba;
            int secilenDonanim;
            int secilenYedekParca;
            int quantity;
            string cancel_purchaseALL;

            Console.Write("\n- LIST OF CARS -\n");
            Araba[] arabaListe = Dosya_Stok.DosyadanArabaya(dosyaYoluaraba);
            for(int i=0; i<arabaListe.Length; i++)
            {
                Console.Write($"\n{i+1}- {arabaListe[i].marka} {arabaListe[i].model}");
            }
            Console.Write("\nSelect a car.\n>");
            secilenAraba = Convert.ToInt32(Console.ReadLine());
            while(secilenAraba<1||secilenAraba>arabaListe.Length)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a car.\n>");
                secilenAraba = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nPackage List:");
            Console.Write($"\n1- {arabaListe[secilenAraba-1].donanim[0].isim}\n2- {arabaListe[secilenAraba-1].donanim[1].isim}");
            Console.Write("\nSelect a package of this car.\n>");
            secilenDonanim = Convert.ToInt32(Console.ReadLine());
            while(secilenDonanim!=1&&secilenDonanim!=2)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a package of this car.\n>");
                secilenDonanim = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nSpare Part List:");
            for(int i=0; i<arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca.Length; i++)
            {
                Console.Write
                ($"\n{i+1}- {arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[i].parca} ");
                Console.Write($"- Stok: {arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[i].stok}");
            }
            Console.Write("\nSelect a spare part to purchase.\n>");
            secilenYedekParca = Convert.ToInt32(Console.ReadLine());
            while(secilenYedekParca<1||secilenYedekParca>arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca.Length)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a spare part to purchase.\n>");
                secilenYedekParca = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nEnter quantity of your purchase: ");
            quantity = Convert.ToInt32(Console.ReadLine());
            while(quantity<1)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nEnter quantity of your purchase: ");
                quantity = Convert.ToInt32(Console.ReadLine());
            }
            if(quantity > arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok)
            {
                Console.Write("\nERROR!: Entered quantity is above current stock. ");
                Console.Write("Would you like to purchase all of the stock of this item or cancel your purchase?(Purchase/Cancel)\n>");
                cancel_purchaseALL = Convert.ToString(Console.ReadLine());
                while(cancel_purchaseALL != "Purchase" &&  cancel_purchaseALL != "purchase"&&
                cancel_purchaseALL != "Cancel" && cancel_purchaseALL != "cancel")
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
                    (dosyaYoluSepet,arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok+" ");
                    File.AppendAllText(dosyaYoluSepet,"Pending\n");
                    Console.Write("\nPurchase request is successful.");
                }
                else if(cancel_purchaseALL == "Cancel" || cancel_purchaseALL == "cancel")
                {
                    Ekran.MusteriEkrani(dosyaYoluaraba,dosyayoluKullanici,dosyaYoluSepet,musteri);
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
                (dosyaYoluSepet,arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok+" ");
                File.AppendAllText(dosyaYoluSepet,"Pending\n");
                Console.Write("\nPurchase request is successful.");
            } 
        }
        internal void SepetListele(string dosyaYoluSepet,Musteri musteri) //musterinin sepetini yazdirir
        {
            Console.Write("\nMy Cart:");
            int j=1;
            string[] SepetDosya = File.ReadAllLines(dosyaYoluSepet);
            for(int i=0; i<SepetDosya.Length; i+=2)
            {
                if(SepetDosya[i] == musteri.kullaniciAdi)
                {
                    string[] yedekParcaBilgileri = SepetDosya[i+1].Split(" ");
                    Console.Write($"\n{j}- ");
                    Console.Write($"{yedekParcaBilgileri[0]} {yedekParcaBilgileri[1]} {yedekParcaBilgileri[2]} ");
                    Console.Write($"{yedekParcaBilgileri[3]}:{yedekParcaBilgileri[4]}-{yedekParcaBilgileri[5]}");
                }
            } 
        }
    }
    public class Satici : Kullanici // kullanici turunun alt sinifi
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
        internal void ArabaListele(string dosyaYolu) //araba listesi yazdirir
        {
            Console.Write($"\n- LIST OF CARS -\n");
            Araba[] arabaListesi = Dosya_Stok.DosyadanArabaya(dosyaYolu);
            for(int i=0; i<arabaListesi.Length; i++)
            {
                Console.Write($"\n{i+1}-");
                arabaListesi[i].arabaBilgi();
            }
        }
        internal void ArabaSil(string dosyaYolu,string dosyaYoluSepet) //arabayi sistemden(dosyadan) siler
        {                                                               //sepette ise silmez, uyari verir
            bool sepetteVar = false;
            Araba[] arabaListeeski = Dosya_Stok.DosyadanArabaya(dosyaYolu);
            string[] sepet = File.ReadAllLines(dosyaYoluSepet);
            string[][] sepetDosya = new string[sepet.Length][];
            for(int i=0; i<sepet.Length; i++)
            {
                sepetDosya[i] = sepet[i].Split(" ");
            }
            int secilenAraba;
            for(int i=0; i<arabaListeeski.Length; i++)
            {
                Console.Write($"\n{i+1}- {arabaListeeski[i].marka} {arabaListeeski[i].model}");
            }
            Console.Write("\nSelect a car to delete.\n>");
            secilenAraba = Convert.ToInt32(Console.ReadLine());
            while(secilenAraba<1||secilenAraba>arabaListeeski.Length)
            {
                Console.Write("\nERROR!: Invalid enter.");
                Console.Write("\nSelect a car to delete.\n>");
                secilenAraba = Convert.ToInt32(Console.ReadLine());
            }
            for(int i=1; i<sepet.Length; i+=2)
            {
                if(sepetDosya[i][0]==arabaListeeski[secilenAraba-1].marka && sepetDosya[i][5]=="Pending") //sepette olup olmadigina bakan kontrol
                {
                    sepetteVar = true;
                }
            }
            if(!sepetteVar)
            {
                for(int i=secilenAraba-1; i<arabaListeeski.Length-1; i++)
                {
                    arabaListeeski[i] = arabaListeeski[i+1];
                }
                Araba[] arabaListeYeni = new Araba[arabaListeeski.Length-1];
                for(int i=0; i<arabaListeeski.Length-1; i++)
                {
                    arabaListeYeni[i] = arabaListeeski[i];
                }
                Dosya_Stok.ArabadanDosyaya(dosyaYolu,arabaListeYeni);
            }
            else
            {
                Console.Write("ERROR!: Please answer customer purchase request of this car first.");
            }
        }
        internal void yedekparcaGüncelle(string dosyaYolu) //secilen yedek parcanin stok bilgisi degistirilip dosyaya tekrar yazdirilir
        {
            int secilenAraba = 0;
            int secilenDonanim = 0;
            int secilenYedekParca = 0;
            int stok = 0;
            Console.Write("\nCar list:");
            Araba[] arabaListe = Dosya_Stok.DosyadanArabaya(dosyaYolu);
            for(int i=0; i<arabaListe.Length; i++)
            {
                Console.Write($"\n{i+1}- {arabaListe[i].marka} {arabaListe[i].model}");
            }
            Console.Write("\nSelect a car to update its stock information.\n>");
            secilenAraba = Convert.ToInt32(Console.ReadLine());
            while(secilenAraba<1||secilenAraba>arabaListe.Length)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a car to update its stock information.\n>");
                secilenAraba = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nPackage List:");
            Console.Write($"\n1- {arabaListe[secilenAraba-1].donanim[0].isim}\n2- {arabaListe[secilenAraba-1].donanim[1].isim}");
            Console.Write("\nSelect a package of this car.\n>");
            secilenDonanim = Convert.ToInt32(Console.ReadLine());
            while(secilenDonanim!=1&&secilenDonanim!=2)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a package of this car.\n>");
                secilenDonanim = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nSpare Part List:");
            for(int i=0; i<arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca.Length; i++)
            {
                Console.Write
                ($"\n{i+1}- {arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[i].parca} ");
                Console.Write($"- Stok: {arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[i].stok}");
            }
            Console.Write("\nSelect a spare part.\n>");
            secilenYedekParca = Convert.ToInt32(Console.ReadLine());
            while(secilenYedekParca<1||secilenYedekParca>arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca.Length)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a spare part.\n>");
                secilenYedekParca = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nEnter updated stock: ");
            stok = Convert.ToInt32(Console.ReadLine());
            while(stok<0)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nEnter updated stock: ");
                stok = Convert.ToInt32(Console.ReadLine());
            }
            arabaListe[secilenAraba-1].donanim[secilenDonanim-1].yedekParca[secilenYedekParca-1].stok = stok;
            Dosya_Stok.ArabadanDosyaya(dosyaYolu,arabaListe);
            Console.Write("\nUpdate is successful.");
        }
        internal void ArabaOlustur(string dosyaYolu) //yeni araba olusturur ve sisteme(dosyaya ekler)
        {
            int YedekParcaSayisi = 0;
            
            Console.Write("\nYour cars:\nBrand:");
            string marka = Convert.ToString(Console.ReadLine());
            while(kayitKontrol.boslukVar(marka))
            {
                Console.Write("\nERROR!: Invalid enter: space character is used. Try again.\nBrand:");
                marka = Convert.ToString(Console.ReadLine());
            }
            Console.Write("\nModel:");
            string model = Convert.ToString(Console.ReadLine());
            while(kayitKontrol.boslukVar(model))
            {
                Console.Write("\nERROR!: Invalid enter: space character is used. Try again.\nModel:");
                model = Convert.ToString(Console.ReadLine());
            }
            Console.Write("\nFirst package:");
            string donanim1 = Convert.ToString(Console.ReadLine());
            while(kayitKontrol.boslukVar(donanim1))
            {
                Console.Write("\nERROR!: Invalid enter: space character is used. Try again.\nFirst Package:");
                donanim1 = Convert.ToString(Console.ReadLine());
            }
            Console.Write("\nSecond package:");
            string donanim2 = Convert.ToString(Console.ReadLine());
            while(kayitKontrol.boslukVar(donanim2))
            {
                Console.Write("\nERROR!: Invalid enter: space character is used. Try again.\nSecond Package:");
                donanim2 = Convert.ToString(Console.ReadLine());
            }
            Console.Write("\nHow many spare part you would like to enter?\n");//iki donanima girilecek yedek parca sayisi aynidir
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
                while(kayitKontrol.boslukVar(araba.donanim[0].yedekParca[i].parca))
                {
                    Console.Write("\nERROR!: Invalid enter: space character is used. Try again.\nSpare part:");
                    araba.donanim[0].yedekParca[i].parca = Convert.ToString(Console.ReadLine());
                }
                Console.Write("\nStock:");
                araba.donanim[0].yedekParca[i].stok = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\nEnter spare parts of your second package");
            for(int i=0; i<YedekParcaSayisi; i++)
            {
                Console.Write("\nSpare part:");
                araba.donanim[1].yedekParca[i].parca = Convert.ToString(Console.ReadLine());
                while(kayitKontrol.boslukVar(araba.donanim[1].yedekParca[i].parca))
                {
                    Console.Write("\nERROR!: Invalid enter: space character is used. Try again.\nSpare part:");
                    araba.donanim[1].yedekParca[i].parca = Convert.ToString(Console.ReadLine());
                }
                Console.Write("\nStock:");
                araba.donanim[1].yedekParca[i].stok = Convert.ToInt32(Console.ReadLine());
            }
            araba.arabaEkle(dosyaYolu,YedekParcaSayisi);
        }
        internal void SatisTalepleriGoster(string dosyaYoluSepet,string dosyayoluKullanici,string dosyaYoluaraba) //musterilerin yaptigi cevaplanmamis talepleri yazdirir
        {                                                                                                         //taleplere yapilacak islemi sectirir
            int SecilenTalep;                                                                                    //talep stogu asmis ise onaya izin vermez
            int SecilenIslem;                                                                                       //talebi yapan musteri bilgirini yazdirir
            int j=1;
            Console.Write("\nPenging Purchase Requests");
            string[] SepetDosya = File.ReadAllLines(dosyaYoluSepet);
            for(int i=0; i<SepetDosya.Length; i+=2)
            {
                string[] yedekParcaBilgi = SepetDosya[i+1].Split(" ");
                if(yedekParcaBilgi[5]=="Pending")
                {
                    Console.Write($"\n{j}- {SepetDosya[i]}: {yedekParcaBilgi[0]} {yedekParcaBilgi[1]}");
                    Console.Write($" {yedekParcaBilgi[2]} {yedekParcaBilgi[3]} {yedekParcaBilgi[4]} {yedekParcaBilgi[5]}");
                    j +=1;
                }
            }
            j= 0;
            int[] i_denkSayi = new int[20];
            for(int i=0; i<SepetDosya.Length; i+=2)
            {
                string[] yedekParcaBilgi = SepetDosya[i+1].Split(" ");
                if(yedekParcaBilgi[5]=="Pending")
                {
                    i_denkSayi[j] = i;
                    j +=1;
                }
            }
            Console.Write("\nSelect a request to approve or decline: ");
            SecilenTalep = Convert.ToInt32(Console.ReadLine());
            while(SecilenTalep<1||SecilenTalep>j)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\nSelect a request to approve or decline: ");
                SecilenTalep = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("\n1- Accept\n2- Decline\n3- Show customer information\n>");
            SecilenIslem = Convert.ToInt32(Console.ReadLine());
            while(SecilenIslem!=1&&SecilenIslem!=2&&SecilenIslem!=3)
            {
                Console.Write("\nERROR!: Invalid enter. Try again.");
                Console.Write("\n1- Accept\n2- Decline\n3- Show customer information\n>");
                SecilenIslem = Convert.ToInt32(Console.ReadLine());
            }
            switch(SecilenIslem)
            {
                case 1:
                    File.WriteAllText(dosyaYoluSepet,"");
                    for(int i=0; i<SepetDosya.Length; i+=2)
                    {
                        string[] yedekParcaBilgi = SepetDosya[i+1].Split(" ");
                        if(i_denkSayi[SecilenTalep-1] == i)
                        {
                            if(!Dosya_Stok.StoktanFazla(dosyaYoluaraba,yedekParcaBilgi))//stogu asip asmadigina bakan kontrol
                            {
                                yedekParcaBilgi[5] = "Accepted"; 
                                Dosya_Stok.SatistanStokGuncelle(dosyaYoluaraba,yedekParcaBilgi);
                            }
                            else
                            {
                                Console.Write("\nERROR!: Lack of item! Accept is impossible without updating stock.");
                            }  
                        }
                        File.AppendAllText(dosyaYoluSepet,SepetDosya[i]+"\n");
                        for(int k=0; k<5; k++)
                        {   
                            File.AppendAllText(dosyaYoluSepet,yedekParcaBilgi[k]+" ");
                        }
                        File.AppendAllText(dosyaYoluSepet,yedekParcaBilgi[5]+"\n");
                    }
                    break;
                case 2:
                    File.WriteAllText(dosyaYoluSepet,"");
                    for(int i=0; i<SepetDosya.Length; i+=2)
                    {
                        string[] yedekParcaBilgi = SepetDosya[i+1].Split(" ");
                        if(i_denkSayi[SecilenTalep-1] == i)
                        {
                            yedekParcaBilgi[5] = "Declined";   
                        }
                        File.AppendAllText(dosyaYoluSepet,SepetDosya[i]+"\n");
                        for(int k=0; k<5; k++)
                        {
                            File.AppendAllText(dosyaYoluSepet,yedekParcaBilgi[k]+" ");
                        }
                        File.AppendAllText(dosyaYoluSepet,yedekParcaBilgi[5]+"\n");
                    }
                    break;
                case 3:        
                    bool kullaniciEslendi = false;                                               //talebi yapan musteri bilgileri
                    Kullanici[] kullanici = Dosya_Stok.DosyadanKullaniciya(dosyayoluKullanici);
                    for(int i=0; i<kullanici.Length; i++)
                    {
                        if(kullanici[i].kullaniciAdi == SepetDosya[i_denkSayi[SecilenTalep-1]])
                        {   
                            kullaniciEslendi = true;
                            Console.Write("\nCustomer Information");
                            Console.Write($"\nUser id: {kullanici[i].kullaniciAdi}");
                            Console.Write($"\nCustomer name: {kullanici[i].isim}");
                            Console.Write($"\nCustomer e-mail: {kullanici[i].eposta}");
                            Console.Write($"\nCustomer phone number: {kullanici[i].telefon}");
                        }
                    }
                    if(!kullaniciEslendi)
                    {
                        Console.Write($"\nThis customer no longer exist.");
                    }
                    break;
            }
        }
    } 
}