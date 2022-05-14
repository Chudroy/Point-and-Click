using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.SceneManagement
{
    public class TransitionFader : MonoBehaviour
    {
        Coroutine currentActiveFade = null;
        Image image;
        float alpha = 0f;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void FadeOutImmediate()
        {
            alpha = 1f;
            image.color = new Color(0, 0, 0, alpha);
        }
        public Coroutine FadeOut(float time)
        {
            if (currentActiveFade != null) StopCoroutine(currentActiveFade);
            currentActiveFade = StartCoroutine(FadeOutRoutine(time));
            return currentActiveFade;
        }
        public Coroutine FadeIn(float time)
        {
            if (currentActiveFade != null) StopCoroutine(currentActiveFade);
            currentActiveFade = StartCoroutine(FadeInRoutine(time));
            return currentActiveFade;
        }

        IEnumerator FadeOutRoutine(float time)
        {
            while (alpha <= 1)
            {
                alpha += Time.deltaTime / time;
                image.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
        IEnumerator FadeInRoutine(float time)
        {
            while (alpha >= 0)
            {
                alpha -= Time.deltaTime / time;
                image.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
    }
}
