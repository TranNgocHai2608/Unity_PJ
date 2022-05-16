using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<floatingText> floatingTexts = new List<floatingText>();

    private void Update()
    {
        foreach(floatingText txt in floatingTexts)
        {
            txt.updateFloatingText();
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingText floatingText = GetFloatingText();
        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;

        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); //Tranfer world space to screne space so we can use it in the UI
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.show();
    }

    private floatingText GetFloatingText()
    {
        floatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new floatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
