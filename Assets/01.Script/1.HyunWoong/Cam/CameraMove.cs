using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class CameraMove : MonoBehaviour
{
    public float m_sensitivity = 1;

    float xRotation;

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
    }

    private void Update()
    {
        MouseControll();
        SensitiveControll();
    }
    private void MouseControll()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        transform.localRotation = Quaternion.Euler(xRotation * m_sensitivity, 0, 0);

        player.Rotate(0, mouseX * m_sensitivity, 0);
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
