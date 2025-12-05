using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace StateMachineAI
{

    public class SA_Death : State<SystemAI>
    {
        //待機時間
        public float m_DeathTime;


        public SA_Death(SystemAI owner) : base(owner) { }

        public override void Enter()
        {
            //消滅する秒数
            m_DeathTime = 3.0f;
            //ナビゲーションを停止
            owner.m_NavMeshAgent.enabled = false;
        }
        //このAIが起動中に常に実行(Updateと同義)
        public override void Stay()
        {
            Brain();
        }
        public override void Exit() { }

        public void Brain()
        {
            if (m_DeathTime <= 0.0f)
            {
                owner.SetDestroy();
            }
            else
            {
                m_DeathTime -= Time.deltaTime;
            }
        }
    }
}
