using UnityEngine;
using System.Collections; // Zamanlayıcılar için gerekli

public class MolePop : MonoBehaviour
{
    // --- AYARLAR ---
    public float yerUstundeKalmaSuresi = 2f; // Ne kadar süre görünecek?
    public float yerAltindaKalmaSuresi = 3f; // Ne kadar süre saklanacak?
    public float baslangicGecikmesi = 0f;    // Oyuna başlar başlamaz mı çıksın?

    private SpriteRenderer resim;
    private Collider2D kutu; // Çarpışma kutusu (Seni öldüren kısım)

    void Start()
    {
        resim = GetComponent<SpriteRenderer>();
        kutu = GetComponent<Collider2D>(); // BoxCollider veya PolygonCollider fark etmez, bulur.
        
        // Döngüyü başlat
        StartCoroutine(KostebekDongusu());
    }

    // Sonsuz döngü (Coroutine)
    IEnumerator KostebekDongusu()
    {
        // İstersen başta biraz bekletelim (Her köstebek aynı anda çıkmasın diye)
        yield return new WaitForSeconds(baslangicGecikmesi);

        while (true)
        {
            // --- GÖRÜNMEK (TEHLİKELİ AN) ---
            resim.enabled = true; // Resmi aç
            kutu.enabled = true;  // Tuzağı aç (Çarparsa öldürür)
            
            // Belirlenen süre kadar bekle
            yield return new WaitForSeconds(yerUstundeKalmaSuresi);

            // --- SAKLANMAK (GÜVENLİ AN) ---
            resim.enabled = false; // Resmi kapat
            kutu.enabled = false;  // Tuzağı kapat (Üstünden geçebilirsin)
            
            // Belirlenen süre kadar bekle
            yield return new WaitForSeconds(yerAltindaKalmaSuresi);
        }
    }
}