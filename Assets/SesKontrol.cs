using UnityEngine;

public class SesKontrol : MonoBehaviour
{
    [Header("Resim Objeleri")]
    public GameObject sesAcikResmi;   // Hiyerarşideki "Ses Açık" resmi
    public GameObject sesKapaliResmi; // Hiyerarşideki "Ses Kapalı" resmi

    private bool sesAcikMi = true;

    void Start()
    {
        // Oyun açılınca ses durumuna bak
        sesAcikMi = (AudioListener.volume == 1);
        GoruntuyuGuncelle();
    }

    public void SesiAcKapat()
    {
        // Durumu tersine çevir
        sesAcikMi = !sesAcikMi;

        if (sesAcikMi)
        {
            AudioListener.volume = 1; // Sesi Aç
        }
        else
        {
            AudioListener.volume = 0; // Sesi Kapat
        }

        GoruntuyuGuncelle();
    }

    void GoruntuyuGuncelle()
    {
        if (sesAcikMi)
        {
            // Ses AÇIKSA: Açık resmini göster, Kapalıyı gizle
            sesAcikResmi.SetActive(true);
            sesKapaliResmi.SetActive(false);
        }
        else
        {
            // Ses KAPALIYSA: Kapalı resmini göster, Açığı gizle
            sesAcikResmi.SetActive(false);
            sesKapaliResmi.SetActive(true);
        }
    }
}