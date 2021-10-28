using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour
{
	public float distance = 0.1f;
    public Transform pupil;
	private Transform player;

	private void Start()
	{
		player = FindObjectOfType<Player>().transform;
		transform.rotation = Quaternion.identity;
	}

	private void Update()
    {
		Vector2 lookDir = (player.position - transform.position).normalized;
		pupil.localPosition = lookDir * distance;
    }
}
