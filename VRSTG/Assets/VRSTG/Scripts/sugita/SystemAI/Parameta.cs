using UnityEngine;
using StateMachineAI;

public class Parameta : MonoBehaviour
{
    public SystemAI m_SAI;
    public bool m_Flag;
    public int m_Hp;
    
    private void Start()
    {
        m_SAI = GetComponent<SystemAI>();
        m_Flag = false;
    }


    public bool TakeDamage(int Damage)
    {
        bool Flag = false;
        if (m_Hp > 0)
        {

        }
        else
        {
            m_Hp -= Damage;
            if (m_Hp <= 0)
            {
                //Ž€–S
               // m_SAI.Death():
                //Ž€–S‚µ‚½Ž–‚ðUŒ‚ŽÒ‚É’Ê’m
                Flag = true;
            }
            else
            {
              //  m_SAI.Hit();
            }

        }
        return Flag;
    }  
}