using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuYoneticisi : MonoBehaviour
{
   public void OyunuBaslat() // BAŞINA public EKLE!
{
    SceneManager.LoadScene("SampleScene");
}
}