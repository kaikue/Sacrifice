using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKilled : MonoBehaviour
{
    private const float waitTime = 1;

    private void Start()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
	{
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
