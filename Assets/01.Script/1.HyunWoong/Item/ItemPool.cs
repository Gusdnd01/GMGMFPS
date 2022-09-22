using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : PoolAbleObject
{
    [SerializeField]
    protected ItemData _data;

    [SerializeField]
    private List<GameObject> _items = new List<GameObject>();

    public override void Init_Pop()//Reset
    {
        for (int i = 0; i < _items.Count; i++)
        {
            GameObject obj = Instantiate(_data.items[i]);
            obj.transform.SetParent(transform);
            obj.transform.position = transform.position;
            obj.SetActive(false);

            _items[i] = obj;
        }
    }

    public override void Init_Push()//들어갈 때
    {

    }

    public void ItemActive(int index)
    {
        if (index == 2)
        {
            PoolManager.Instance.Push(PoolType.Item, gameObject);
            return;
        }

        _items[index].GetComponent<Disssolve>().IsDissolve = true;
        _items[index].SetActive(true);
    }
}
