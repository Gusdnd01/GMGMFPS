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
        GameObject obj = Instantiate(objects[index], new Vector3(rand.x, 1 ,rand.y), Quaternion.identity);

        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void Update(){
        if(isDissolve){
            fade -= 0.1f;
            if(fade <= -1){
                fade = -1;
                isDissolve = false;

                Destroy(gameObject);
            }

            mat.SetFloat("_Cutoff_Height", fade);
        }
    }
}
