using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]


public class DamageEffect : MonoBehaviour
{
    [Header("Flash Settings")]
    [SerializeField] private Renderer targetRenderer;       //敵のモデルのRenderer
    [SerializeField] private Color flashColor = Color.red;  //ダメージ時の色
    [SerializeField] private float flashDuration = 0.1f;    //光る時間（秒）

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 5f;

    private Rigidbody _rd;
    private MaterialPropertyBlock _propBlock;
    private Color _originalColor;

    //シェーダーのプロパティ名をIDに変換（文字列比較を避けるため高速）
    private static readonly int ColorPropertyID = Shader.PropertyToID("_BaseColor");

    private void Awake()
    {
        _rd = GetComponent<Rigidbody>();
        _propBlock = new MaterialPropertyBlock();

        //元の色を取得しておく
        if(targetRenderer != null)
        {
            //初期状態の色を取得
            _originalColor = targetRenderer.sharedMaterial.hasProperty(ColorPropertyID);
        }
    }

    public void PlayDamageEffect()
    {
        if(targetRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        //赤くする設定
        SetColor(flashColor);

        yield return new WaitForSeconds(flashDuration);

        // 元の色に戻す設定
        SetColor(_originalColor);
    }

    private void SetColor(Color color)
    {
        //現在のPropertyBlockを取得
        targetRenderer.GetPropertyBlock(_propBlock);

        //色を書き換える
        _propBlock.SetColor(ColorPropertyID, color);

        //Rendererに反映する（マテリアル自体は書き換わらない）
        targetRenderer.SetPropertyBlock(_propBlock);
    }
}

