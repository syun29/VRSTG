using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float m_lifeTime = 30f;

    private void Awake()
    {
        Invoke("Death", m_lifeTime);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
