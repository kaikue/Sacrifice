using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistent : MonoBehaviour
{
    [HideInInspector]
    public bool destroying = false;
    [HideInInspector]
    public int gems = 0;
    [HideInInspector]
    public int possibleGems = 0;
    [HideInInspector]
    public float time = 0;
    [HideInInspector]
    public bool sacrificedDash = false;
    [HideInInspector]
    public bool sacrificedDoubleJump = false;
    [HideInInspector]
    public bool sacrificedHearts = false;

    private void Awake()
    {
        Persistent[] persistents = FindObjectsOfType<Persistent>();

        if (persistents.Length > 1)
        {
            destroying = true;
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        int levelGems = FindObjectsOfType<Gem>().Length;
        possibleGems += levelGems;
    }

    private void Update()
    {
        if (!SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            time += Time.deltaTime;
        }
    }
}
