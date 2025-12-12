using UnityEngine;
using UnityEngine.UIElements;

namespace StateMachineAI
{
    /// <summary>
    /// SA_Patorol(徘徊モード)
    /// </summary>
    public class SA_Patrol : State<SystemAI>
    {
        public Vector3 m_PatrolPoint;
        //コンストラクタ
        public SA_Patrol(SystemAI owner) : base(owner) { }
        //このAIが起動した瞬間に実行(Startと同義)
        public override void Enter()
        {
            //プレイヤーがいない場合
            if (!owner.m_Target)
                owner.SetTaget();

            //ナビゲーションを起動
            owner.m_NavMeshAgent.enabled = true;

            //AnimatorはStateを徘徊もーどへブレンド
            owner.AnimatorStateSetUp("徘徊モード");
            //Animatorは待機モードを実行
            owner.m_Animator.SetInteger("モード", 1);

            //適当な場所を指定
            m_PatrolPoint = new Vector3(Random.Range(10.0f, -10.0f), 0, Random.Range(10.0f, -10.0f));
        }
        //このAIが起動中に常に実行(Updateと同義)
        public override void Stay()
        {
            Brain();
        }
        public override void Exit(){ }

        public void Brain()
        {
            if (Vector3.Distance(owner.transform.position, m_PatrolPoint) <= 3.0f) 
            {
                //パトロール終了時に待機を実行
                owner.ChangeState(AIState_SystemType.Idle);
            }
            else
            {
                //パトロールポイントに向かう
                owner.m_NavMeshAgent.SetDestination(m_PatrolPoint);
            }
            //敵を発見
            if (owner.Sensor_EnemyDetected())
            {
                //敵を発見したらChaseを起動
                owner.ChangeState(AIState_SystemType.Chase);
            }
        }
    }
}
