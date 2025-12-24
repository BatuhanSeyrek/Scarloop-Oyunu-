using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform top;  // Takip edilecek hedef (Bizim topumuz)
    public Vector3 mesafe = new Vector3(0, 0, -10); // Kamera ile top arasındaki mesafe

    void LateUpdate()
    {
        // Eğer top yok olmamışsa (düşüp ölmediysek)
        if (top != null)
        {
            // Kameranın pozisyonunu topun olduğu yere eşitle (mesafeyi koruyarak)
            transform.position = top.position + mesafe;
        }
    }
}