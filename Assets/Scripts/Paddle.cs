using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 2.0f;

    [SerializeField]
    private float m_maxMovement = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * m_speed * Time.deltaTime;

        if (pos.x > m_maxMovement)
            pos.x = m_maxMovement;
        else if (pos.x < -m_maxMovement)
            pos.x = -m_maxMovement;

        transform.position = pos;
    }
}
