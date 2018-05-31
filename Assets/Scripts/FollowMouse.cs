using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public SpriteRenderer sr;
    public Sprite normal, touching;

    //dictionary to sort nearby audio sources by distance 
    Dictionary<AudioSource, float> soundCreators = new Dictionary<AudioSource, float>();


    void Start () {
        sr = GetComponent<SpriteRenderer>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
	
	void Update () {
        transform.position = Input.mousePosition;

        ResetNearbyAudioSources();
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

    //this function shifts all audio source priorities dynamically
    void ResetNearbyAudioSources()
    {
        //empty dictionary
        soundCreators.Clear();
        //overlap sphere to find nearby sound creators
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1000);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //check to see if obj is plant or rock
            if (hitColliders[i].gameObject.tag == "Plant" || hitColliders[i].gameObject.tag == "Rock" ||
                hitColliders[i].gameObject.tag == "NPC" || hitColliders[i].gameObject.tag == "RainSplash"
                || hitColliders[i].gameObject.tag == "Ambient" || hitColliders[i].gameObject.tag == "Animal"
                || hitColliders[i].gameObject.tag == "Seed")
            {
                //check distance and add to list
                float distanceAway = Vector3.Distance(hitColliders[i].transform.position, transform.position);
                //add to audiosource and distance to dictionary
                soundCreators.Add(hitColliders[i].gameObject.GetComponent<AudioSource>(), distanceAway);
            }
            i++;
        }

        int priority = 0;
        //sort the dictionary by order of ascending distance away
        foreach (KeyValuePair<AudioSource, float> item in soundCreators.OrderBy(key => key.Value))
        {
            // do something with item.Key and item.Value
            item.Key.priority = priority;
            priority++;
        }
    }


}
