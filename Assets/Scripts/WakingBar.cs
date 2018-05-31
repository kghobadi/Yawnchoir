using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakingBar : MonoBehaviour {

    RectTransform canvasRect;
    Camera cammy;

    // Use this for initialization
    void Start () {
        canvasRect = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
        cammy = Camera.main;
    }
	

    //move UI to where signpost is on screen
    public void CorrectUIPos(Transform worldObject, RectTransform uiObject, float xAdjust, float yAdjust)
    {
        Vector2 ViewportPosition = cammy.GetComponent<Camera>().WorldToViewportPoint(worldObject.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f) + xAdjust),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f) + yAdjust));

        //now you can set the position of the ui element
        uiObject.anchoredPosition = WorldObject_ScreenPosition;
    }

}
