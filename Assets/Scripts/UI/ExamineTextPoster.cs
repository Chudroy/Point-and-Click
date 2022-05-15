using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamineTextPoster : MonoBehaviour
{
    [SerializeField] float textFadeDelay = 1f;
    [SerializeField] float textFadeOutTime = 1f;
    Coroutine currentFadeRoutine;
    Text text;
    List<CanvasRenderer> canvasRenderers = new List<CanvasRenderer>();
    float alpha = 1f;

    public static ExamineTextPoster GetExamineTextPoster()
    {
        var UICanvas = GameObject.FindWithTag("UICanvas");
        return UICanvas.GetComponentInChildren<ExamineTextPoster>(true);
    }

    private void Awake()
    {
        text = GetComponentInChildren<Text>();

        foreach (CanvasRenderer c in GetComponentsInChildren<CanvasRenderer>(true))
        {
            canvasRenderers.Add(c);
        }

    }

    private void Update()
    {
        // DebugCanvasRenderers();
    }

    private void DebugCanvasRenderers()
    {
        Debug.Log(canvasRenderers[1].GetAlpha());
    }

    public void SetExamineText(string examineText)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            ResetAlpha();
            this.gameObject.SetActive(true);
            text.text = examineText;
        }

        if (currentFadeRoutine != null)
        {
            StopCoroutine(currentFadeRoutine);
            ResetAlpha();
        }

        currentFadeRoutine = StartCoroutine(FadeOutRoutine(textFadeOutTime));


    }

    IEnumerator FadeOutRoutine(float time)
    {
        yield return new WaitForSeconds(textFadeDelay);

        while (alpha >= 0)
        {
            alpha -= Time.deltaTime / time;
            foreach (CanvasRenderer c in canvasRenderers)
            {
                c.SetAlpha(alpha);
            }
            yield return null;
        }

        if (alpha <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    void ResetAlpha()
    {
        foreach (CanvasRenderer c in canvasRenderers)
        {
            alpha = 1f;
            c.SetAlpha(1f);
        }
    }
}
