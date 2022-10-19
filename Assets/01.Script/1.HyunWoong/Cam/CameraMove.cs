using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class CameraMove : MonoBehaviour
{
    public float m_sensitivity = 1;
    [Header("회전관련 수치")]
    [SerializeField] private Transform cam;
    [SerializeField] private Vector2 camLimit = new Vector2(-45f, 45f);
    [SerializeField] private float senservity = 500f;
    [SerializeField] private AnimationCurve recoilControllCurve;
    [SerializeField] private Transform camAxis;
    private Vector2 recoil;
    private float fixSenservityValue = 1;
    private float camY;
    private float camZ;
    private bool useFixValue = false;
    private bool isCamRight;
    private bool isCamLeft;

    [SerializeField]
    protected MMF_Player feedbacks;

    [SerializeField]
    Transform player;

    [SerializeField]
    RectTransform interactableImage;

    private bool playerDie = false;
    private bool interactable = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        StartCoroutine(Interact());
        StartCoroutine(MoveImage());
        StartCoroutine(Attack());
    }

    private void Update()
    {
        Rotate();
        SensitiveControll();
    }
    public void Recoil(Vector2 value, float time)
    {
        StartCoroutine(Recol(time, value));
    }
    IEnumerator Recol(float time, Vector2 value)
    {
        Camera camera = cam.GetComponent<Camera>();
        float defaultFOV = camera.fieldOfView;
        camera.fieldOfView = defaultFOV * 1.035f;
        float fixValue = 1f / time;
        float curTime = 0;
        while (1 >= curTime)
        {
            curTime += Time.deltaTime * fixValue;
            recoil.y = recoilControllCurve.Evaluate(curTime) * value.y * 0.1f;
            recoil.x = recoilControllCurve.Evaluate(curTime) * value.x * 0.1f;
            yield return null;
        }
        recoil = Vector2.zero;
        camera.fieldOfView = defaultFOV;
    }
    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        float inputX = (mouseX + recoil.x) * (useFixValue ? fixSenservityValue : 1)
            * senservity * Time.deltaTime;
        float inputY = (mouseY + recoil.y) * (useFixValue ? fixSenservityValue : 1)
            * senservity * Time.deltaTime;
        transform.rotation = transform.rotation * Quaternion.Euler(0, inputX, 0);
        camY += inputY;
        camY = Mathf.Clamp(camY, camLimit.x, camLimit.y);
        cam.localEulerAngles = new Vector3(camY, cam.localEulerAngles.y, cam.localEulerAngles.z);
    }

    private IEnumerator MoveImage()
    {
        while (true)
        {
            if (interactable)
            {
                interactableImage.anchoredPosition = new Vector3(Mathf.Lerp(interactableImage.anchoredPosition.x, -50, 0.1f), -50, 0);
            }
            else
            {
                interactableImage.anchoredPosition = new Vector3(Mathf.Lerp(interactableImage.anchoredPosition.x, 600, 0.1f), -50, 0);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void SensitiveControll()
    {
        m_sensitivity = Mathf.Clamp(m_sensitivity, 0, 2);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_sensitivity += 0.1f;
            print(m_sensitivity);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_sensitivity -= 0.1f;
            print(m_sensitivity);
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            Recoil(new Vector2(0.5f, 0.5f), 0.1f);
        }
    }

    private IEnumerator Interact()
    {
        while (!playerDie)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1))
            {
                interactable = true;
                Debug.DrawRay(transform.position, transform.forward, Color.blue); 
            }
            else
                interactable = false;

            if (interactable)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                if (hit.transform.GetComponent<IInteract>() == null)
                {
                    continue;
                }
                else
                {
                    feedbacks.PlayFeedbacks();

                    hit.transform.GetComponent<IInteract>().OnInteractive();
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

}
