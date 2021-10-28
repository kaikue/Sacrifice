using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private const float decayTime = 0.1f;

    private int[] HEARTS = new int[] { 1, 2, 4, 6 };
    private float[] KNOCKBACK = new float[] { 3, 6, 9, 12 };

    public int hearts;
    public float knockback;

	public void Activate(int power)
    {
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
