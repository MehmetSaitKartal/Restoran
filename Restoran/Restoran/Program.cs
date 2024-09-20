using System.ComponentModel;

namespace Restoran
{
    internal class Program
    {
        static List<Masa> masalar = new List<Masa>();
        static List<Yemek> menu = new List<Yemek>();
        static void Main(string[] args)
        {

            for (int i = 0; i < 5; i++)
            {
                masalar.Add(new Masa());
            }

            menu.Add(new Yemek("Kebap", 100));
            menu.Add(new Yemek("Lahmacun", 50));
            menu.Add(new Yemek("Döner", 80));

            while (true)
            {
                Console.WriteLine("Menü");
                Console.WriteLine("1-Sipariş Al");
                Console.WriteLine("2-Hesap Al");
                Console.WriteLine("3-Menü Düzenle");
                Console.WriteLine("4-Çıkış");
                int secim = Convert.ToInt32(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        SiparisAl();
                        break;
                    case 2:
                        HesapAl();
                        break;
                    case 3:
                        MenuDuzenle();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Geçersiz Seçim, Lütfen Tekrar Deneyin.");
                        break;
                }
            }
        }

        static void SiparisAl()
        {
            Masa bosMasa = masalar.Find(masa => masa.BosMu);

            if (bosMasa == null)
            {
                Console.WriteLine("Tüm Masalar Dolu...");
                return;
            }

            Console.WriteLine("Kaç Kişisiniz?");
            int kisiSayisi = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < kisiSayisi; i++)
            {
                Console.WriteLine($"Müşteri {i + 1} için sipariş alınıyor.");
                bool devamEt = true;

                while (devamEt)
                {
                    YemekSecimi(bosMasa);
                    Console.WriteLine("Başka bir isteğiniz var mı? (Evet/Hayır)");
                    string cevap = Console.ReadLine().ToLower();
                    devamEt = cevap == "evet";
                }
            }

            bosMasa.BosMu = false;
            Console.WriteLine("Sipariş tamamlandı.");
        }

        static void YemekSecimi(Masa masa)
        {
            Console.WriteLine("Menü:");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {menu[i].Ad} - {menu[i].Fiyat} TL");
            }

            Console.WriteLine("Seçiminizi yapınız:");
            int yemekSecim = Convert.ToInt32(Console.ReadLine()) - 1;

            if (yemekSecim < 0 || yemekSecim >= menu.Count)
            {
                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                return;
            }

            Yemek secilenYemek = menu[yemekSecim];
            masa.Siparisler.Add(secilenYemek);
            Console.WriteLine($"{secilenYemek.Ad} sipariş edildi.");
        }

        static void HesapAl()
        {
            Console.WriteLine("Hangi masanın hesabını almak istersiniz? (1-5)");
            int masaNo = Convert.ToInt32(Console.ReadLine()) - 1;

            if (masaNo < 0 || masaNo >= masalar.Count || masalar[masaNo].BosMu)
            {
                Console.WriteLine("Geçersiz veya boş masa.");
                return;
            }

            Masa masa = masalar[masaNo];
            int toplamHesap = 0;

            foreach (var siparis in masa.Siparisler)
            {
                toplamHesap += siparis.Fiyat;
            }

            Console.WriteLine($"Hesap: {toplamHesap} TL");
            masa.Siparisler.Clear();
            masa.BosMu = true;
        }
        static void MenuDuzenle()
        {
            Console.WriteLine("Menüye yemek eklemek ister misiniz? (Evet/Hayır)");
            string cevap = Console.ReadLine().ToLower();

            if (cevap == "evet")
            {
                Console.WriteLine("Eklemek istediğiniz yemeğin adı:");
                string yemekAd = Console.ReadLine();

                Console.WriteLine("Eklemek istediğiniz yemeğin fiyatı:");
                int yemekFiyat = Convert.ToInt32(Console.ReadLine());

                menu.Add(new Yemek(yemekAd, yemekFiyat));
                Console.WriteLine("Yemek menüye eklendi.");
            }
            else if (cevap == "hayır")
            {
                Console.WriteLine("Menüden yemek çıkartmak ister misiniz? (Evet/Hayır)");
                string cevap_ = Console.ReadLine().ToLower();

                if (cevap_ == "evet")
                {
                    Console.WriteLine("Menüden çıkartmak istediğiniz yemeğin adını giriniz:");
                    string yemekAd = Console.ReadLine();

                    Yemek yemek = menu.Find(y => y.Ad == yemekAd);
                    if (yemek != null)
                    {
                        menu.Remove(yemek);
                        Console.WriteLine($"{yemekAd} menüden çıkartıldı.");
                    }
                    else
                    {
                        Console.WriteLine("Yemek bulunamadı.");
                    }
                }
            }
        }
    }

    class Masa
    {
        public bool BosMu { get; set; } = true;
        public List<Yemek> Siparisler { get; set; }

        public Masa()
        {
            Siparisler = new List<Yemek>();
        }
    }

    class Yemek
    {
        public string Ad { get; private set; }
        public int Fiyat { get; private set; }

        public Yemek(string ad, int fiyat)
        {
            Ad = ad;
            Fiyat = fiyat;
        }
    }
}

