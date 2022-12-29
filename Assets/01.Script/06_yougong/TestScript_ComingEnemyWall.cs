using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        StartCoroutine(Command());
        Debug.Log("리로드");
    }
    void AttackCommand()
    {
        GameObject obj = Instantiate(ShootObj);
        obj.transform.position = _StartPos[Random.Range(0, _StartPos.Count)].position;
        obj.GetComponent<Rigidbody>().velocity = transform.forward * obj.GetComponent<GroundSlash>().speed * 2f;
        obj.transform.localScale = new Vector3(1,1,1) *  2;
        Debug.Log("발사");
    }
    

    void Update()
    {
        direction = (Player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * 5 * Time.deltaTime;
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
