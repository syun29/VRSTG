using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_forceUpRaito = 0.25f;
    [SerializeField] private float m_impactForce = 500f;
    [SerializeField] private GameObject m_effectPrefab;
    private float m_shotSpeed;
    private float m_flyingDist;
    private float m_currDist;
    private Vector3 m_lastPos;

    public void Shot (float speed,float dist)
    {
        m_shotSpeed = speed;
        m_flyingDist = dist;
    }

    private IEnumerator DelayedDestroy(GameObject target, float delay)
    {
        // 敵の色を変えて「削除予定」を可視化
        Renderer rend = target.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.gray;
        }

        yield return new WaitForSeconds(delay);

        Destroy(target);
    }




    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
    //        if (agent != null) agent.enabled = false;

    //        Rigidbody rb = other.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.isKinematic = false;
    //            rb.useGravity = true;
    //            Vector3 forceDir = transform.forward + Vector3.up * 0.5f;
    //            rb.AddForce(forceDir.normalized * 500f, ForceMode.Impulse);
    //        }

    //        // エフェクト再生（必要ならここで）
    //        // GameObject effect = Instantiate(m_effectPrefab, other.transform.position, Quaternion.identity);
    //        // Destroy(effect, 2f);

    //        // 敵を非アクティブ化してから遅延削除
    //        other.gameObject.SetActive(false);
    //        StartCoroutine(DelayedDestroy(other.gameObject, 2f));

    //        Destroy(gameObject);
    //    }

    //}


    void Update()
    {
        m_lastPos = transform.position;


        float moveSpeed = m_shotSpeed * Time.deltaTime;
        transform.Translate(0f, 0f, moveSpeed);

        Vector3 vec = transform.position - m_lastPos;
        Ray ray = new Ray(m_lastPos, vec.normalized);
        int layerMask = LayerMask.GetMask("Enemy");
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, vec.magnitude, layerMask))
        {
            GameObject rootObj = hit.collider.transform.root.gameObject;

            Rigidbody rb = rootObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                Vector3 forceDir = Vector3.up * 2.0f + transform.forward * 0.2f;
                rb.AddForce(forceDir.normalized * 300f, ForceMode.Impulse);
            }

            Instantiate(m_effectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            StartCoroutine(DelayedDestroy(rootObj, 3f));

            Parameta param = hit.collider.GetComponentInParent<Parameta>();
            if (param != null)
            {
                param.TakeDamage(10);
            }

            Destroy(gameObject);
            return;
        }

     

        m_currDist += moveSpeed;
        if (m_currDist >= m_flyingDist)
        {
            Destroy(gameObject);
        }
    }

}
