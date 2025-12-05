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

    public class SystemAI
        : StatefulObjectBase<SystemAI, AIState_SystemType>
    {
        [Header("アニメーターリンク")]
        public Animator m_Animator;
        [Header("Navigationリンク")]
        public NavMeshAgent m_NavMeshAgent;
        [Header("ターゲット指定")]
        public Transform m_Taget;
        [Header("戦闘中の旋回速度")]
        public float m_RotateSpeed = 60.0f;




        void Start()
        {
            //Animatorをリンクする
            m_Animator = GetComponent<Animator>();
            //新しくscriptを作り同じ名前で追加する
            
            stateList.Add(new SA_Idol(this));       //待機
            stateList.Add(new SA_Patrol(this));     //徘徊
            stateList.Add(new SA_Chase(this));      //追跡
            stateList.Add(new SA_Battle(this));     //戦闘
            stateList.Add(new SA_Death(this));      //死亡
            
            //ステートマシーンを自身として設定
            stateMachine = new StateMachine<SystemAI>();

            //最初はIdolが起動
            ChangeState(AIState_SystemType.Idle);
        }

        /// <summary>
        /// <param name="StateName">レイヤー・ステート名
        /// </summary>
        public void AnimatorStateSetUp(string StateName)
        {
            //StateName内の名前のレイヤー番号を取得
            int layerIndex = m_Animator.GetLayerIndex(StateName);
            //現在のアニメーションからStateNameの名前のステートへ0.1秒かけてブレンド
            m_Animator.CrossFade(StateName, 0.1f, layerIndex, 0f);
        }

        public bool Sensor_EnemyDetected()
        {
            //フラグ無し
            bool Flag = false;
            //プレイヤーがいる
            if (m_Taget)
            {
                //相対距離10m以内
                if (Vector3.Distance(transform.position, m_Taget.position) < 10.0f)
                {
                    //フラグオン
                    Flag = true;
                }
            }
            //フラグを返す
            return Flag;
        }
        /// <summary>
        /// センサーが敵との交戦距離に入ったことを伝える
        /// </summary>
        /// <param name="AddPoint"></param>
        /// <returns></returns>
        public bool Sensor_AttackEnemyDistance(float AddPoint)
        {
            //フラグ無し
            bool Flag = false;
            //プレイヤーがいる
            if (m_Taget)
            {
                //相対距離3m以内
                if (Vector3.Distance(transform.position, m_Taget.position) < 3.0f + AddPoint)
                {
                    //フラグオン
                    Flag = true;
                }
            }
            //フラグを返す
            return Flag;
        }
        public void SetTaget()
        {
            //
            if (!m_Taget) 
            {
                //全てのオブジェクトで[プレイヤータグ]を全て洗い出す
                GameObject[] Dummy = GameObject.FindGameObjectsWithTag("Taget");
                //
                m_Taget = Dummy[UnityEngine.Random.Range(0, Dummy.Length)].transform;
                //
                if (m_Taget == transform)
                    m_Taget = null;
                else
                {
                    //
                    if (m_Taget.GetComponent<Parameta>().m_Hp <= 0)
                        m_Taget = null;
                }
            }
        }
        /// <summary>
        /// 被弾
        /// プラスは正面から、マイナスは後ろから
        /// </summary>
        public void Hit()
        {
            //[被弾]という名前のレイヤー番号を取得
            int layerIndex = m_Animator.GetLayerIndex("被弾");
            //正面被弾
             m_Animator.SetInteger("被弾", UnityEngine.Random.Range(0, 2));
            //現在のアニメーションから[Hit]ステートへ0.1秒ブレンド
             m_Animator.CrossFade("被弾", 0.1f, layerIndex, 0f);
        }

        public void Death()
        {
            int layerIndex = m_Animator.GetLayerIndex("死亡");
            m_Animator.CrossFade("死亡", 0.1f, layerIndex, 0f);
            ChangeState(AIState_SystemType.Death);
        }
        public void SetDestroy() 
        {
            Destroy(gameObject);
        }
    }
}

