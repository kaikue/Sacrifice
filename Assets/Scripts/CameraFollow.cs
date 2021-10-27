using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
	public Transform cameraObj;

	private const int SHAKES = 10;
	private const float SHAKE_DISTANCE = 0.2f;
	private const float SHAKE_TIME = 0.02f;

	private void Start()
	{
		player = FindObjectOfType<Player>().gameObject;
	}

	public void Shake()
	{
		StartCoroutine(CrtShake());
	}

	private IEnumerator CrtShake()
	{
		for (int i = 0; i < SHAKES; i++)
		{
			cameraObj.localPosition = new Vector3(Random.Range(-SHAKE_DISTANCE, SHAKE_DISTANCE), Random.Range(-SHAKE_DISTANCE, SHAKE_DISTANCE), 0);
			yield return new WaitForSeconds(SHAKE_TIME);
		}
		cameraObj.localPosition = Vector3.zero;
	}

	private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }
}
