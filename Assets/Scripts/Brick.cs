using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;

    private int m_pointValue;

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (m_pointValue)
        {
            case 1:
                block.SetColor("_BaseColor", Color.green);
                break;
            case 2:
                block.SetColor("_BaseColor", Color.yellow);
                break;
            case 5:
                block.SetColor("_BaseColor", Color.blue);
                break;
            default:
                block.SetColor("_BaseColor", Color.red);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(m_pointValue);

        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.01f);
    }

    public int GetPointValue()
    {
        return m_pointValue;
    }

    public void SetPointValue(int pointValue)
    {
        m_pointValue = pointValue;
    }
}
