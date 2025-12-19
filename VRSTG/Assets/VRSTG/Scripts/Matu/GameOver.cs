using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeCanvas; //黒フェード用
    void Start()
    {
        //ゲームオーバーの移動
        var transformCanhe = transform;
        //本来表示したい位置
        var defaultPosition = transformCanhe.localPosition;
        //フェイドのアルファ値の変更
        var tr = transform;
        var defaultPos = tr.localPosition;
        //演出のために文字を上に移動する
        transformCanhe.localPosition = new Vector3(0, 300f);

        tr.localPosition = new Vector3(0, 300f);
        tr.DOLocalMove(defaultPos, 1f)
             .SetEase(Ease.Linear)
             .OnComplete(() =>
             {
                 //文字が揺れる時間やその揺れの大きさ
                 tr.DOShakePosition(1.5f, 100);
             });
            DOVirtual.DelayedCall(5f, FadeOut);

        transformCanhe.DOLocalMove(defaultPosition, 1f)
             .SetEase(Ease.Linear)
             .OnComplete(() =>
             {
                 Debug.Log("GameOver!w");

                 transformCanhe.DOShakePosition(1.5f, 100);
             });
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
