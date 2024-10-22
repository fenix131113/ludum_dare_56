using TMPro;
using UnityEngine;

namespace Core.LanguageChoose
{
    [RequireComponent(typeof(TMP_Text))]
    public class LanguageLabel : MonoBehaviour
    {
        [SerializeField] private string enText;
        [SerializeField] private string ruText;

        private TMP_Text _targetText;

        public void SetText(bool russian)
        {
            _targetText = GetComponent<TMP_Text>();
            _targetText.text = russian ? ruText : enText;
        }
    }
}