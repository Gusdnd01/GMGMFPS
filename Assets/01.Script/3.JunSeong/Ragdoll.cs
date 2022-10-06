using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private GameObject originObj;
    [SerializeField] private GameObject ragdollObj;

    //ragdollObj의 Rigidbody
    [SerializeField] private Rigidbody head;
    [SerializeField] private Rigidbody leftArm;
    [SerializeField] private Rigidbody rightArm;
    [SerializeField] private Rigidbody leftKnee;
    [SerializeField] private Rigidbody rightKnee;
    [SerializeField] private Rigidbody ribs;
    [SerializeField] private Rigidbody hip;

    public void AddForceToRagdoll(Vector3 forceVactor)
    {
        if(originObj.activeSelf == true)
        {
            ChangeRagdoll();
            CopyTransformOriginToRagdoll(originObj.transform, ragdollObj.transform);
        }

        ribs.AddForce(forceVactor, ForceMode.Impulse);
    }

    private void ChangeRagdoll()
    {   
        if(originObj.activeSelf == true)
        {
            originObj.SetActive(false);
            ragdollObj.SetActive(true);
        }
    }

    private void CopyTransformOriginToRagdoll(Transform origin, Transform ragdoll)
    {
        for(int i = 0; i < origin.childCount; i++)
        {
            if(origin.childCount != 0)
            {
                CopyTransformOriginToRagdoll(origin.GetChild(i), ragdoll.GetChild(i));
            }

            ragdoll.GetChild(i).localPosition = origin.GetChild(i).localPosition;
            ragdoll.GetChild(i).localRotation = origin.GetChild(i).localRotation;
        }
    }
}
