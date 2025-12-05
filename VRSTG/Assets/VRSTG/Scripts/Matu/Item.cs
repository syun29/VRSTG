using UnityEngine;
using DG.Tweening;
//using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UIElements;


public class Item : MonoBehaviour
{
    public enum ItemType
    {
        AssaultRifle,
        ShotGun,
        RocketLauncher,
        Gun,
    }

    [SerializeField] private ItemType type;

    public void Initialize()
    {
        var colliderCache = GetComponent<Collider>();
        colliderCache.enabled = false;

        var transformCache = transform;
        var dropPosition = transform.localPosition +
            new Vector3(Random.Range(-1f, -1f), 0, Random.Range(-1f, 1f));
        transformCache.DOLocalMove(dropPosition, 0.5f);
        var defaultScale = transformCache.localScale;
        transformCache.localScale = Vector3.zero;
        transformCache.DOScale(defaultScale, 0.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                colliderCache.enabled = true;
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        OwnedItemsData.Instance.Add(type);
        OwnedItemsData.Instance.Save();
        foreach (var item in OwnedItemsData.Instance.OwnedItems)
        {
            Debug.Log(item.Type + "Ç" + item.Number + "å¬èäéù");
        }

        Destroy(gameObject);
    }
}
