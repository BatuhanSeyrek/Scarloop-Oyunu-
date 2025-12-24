using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // --- AYARLAR ---
    public float hiz = 5f;
    public float ziplamaGucu = 10f;
    
    // --- SKOR VE UI ---
    public int puan = 0;
    public TextMeshProUGUI puanYazisi;     // Sol üstteki skor
    public GameObject kazanmaMesaji;       // Ortada çıkacak "Tebrikler" yazısı

    private Rigidbody2D rb;
    private bool yerdeMi = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GuncelleSkorYazisi();
        
        // Oyun başlarken kazanma yazısı açıksa garanti olsun diye kapatalım
        if(kazanmaMesaji != null)
        {
            kazanmaMesaji.SetActive(false);
        }
    }

    void Update()
    {
        // Hareket
        float hareket = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hareket * hiz, rb.velocity.y);

        // Zıplama
        if (Input.GetKeyDown(KeyCode.UpArrow) && yerdeMi == true)
        {
            rb.AddForce(Vector2.up * ziplamaGucu, ForceMode2D.Impulse);
            yerdeMi = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TUZAK
        if (other.gameObject.CompareTag("Trap"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        // YILDIZ
        if (other.gameObject.CompareTag("Star"))
        {
            puan++;
            GuncelleSkorYazisi();
            Destroy(other.gameObject);

            // --- 5 YILDIZ KONTROLÜ ---
            if (puan >= 5)
            {
                LevelBittiEfekti(); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) yerdeMi = true;
        
        if (other.gameObject.CompareTag("Trap"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // --- ÖZEL FONKSİYONLAR ---

    void LevelBittiEfekti()
    {
        // 1. Yazıyı görünür yap
        if (kazanmaMesaji != null)
        {
            kazanmaMesaji.SetActive(true);
        }

        // 2. Oyuncuyu durdur ve kontrolleri kapat
        rb.velocity = Vector2.zero;
        this.enabled = false; 

        // 3. 2 saniye sonra "DigerSahneyeGec" komutunu çalıştır
        Invoke("DigerSahneyeGec", 2f);
    }

    void DigerSahneyeGec()
    {
        // Şu anki sahne numarasını al
        int aktifSahneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // Bir sonraki sahne numarasını hesapla
        int sonrakiSahneIndex = aktifSahneIndex + 1;

        // KONTROL: Eğer sıradaki sahne Build Settings listesinde varsa oraya git
        if (sonrakiSahneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sonrakiSahneIndex);
        }
        // YOKSA (Oyun bitti demektir), en başa (0. sahneye) dön
        else
        {
            Debug.Log("Oyun Bitti! Başa dönülüyor...");
            SceneManager.LoadScene(0); 
        }
    }

    void GuncelleSkorYazisi()
    {
        if (puanYazisi != null) puanYazisi.text = "Skor: " + puan;
    }
}