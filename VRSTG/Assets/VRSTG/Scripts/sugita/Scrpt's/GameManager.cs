using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float m_Score = 0;
    public float m_Timer = 120f;//制限時間

    public Text m_ScoreText;    //スコア表示
    public Text m_TimerText;    //制限時間表示
    public Text m_ResultText;   //ゲーム終了後のテキスト

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //時間経過
        m_Timer -= Time.deltaTime;

        
        
    }
}
