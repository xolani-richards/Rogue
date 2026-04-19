using System.Collections;
using UnityEngine;
// using MEC;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen instance;

    [SerializeField] CanvasGroup canvasGroup;
    [field: SerializeField] public float progress { get; protected set; }
    

    void Awake()
    {
        if(instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);

        if(canvasGroup == null) canvasGroup = GetComponentInChildren<CanvasGroup>();

        canvasGroup.alpha = 0f;
        // FadeIn(2f);
    }

    public IEnumerator FadeToBlack (float duration)  { 
        yield return Fade(duration, 0f, 1f);
    }
    public IEnumerator FadeToClear (float duration) { 
        yield return Fade(duration, 1f, 0f);
    }

    IEnumerator Fade(float duration, float start, float end)
    {
        progress = 0;
        canvasGroup.alpha = start;
        while (progress < 1f)
        {
            progress += Time.deltaTime / duration;
            float value = Mathf.Lerp(start, end, progress);
            canvasGroup.alpha = value;
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}
