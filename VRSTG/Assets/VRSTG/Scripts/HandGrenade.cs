using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HandGrenage : MonoBehaviour
{
    [SerializeField] private float m_explodeTime = 5f;
    [SerializeField] private float m_forceUpRatio = 2f;
    [SerializeField] private float m_impactForce = 1000f;
    [SerializeField] private float m_pullPinForce = 100f;
    [SerializeField] private Rigidbody m_pinRb;
    [SerializeField] private GameObject m_effectPrefab;
    [SerializeField] private ColliderEvent m_searchColEvent;
    [SerializeField] private AudioSource m_selectSE;
    [SerializeField] private AudioSource m_pullPinSE;
    [SerializeField] private AudioSource m_explodeSE;

    private List<Rigidbody> m_inRangeRbs = new List<Rigidbody>();
    private bool m_isPulledPin;
    private XRGrabInteractable m_interactable;

    private void Awake()
    {
        m_searchColEvent.triggerEnter.AddListener(OnSearchTriggerEnter);
        m_searchColEvent.triggerExit.AddListener(OnSearchTriggerExit);

        m_interactable = GetComponent<XRGrabInteractable>();
        m_interactable.selectEntered.AddListener(OnSelectEntered);
        m_interactable.selectExited.AddListener(OnSelectExited);
        m_interactable.activated.AddListener(OnActivate);
    }

    private void OnSearchTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            m_inRangeRbs.Add(rb);
        }
    }

    private void OnSearchTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if(rb != null)
        {
            m_inRangeRbs.Remove(rb);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        m_selectSE.Stop();
        m_selectSE.Play();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {

    }

    private void OnActivate(ActivateEventArgs args)
    {
        if (m_isPulledPin) return;

        Invoke("Explode",m_explodeTime);

        m_isPulledPin = true;


        m_pinRb.transform.parent = null;
        m_pinRb.isKinematic = false;
        Vector3 dir = (transform.up + Vector3.up).normalized;
        m_pinRb.AddForce(dir * m_pullPinForce);

        Destroy(m_pinRb.gameObject, m_explodeTime);

        m_pullPinSE.Play();
    }

    private void Explode()
    {
        foreach(Rigidbody rb in m_inRangeRbs)
        {
            if (rb == null) continue;

            Vector3 vec = rb.position - transform.position;
            Vector3 dir = (vec.normalized + Vector3.up * m_forceUpRatio).normalized;
            float forceRaito = Mathf.InverseLerp(3f, 0f, vec.magnitude);
            float force = m_impactForce * forceRaito;
            rb.AddForceAtPosition(dir * force, transform.position);

        }
        Instantiate(m_effectPrefab, transform.position, Quaternion.identity);

        m_explodeSE.transform.parent = null;
        m_explodeSE.Play();

        Destroy(gameObject);
    }

    private void Update()
    {
        m_inRangeRbs.RemoveAll(_ => _ == null);

        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OnActivate(null);
        }
    }
}
