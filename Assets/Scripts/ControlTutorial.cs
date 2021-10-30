using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTutorial : MonoBehaviour
{
    public Sprite keyboardSprite;
    public Sprite controllerSprite;
    private SpriteRenderer sr;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (Input.GetJoystickNames().Length > 0)
        {
            SetSprite(controllerSprite);
        }
        else
		{
            SetSprite(keyboardSprite);
		}
    }

	private void Update()
	{
        KeyCode[] keyboardCodes = { KeyCode.Space, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.UpArrow,
            KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W, KeyCode.LeftShift, KeyCode.RightShift };
        foreach (KeyCode keyboardCode in keyboardCodes)
		{
            if (Input.GetKeyDown(keyboardCode))
			{
                SetSprite(keyboardSprite);
			}
        }

        KeyCode[] controllerCodes = { KeyCode.JoystickButton0, KeyCode.JoystickButton1, KeyCode.JoystickButton2, 
            KeyCode.JoystickButton3, KeyCode.JoystickButton4, KeyCode.JoystickButton5, KeyCode.JoystickButton6 };
        foreach (KeyCode controllerCode in controllerCodes)
        {
            if (Input.GetKeyDown(controllerCode))
            {
                SetSprite(controllerSprite);
            }
        }

        if (Input.GetAxis("ControllerHorizontal") != 0 || Input.GetAxis("ControllerVertical") != 0 || 
            Input.GetAxis("LTrigger") != 0 || Input.GetAxis("RTrigger") != 0)
		{
            SetSprite(controllerSprite);
        }
    }

    protected virtual void SetSprite(Sprite sprite)
	{
        sr.sprite = sprite;
	}
}
