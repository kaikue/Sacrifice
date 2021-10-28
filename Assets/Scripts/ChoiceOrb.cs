using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceOrb : MonoBehaviour
{
    protected Persistent persistent;
    public GameObject activateEffectPrefab;
    public GameObject activateDialog;

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
    }

    public virtual void Activate()
	{
        Dialog[] existingDialogs = FindObjectsOfType<Dialog>();
        foreach (Dialog existingDialog in existingDialogs)
		{
            Destroy(existingDialog.gameObject);
		}
        Instantiate(activateEffectPrefab, transform.position, Quaternion.identity);
        Instantiate(activateDialog, transform.position, Quaternion.identity);
        Destroy(gameObject);
	}
}
