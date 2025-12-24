using UnityEngine;
using UnityEngine.SceneManagement; // Sahne resetlemek için şart

public class PauseManager : MonoBehaviour
{
    [Header("Bağlantılar")]
    // Tasarladığımız o siyah perdeli komple menüyü buraya atacağız
    public GameObject pausePerdesi; 
    
    // Köşedeki küçük butonu da buraya atalım ki menü açılınca onu gizleyelim
    public GameObject anaDurdurButonu; 

    // --- KÖŞEDEKİ BUTONA TIKLANINCA ---
    public void MenuyuAc()
    {
        pausePerdesi.SetActive(true);     // Menüyü göster
        anaDurdurButonu.SetActive(false); // Köşedeki butonu gizle (kalabalık yapmasın)
        Time.timeScale = 0f;              // ZAMANI DONDUR
    }

    // --- "DEVAM ET" BUTONUNA TIKLANINCA ---
    public void DevamEt()
    {
        pausePerdesi.SetActive(false);    // Menüyü gizle
        anaDurdurButonu.SetActive(true);  // Köşedeki butonu geri getir
        Time.timeScale = 1f;              // ZAMANI BAŞLAT
    }

    // --- "YENİDEN BAŞLAT" BUTONUNA TIKLANINCA ---
    public void YenidenBaslat()
    {
        // ÇOK ÖNEMLİ: Sahne değişmeden önce zamanı normale döndür.
        // Yoksa yeni oyun donmuş başlar!
        Time.timeScale = 1f;

        // Aktif sahneyi baştan yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void IlkBolumdenBaslat()
    {
        // Zamanı normale döndür
        Time.timeScale = 1f;

        // Buradaki "1" sayısı, Build Settings'deki Level 1'in numarasıdır.
        // Eğer 1. bölümünün adı "SampleScene" ise tırnak içinde ismini de yazabilirsin:
        // SceneManager.LoadScene("SampleScene"); 
        SceneManager.LoadScene("SampleScene"); 
    }
}