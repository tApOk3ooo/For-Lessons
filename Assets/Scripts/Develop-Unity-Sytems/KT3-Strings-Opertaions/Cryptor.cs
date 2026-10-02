using System;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Cryptor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;

    [TextAreaAttribute(5, 10)]
    [SerializeField] private string startUserText;

    [SerializeField] private char decryptSymbol;

    [SerializeField] private string correctString;

    [SerializeField] private char trimSymbol;


    private string updatedText = null;

    private string decryptedText;

    private string correctedText;

    private string peeledText;

    private string encyptedText;



    void Start()
    {
        SetDefaultText();
    }

    private void SetDefaultText()
    {
        if (textObject != null)
        {
            textObject.text = startUserText;
        }
    }

    private void UpdateText()
    {
        textObject.text = updatedText;
    }

    public void Decypt()
    {
        if (updatedText == null)
        {
            decryptedText = startUserText.Replace("*", $"{decryptSymbol}");

            updatedText = decryptedText;
        }
        else if (updatedText != null)
        {
            updatedText = updatedText.Replace("*", $"{decryptSymbol}");
        }

        UpdateText();
    }

    public void Correct()
    {
        if (updatedText == null)
        {
            correctedText = startUserText.Replace(">", $"{correctString}");

            updatedText = correctedText;
        }
        else if(updatedText != null)
        {
            updatedText = updatedText.Replace(">", $"{correctString}");
        }

        UpdateText();

    }

    public void Peel()
    {
        if (updatedText == null)
        {
            peeledText = startUserText.Replace("$", "");

            updatedText = peeledText;

            updatedText = updatedText.TrimEnd(trimSymbol);
        }
        else if (updatedText != null)
        {
            updatedText = updatedText.Replace("$", "");

            updatedText = updatedText.TrimEnd(trimSymbol);
        }

        UpdateText();
    }

    public void Encrypt()
    {
        StringBuilder sb = new StringBuilder();

        if (updatedText == null)
        {
            encyptedText = startUserText.Replace("a", "5");
            updatedText = encyptedText;

            updatedText = updatedText.Replace("c", "1");

            updatedText = updatedText.Replace(" ", "");

            updatedText = updatedText.Replace(".", "143");

            updatedText = updatedText.Replace("o", "b");

            updatedText = updatedText.Replace("e", "%");
        }
        else if(updatedText != null)
        {
            updatedText = updatedText.Replace("a", "5");
            updatedText = updatedText.Replace("c", "1");
            updatedText = updatedText.Replace(" ", "");
            updatedText = updatedText.Replace(".", "143");
            updatedText = updatedText.Replace("o", "b");
            updatedText = updatedText.Replace("e", "%");

        }

        UpdateText();
    }

    public void RessetText()
    {
        textObject.text = startUserText;
        updatedText = null;
    }
}
