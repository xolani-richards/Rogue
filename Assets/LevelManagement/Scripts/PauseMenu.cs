// using GameDevTV.Saving;
using UnityEngine;
// using ONI.Events;

namespace ONI.Menus
{
    public class PauseMenu: Menu<PauseMenu> //, IObserver
    {
        string _name = "NewSave";
        public void OnSettings()
        {
            SettingsMenu.Open();
        }
        public void OnResume ()
        {
            GameManager.Instance.SwitchState(GameState.PLAYING);
            MenuManager.Instance.CloseMenu();
        }

        public override void OnBackPressed()
        {
            GameManager.Instance.ReturnToMainMenu();
            MenuManager.Instance.DelayClose(1f); 
        }

        public void OnSave ()
        {
            // SavingSystem.Instance.Save(_name);
            // EventBus.Instance.Publish(EventTypes.GAME_EVENT_SAVE, null);
            OnResume();
        }

        public void OnLoad ()
        {
            // EventBus.Instance.Publish(GameEventType.GAME_EVENT_LOAD, null);
            // SavingSystem.Instance.Load(_name);
            // MenuManager.Instance.CloseMenu();
        }

        // void OnEnable()
        // {
        //     Debug.Log("Enabled");
        //     // EventBus.Instance.Register(EventTypes.PLAYER_INPUT_ESC, this);
        // }

        // void OnDisable ()
        // {
        //     Debug.Log("Disabled");
        //     // EventBus.Instance.Unregister(EventTypes.PLAYER_INPUT_ESC, this);
        // }

        // public void OnNotify(EventTypes eventType, object data)
        // {
        //     if(eventType == EventTypes.PLAYER_INPUT_ESC) OnResume();
        // }
    }
}