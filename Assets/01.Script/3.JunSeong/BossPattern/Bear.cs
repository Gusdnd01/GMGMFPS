using UnityEngine;

public class Bear : Boss
{
    [Header("앞발 휘두르기")]
    public int wieldDamage; 
    public float wieldRange;
    public float wieldTime;

    [Header("앞발 내려치기")]
    public int downSlapDamage;
    public float downSlapRange;
    public float downSlapTime;

    [Header("크게 내려치기")]
    public int bigDownSlapDamage;
    public float bigDownSlapRange;
    public float bigDownSlapTime;

    [Header("돌진")]
    public int rushDamage;
    public float rushSpeed;
    public float rushTime;
    public LayerMask wallLayer;
    private float wallRaycastDistance;

    [Header("포효")]
    public float ShoutTime;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void Attacking()
    {
        if(Distance <= wieldRange)
        {
            Wield();
        }
        else if(Distance <= downSlapRange)
        {
            DownSlap();
        }
        else if(Distance <= bigDownSlapRange)
        {
            BigDownSlap();
        }
        else
        {
            Rush();
        }
    } 

    private void Wield()
    {
        Debug.Log("Wield");

        Vector3 WieldPosition = transform.position + new Vector3(0, 0, wieldRange / 2);

        Collider[] hit = Physics.OverlapSphere(WieldPosition, wieldRange / 2, playerLayer);

        if(hit.Length > 0)
        {
            //player hit
        }
    }

    private void DownSlap()
    {
        Debug.Log("DownSlap");

        Vector3 downSlapPosition = transform.position;

        Collider[] hit = Physics.OverlapSphere(downSlapPosition, downSlapRange / 2, playerLayer);

        if (hit.Length > 0)
        {
            //player hit
        }
    }

    private void BigDownSlap()
    {
        Debug.Log("BigDownSlap");

        Vector3 bigDownSlapPosition = transform.position;

        Collider[] hit = Physics.OverlapSphere(bigDownSlapPosition, bigDownSlapRange / 2, playerLayer);

        if (hit.Length > 0)
        {
            //player hit
        }
    }

    private void Rush()
    {
        Debug.Log("Rush");

        Vector3 raycastStartPos = Vector3.zero; //나중에 바꿀거임
        Vector3 rushDir = transform.forward;
        bool isWall = false;
        
        /*while(!isWall)
        {
            isWall = Physics.Raycast(raycastStartPos, transform.forward, wallRaycastDistance, wallLayer);
            controller.Move(rushDir * rushSpeed * Time.deltaTime);
            //playerhit OverlapSphere
        }*/

        controller.Move(Vector3.zero);
    }

    private void Shout()
    {
        Debug.Log("Shout");

        //
    }
}                                                                           
