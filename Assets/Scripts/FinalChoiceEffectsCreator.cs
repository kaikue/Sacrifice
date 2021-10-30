using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChoiceEffectsCreator : MonoBehaviour
{
    private FinalChoiceEffects fce;
    public GameObject fcePrefab;

    private void Start()
    {
        fce = Instantiate(fcePrefab, Vector3.zero, Quaternion.identity).GetComponent<FinalChoiceEffects>();
    }

    public void Rumble()
	{
        fce.StartRumbling();
	}

    public void Fade()
	{
        fce.FadeToWhite();
	}
}
