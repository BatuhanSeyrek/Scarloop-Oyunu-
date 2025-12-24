using UnityEngine;

public class CrabMove : MonoBehaviour
{
    // --- AYARLAR ---
    public float hiz = 3f;
    public float yurumeSuresi = 2f;
    public float beklemeSuresi = 2f;

    // --- ANİMASYON AYARLARI ---
    public float yurumeSallanmaHizi = 15f; 
    public float yurumeSallanmaGucu = 10f; 
    
    // Gözetleme (Sağa sola bakma) hızı
    public float etrafaBakmaHizi = 0.5f; // Her yarım saniyede bir yön değiştirecek

    private Rigidbody2D rb;
    private SpriteRenderer resim;
    
    private float anaZamanlayici;
    private float bakmaZamanlayici; // Bakma süresini sayacak
    private bool yuruyorMu = true;
    private int yon = 1;
    private Vector3 orjinalBoyut;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        resim = GetComponent<SpriteRenderer>();
        orjinalBoyut = transform.localScale;
        anaZamanlayici = yurumeSuresi;
    }

    void Update()
    {
        anaZamanlayici -= Time.deltaTime;

        if (anaZamanlayici <= 0)
        {
            DurumDegistir();
        }

        if (yuruyorMu)
        {
            // --- YÜRÜME MODU ---
            // Sallanarak yürüme (Bacak hareketi efekti)
            rb.velocity = new Vector2(hiz * yon, rb.velocity.y);
            
            if (yon == 1) resim.flipX = true;
            else resim.flipX = false;

            // Yürürken sallanma
            float sallanti = Mathf.Sin(Time.time * yurumeSallanmaHizi) * yurumeSallanmaGucu;
            transform.rotation = Quaternion.Euler(0, 0, sallanti);
        }
        else
        {
            // --- BEKLEME VE BAKINMA MODU ---
            rb.velocity = new Vector2(0, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0); // Düz dur

            // Gözetleme Sayacı
            bakmaZamanlayici -= Time.deltaTime;
            
            // Süre dolunca kafayı diğer yana çevir
            if (bakmaZamanlayici <= 0)
            {
                resim.flipX = !resim.flipX; // Yönü tam tersine çevir
                bakmaZamanlayici = etrafaBakmaHizi; // Sayacı sıfırla
            }
        }
    }

    void DurumDegistir()
    {
        if (yuruyorMu)
        {
            // Yürümeyi bitir -> Beklemeye başla
            yuruyorMu = false;
            anaZamanlayici = beklemeSuresi;
            bakmaZamanlayici = etrafaBakmaHizi; // Bakmaya hemen başla
        }
        else
        {
            // Beklemeyi bitir -> Yürümeye başla
            yuruyorMu = true;
            anaZamanlayici = yurumeSuresi;
            yon = yon * -1; // Yönü değiştirip geri dön
        }
    }
}