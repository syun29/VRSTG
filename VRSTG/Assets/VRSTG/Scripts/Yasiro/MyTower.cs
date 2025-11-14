using UnityEngine;

public class MyTower : MonoBehaviour
{
    [SerializeField] public int m_hp;

    private void Update()
    {
        if(m_hp <= 0)
        {
            BreakTower();
        }
    }

    public void TakeDamage(int damage)
    {
        m_hp -= damage;
    }

    private void BreakTower()
    {
        Destroy(gameObject);
    }
}
