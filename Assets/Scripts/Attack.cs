using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float decayTime = 0.1f;

    private int[] HEARTS = new int[] { 1, 2, 3, 4 };
    private float[] KNOCKBACK = new float[] { 3, 6, 9, 12 };
    private SpriteRenderer sr;

    public int hearts;
    public float knockback;
    public Sprite[] attackSprites;

	private void Awake()
	{
        sr = GetComponent<SpriteRenderer>();
	}

	public void Activate(int power)
    {
        sr.sprite = attackSprites[power];
        hearts = HEARTS[power];
        knockback = KNOCKBACK[power];
        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
	{
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
	}
}
