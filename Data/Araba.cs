//220229043_GüneşBalcı

using System;

namespace Proje
{
    public class Araba //araba sinifi
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
        internal void arabaEkle(string dosyaYolu,int yedekParcaSize) //dosyaya araba turundeki degeri ekler
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
        internal void arabaBilgi()  //arabanin bilgilerini yazdirir
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
    public class YedekParca //arabanin donanimina ait yedek parca sinifi
    {
        internal string parca;
        internal int stok;
        internal YedekParca(string parca,int stok)
        {
            this.parca = parca;
            this.stok = stok;
        }
    }
    public class Donanim  //arabaya ait donanim sinifi
    {
        internal string isim;
        internal YedekParca[] yedekParca;
        internal Donanim(string isim,YedekParca[] yedekParca)
        {
            this.isim = isim;
            this.yedekParca = yedekParca;
        }
    }
}