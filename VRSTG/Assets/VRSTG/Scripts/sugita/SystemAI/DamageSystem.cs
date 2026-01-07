using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public Parameta m_Parameta;
    public int DMG = 1;

    public void Update()
    {
         if (m_Parameta.m_Flag)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent <BoxCollider>().enabled = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Parameta>())
        {
            Parameta P = other.GetComponent<Parameta>();
            if (P != m_Parameta)
            {
                //ダメージを与える
                if (P.TakeDamage(DMG))
                {
                    //死亡している場合は、ターゲットから除外
                    m_Parameta.m_SAAI.m_Target = null;
                }
            }
        }
    }
}
