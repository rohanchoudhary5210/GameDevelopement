public void FadeCanvasGroup(CanvasGroup canvasGroup, bool fadeIn)
    {
        StartCoroutine(FadeRoutine(canvasGroup, fadeIn));
    }
    private IEnumerator FadeRoutine(CanvasGroup canvasGroup, bool fadeIn)
    {

        float startAlpha = canvasGroup.alpha;
        float endAlpha = fadeIn ? 1f : 0f;
        float time = 0f;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        canvasGroup.interactable = fadeIn;
        canvasGroup.blocksRaycasts = fadeIn;
    }
