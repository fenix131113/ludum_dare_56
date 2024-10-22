using UnityEngine;

namespace Core.LanguageChoose
{
    public class LanguageStartSetter : MonoBehaviour
    {
        [SerializeField] private bool changeOnStart = true;
        [SerializeField] private LanguageLabel[] labels;

        private void Start()
        {
            if(!changeOnStart)
                return;
            
            ChangeLanguage();
        }

        public void ChangeLanguage()
        {
            foreach (var label in labels)
                label.SetText(PlayerPrefs.GetString("Language") == "RU");
        }
    }
}