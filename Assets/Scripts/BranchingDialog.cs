using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchingDialog : MonoBehaviour
{
    public GameObject noSacrificesDialog;
    public GameObject anySacrificesDialog;
    private Persistent persistent;

    private void Start()
    {
        Persistent[] persistents = FindObjectsOfType<Persistent>();
        foreach (Persistent p in persistents)
        {
            if (!p.destroying)
            {
                persistent = p;
                break;
            }
        }

        if (persistent.NumSacrifices() == 0)
		{
            Instantiate(noSacrificesDialog, Vector3.zero, Quaternion.identity);
		}
        else
		{
            Instantiate(anySacrificesDialog, Vector3.zero, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
