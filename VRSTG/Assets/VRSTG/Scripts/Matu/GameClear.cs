using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameClear : MonoBehaviour
{
    [SerializeField] private RectTransform clearTextTransform;
    [SerializeField] CanvasGroup fadeCanvas; //黒フェード用
     public void ShowClearUI()
    {
        Debug.Log("ShowClearUIが呼ばれました！");
        //初期状態の設定：巨大にして透明にする
        clearTextTransform.localScale = Vector3.one * 5f;
        fadeCanvas.alpha = 0f;
        clearTextTransform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        //0.2秒かけてフェードインさせる
        fadeCanvas.DOFade(1f, 0.2f);
        //5秒後に実行するという処理
        DOVirtual.DelayedCall(7, () =>
        {
            SceneManager.LoadScene("Title");
        });
    }
    void FadeOut()
    {
        fadeCanvas.DOFade(1f, 1.5f).OnComplete(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}
