using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAdapt : MonoBehaviour
{
    public CanvasScaler CanvasScaler;

    public float StandardWidth = 1280;

    public float StandardHeight = 720;

	// Use this for initialization
	void Awake ()
	{
	    float deviceWidth = Screen.width;
	    int deviceHeight = Screen.height;
	    float standardAspect = StandardWidth / StandardHeight;
	    float deviceAspect = deviceWidth / deviceHeight;
	    if (deviceAspect > standardAspect)
	    {
	        CanvasScaler.matchWidthOrHeight = 1;
        }
	    else
	    {
	        CanvasScaler.matchWidthOrHeight = 0;
        }
    }

}
