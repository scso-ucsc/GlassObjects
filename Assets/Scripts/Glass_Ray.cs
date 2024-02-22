using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Ray : MonoBehaviour
{
    [SerializeField] private float raycastRange = 5;
    [SerializeField] private bool isActive = false; //If active, fire light source
    [SerializeField] private GameObject lightRay;
    //[SerializeField] private bool isHitByLight = false;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false; //Set isActive and isHitByLight to false upon start of level
        lightRay.SetActive(false);
        //isHitByLight = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 raycastDirection = Vector3.forward; //Generating RayCast
        Ray lightBeam = new Ray(transform.position, transform.TransformDirection(raycastDirection * raycastRange));
        
        if(isActive == true){ //For debugging, make RayCast visible
            Debug.DrawRay(transform.position, transform.TransformDirection(raycastDirection * raycastRange), Color.red); //Make RayCast RED if is hit by light
        } else{
            Debug.DrawRay(transform.position, transform.TransformDirection(raycastDirection * raycastRange)); //RayCast WHITE if not active
        }

        if(Physics.Raycast(lightBeam, out RaycastHit hitObject, raycastRange)){
            if(hitObject.collider.tag == "Glass"){ //If this Glass Object's RayCast hits another, switchToActive()
                //Debug.Log(hitObject.distance); //Printing Raycast Hit Distance
                // lightRay.SetActive(true);
                // lightRay.transform.localScale += new Vector3(0, 0, hitObject.distance);
                var hitObjectScript = hitObject.collider.gameObject.GetComponent<Glass_Ray>(); //Accessing Script of RayCastHit Object
                if(isActive == true){
                    Debug.Log("Attempting to set to active");
                    hitObjectScript.switchToActive();
                } else{
                    hitObjectScript.switchToInactive();
                }
            }
        }
    }

    private void switchToActive(){
        //Debug.Log("Switching to true!");
        isActive = true;
    }

    private void switchToInactive(){
        //Debug.Log("Switching to true!");
        isActive = false;
    }
}
