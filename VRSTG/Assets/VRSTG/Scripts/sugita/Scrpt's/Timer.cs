using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("ClearTimer")]
    
    [SerializeField] float m_Time = 60f;    //ゲームクリアまでに耐える時間
    float m_RemainingTime;                  //残り時間を管理する内部カウンター

    [Header("スコア")]
    [SerializeField] int m_Score;           //現在のスコア
    [SerializeField] TextMesh m_TimeText;   //残り時間の表示用UI
    [SerializeField] TextMesh m_ScortText;  //スコア表示のUI


    bool m_Clear;   //ゲームクリア済みかどうかのフラグ

    private void Start()
    {
        //残り時間を初期化（制限時間からスタート）
        m_RemainingTime = m_Time;

        // スコア初期化
        m_Score = 0;

        UpdateUI();
    }
    private void Update()
    {

        if (m_Clear) return;

        m_RemainingTime -= Time.deltaTime;

        //生存スコア
        //1秒あたり10点ずつ増える
        m_Score += Mathf.FloorToInt(Time.deltaTime * 10);

        UpdateUI();

        //残り時間が0以下になったらゲームクリア
        if(m_RemainingTime <= 0f)
        {
            GameClear();
        }
    }

    private void UpdateUI()
    {
        //残り時間は切り上げて表示(0秒表示を防ぐ)
        m_TimeText.text = Mathf.CeilToInt(m_RemainingTime).ToString();

        //現在のスコアを表示
        m_ScortText.text = m_Score.ToString();
    }

    private void GameClear()
    {
        //クリアフラグを立ててUpdate処理を止める
        m_Clear = true;

        //デバッグログにスコアを表示
        Debug.Log("GAME CLEAR : Score =" + m_Score);
    }
}
