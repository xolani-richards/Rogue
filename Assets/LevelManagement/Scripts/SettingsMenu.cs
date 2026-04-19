namespace ONI.Menus
{
    public class SettingsMenu: Menu<SettingsMenu>
    {
        public void OnNewGamePressed()
        {
            //open new game menu
            MenuManager.Instance.CloseMenu();
            GameManager.Instance.LoadNewGame();
        }
    }
}