using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image _fadePanel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Transition()
    {
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        yield return StartCoroutine(FadeToBlack());

        SceneManager.LoadScene("KT_Corutines_Gameplay");

        yield return StartCoroutine(FadeToScreen());
    }

    private IEnumerator FadeToBlack()
    {
        for (float i = 0; i <= 1f; i += 0.05f)
        {
            _fadePanel.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.05f);
        }

        _fadePanel.color = new Color(0, 0, 0, 1f);
    }

    private IEnumerator FadeToScreen()
    {
        for (float i = 1f; i >= 0f; i -= 0.05f)
        {
            _fadePanel.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.05f);
        }

        _fadePanel.color = new Color(0, 0, 0, 0f);
    }
}