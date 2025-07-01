using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField]
    private GameManager m_manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        m_manager.GameOver();
    }
}
