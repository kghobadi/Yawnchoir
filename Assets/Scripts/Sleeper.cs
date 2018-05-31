using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sleeper : MonoBehaviour {
    Manager _manager;
    public bool asleep, timeToParty;
    public GameObject sleeping, awake, dancing;

    //timers for wake up
    public float wakeUpTime;
    float wakingUp = 0;

    GameObject wakingBar;
    WakingBar wakingBarScript;
    Image wakingBarImage;
    RectTransform wakingBarRT;

    void Start () {
        _manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();

        //waking bar refs
        wakingBar = GameObject.FindGameObjectWithTag("WakingBar");
        wakingBarScript = wakingBar.GetComponent<WakingBar>();
        wakingBarImage = wakingBar.GetComponent<Image>();
        wakingBarRT = wakingBar.GetComponent<RectTransform>();

        wakingBarImage.fillAmount = 0;

        //sleeping at start
        sleeping.SetActive(true);
        awake.SetActive(false);
        dancing.SetActive(false);

        asleep = true;
       
	}

    void OnMouseOver()
    {
        if(asleep && wakingUp < wakeUpTime)
        {
            wakingBarScript.CorrectUIPos(transform, wakingBarRT, 0, 100);
            wakingBarImage.fillAmount += 1 / wakeUpTime * Time.deltaTime;
            wakingUp += Time.deltaTime;
            Debug.Log(wakingUp);
        }
        else if (asleep && wakingUp > wakeUpTime)
        {
            sleeping.SetActive(false);
            awake.SetActive(true);
            asleep = false;
            _manager.awakenedSleepers++;
        }

        if (timeToParty)
        {
            //sing when mouse over
        }
    }

    void OnMouseExit()
    {
        if (asleep)
        {
            wakingUp = 0;
        }

        wakingBarImage.fillAmount = 0;
    }
}
