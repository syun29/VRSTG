using UnityEngine;
using UnityEngine.AI;

namespace StateMachineAI
{
    public class SA_Idol : State<SystemAI>
    {
        //
        public float m_CheckTime;

        //
        public SA_Idol(SystemAI owner) : base(owner) 
        {
            //
            if (!owner.m_Taget)
                owner.SetTaget();

            //ナビゲーションを停止
            owner.m_NavMeshAgent.enabled = false;
            //AnimatorのStateを待機モードへブレンド
            owner.AnimatorStateSetUp("待機モード");
            //Animatorは待機モードを実施（モード0を起動）
            owner.m_Animator.SetInteger("モード", 0);

            //パトロール切り替え時間2～4秒
            m_CheckTime = Random.Range(2.0f, 4.0f);
        }
        //

        public override void Stay()
        {
            Brain();
        }
        public override void Exit()
        {
        }
        public void Brain()
        {
            if (m_CheckTime <= 0.0f)
            {
                //パトロールを実行
                owner.ChangeState(AIState_SystemType.Patrol);
            }
            else 
            {
                //時間減少
                m_CheckTime -= 1.0f * Time.deltaTime;
            }
            //敵を発見
            if (owner.Sensor_EnemyDetected())
            {
                //追跡実行
                owner.ChangeState(AIState_SystemType.Chase);
            }
        }
    }
}
