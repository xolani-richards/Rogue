using System;
using TMPro;
using UnityEngine;

public class SelectionPopup: MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text content;
    [SerializeField] GameObject RejectBTN;
    Action onAccept;
    Action onReject;

    public void Bind(string title, string content, Action accept, Action reject = null)
    {
        this.title.text = title;
        this.content.text = content;
        this.onAccept = accept;
        this.onReject = reject;
        if(reject == null) RejectBTN.SetActive(false);
    }

    public void OnAccept () {
        onAccept?.Invoke();
        Close();
    }
    public void OnReject () {
        onReject?.Invoke();
        Close();
    }
    
    void Close ()
    {
        Destroy(gameObject, 0.2f);
    }

}