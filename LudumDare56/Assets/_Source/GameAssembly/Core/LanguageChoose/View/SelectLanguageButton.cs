using UnityEngine;
using UnityEngine.UI;

namespace Core.LanguageChoose.View
{
    public class SelectLanguageButton : MonoBehaviour
    {
        [SerializeField] private string selectCode;
        [SerializeField] private Button targetButton;
        [SerializeField] private GameObject storyPanel;
        [SerializeField] private GameObject languagePanel;
        [SerializeField] private LanguageStartSetter languageSetter;

        private void Start() => targetButton.onClick.AddListener(SelectLanguage);

        private void SelectLanguage()
        {
            languagePanel.SetActive(false);
            PlayerPrefs.SetString("Language", selectCode);
            storyPanel.SetActive(true);
            targetButton.onClick.RemoveAllListeners();
            languageSetter.ChangeLanguage();
        }
    }
}