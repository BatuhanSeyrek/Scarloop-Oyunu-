using UnityEngine;
using System.Collections;

public class BalikKontrol : MonoBehaviour
{
    public float zeminZiplamaGucu = 10f;
    public float suZiplamaGucu = 15f;
    public float sudaBeklemeSuresi = 2f;
    
    // YENİ: Balık suya değdikten sonra kaç saniye daha aşağı insin?
    public float batmaSuresi = 0.5f; 

    private Rigidbody2D rb;
    private float orjinalYercekimi;
    private bool sudaMi = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orjinalYercekimi = rb.gravityScale;
    }



    // 2. DURUM: SU (TRIGGER)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap") && !sudaMi)
        {
            StartCoroutine(SudaBekleVeFirla());
        }
    }

    IEnumerator SudaBekleVeFirla()
    {
        sudaMi = true;

        // --- DEĞİŞİKLİK BURADA ---
        // Hemen dondurmuyoruz. Belirlenen süre kadar (batmaSuresi) aşağı düşmesine izin veriyoruz.
        yield return new WaitForSeconds(batmaSuresi); 

        // Şimdi balığı dondur (Suyun içindeyken)
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0; 

        // Fırlatmadan önce bekle
        yield return new WaitForSeconds(sudaBeklemeSuresi);

        // Yerçekimini aç ve fırlat
        rb.gravityScale = orjinalYercekimi;
        rb.AddForce(Vector2.up * suZiplamaGucu, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f); 
        sudaMi = false;
    }
}