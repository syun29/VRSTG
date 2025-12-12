using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Gun : MonoBehaviour
{
    [SerializeField] private int m_bulletCnt = 10;
    [SerializeField] private float m_reloadTime = 2f;
    [SerializeField] private float m_shotSpeed = 100f;
    [SerializeField] private float m__flyingDist = 1000f;
    [SerializeField] private Transform m_muzzleTF;
    [SerializeField] private Bullet m_bulletPrefab;
    [SerializeField] private GameObject m_effectPrefab;
    [SerializeField] private Transform m_effectTF;
    [SerializeField] private Text m_bulletCntText;
    [SerializeField] private Slider m_reloadGauge;
    [SerializeField] private AudioSource m_selectSE;
    [SerializeField] private AudioSource m_shotSE;
    [SerializeField] private AudioSource m_emptySE;

    private int m_remainCnt;
    private Coroutine m_reloadCor;

    private XRGrabInteractable m_interactable;
    private PlayerInput m_input;
    private InputAction m_reloadActinon;

    private void Awake()
    {
        m_interactable = GetComponent<XRGrabInteractable>();
        m_interactable.selectEntered.AddListener(OnSelectEntered);
        m_interactable.selectExited.AddListener(OnSelectExited);
        m_interactable.activated.AddListener(OnActivate);

        m_input = GetComponent<PlayerInput>();

        m_remainCnt = m_bulletCnt;

        m_reloadGauge.gameObject.SetActive(false);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        bool left = m_interactable.IsSelectedByLeft();
        m_input.SwitchCurrentActionMap(left ? "GlabL" : "GrabR");

        m_reloadActinon = m_input.currentActionMap.FindAction("Reload");

        m_selectSE.Stop();
        m_selectSE.Play();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {

    }

    private void OnActivate(ActivateEventArgs args)
    {
        if (m_reloadCor != null) return;

        if (m_remainCnt > 0)
        {
            Instantiate
        (
            m_effectPrefab,
            m_effectTF.position,
            m_effectTF.rotation
         );

            Bullet bullet = Instantiate
            (
                m_bulletPrefab,
                m_muzzleTF.position,
                m_muzzleTF.rotation
             );
            bullet.Shot(m_shotSpeed, m__flyingDist);

            m_shotSE.Stop();
            m_shotSE.Play();

            m_remainCnt--;
        }

        else
        {
            m_emptySE.Stop();
            m_emptySE.Play();
        }
    }

    private IEnumerator Co_Reload()
    {
        m_reloadGauge.gameObject.SetActive(true);

        float elapsed = 0f;
        float time = m_reloadTime;
        while (elapsed < time)
        {
            float alpha = elapsed / time;

            m_reloadGauge.value = alpha;
            yield return null;

            elapsed += Time.deltaTime;
        }

        m_reloadGauge.gameObject.SetActive(false);

        m_remainCnt = m_bulletCnt;

        m_reloadCor = null;
    }


    private void Update()
    {
        if (m_interactable.isSelected)
        {
            if (m_reloadCor == null && m_reloadActinon.WasPressedThisFrame())
            {
                if (m_remainCnt < m_bulletCnt)
                {
                    m_reloadCor = StartCoroutine(Co_Reload());
                }
            }
        }

        m_bulletCntText.text = m_remainCnt.ToString("00");

        DebugCanVas.Print("e‚ÌÀ•W ; {0}", transform.position);
    }
}
