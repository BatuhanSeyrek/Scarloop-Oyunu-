using UnityEngine;

public class KayanYazi : MonoBehaviour
{
    [Header("Yazı Ayarları")]
    public float hiz = 50f;
    public float ekraninTepesi = 800f; 

    [Header("Açılacaklar")]
    public GameObject baslatButonu; // Buton tek başına kalsın
    
    // İşte burası LİSTE (Köşeli parantez liste demektir)
    public GameObject[] digerNesneler; 

    [Header("Arka Plan Rengi")]
    public Color maviRenk = new Color(0.2f, 0.4f, 1f); 
    private Camera anaKamera;

    private RectTransform rectTransform;
    private float yaziBoyu; 

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        yaziBoyu = rectTransform.rect.height;
        anaKamera = Camera.main;

        // 1. Butonu Gizle
        if (baslatButonu != null) baslatButonu.SetActive(false);

        // 2. Listedeki TÜM nesneleri teker teker bul ve gizle
        foreach (GameObject nesne in digerNesneler)
        {
            if (nesne != null)
            {
                nesne.SetActive(false);
            }
        }
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * hiz * Time.deltaTime;

        if (rectTransform.anchoredPosition.y - (yaziBoyu / 2) > ekraninTepesi)
        {
            // Yazı bitti, operasyon başlasın!

            // 1. Butonu Aç
            if (baslatButonu != null) baslatButonu.SetActive(true);

            // 2. Listedeki TÜM nesneleri teker teker aç
            foreach (GameObject nesne in digerNesneler)
            {
                if (nesne != null)
                {
                    nesne.SetActive(true);
                }
            }

            // 3. Arka planı mavi yap
            if (anaKamera != null)
            {
                anaKamera.clearFlags = CameraClearFlags.SolidColor;
                anaKamera.backgroundColor = maviRenk;
            }

            gameObject.SetActive(false); // Yazıyı kapat
        }
    }
}