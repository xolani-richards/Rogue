using TMPro;
using UnityEngine;

namespace ONI.Menus
{
    public class LoadingMenu: Menu<LoadingMenu>
    {
        [SerializeField] TMP_Text description;

        public void SetDescription(string text) => description.text = text;
    }
}