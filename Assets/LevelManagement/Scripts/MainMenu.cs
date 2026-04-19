using UnityEngine;

namespace ONI.Menus
{
    public class MainMenu : Menu<MainMenu>
    {
        public void OnSettings()
        {
            SettingsMenu.Open();
        }

        public void OnNewGamePressed()
        {
            //open new game menu
            GameManager.Instance.LoadNewGame();
            MenuManager.Instance.DelayClose(1f);
        }

        public void OnContinue()
        {
            //open load game menu
        }

        public void OnCredits()
        {
            CreditsMenu.Open();
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}