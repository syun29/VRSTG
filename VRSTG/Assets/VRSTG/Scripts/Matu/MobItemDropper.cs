using StateMachineAI;
using UnityEngine;
using Random = UnityEngine.Random;

//アタッチミス防止用
[RequireComponent(typeof(Parameta))]

public class MobItemDropper : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float dropRate = 0.1f;
    [SerializeField] private Item itemPrefab;
    [SerializeField] private int number = 1;

    private Parameta _status;
    private bool _isDropInvoked;

    private void Start()
    {
        _status = GetComponent<Parameta>();
    }

    private void Update()
    {
        if(_status.m_Hp <= 0)
        {
            DropIfNeeded();
        }
    }

    private void DropIfNeeded()
    {
        if (_isDropInvoked) return;

        _isDropInvoked = true;
        if (Random.Range(0, 1f) >= dropRate) return;
    
    for(int i = 0; i < number; i++)
        {
            Item item = Instantiate(itemPrefab,transform.position,
            Quaternion.identity);
            item.Initialize();
        }
    }

}
