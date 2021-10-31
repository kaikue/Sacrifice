using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject goodFinalDialog;
    public GameObject okFinalDialog;

    private int numEyes;
    private int numPillars;
    private FinalChoiceEffects effects;
    private BossSpawner[] bossSpawners;

    public GameObject[] enemySpawns;
    private int enemySpawnIndex = 0;

    private void Start()
    {
        numEyes = FindObjectsOfType<BossEye>().Length;
        numPillars = FindObjectsOfType<BossPillar>().Length;
        effects = GetComponent<FinalChoiceEffects>();
        bossSpawners = FindObjectsOfType<BossSpawner>();
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach(BossSpawner spawner in bossSpawners)
		{
            spawner.Spawn(enemySpawns[enemySpawnIndex]);
		}
        enemySpawnIndex++;
        if (enemySpawnIndex >= enemySpawns.Length)
		{
            enemySpawnIndex = 0;
		}
    }

    public void DestroyEye()
	{
        numEyes--;
        if (numEyes == 0)
        {
            Instantiate(goodFinalDialog, Vector3.zero, Quaternion.identity);
            effects.StartRumbling();
            effects.FadeToWhite("GoodEnd");
        }
        else
        {
            effects.StartRumbling(2);
            SpawnEnemies();
        }
    }

    public void DestroyPillar()
	{
        numPillars--;
        if (numPillars == 0)
		{
            Instantiate(okFinalDialog, Vector3.zero, Quaternion.identity);
            effects.StartRumbling();
            effects.FadeToWhite("OkEnd");
		}
        else
		{
            effects.StartRumbling(2);
            SpawnEnemies();
        }
	}
}
