using StateMachineAI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab;      //生成する敵
    [SerializeField] private Transform[] m_SpawnPoints;     //上下左右の4つのスポーン
    [SerializeField] private float m_SpawnInterval = 2f;    //生成間隔（秒）
    [SerializeField] private Transform m_TargetTF;

    //[SerializeField] private int m_MaxEnemyCount = 100;     //敵の最大数

    private float m_Timer;
    //private int m_CurrentEnemyCount = 0;    //現在の敵の数

    public void Update()
    {


        m_Timer += Time.deltaTime;

        if (m_Timer >= m_SpawnInterval)
        {
            SpawnEnemy();
            m_Timer = 0;
        }
    }
    private void SpawnEnemy()
    {
        if (m_EnemyPrefab == null || m_SpawnPoints.Length == 0)
        {
            Debug.LogWarning("EnemyPrefabまたはSpawnPointsが設定されていません");
            return;
        }


        //0～m_SpawnPoints.Length-1のランダムなインデックスを取得
        int index = Random.Range(0, m_SpawnPoints.Length);
        //敵を生成
        Instantiate(m_EnemyPrefab, m_SpawnPoints[index].position, m_SpawnPoints[index].rotation);
    }
}
