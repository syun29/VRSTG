using UnityEngine;
using StateMachineAI;

public class Parameta : MonoBehaviour
{
    public SystemAI m_SystemAI;
    public bool m_Flag;
    public int m_Hp;
    
    private void Start()
    {
        m_SystemAI = GetComponent<SystemAI>();
        m_Flag = false;
    }
    
    /*
    public bool TakeDamage(int Damage)
    {
        bool Flag = false;
        if (m_Hp > 0)
        {

        }
    }
    */
    
}
