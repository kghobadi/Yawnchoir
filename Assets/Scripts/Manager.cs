using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour {
    public GameObject[] sleepers;

    public GameObject sleepingSun, awakenedSun;

    public float sleepersNecessary;
    public int awakenedSleepers = 0;

    //check this to randomize required wake up pattern at start
    public bool randomMemory;
    public GameObject flashingIndicator;

	void Start () {
        sleepers = GameObject.FindGameObjectsWithTag("Sleeper");
        sleepersNecessary = sleepers.Length;

        sleepingSun.SetActive(true);
        awakenedSun.SetActive(false);

        //reorder the array
        if (randomMemory)
        {
            System.Random ran = new System.Random();

            sleepers = sleepers.OrderBy(x => ran.Next()).ToArray();

            flashingIndicator.SetActive(true);
        }
        else
        {
            flashingIndicator.SetActive(false);
        }
	}
	
	void Update () {
        if (randomMemory && awakenedSleepers < sleepersNecessary)
        {
            flashingIndicator.transform.position = sleepers[awakenedSleepers].transform.position;
        }


		if(awakenedSleepers == sleepersNecessary)
        {
            sleepingSun.SetActive(false);
            awakenedSun.SetActive(true);
            flashingIndicator.SetActive(false);

            for (int i = 0; i < sleepers.Length; i++)
            {
                sleepers[i].GetComponent<Sleeper>().awake.SetActive(false);
                sleepers[i].GetComponent<Sleeper>().dancing.SetActive(true);
                sleepers[i].GetComponent<Sleeper>().timeToParty = true;
            }
        }
	}

}
