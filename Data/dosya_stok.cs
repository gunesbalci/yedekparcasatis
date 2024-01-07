//220229043_GüneşBalcı

using System;

namespace Proje
{
    class Dosya_Stok //dosya veya stokla ilgili fonksiyonlari icerir
    {
        internal static Kullanici[] DosyadanKullaniciya(string dosyaYolu) //Dosyadan okunan bilgiyi kullanici dizi turune cevirir
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
        internal static void KullanicidanDosyaya(string dosyaYolu, Kullanici[] kullaniciListe) //Kullanici dizi türündeki bilgileri dosyaya aktarir
        {
            File.WriteAllText(dosyaYolu,"");
            for(int i=0; i<kullaniciListe.Length; i++)
            {
                kullaniciListe[i].KullaniciEkle(dosyaYolu);
            }
        } 
        internal static Araba[] DosyadanArabaya(string dosyaYolu)  //Dosyadan okunan bilgiyi araba dizi turune cevirir
        {
            string[] stringDosya = File.ReadAllLines(dosyaYolu);
            Araba[] arabaListe = new Araba[stringDosya.Length];
            for(int i=0; i<stringDosya.Length; i++)
            {
                int satirBoyut = stringDosya[i].Split(" ").Length;
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
        internal static void ArabadanDosyaya(string dosyaYolu, Araba[] arabaListe)   //Araba dizi türündeki bilgileri dosyaya aktarir
        {
            File.WriteAllText(dosyaYolu,"");
            int yedekParcaSize;
            for(int i=0; i<arabaListe.Length; i++)
            {
                yedekParcaSize = arabaListe[i].donanim[0].yedekParca.Length;
                arabaListe[i].arabaEkle(dosyaYolu,yedekParcaSize); 
            }
        }
        internal static void SatistanStokGuncelle(string dosyaYoluaraba,string[] yedekParcaBilgi) //Talep onaylandiginda stogu guncellemek icin kullanilir
        {
            Araba[] arabaListesi = DosyadanArabaya(dosyaYoluaraba);
            for(int i=0; i<arabaListesi.Length; i++)
            {
                if(arabaListesi[i].marka == yedekParcaBilgi[0])
                {
                    for(int j=0; j<2; j++)
                    {
                        if(arabaListesi[i].donanim[j].isim == yedekParcaBilgi[2])
                        {
                            for(int k=0; k<arabaListesi[i].donanim[j].yedekParca.Length; k++)
                            {
                                if(arabaListesi[i].donanim[j].yedekParca[k].parca == yedekParcaBilgi[3])
                                {
                                    arabaListesi[i].donanim[j].yedekParca[k].stok -= Convert.ToInt32(yedekParcaBilgi[4]);
                                }
                            }
                        }
                    }
                }
            }
            ArabadanDosyaya(dosyaYoluaraba,arabaListesi);
        }
        internal static bool StoktanFazla(string dosyaYoluaraba,string[] yedekParcaBilgi) //Musterinin istedigi miktar var olan stoktan fazla mi diye kontrol eder
        {
            Araba[] arabaListesi = DosyadanArabaya(dosyaYoluaraba);
            for(int i=0; i<arabaListesi.Length; i++)
            {
                if(arabaListesi[i].marka == yedekParcaBilgi[0])
                {
                    for(int j=0; j<2; j++)
                    {
                        if(arabaListesi[i].donanim[j].isim == yedekParcaBilgi[2])
                        {
                            for(int k=0; k<arabaListesi[i].donanim[j].yedekParca.Length; k++)
                            {
                                if(arabaListesi[i].donanim[j].yedekParca[k].parca == yedekParcaBilgi[3])
                                {
                                    if(arabaListesi[i].donanim[j].yedekParca[k].stok < Convert.ToInt32(yedekParcaBilgi[4]))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
         internal static void ArabaListe(string dosyaYoluaraba) //dosyadan okudugu bilgilerle araba listesi olusturur
        {
            Console.Write("\nCar List");
            Araba[] arabaListe = DosyadanArabaya(dosyaYoluaraba);
            for(int i=0; i<arabaListe.Length; i++)
            {
                arabaListe[i].arabaBilgi();
            }
        }
    }
}