using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FadeOutScreen: MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] float fadeInTime = 0.5f;
    [SerializeField] float fadeOutTime = 0.5f;
    [SerializeField] AnimationCurve fadeCurve = new();

    private static FadeOutScreen _instance;
    public static FadeOutScreen Instance => _instance;

    public bool Completed = false;
    Color _color;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _color = _image.color;
        _image.gameObject.SetActive(false);
    }

    public void Hide ()
    {
        _image.gameObject.SetActive(false);
    }

    public IEnumerator OnFadeIn ()
    {
        yield return OnFadeIn(fadeInTime);
    }

    public IEnumerator OnFadeIn (float fadeTime)
    {
        transform.SetAsLastSibling();
        Completed = false;
        yield return StartCoroutine(FadeIn(fadeTime));
    }

    public IEnumerator OnFadeOut ()
    {
        yield return OnFadeOut(fadeOutTime);
    }

    public IEnumerator OnFadeOut (float fadeTime)
    {
        Completed = false;
        yield return StartCoroutine(FadeOut(fadeTime));
    }

    IEnumerator FadeIn (float fadeInTime)
    {
        float fade = 0;
        float time = 0;
        float deltaTime = fadeInTime/100;
        _image.gameObject.SetActive(true);
        while(fade < 1f)
        {
            fade = fadeCurve.Evaluate(time);
            time += deltaTime;
            _color = new Color(_color.r, _color.g, _color.b, fade);
            _image.color = _color;
            yield return new WaitForSecondsRealtime(deltaTime);
        }
        Completed = true;
        
    }

    IEnumerator FadeOut (float fadeOutTime) 
    {
        float fade = 1;
        float time = 1;
        float deltaTime = fadeOutTime/100;
        _image.gameObject.SetActive(true);
        while(fade > 0f)
        {
            fade = fadeCurve.Evaluate(time);
            time -= deltaTime;
            _color = new Color(_color.r, _color.g, _color.b, fade);
            _image.color = _color;
            yield return new WaitForSecondsRealtime(deltaTime);
        }
        _image.gameObject.SetActive(false);
        Completed = true;
    }
}