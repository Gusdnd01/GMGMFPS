using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    //일단 만들어야징
    public float speed = 30f;
    public float slowDownRate = .01f;
    public float detectingDistance = .1f;
    public float destroyDelay = 5f;

    private Rigidbody rb;
    private bool stopped = false;

    void Start(){
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        if(GetComponent<Rigidbody>() != null){
            rb = GetComponent<Rigidbody>();
        }
        else{
            print($"{gameObject.name} haven't Rigidbody");
        }

        Destroy(gameObject, destroyDelay);
    }

    private void FixedUpdate() {
        if(!stopped){
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y+1,transform.position.z );
            if(Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectingDistance)){
                transform.position = new Vector3(transform.position.x, hit.point.y,transform.position.z);
            }
            else{
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance), Color.blue);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.GetComponent<IDamage>().OnDamaged(33);
        }
    }
}
