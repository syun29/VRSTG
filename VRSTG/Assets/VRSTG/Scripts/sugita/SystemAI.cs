using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

namespace StateMachineAI
{ 
public enum AIState_SystemType
{
    Idle,       //待機
    Patrol,     //徘徊
    Chase,      //追跡
    Battle,     //戦闘
    Death,      //死亡

}
/*
    public class SystemAI
        : StatefulObjectBase<SystemAI, AIState_SystemType>
    {
        [Header("アニメーターリンク")]
        public Animation m_Animator;
        [Header("Navigationリンク")]
        public NavMeshAgent m_NavMeshAgent;
        [Header("ターゲット指定")]
        public Transform m_Player;
        [Header("戦闘中の旋回速度")]
        public float m_RotateSpeed = 60.0f;




        void Start()
        {
            //Animatorをリンクする
            m_Animator = GetComponent<Animation>();
            
        } 

    }
*/
    
}
