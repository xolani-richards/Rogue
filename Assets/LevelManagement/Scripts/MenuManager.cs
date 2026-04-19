using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace ONI.Menus
{
    public class MenuManager: MonoBehaviour
    {
        [SerializeField] Menu settingMenuPrefab;
        [SerializeField] Menu mainMenuPrefab;
        [SerializeField] Menu creditsMenuPrefab;
        [SerializeField] Menu loadGameMenuPrefab;
        [SerializeField] Menu pauseMenuPrefab;
        [SerializeField] Menu inGameMenuPrefab;
        [SerializeField] Menu loadingMenuPrefab;
        [SerializeField] Menu startGameMenuPrefab;

        [SerializeField] private Transform _menuParent;
        private Stack<Menu> _menuStack = new ();
        Menu currentMenu;

        static MenuManager _instance;
        public static MenuManager Instance => _instance;

        void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("duplicate menuManager" + gameObject.name);
                Destroy(gameObject);
                return;
            }
            _instance = this;
            InitalizeMenus();
            DontDestroyOnLoad(gameObject);
        }

        void OnDestroy() => Debug.Log("menu manager destroyed!");

        private void InitalizeMenus()
        {
            if (_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }

            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            FieldInfo[] fields = this.GetType().GetFields(myFlags);

            foreach (FieldInfo field in fields)
            {
                Menu prefab = field.GetValue(this) as Menu;

                if (prefab != null)
                {
                    Menu menuInstance = Instantiate(prefab, _menuParent);
                    menuInstance.gameObject.SetActive(false);
                    // if (prefab != mainMenuPrefab)
                    // {
                    //     menuInstance.gameObject.SetActive(false);
                    // }
                    // else
                    // {
                    //     OpenMenu(menuInstance);
                    // }
                }
            }
        }

        public void OpenMenu(Menu menuInstance)
        {
            if(menuInstance == null) {
                Debug.LogWarning("MENUMANAGER OpenMenu ERROR: menuInstance is null");
                return;
            }
            if(_menuStack.Count > 0)
            {
                foreach (Menu menu in _menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }
           
            menuInstance.gameObject.SetActive(true);
            _menuStack.Push(menuInstance);
        }

        public void CloseMenu()
        {
            if(_menuStack.Count == 0) {
                Debug.LogWarning("MENUMANAGER CloseMenu ERROR: menuStack is empty");
                return;
            }

            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if(_menuStack.Count > 0)
            {
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }

        public void DelayClose(float delay)
        {
            Invoke("CloseMenu", delay);
        }

        public void CloseAll()
        {
            if (_menuStack.Count == 0)
            {
                Debug.LogWarning("MENUMANAGER CloseMenu ERROR: menuStack is empty");
                return;
            }
            while (_menuStack.Count > 0) CloseMenu();
            Debug.Log("All menus closed");
        }
    }
}