using System.Collections.Generic;
using UnityEngine;
using MEC;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen instance;

    [SerializeField] CanvasGroup canvasGroup;
    [field: SerializeField] public float progress { get; protected set; }
    

    void Awake()
    {
        if(!ServiceLocator.Register<FadeScreen>(this)) Destroy(gameObject);
        if(canvasGroup == null) canvasGroup = GetComponentInChildren<CanvasGroup>();

        canvasGroup.alpha = 0f;
        // FadeIn(2f);
    }

    public void FadeIn(float speed)
    {
        Timing.RunCoroutine(Fade(speed, 1f, 0f));
    }

    public void FadeOut(float speed)
    {
        Timing.RunCoroutine(Fade(speed, 0f, 1f));
    }

    IEnumerator<float> Fade(float duration, float start, float end)
    {
        progress = 0;
        canvasGroup.alpha = start;
        while (progress < 1f)
        {
            progress += Timing.DeltaTime / duration;
            float value = Mathf.Lerp(start, end, progress);
            canvasGroup.alpha = value;
            yield return value;
        }
        canvasGroup.alpha = end;
    }
}
