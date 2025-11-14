using System.Collections;
using UnityEngine;

public class WeponSpawner : MonoBehaviour
{
    [SerializeField] private float m_handGrenadeSpawnInterval = 2f;
    [SerializeField] private Transform m_handGrenadeSpawnTF;
    [SerializeField] private HandGrenage m_handGrenadePrefab;

    private HandGrenage m_handGrenade;
    private Coroutine m_createHandGrenadeCor;

    private IEnumerator Co_CreateHandGrenade()
    {
        yield return new WaitForSeconds(m_handGrenadeSpawnInterval);

        m_handGrenade = Instantiate
       (
         m_handGrenadePrefab,
         m_handGrenadeSpawnTF.position,
         m_handGrenadeSpawnTF.rotation
       );
        m_createHandGrenadeCor = null; 
    }
    private void Update()
    {
        if(m_handGrenade==null&&m_createHandGrenadeCor==null)
        {
            m_createHandGrenadeCor = StartCoroutine(Co_CreateHandGrenade());
        }
    }
}
