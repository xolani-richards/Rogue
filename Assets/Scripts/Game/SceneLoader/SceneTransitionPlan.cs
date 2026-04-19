using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPlan
    {
        public Dictionary<string, string> scenesToLoad { get; } = new ();
        public List<string> scenesToUnload { get; } = new ();
        public string ActiveSceneName { get; private set; }
        public bool clearUnusedAssets { get; private set; } = false;
        public bool overlay { get; private set; } = false;
        public bool loadingMenu { get; private set; } = false;
        public bool clearAllScenes { get; private set; } = false;

        public SceneTransitionPlan Load (string slotKey, string sceneName, bool setActive = false)
        {
            scenesToLoad[slotKey] = sceneName;
            if (setActive) ActiveSceneName = sceneName;
            return this;
        }

        public SceneTransitionPlan Unload(string slotKey)
        {
            scenesToUnload.Add(slotKey);
            return this;
        }

        public SceneTransitionPlan UnloadAll()
        {
            clearAllScenes = true;
            return this;
        }

        public SceneTransitionPlan WithOverlay ()
        {
            overlay = true;
            return this;
        }

        public SceneTransitionPlan WithLoadingMenu ()
        {
            loadingMenu = true;
            return this;
        }

        public SceneTransitionPlan WithClearUnusedAssets ()
        {
            clearUnusedAssets = true;
            return this;
        }

        public Coroutine Perform ()
        {
            return SceneController.Instance.ExecutePlan(this);
        }
    }