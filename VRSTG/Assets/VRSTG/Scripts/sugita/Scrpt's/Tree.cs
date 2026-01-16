using StateMachineAI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tree : MonoBehaviour
{
    private List<SystemAI> m_enmies = new List<SystemAI>();
    private Parameta m_param;
    private float m_elapsedTime;

    private void Awake()
    {
        m_param = GetComponent<Parameta>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SystemAI ai = other.GetComponent<SystemAI>();
        if (ai != null)
        {
            m_enmies.Add(ai);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SystemAI ai = other.GetComponent<SystemAI>();
        if (ai != null)
        {
            m_enmies.Remove(ai);
        }
    }

    private void Update()
    {
        m_enmies.RemoveAll(_ => _ == null);

        if(m_enmies.Count > 0)
        {
            float damageTime = 1f;
            if(m_elapsedTime >= damageTime) 
            {
                m_elapsedTime -= damageTime;
                m_param.TakeDamage(m_enmies.Count);
                if(m_param.m_Hp <= 0)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
            m_elapsedTime += Time.deltaTime;

        }
    }
}
