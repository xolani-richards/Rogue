using System;
using System.Collections;
using System.Collections.Generic;
using ONI.Menus;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    #region Singleton
    public static SceneController Instance;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else if(Instance != this) Destroy(gameObject);
    }

    #endregion

    private Dictionary<string, string> loadedSceneBySlot = new ();
    private bool isBusy = false;

    public SceneTransitionPlan NewTransition ()
    {
        return new SceneTransitionPlan ();
    }

    public void UpdateSceneIndex(string slotKey, string sceneName)
    {
        if(!loadedSceneBySlot.ContainsKey(slotKey)) loadedSceneBySlot[slotKey] = sceneName;
    }

    public Coroutine ExecutePlan(SceneTransitionPlan plan)
    {
        if(isBusy)
        {
            Debug.LogWarning($"Scene change already in progess.");
            return null;
        }
        isBusy = true;
        return StartCoroutine(ChangeSceneRoutine(plan));
    }

    private IEnumerator ChangeSceneRoutine(SceneTransitionPlan plan)
    {
        float startTime = Time.time;
        if(plan.overlay) {
            yield return FadeScreen.instance.FadeToBlack(1f);
            yield return new WaitForSeconds(0.5f);
        }

        if(plan.loadingMenu) {
            LoadingMenu.Open();
            yield return FadeScreen.instance.FadeToClear(1f);
        }

        // MenuManager.Instance.CloseAll();
        if(plan.clearAllScenes) plan.scenesToUnload.AddRange(loadedSceneBySlot.Keys);

        foreach(string slotKey in plan.scenesToUnload)
        {
            yield return UnloadSceneRoutine(slotKey);    
        }

        if(plan.clearUnusedAssets) yield return ClearUnusedAssetsRoutine();

        foreach (var kvp in plan.scenesToLoad)
        {
            if(loadedSceneBySlot.ContainsKey(kvp.Key)) yield return UnloadSceneRoutine(kvp.Key);
            yield return LoadAdditiveRoutine(kvp.Key, kvp.Value, plan.ActiveSceneName == kvp.Value);
        }

        float runTime = Time.time - startTime;
        if(runTime < 2f) yield return new WaitForSeconds(2f - runTime);

        if(plan.loadingMenu) // Close loading Menu
        {
            yield return FadeScreen.instance.FadeToBlack(1f);
            MenuManager.Instance.CloseMenu();
            yield return new WaitForSeconds(0.5f);
        }

        if(plan.overlay) yield return FadeScreen.instance.FadeToClear(1f);
        isBusy = false;
    }

    private IEnumerator LoadAdditiveRoutine(string slotKey, string sceneName, bool setActive)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if(loadOp == null) yield break;
        loadOp.allowSceneActivation = false;
        while (loadOp.progress < 0.9f)
        {
            yield return null;
        }
        loadOp.allowSceneActivation = true;
        while (!loadOp.isDone)
        {
            yield return null;
        }

        if(setActive)
        {
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            if(newScene.IsValid() && newScene.isLoaded)
            {
                SceneManager.SetActiveScene(newScene);
            }
        }

        loadedSceneBySlot[slotKey] = sceneName;
    }

    private IEnumerator UnloadSceneRoutine(string slotKey)
    {
        if(!loadedSceneBySlot.TryGetValue(slotKey, out string sceneName)) yield break;
        if(string.IsNullOrEmpty(sceneName)) yield break;

        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneName);
        if(unloadOp != null)
        {
            while (!unloadOp.isDone) yield return null;
        }

        loadedSceneBySlot.Remove(slotKey);
    }

    private IEnumerator ClearUnusedAssetsRoutine()
    {
        AsyncOperation cleanupOp = Resources.UnloadUnusedAssets();
        while(!cleanupOp.isDone) yield return null;
    }

}