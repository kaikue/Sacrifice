using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Enemy
{
    public Sprite[] sprites;
    private SpriteRenderer sr;

    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
    }

	private void Update()
	{
        sr.sprite = sprites[hits - health];
	}
}
