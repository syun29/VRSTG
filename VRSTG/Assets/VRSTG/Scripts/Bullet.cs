using UnityEngine;

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

    private void Update()
    {
        m_lastPos = transform.position;

        float moveSpeed = m_shotSpeed * Time.deltaTime;
        transform.Translate(0f, 0f, moveSpeed);

        Vector3 vec = transform.position - m_lastPos;
        Ray ray = new Ray(m_lastPos, vec.normalized);
        int layerMask = LayerMask.GetMask("Default");
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,vec.magnitude,layerMask))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                Vector3 dir = (transform.forward +
                    Vector3.up * m_forceUpRaito).normalized;

                rb.AddForceAtPosition(dir * m_impactForce, hit.point);
            }
            Instantiate
            (
                m_effectPrefab,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

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
