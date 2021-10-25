using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private const float charWaitTime = 0.05f;
    private int currentLine = 0;
    private int posInLine = 0;
    public TextMeshProUGUI text;
    private bool progressing = true;
    private float charWaitTimer = 0;

    public string[] lines;

    private void Awake()
    {
        text.text = "";
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
		{
            if (progressing)
			{
                posInLine = lines[currentLine].Length;
                UpdateText();
			}
            else
			{
                AdvanceText();
			}
            return;
		}

        if (progressing)
        {
            charWaitTimer += Time.deltaTime;
            if (charWaitTimer >= charWaitTime)
			{
                charWaitTimer = 0;
                posInLine++;
                UpdateText();
            }
        }
    }

    private void UpdateText()
	{
        if (posInLine >= lines[currentLine].Length)
        {
            posInLine = lines[currentLine].Length;
            progressing = false;
        }
        string shownText = lines[currentLine].Substring(0, posInLine);
        string hiddenText = lines[currentLine].Substring(posInLine, lines[currentLine].Length - posInLine);
        text.text = shownText + "<color=black>" + hiddenText + "</color>";
    }

    private void AdvanceText()
    {
        charWaitTimer = 0;
        currentLine++;
        if (currentLine >= lines.Length)
		{
            Destroy(gameObject);
            return;
		}
        posInLine = 0;
        progressing = true;
        UpdateText();
    }
}
