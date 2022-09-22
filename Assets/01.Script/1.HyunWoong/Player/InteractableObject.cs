using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteract
{
    public List<GameObject> objects = new List<GameObject>();
    private Material mat;
    private int index = 0;
    private bool isDissolve = false;
    private float fade = 1;

    private void Start(){
        mat = GetComponent<MeshRenderer>().material;
    }
    public void OnInteractive()
    {
        isDissolve = true;
        print("asd");
        index = Random.Range(0, objects.Count);
        Vector3 rand = Random.insideUnitCircle * 5f;


        PoolManager.Instance.Pop(PoolType.Item).GetComponent<ItemPool>().ItemActive(CheckIndex());

        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private int CheckIndex()
    {
        int index = Random.Range(0, 10);
        print(index);
        int result = 0;

        if (index >= 0 && index < 2)
        {
            result = 0;
        }
        else if(index >= 2 && index < 4)
        {
            result = 1;
        }
        else
        {
            result = 2;
        }
        print(result);

        return result;
    }

    private void Update(){
        Dissolve();
    }

    void Dissolve()
    {
        if (isDissolve)
        {
            fade -= 0.1f;
            if (fade <= -1)
            {
                fade = -1;
                isDissolve = false;

                Destroy(gameObject);
            }

            mat.SetFloat("_Cutoff_Height", fade);
        }
    }
}
