using UnityEngine;
using Utils;

namespace Core.Menu
{
	public class MainMenuStory : MonoBehaviour
	{
		[SerializeField] private TextOutFader[] textFader;
		[SerializeField] private GameObject storyPanel;

		private int _index;
		
		private void Update()
		{
			if(Input.anyKeyDown)
				NextText();	
		}

		private void NextText()
		{
			if (_index >= textFader.Length)
			{
				storyPanel.SetActive(false);
				return;
			}
			
			if(_index > 0)
				textFader[_index - 1].FadeIn();
			
			textFader[_index].gameObject.SetActive(true);
			
			_index++;
		}
	}
}
