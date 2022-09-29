using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : PoolAbleObject
{
    [SerializeField]
    protected ItemData _data;

    [SerializeField]
    private List<GameObject> _items = new List<GameObject>();
    Vector3 _trm;

    public override void Init_Pop()//Reset
    {
        for (int i = 0; i < _items.Count; i++)
        {
            GameObject obj = Instantiate(_data.items[i], gameObject.transform.position, Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.SetActive(false);

            _items[i] = obj;
        }
    }

    public override void Init_Push()//들어갈 때
    {

    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.5f * Mathf.Sin(Time.time) + 1, transform.position.z);
    }

    public void SetPosition(Vector3 trm)
    {
        _trm = trm;
    }

    public void ItemActive(int index)
    {
        print(_trm);
        gameObject.transform.position = _trm;

        if (index == 2)
        {
            PoolManager.Instance.Push(PoolType.Item, gameObject);
            return;
        }

        _items[index].GetComponent<Disssolve>().IsDissolve = true;
        _items[index].SetActive(true);
    }
}
