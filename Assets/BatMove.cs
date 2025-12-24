using UnityEngine;

public class BatMove : MonoBehaviour
{
    public float hiz = 1.5f;      
    public float mesafe = 4f;     
    public float egimAcisi = 25f; 

    public float cirpmaHizi = 20f;
    public float cirpmaGucu = 0.15f;
    
    private Vector3 baslangicYeri;
    private Vector3 baslangicBoyutu;
    private SpriteRenderer yarasaResmi; 

    void Start()
    {
        baslangicYeri = transform.position;
        baslangicBoyutu = transform.localScale; 
        yarasaResmi = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. HAREKET
        float sinyal = Mathf.Sin(Time.time * hiz);
        float hedefX = baslangicYeri.x + (sinyal * mesafe);
        float hareketFarki = hedefX - transform.position.x;

        transform.position = new Vector3(hedefX, transform.position.y, 0f);

        // 2. YÖN DÜZELTME (BURAYI DEĞİŞTİRDİK)
        if (hareketFarki > 0.001f) 
        {
            // SAĞA GİDİYOR
            yarasaResmi.flipX = false; // (Önceki true idi, false yaptık)
            transform.rotation = Quaternion.Euler(0, 0, -egimAcisi);
        }
        else if (hareketFarki < -0.001f) 
        {
            // SOLA GİDİYOR
            yarasaResmi.flipX = true; // (Önceki false idi, true yaptık)
            transform.rotation = Quaternion.Euler(0, 0, egimAcisi);
        }

        // 3. KANAT ÇIRPMA
        float yeniBoyY = baslangicBoyutu.y + (Mathf.Sin(Time.time * cirpmaHizi) * cirpmaGucu);
        transform.localScale = new Vector3(baslangicBoyutu.x, yeniBoyY, baslangicBoyutu.z);
    }
}