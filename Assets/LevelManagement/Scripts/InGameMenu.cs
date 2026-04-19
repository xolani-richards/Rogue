namespace ONI.Menus
{
    public class InGameMenu: Menu<InGameMenu>
    {
        PlayerInput controls;

        protected override void Awake()
        {
            base.Awake();
            controls = new();
            controls.Player.Pause.performed += ctx => OnMenuPressed();
            controls.Player.Esc.performed += ctx => OnMenuPressed();
        }

        void OnEnable()
        {
            controls.Player.Enable();
        }

        void OnDisable()
        {
            controls.Player.Disable();
        }
        public void OnMenuPressed()
        {
            PauseMenu.Open();
        }
    }
}