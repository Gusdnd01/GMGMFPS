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
    float t = 0;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _tile.Add(transform.GetChild(i));
        }
        if(_dropTime/5 < 1)
        {
            t = 1;
        }
        else
        {
            t = _dropTime / 5;
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
                    _tile[count].DOMoveY(_tile[count].position.y - _dropedLength, t);
                    _dropedtile.Add(_tile[count]);
                    _tile.Remove(_tile[count]);
                    Debug.Log("¶³±ÅÁü");
                }
            }
            yield return new WaitForSeconds(t);

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
                _dropedtile[rand].DOMoveY(_dropedtile[rand].position.y + _dropedLength, t - 0.1f);
                _tile.Add(_dropedtile[rand]);
                _dropedtile.Remove(_dropedtile[rand]);
            }
        }
    }
}

