using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    // --- HAREKET AYARLARI ---
    public float hiz = 2f;      // Bulutun hızı
    public float mesafe = 4f;   // Ne kadar uzağa gidecek?
    
    private Vector3 baslangicYeri;

    void Start()
    {
        baslangicYeri = transform.position; // Bulutun ilk yerini kaydet
    }

    void Update()
    {
        // SAĞA SOLA GİTME (PingPong)
        // Matematiksel olarak pozisyonu sürekli değiştiriyoruz
        float yeniX = baslangicYeri.x + Mathf.PingPong(Time.time * hiz, mesafe);
        transform.position = new Vector3(yeniX, transform.position.y, transform.position.z);
    }

    // --- YAPIŞTIRMA SİSTEMİ (ÖNEMLİ) ---
    
    // Biri bulutun üstüne basarsa
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Eğer çarpan şey "Player" ise
        if (other.gameObject.CompareTag("Player"))
        {
            // Oyuncuyu bulutun "Çocuğu" yap (Böylece bulutla beraber hareket eder)
            other.transform.SetParent(transform);
        }
    }

    // Biri buluttan ayrılırsa (Zıplarsa)
    private void OnCollisionExit2D(Collision2D other)
    {
        // Eğer ayrılan şey "Player" ise
        if (other.gameObject.CompareTag("Player"))
        {
            // Akrabalığı bitir (Artık özgürsün)
            other.transform.SetParent(null);
        }
    }
}