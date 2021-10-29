using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceOrbActivator : MonoBehaviour
{
	private bool playerInside = false;
	private bool shouldActivate = false;
	public GameObject orb;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject collider = collision.gameObject;
		Player player = collider.GetComponent<Player>();
		if (player != null)
		{
			playerInside = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		GameObject collider = collision.gameObject;
		Player player = collider.GetComponent<Player>();
		if (player != null)
		{
			playerInside = false;
		}
	}

	public void Activate()
	{
		shouldActivate = true;
	}

	private void Update()
	{
		if (shouldActivate && !playerInside)
		{
			shouldActivate = false;
			orb.SetActive(true);
		}
	}
}
