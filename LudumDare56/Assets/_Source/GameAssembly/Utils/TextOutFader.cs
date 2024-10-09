using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Utils
{
	public class TextOutFader : MonoBehaviour
	{
		[SerializeField] private TMP_Text label;
		[SerializeField] private float animTime;

		private void Start()
		{
			FadeOut();
		}

		private void FadeOut()
		{
			label.transform.DOLocalMoveY(0, animTime);
			DOTween.To(() => label.color, x => label.color = x, new Color(255, 255, 255, 1f), animTime);
		}
	}
}
