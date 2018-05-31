using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {
    
	void Start () {
        Cursor.visible = false;
    }
	
	void Update () {
        transform.position = Input.mousePosition;

        //RaycastToZ();
	}

    void RaycastToZ()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 50))
        {
            Debug.Log("hit");
        }
            
    }

}
