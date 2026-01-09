using UnityEngine;
using UnityEngine.UI;

public class ShowUIWhenLookedAt : MonoBehaviour
{
   [SerializeField] private Transform CameraTransform; // VRカメラ
   [SerializeField] private Transform Target;          // 対象オブジェクト
   [SerializeField] private Canvas UICanvas;           // 表示するUI
   [SerializeField] private float AngleThreshold = 60f; // 表示する視野角

   [SerializeField] private Vector2 hpBarSize = new Vector2(200f, 20f); 
   [SerializeField] private Vector3 hpBarOffset = new Vector3(0f, 2f, 0f);

    private Parameta Object;
    private bool isInit;

    private void Start()
    {
        Object = Target.GetComponent<Parameta>();


        //UICanvas.transform.position = Target.position + Vector3.up * 5f;
        UICanvas.transform.localScale = Vector3.one * 0.01f;
    }

    void Update()
    {
        if (!isInit)
        {
            if (Object != null && Object.lifeGauge != null)
            {
                // HPバーのサイズを設定
                Object.lifeGauge.Setup(hpBarSize, hpBarOffset);
                isInit = true;
            }
        }
        //Vector3 toTarget = (Target.position - CameraTransform.position).normalized;
        //float angle = Vector3.Angle(CameraTransform.forward, toTarget);


        //// UIの位置と向き
        //UICanvas.transform.position = Target.position + hpBarOffset;
        //UICanvas.transform.LookAt(CameraTransform);
        //UICanvas.transform.Rotate(0, 180f, 0);


        //UICanvas.enabled = angle < AngleThreshold;
    }
}

