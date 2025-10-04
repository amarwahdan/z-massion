using UnityEngine;

public class Screenshot : MonoBehaviour
{
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot("Screen_" + Random.Range(0, 999999).ToString() + ".png", 2);
        }
	}
}
