using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Sprite heartFilledSprite;
    public Sprite heartEmptySprite;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public TextMeshProUGUI gemText;

    private Persistent persistent;
    private Player player;

    private void Start()
    {
        persistent = FindObjectOfType<Persistent>();
        player = FindObjectOfType<Player>();

        if (persistent.sacrificedHearts)
		{
            heart2.enabled = false;
            heart3.enabled = false;
		}
    }

    private void Update()
    {
        heart1.sprite = GetHeartSprite(1);
        heart2.sprite = GetHeartSprite(2);
        heart3.sprite = GetHeartSprite(3);

        gemText.text = persistent.gems.ToString();
    }

    private Sprite GetHeartSprite(int heart)
	{
        int hearts = player.GetHearts();
        return hearts >= heart ? heartFilledSprite : heartEmptySprite;
	}
}
