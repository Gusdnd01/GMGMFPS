using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TestScript_ComingEnemyWall : MonoBehaviour
{
    Vector3 direction = Vector3.zero;

    [Header("발사체")]
    [SerializeField] GameObject ShootObj;

    [Header("발사 시간")]
    [SerializeField] float _minShootTime = 0.5f;
    [SerializeField] float _maxShootTime = 1.5f;
    private Transform _player = null;
    public Transform Player
    {
        get
        {
            if(_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _player;
        }
    }

    [Header("디버그용 패널들")]
    [SerializeField] List<Image> _panels = new List<Image>();
    [SerializeField] List<Transform> _StartPos = new List<Transform>();
    [SerializeField] List<Transform> _endPos = new List<Transform>();

    private void Awake()
    {
        StartCoroutine(Setting());
        StartCoroutine(Command());
    }

    IEnumerator Setting()
    {
        Transform panel = GameObject.FindGameObjectWithTag("DangerousPanel").transform;
        for(int i =0; i < panel.childCount; i++)
        {
            _panels.Add(panel.GetChild(i).GetComponent<Image>());
            _endPos.Add(_panels[i].transform.GetChild(0));
            _panels[i].color = new Vector4(1, 1, 1, 0);
            _StartPos.Add(transform.GetChild(i));
            yield return null;
        }
    }
    IEnumerator Command()
    {
        float t = Random.Range(_minShootTime, _maxShootTime);
        yield return new WaitForSeconds(t);
        AttackCommand();
        Debug.Log("리로드");
    }
    void AttackCommand()
    {
        int t = Random.Range(0, _StartPos.Count);
        Debug.Log("발사");
        _panels[t].color = new Color(1, 0, 0);
        _panels[t].DOFade(0.4f, 0.3f).SetEase(Ease.InOutBounce).OnComplete(() =>
         {
             GameObject obj = Instantiate(ShootObj);
             obj.transform.position = _StartPos[t].position;
             obj.GetComponent<Rigidbody>().velocity = transform.forward * obj.GetComponent<GroundSlash>().speed * 2f;
             obj.transform.localScale = new Vector3(1, 1, 1) * 2;
             _panels[t].DOFade(0, 0.3f);
             StartCoroutine(Command());
         });
    }
    

    void Update()
    {
        //direction = (Player.position - transform.position).normalized;
        //direction.y = 0;

        transform.position += transform.forward * 5 * Time.deltaTime;
        if(FindObjectOfType<GameEnd>().isGameEnd)
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            Player.GetComponent<PlayerController>().OnDamaged(99999);
    }
    
}
