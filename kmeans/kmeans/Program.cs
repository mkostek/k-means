/*
 * SharpDevelop tarafından düzenlendi.
 * Kullanıcı: Asus
 * Tarih: 2.05.2019
 * Zaman: 14:12
 * 
 * Bu şablonu değiştirmek için Araçlar | Seçenekler | Kodlama | Standart Başlıkları Düzenle 'yi kullanın.
 */
using System;

namespace kmeans
{
	class Program
	{
		public static int[] deger=new int[kume];
		public static Random rastgele=new Random();//rastgele değerler için oluşturulan bir başlangıç
		public static int adet;//kaç adet veri olacağı
		public static int boyut;//kaç boyutlu bir data olacağı
		public static double[,] a;//veri matrisimiz
		public static int kume;//kümeleme adetimiz
		public static double[,] clstr;//kümelerin merkez bilgileri temporal
		public static double[,] minclstr;//kümelerin en optimum merkezi
		public static double[,] uzaklık;//her bir elemanın merkeze olan uzaklığı bulunacak
		public static double enkucuk=32769;//başlangıç için bir mesafe tanımladım
		public static int döngü=0;
		public static double sum=0;//mutlak uzaklık skoru
		public static double[] temp=new double[boyut];
		public static int h=0;
		public static int mini(double[,] a,int j){/// <summary>
			/// a matrisinin içinde j.elemanlardan kaçıncı inidisindekinin en yakın olduğu
			/// 
			/// </summary>
			/// <param name="a"></param>
			/// <param name="j"></param>
			/// <returns></returns>
			int x=0;
			double min=a[j,0];
			for(int i=1;i<kume;i++){
				if(min>a[j,i])
					x=i;
			}
			return x;
		}
		public static double min(double[,] a,int j){
			/// <summary>
			/// 
			/// </summary>
			/// <param name="kume">a[j] satırındaki hangi elemanın en yakın uzaklığını verir </param>
			/// <param name="tane"></param>
			/// <returns></returns>
			double min=a[j,0];
			for(int i=1;i<kume;i++){
				if(min>a[j,i])
					min=a[j,i];
			}
			return min;
		}
		public static void rastgeleVer(int kume,int tane,bool flag){/// <summary>
			/// rastgele olarak kaç tane eleman varsa o aralıkta biribirinden farklı küme sayısı kadar numara çıktılar
			/// </summary>
			/// <param name="args"></param>
			deger=new int[kume];
			deger[0]=rastgele.Next(tane);
			for(int i=1;i<kume;i++){
				deger[i]=rastgele.Next(tane);
				for(int j=0;j<i;j++){
					if(deger[j]==deger[i])
					{
						i--;
					}
				}
			}
			if(flag)
				for(int tr=0;tr<kume;tr++)//üretilen rastgele sayılar küme adeti kadar seçiliyor ve clstr dizinden hesaplayaacağız
			{
				for(int j=0;j<boyut;j++){//rastsal olarak kümemerkezleri oluşturuldu
					minclstr[tr,j]=clstr[tr,j]=a[deger[tr],j];//ve bunu en küçük olarak tanımladık
				}
			}
			else
				for(int tr=0;tr<kume;tr++)//üretilen rastgele sayılar küme adeti kadar seçiliyor ve clstr dizinden hesaplayaacağız
			{
				for(int j=0;j<boyut;j++){//rastsal olarak kümemerkezleri oluşturuldu
					clstr[tr,j]=a[deger[tr],j];//ve bunu en küçük olarak tanımladık
				}
			}
		}
		public static void Main(string[] args)
		{
			
			Console.WriteLine("Kaç tane veri gireceksiniz");//veri kümesindeki datalar için
			object t=Console.ReadLine();
			adet=Convert.ToInt32(t);
			Console.WriteLine("Kaç tane boyutu var");//girilen veri kaç boyutlu olacak
			object r=Console.ReadLine();
			boyut=Convert.ToInt32(r);
			a=new double[adet,boyut+1];//verilerin alınacağı matris yada dizi tipi değişken tanımlandı
			Console.WriteLine("Verilerinizi giriniz");
			// TODO: Implement Functionality Here
			for(int i=0;i<adet;i++){
				for(int j=0;j<boyut;j++){
					a[i,j]=Convert.ToDouble(Console.ReadLine());//ve bu dizi tipi değişkene girişimiz gerçekleşecek
				}
			}
			Console.WriteLine("Kaç tane kümeniz olacak?");//kaç tane küme istiyoruz
			object y=Console.ReadLine();
			kume=Convert.ToInt32(y);
			Console.WriteLine("Kaç döngü olacak?");//kaç döngü istiyoruz
			y=Console.ReadLine();
			döngü=Convert.ToInt32(y);
			//	sınıflar=new int[adet,boyut+1];//sınıflar a hangi datalar mensup olacağının bilgisi tutulacak
			uzaklık=new double[adet,kume];//kümenin merkezlerine uzaklıkların tutulacağı
			clstr=new double[kume,boyut];//demetleminin merkezi noktaları
			minclstr=new double[kume,boyut];//en kucuk demetleminin merkezi noktaları
			//kümeleme değerlerini yazdırdık
			
			//----------------------------------------------------
			int[] d=new int[kume];
			rastgeleVer(kume,adet,true);//rastgele olarak küme sayısı kadar merkez datalardanseçilece
			minClusterYazdır();//önce minimum demet merkezlerimizi yazdır
			mutlakUzaklıkVer(true);//her bir noktanın 
			ortalamaMerkezBul();//ortasını bul
			mutlakUzaklıkVer(true);//bu ortaya ait noktaların kümelerini güncelle
			enkucuk=sum;//en kucuk olarak ilkle
			
			//------------------------------------
			for(int i=0;i<döngü;i++)//döngü sayısı kadar 
			{
				Console.WriteLine("-------------------------");
				sum=0;//sum * la
				rastgeleVer(kume,adet,false);//rastgele olarak küme sayısı kadar merkez datalardanseçilecek
				ClusterYazdır();//yazdır
				minClusterYazdır();//min olanı yazdır
				mutlakUzaklıkVer(false);//rastgele iki küme merkezinin toplam uzaklıklarını hesapladı sum a atadı bu fonksiyon				
				ortalamaMerkezBul();//güncel demet merkezleri yapıldı
		//		mutlakUzaklıkVer(false);//rastgele iki küme merkezinin toplam uzaklıklarını hesapladı sum a atadı bu fonksiyon
				if(sum<enkucuk){
					enkucuk=sum;//en küçüğe sum  atandı
					mutlakUzaklıkVer(true);//mutlak uzaklık her bir hücrenin uzaklık,ait olduğu küme bilgisi ve kümelere olan toplan uzunluğu
					ortalamaMerkezBul();//güncel demet merkezleri yapıldı
					mutlakUzaklıkVer(true);//bu güncel demet merkezlerine göre yeniden hesap yapıldı
					for(int tr=0;tr<kume;tr++)//üretilen rastgele sayılar küme adeti kadar seçiliyor ve clstr dizinden hesaplayaacağız
					{
						for(int j=0;j<boyut;j++){//bu son bilgilere göre clstrlar güncellendi
							minclstr[tr,j]=clstr[tr,j];
						}
					}
				}
				
				for(int c=0;c<adet;c++){//her bir elemanı mensubu olduğu sınıfı ile birlikte yazıldı
					for(int n=0;n<boyut+1;n++){
						Console.Write(a[c,n]+",");
					}
					Console.WriteLine("");
				}
				Console.WriteLine("skor:"+enkucuk);//en kucuk skoruda yazdık
				Console.WriteLine("-------------------------");
			}
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		static void minClusterYazdır()//min olanclusteri yazdıran metod
		{
			Console.Write("minimumlar\n");
			for(int rt=0;rt<kume;rt++){
				for(int j=0;j<boyut;j++){
					Console.Write(minclstr[rt,j]+" ");//rastsal olarak oluşturulan küme merkezlerini yazdırdım
				}
				Console.WriteLine("");
			}
		}
		static void ClusterYazdır()//rastgele clusteri yazdıran metod
		{
			Console.Write("temp\n");
			for(int rt=0;rt<kume;rt++){
				for(int j=0;j<boyut;j++){
					Console.Write(clstr[rt,j]+" ");//rastsal olarak oluşturulan küme merkezlerini yazdırdım
				}
				Console.WriteLine("");
			}
		}
		static void ortalamaMerkezBul()/// <summary>
		/// Bu method ise ait olduğu kümenin elemanlarının aritmetik aritmetik ortası olarak güncelleyen fonksiyondur
		/// </summary>
		/// <param name="flag"></param>
		{
			temp=new double[boyut];//son olarakta geçici değişkeni
			for(int o=0;o<kume;o++)
			{
				for(int i=0;i<kume;i++)//hwe küme için
				{
					for(int j=0;j<adet;j++)//tüm verilerde
					{
						if(i==a[j,boyut])//bünyesinde bulunduğu kümede
						{
							for(int m=0;m<boyut;m++)//tüm boyutlar için
							{
								temp[m]+=a[j,m];//geçici değişkenin indislerine topla
							}
							h++;//sayacı bir artır
						}
					}
					for(int p=0;p<boyut;p++)//yine tüm boyutlar için
					{
						if(h==0)//h sıfırsa
							h=1;//birle
						minclstr[i,p]=temp[p]/h;//minclstr i. kümede p. boyutuna geçici değişkenin p. indisindeki değerini toplam sayısına böl
					}
					h=0;//h yi sıfırla
					temp=new double[boyut];//son olarakta geçici değişkeni
					
				}
			}
		}
		
		static void mutlakUzaklıkVer(bool flag){
			////Her bir data daki noktaların küme merkezlerine olan toplam uzaklıkları(flag false ise)
			////Her bir data daki noktaların kümemerkezlerine olan toplam uzaklıkları ve en yakın 
			/// olanın hangi kümeye ait olduğu a nın boyutuncu indeksinde tutulur(flag true ise)
			for(int j=0;j<adet;j++){
				for(int u=0;u<kume;u++){
					for(int k=0;k<boyut;k++){
						uzaklık[j,u]+=Math.Pow(a[j,k]-clstr[u,k],2);
					}
					uzaklık[j,u]=Math.Sqrt(uzaklık[j,u]);
				}
				sum+=min(uzaklık,j);
				if(flag)
					a[j,boyut]=(double)(mini(uzaklık,j));
				
			}
		}
	}
}