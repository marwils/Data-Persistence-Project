using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
