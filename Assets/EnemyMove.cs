using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // --- HAREKET AYARLARI ---
    public float hiz = 2f;
    public float mesafe = 3f;

    // --- KANAT ÇIRPMA AYARLARI (YENİ) ---
    public float cirpmaHizi = 15f; // Kanat ne kadar hızlı kıpırdayacak?
    public float cirpmaGucu = 0.1f; // Ne kadar ezilip büzülecek? (Küçük sayı daha iyidir)
    
    private Vector3 baslangicYeri;
    private Vector3 baslangicBoyutu; // Kuşun orijinal boyutunu saklayacağız
    private SpriteRenderer kusResmi; 

    void Start()
    {
        baslangicYeri = transform.position;
        // Oyunun başında ayarladığın boyutu (örn: 0.3) hafızaya al
        baslangicBoyutu = transform.localScale; 
        kusResmi = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. SAĞA SOLA GİTME (Eski Kısım)
        float eskiX = transform.position.x;
        float yeniX = baslangicYeri.x + Mathf.PingPong(Time.time * hiz, mesafe);
        transform.position = new Vector3(yeniX, transform.position.y, transform.position.z);

        // 2. YÖN ÇEVİRME (Eski Kısım)
        // Eğer senin kuşun ters çalışıyorsa buradaki true/false yerlerini değiştirirsin
        if (yeniX > eskiX) kusResmi.flipX = true;     
        else if (yeniX < eskiX) kusResmi.flipX = false; 

        // 3. KANAT ÇIRPMA EFEKTİ (YENİ KISIM)
        // Sinüs dalgası (Mathf.Sin) sürekli inip çıkan bir sayıdır (-1 ile 1 arası)
        // Bunu kullanarak kuşun Y (Boy) eksenini sürekli değiştiriyoruz
        float yeniBoyY = baslangicBoyutu.y + (Mathf.Sin(Time.time * cirpmaHizi) * cirpmaGucu);
        
        // Yeni boyutu kuşa uygula (Sadece Y değişiyor, X ve Z aynı kalıyor)
        transform.localScale = new Vector3(baslangicBoyutu.x, yeniBoyY, baslangicBoyutu.z);
    }
}