using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIImage : ControlTutorial
{
    private Image img;

    protected override void Start()
    {
        img = GetComponent<Image>();
        base.Start();
    }

	protected override void SetSprite(Sprite sprite)
	{
        img.sprite = sprite;
        img.SetNativeSize();

    }
}
