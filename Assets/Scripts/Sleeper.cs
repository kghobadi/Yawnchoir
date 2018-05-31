using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sleeper : MonoBehaviour {
    Manager _manager;
    FollowMouse cursor;
    public bool asleep, timeToParty;
    public GameObject sleeping, awake, dancing;

    //timers for wake up
    public float wakeUpTime;
    float wakingUp = 0;

    //refs for waking bar object
    GameObject wakingBar;
    WakingBar wakingBarScript;
    Image wakingBarImage;
    RectTransform wakingBarRT;

    //audio stuff
    AudioSource myAudio;
    public AudioClip[] snores, yawns, singing;

    void Start () {
        _manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        cursor = GameObject.FindGameObjectWithTag("Symbol").GetComponent<FollowMouse>();

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

        //randomize audio
        int randomSnore = Random.Range(0, snores.Length);

        myAudio = GetComponent<AudioSource>();
        myAudio.clip = snores[randomSnore];
        myAudio.Play();
    }

    void OnMouseOver()
    {
        cursor.sr.sprite = cursor.touching;

        if(asleep && wakingUp < wakeUpTime)
        {
            if (_manager.randomMemory )
            {
                if(_manager.sleepers[_manager.awakenedSleepers] == gameObject)
                {
                    wakingBarScript.CorrectUIPos(transform, wakingBarRT, 0, 100);
                    wakingBarImage.fillAmount += 1 / wakeUpTime * Time.deltaTime;
                    wakingUp += Time.deltaTime;
                }
            }
            else
            {
                wakingBarScript.CorrectUIPos(transform, wakingBarRT, 0, 100);
                wakingBarImage.fillAmount += 1 / wakeUpTime * Time.deltaTime;
                wakingUp += Time.deltaTime;
            }
        }
        else if (asleep && wakingUp > wakeUpTime)
        {
            sleeping.SetActive(false);
            awake.SetActive(true);
            asleep = false;
            _manager.awakenedSleepers++;

            //stop snoring and yawn
            myAudio.loop = false;
            myAudio.Stop();
            int randomYawn = Random.Range(0, yawns.Length);
            myAudio.PlayOneShot(yawns[randomYawn]);
        }

        if (timeToParty)
        {
            myAudio.spatialBlend = 0;
            //sing when mouse over
            if (!myAudio.isPlaying)
            {
                myAudio.loop = true;
                int randomSing = Random.Range(0, singing.Length);
                myAudio.clip = singing[randomSing];
                myAudio.PlayOneShot(myAudio.clip);
            }
                
        }
    }

    void OnMouseExit()
    {
        cursor.sr.sprite = cursor.normal;

        if (asleep)
        {
            wakingUp = 0;
        }

        wakingBarImage.fillAmount = 0;
    }
}
