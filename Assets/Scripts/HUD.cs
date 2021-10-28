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
    public Image gemImage;
    public TextMeshProUGUI gemText;

    private Persistent persistent;
    private Player player;

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

        player = FindObjectOfType<Player>();

        RefreshHearts();
    }

    private void Update()
    {
        heart1.sprite = GetHeartSprite(1);
        heart2.sprite = GetHeartSprite(2);
        heart3.sprite = GetHeartSprite(3);

        int gems = persistent.GetTotalGems();
        if (gems == 0)
        {
            gemImage.gameObject.SetActive(false);
            gemText.gameObject.SetActive(false);
        }
        else
        {
            gemImage.gameObject.SetActive(true);
            gemText.gameObject.SetActive(true);
            gemText.text = gems.ToString();
        }
    }

    private Sprite GetHeartSprite(int heart)
	{
        int hearts = player.GetHearts();
        return hearts >= heart ? heartFilledSprite : heartEmptySprite;
	}

    public void RefreshHearts()
	{
        if (persistent.sacrificedHearts)
        {
            heart2.enabled = false;
            heart3.enabled = false;
        }
    }
}
