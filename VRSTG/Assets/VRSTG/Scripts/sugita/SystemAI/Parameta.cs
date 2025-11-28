using UnityEngine;
using StateMachineAI;

public class Parameta : MonoBehaviour
{
    public SystemAI m_SAAI;
    public bool m_Flag;
    public int m_Hp;
    public int m_HpMax;
    
    private void Start()
    {
        m_SAAI = GetComponent<SystemAI>();
        m_HpMax = m_Hp;
        m_Flag = false;
        LifeGaugeContainer.Instance.Add(this);
    }

    protected virtual void OnDie()
    {
        LifeGaugeContainer.Instance.Remove(this);
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
                m_SAAI.Hit();
            }

        }
        return Flag;
    }  
}