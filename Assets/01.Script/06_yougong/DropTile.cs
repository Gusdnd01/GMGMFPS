using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTile : MonoBehaviour
{
    [SerializeField] List<Transform> _tile = new List<Transform>();
    [SerializeField] List<Transform> _dropedtile = new List<Transform>();
    [SerializeField] int _dropTiles_num = 10;
    [SerializeField] float _dropTime = 3f;
    [SerializeField] float _dropedLength = 10f;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _tile.Add(transform.GetChild(i));
        }
        StartCoroutine(Droping());
    }

    IEnumerator Droping()
    {
        int count;
        while (true)
        {
            for(int i = 0; i < _dropTiles_num; i++)
            {
                _tile[i].GetComponent<MeshCollider>().enabled = true;
            }
            yield return new WaitForSeconds(_dropTime);
            Debug.Log("¶³±¸´ÂÁß");
            for (int i = 0; i < _dropTiles_num; i++)
            {
                count = UnityEngine.Random.Range(0, _tile.Count);
                if (_tile.Count != 0)
                {
                    _tile[count].DOMoveY(_tile[count].position.y - _dropedLength, _dropTime);
                    _dropedtile.Add(_tile[count]);
                    _tile.Remove(_tile[count]);
                    Debug.Log("¶³±ÅÁü");
                }
            }
            yield return new WaitForSeconds(_dropTime);

            Uping();
        }
    }


    void Uping()
    {

        int rand;
        for (int i = 0; i < _dropTiles_num; i++)
        {
            rand = UnityEngine.Random.Range(0, _dropedtile.Count);
            if (_dropedtile.Count != 0)
            {
                _dropedtile[rand].DOMoveY(_dropedtile[rand].position.y + _dropedLength, _dropTime - 0.1f);
                _tile.Add(_dropedtile[rand]);
                _dropedtile.Remove(_dropedtile[rand]);
            }
        }
    }
}

