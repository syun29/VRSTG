using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace StateMachineAI
{
    /// <summary>
    /// 戦闘モード
    /// </summary>
    public class SA_Battle : State<SystemAI>
    {
        float m_EnemyChange;
        //切り替え時間
        float m_CoolTime;
        //コンストラクタ
        public SA_Battle(SystemAI owner) : base(owner) { }
        //このAIが起動した瞬間に実行(Startと同義)
        public override void Enter()
        {
            m_EnemyChange = 10.0f;

            //プレイヤーがいない場合
            if (!owner.m_Taget)
                owner.SetTaget();

            //ナビゲーション停止
            owner.m_NavMeshAgent.enabled = false;

            //AnimatorのStateを戦闘モードへブレンド
            owner.AnimatorStateSetUp("戦闘モード");
            //Animatorは待機モードを実行
            owner.m_Animator.SetInteger("モード", 3);

            //CoolTimeセット
            m_CoolTime = Random.Range(0.5f, 1.0f);
            //攻撃停止(初期化)
            owner.m_Animator.SetInteger("攻撃", 0);
            //戦闘移動前進(初期化)
            owner.m_Animator.SetFloat("戦闘Z", 1.0f);
            owner.m_Animator.SetFloat("戦闘X", 0.0f);

        }
        //このAIが起動中に常に実行(Updateと同義)
        public override void Stay()
        {
            Brain();
        }
        public override void Exit()
        {
            //攻撃停止
            owner.m_Animator.SetInteger("攻撃", 0);
            //戦闘移動停止
            owner.m_Animator.SetFloat("戦闘X", 0);
            owner.m_Animator.SetFloat("戦闘Z", 0);
        }
        public void Brain()
        {
           

            //戦闘行動クールタイム
            if (m_CoolTime <= 0.0f)
            {
                //Action確率算出
                int ActionCheck = Random.Range(0, 100);
                //攻撃停止
                owner.m_Animator.SetInteger("攻撃", 0);
                //戦闘移動停止
                owner.m_Animator.SetFloat("戦闘X", 0);
                owner.m_Animator.SetFloat("戦闘Z", 0);

                
            }
            else
            {
                //ターゲットへゆっくり向く
                LookUnit();
                //クールタイム減少
                m_CoolTime -= Time.deltaTime;
            }

            //プレイヤーがいない
            if (!owner.m_Taget)
            {
                //待機モード
                owner.ChangeState(AIState_SystemType.Idle);
            }
            //攻撃範囲から離れたが索敵範囲にいる
            if (!owner.Sensor_AttackEnemyDistance(2.0f) && owner.Sensor_EnemyDetected())
            {
                //追跡開始
                owner.ChangeState(AIState_SystemType.Chase);
            }
            //索敵範囲から離れた
            if (!owner.Sensor_EnemyDetected())
            {
                //待機モード
                owner.ChangeState(AIState_SystemType.Idle);
            }
        }
        /// <summary>
        /// 攻撃処理
        /// </summary>
        public void AttackAction()
        {
            // 攻撃アニメ再生（Trigger）
            owner.m_Animator.SetTrigger("攻撃");

            // 攻撃中は止まる（移動しない）
            owner.m_Animator.SetFloat("戦闘X", 0);
            owner.m_Animator.SetFloat("戦闘Z", 0);

            // クールタイム設定
            m_CoolTime = Random.Range(0.5f, 1.0f);
        }

        /// <summary>
        /// 相手にゆっくり向く
        /// </summary>
        public void LookUnit()
        {
            //ターゲットがいない場合は実行しない
            if (owner.m_Taget == null) return;
            //ターゲットの向き
            Vector3 direction = owner.m_Taget.position - owner.transform.position;
            //y軸0にして傾きをなくす
            direction.y = 0;
            //向きが変わらないなら処理終了
            if (direction == Vector3.zero) return;
            //相手への向きをクォータニオン化する
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 現在の回転から目標回転へ一定速度で近づける
            owner.transform.rotation = Quaternion.RotateTowards(
                owner.transform.rotation,               //現在のユニットの向き
                targetRotation,                         //ターゲットへのクォータニオン
                owner.m_RotateSpeed * Time.deltaTime    //回転速度(/秒)
            );
        }
    }
}