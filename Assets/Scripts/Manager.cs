using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    public GameObject[] sleepers;

    public float sleepersNecessary;
    public float awakenedSleepers = 0;

	void Start () {
        sleepers = GameObject.FindGameObjectsWithTag("Sleeper");
        sleepersNecessary = sleepers.Length;
	}
	
	void Update () {
		if(awakenedSleepers == sleepersNecessary)
        {
            for(int i = 0; i < sleepers.Length; i++)
            {
                sleepers[i].GetComponent<Sleeper>().awake.SetActive(false);
                sleepers[i].GetComponent<Sleeper>().dancing.SetActive(true);
                sleepers[i].GetComponent<Sleeper>().timeToParty = true;
            }
        }
	}
}
