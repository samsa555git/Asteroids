using UnityEngine;
using System.Collections;
 
public class ET : MonoBehaviour {
 
    // Update is called once per frame
    void Update () {
 
        // Teleport the game object
        if(transform.position.x > 12){
 
            transform.position = new Vector3(-12, transform.position.y, 0);
            GetComponent<TrailRenderer>().Clear();  
 
        }
        else if(transform.position.x < -12){
            transform.position = new Vector3(12, transform.position.y, 0);
            GetComponent<TrailRenderer>().Clear();  
        }
 
        else if(transform.position.y > 6){
            transform.position = new Vector3(transform.position.x, -6, 0);
            GetComponent<TrailRenderer>().Clear();  
        }
 
        else if(transform.position.y < -6){
            transform.position = new Vector3(transform.position.x, 6, 0);
            GetComponent<TrailRenderer>().Clear();     }
    }
}