using UnityEngine;
using UnityEngine.AI;
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

            for (int i = 0; i < 10; i++)
            {
                //適当な場所を指定
                Vector3 pos = new Vector3(Random.Range(10.0f, -10.0f), 0, Random.Range(10.0f, -10.0f));
                if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
                {
                    m_PatrolPoint = hit.position;
                    owner.m_NavMeshAgent.SetDestination(m_PatrolPoint);
                    break;
                }
            }
           
        }
        //このAIが起動中に常に実行(Updateと同義)
        public override void Stay()
        {
            Brain();
        }
        public override void Exit(){ }

        private bool IsEndMove()
        {
            if (owner.m_NavMeshAgent.pathPending) return false;
            if (!owner.m_NavMeshAgent.hasPath) return true;
            if (owner.m_NavMeshAgent.remainingDistance <= 3f) return true;
            return false;
        }
        public void Brain()
        {
            if (IsEndMove()) 
            {
                //パトロール終了時に待機を実行
                owner.ChangeState(AIState_SystemType.Idle);
            }
            else
            {
                //パトロールポイントに向かう
                //owner.m_NavMeshAgent.SetDestination(m_PatrolPoint);
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
