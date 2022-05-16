using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackTextPoster : MonoBehaviour
{
    [SerializeField] float textFadeDelay = 1f;
    [SerializeField] float textFadeOutTime = 1f;
    Coroutine currentFadeRoutine;
    Text text;
    Image image;
    List<CanvasRenderer> canvasRenderers = new List<CanvasRenderer>();
    float alpha = 1f;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();

        foreach (CanvasRenderer c in GetComponentsInChildren<CanvasRenderer>(true))
        {
            canvasRenderers.Add(c);
            c.SetAlpha(0);
        }

        text.enabled = true;
        image.enabled = true;
    }

    private void OnEnable()
    {
        PlayerController.LogNothingHappened += PostNothingHappened;
        Obstacle.LogFailTry += PostFailMessage;
        InventoryItem.LogExamineText += PostExamineMessage;
    }

    private void OnDisable()
    {
        PlayerController.LogNothingHappened -= PostNothingHappened;
        Obstacle.LogFailTry -= PostFailMessage;
        InventoryItem.LogExamineText -= PostExamineMessage;
    }

    void SetMessage(string examineText)
    {
        ResetAlpha();
        text.text = examineText;

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
    }

    void ResetAlpha()
    {
        foreach (CanvasRenderer c in canvasRenderers)
        {
            alpha = 1f;
            c.SetAlpha(1f);
        }
    }

    void PostExamineMessage(string message)
    {
        SetMessage(message);
    }

    void PostFailMessage(string message)
    {
        SetMessage(message);
    }

    void PostNothingHappened()
    {
        SetMessage("Nothing Happened");
    }
}
