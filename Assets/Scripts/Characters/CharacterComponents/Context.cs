using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Context
{
    public Transform target;
    public Vector3 destination;
    [SerializeField] List<string> dataList = new ();
    Dictionary<string, float> data = new ();
    public Action onUpdate;

    public float GetData(string key)
    {
        if (data.ContainsKey(key)) return data[key];
        else return -1f;
    }

    public void SetData(string key, float value)
    {
        Debug.Log("Setting value");
        if(!data.ContainsKey(key)) data.Add(key, 0f);
        data[key] = value;
        UpdateDataList();
        onUpdate?.Invoke();
    }

    void UpdateDataList ()
    {
        dataList.Clear();
        foreach(KeyValuePair<string, float> kvp in data) 
        {
            dataList.Add($"{kvp.Key}: {kvp.Value}");
        }
    }
}