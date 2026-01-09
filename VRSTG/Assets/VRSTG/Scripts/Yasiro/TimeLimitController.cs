using UnityEngine;
using UnityEngine.UI;

public class TimeLimitController : MonoBehaviour
{
    [SerializeField] private float timeLimit = 60f; // 制限時間（秒）
    [SerializeField] private Text timerText;        // 時間表示用のUI（TextMeshProでもOK）
    [SerializeField] private Transform UICanvas;
    [SerializeField] private Transform CameraTransform; // VRカメラ

    private float remainingTime;
    private bool isRunning = true;

    void Start()
    {
        remainingTime = timeLimit;
        transform.position = new Vector3(5, 4, 2);
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        // 時間表示を更新
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();
        }

        // 時間切れ処理
        if (remainingTime <= 0f)
        {
            isRunning = false;
            remainingTime = 0f;
            OnTimeUp();
        }

        UICanvas.transform.SetParent(CameraTransform);
        UICanvas.transform.localPosition = new Vector3(0f, 0f, 2f);
        UICanvas.transform.localRotation = Quaternion.identity;
        UICanvas.transform.localScale = Vector3.one * 0.01f;
    }

    void OnTimeUp()
    {
        // 時間切れ時の処理（例：ゲームオーバー画面を表示）
        Debug.Log("時間切れ！");
        
    }
}
