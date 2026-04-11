using System;
using UnityEngine;

public class UIManager: MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] SelectionPopup selectionPopUpPrefab;
    SelectionPopup popup;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        Time.timeScale = 0;
        ShowPopup("Start", "The game is ready to play!", () => {}, ()=> {});
    }

    public void ShowPopup (string title, string content, Action onAccept, Action onReject)
    {
        if (popup != null) popup.OnReject();
        popup = Instantiate (selectionPopUpPrefab, transform);
        popup.Bind(title, content, () => { onAccept(); OnClose(); }, () => { onReject(); OnClose(); });
        Time.timeScale = 0f;
    }

    void OnClose()
    {
        Debug.Log("Pop up closed");
        Time.timeScale = 1.0f;
    }
}