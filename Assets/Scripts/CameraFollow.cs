using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

	private void Start()
	{
		player = FindObjectOfType<Player>().gameObject;
	}

	private void Update()
    {
		float edgeWidth = 1.5f;
		float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
		float leftPos = GameObject.FindGameObjectWithTag("BarrierLeft").transform.position.x + edgeWidth;
		float rightPos = GameObject.FindGameObjectWithTag("BarrierRight").transform.position.x - edgeWidth;
		float camX = Mathf.Clamp(player.transform.position.x, leftPos + camHalfWidth, rightPos - camHalfWidth);
		transform.position = new Vector3(camX, transform.position.y, transform.position.z);
    }
}
