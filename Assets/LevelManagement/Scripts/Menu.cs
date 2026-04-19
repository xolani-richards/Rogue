using UnityEngine;

namespace ONI.Menus
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T _instance;
        public static T Instance =>_instance; 

        protected virtual void Awake ()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
                Debug.Log($"Creating menu: {name}");
            }
        }

        protected virtual void OnDestroy()
        {
            Debug.Log($"Destroying menu: {name}");
        }

        public static void Open()
        {
            if (MenuManager.Instance == null) Debug.Log("No menu manager");
            if (MenuManager.Instance != null && _instance != null)
            {
                MenuManager.Instance.OpenMenu(_instance);
            }
            else Debug.Log("Menu not available");
        }
    }
    public abstract class Menu: MonoBehaviour
    {
        public virtual void OnBackPressed ()
        {
            MenuManager.Instance.CloseMenu();
        }
        
    }
}