// using ONI.Events;
using UnityEngine;

namespace ONI.Menus
{
    public class StartGameMenu : Menu<StartGameMenu>
    {
        public void StartGame () 
        {
            // EventBus.Instance.Publish(EventTypes.GAME_EVENT_PLAY);
            Time.timeScale = 1f;
            MenuManager.Instance.CloseMenu();
        }
    }
}
