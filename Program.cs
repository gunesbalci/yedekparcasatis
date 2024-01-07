//220229043_GüneşBalcı

using System;
using System.Collections;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Proje
{
    public enum GirisYontemi{giris,kayıt}
    public class Program
    {
        public static void Main()
        {   //Dosya yollarini belirler ve program ilk acildiginda dosyalari olusturur
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
            //programi calistiran fonksiyon
            Ekran.AnaMenu(kullaniciDosya,arabaDosya,sepetDosya);
        }
    }
}