using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Extensions;

namespace LD53.Scenes.Menu {
	public class MenuSceneManager : MonoBehaviour {
		[SerializeField] protected Button _button;

		private void Start() {
			_button.onClick.AddListenerOnce(StartGame);
			StartCoroutine(BlackScreen.Fade(false, .5f));
		}

		private void StartGame() => StartCoroutine(DoStartGame());

		private IEnumerator DoStartGame() {
			yield return StartCoroutine(BlackScreen.Fade(true, .5f));
			SceneManager.LoadSceneAsync("Game");
			yield return null;
		}
	}
}