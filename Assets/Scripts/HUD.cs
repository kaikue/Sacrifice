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
    public GameObject staminaUI;
    public RectTransform staminaBar;
    public GameObject staminaRecharging;
    private float staminaBarWidth;
    private bool staminaLinger = false;
    private const float staminaLingerTime = 0.5f;
    private float staminaLingerTimer;

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
        staminaBarWidth = staminaBar.sizeDelta.x;
        staminaUI.SetActive(false);
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

        float stamina = player.GetStaminaPercent();

        if (stamina == 1 && staminaUI.activeSelf && !staminaLinger)
		{
            staminaLinger = true;
		}
        if (staminaLinger)
		{
            staminaLingerTimer += Time.deltaTime;
            if (staminaLingerTimer >= staminaLingerTime)
			{
                staminaLingerTimer = 0;
                staminaLinger = false;
			}
		}
        staminaUI.SetActive(stamina < 1 || staminaLinger);
        staminaRecharging.SetActive(player.GetStaminaRecharging());
        staminaBar.sizeDelta = new Vector2(staminaBarWidth * stamina, staminaBar.sizeDelta.y);
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
            //heart2.enabled = false;
            heart3.enabled = false;
        }
    }
}
