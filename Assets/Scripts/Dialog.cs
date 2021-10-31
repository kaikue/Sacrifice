using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    [System.Serializable]
    public class DialogLine
	{
        public string text;
        public float charWaitTime = 0.05f;
        public TMP_FontAsset font;
        public float fontSize = 36;
        public UnityEvent lineEvent;
    }

    private int currentLine = 0;
    private int posInLine = 0;
    public TextMeshProUGUI textObj;
    private bool progressing = true;
    private float charWaitTimer = 0;

    public DialogLine[] dialogLines;

    private void Awake()
    {
        textObj.text = "";
        textObj.font = dialogLines[0].font;
        textObj.fontSize = dialogLines[0].fontSize;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
		{
            if (progressing)
			{
                posInLine = dialogLines[currentLine].text.Length;
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
            if (charWaitTimer >= dialogLines[currentLine].charWaitTime)
			{
                charWaitTimer = 0;
                posInLine++;
                UpdateText();
            }
        }
    }

    private void UpdateText()
	{
        string currentText = dialogLines[currentLine].text;
        if (posInLine >= currentText.Length)
        {
            posInLine = currentText.Length;
            progressing = false;
        }
        string shownText = currentText.Substring(0, posInLine);
        string hiddenText = currentText.Substring(posInLine, currentText.Length - posInLine);
        textObj.text = shownText + "<color=black>" + hiddenText + "</color>";
    }

    private void AdvanceText()
    {
        charWaitTimer = 0;
        currentLine++;
        if (currentLine >= dialogLines.Length)
		{
            Destroy(gameObject);
            return;
		}
        dialogLines[currentLine].lineEvent.Invoke();
        textObj.font = dialogLines[currentLine].font;
        textObj.fontSize = dialogLines[currentLine].fontSize;
        posInLine = 0;
        progressing = true;
        UpdateText();
    }

    public void ActivateChoiceOrb()
	{
        FindObjectOfType<ChoiceOrbActivator>().Activate();
	}

    public void NextScene()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
