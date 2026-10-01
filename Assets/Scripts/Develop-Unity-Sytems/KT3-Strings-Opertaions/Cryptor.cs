using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cryptor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;

    [TextAreaAttribute(5, 10)]
    [SerializeField] private string userText;

    //[SerializeField] private Button decryptButton;
    //[SerializeField] private Button peelButton;
    //[SerializeField] private Button correctButton;
    //[SerializeField] private Button encryptButton;
    //[SerializeField] private Button ressetButton;

    //private bool isClicked;

    void Start()
    {
        if (textObject != null)
        {
            // переделать под динамическое
            textObject.text = userText;
        }
    }
    void Update()
    {
        
    }

    void CheckStatus()
    {
        
    }

    void RessetText()
    {
        userText = ("");
    }
}
