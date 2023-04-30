using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour {
	private static BlackScreen instance { get; set; }

	[SerializeField] protected Image _image;
	[SerializeField] protected Color _opaque;
	[SerializeField] protected Color _clear;

	public static IEnumerator Fade(bool toOpaque, float time) {
		instance._image.enabled = true;
		if (time > 0) {
			for (var lerp = 0f; lerp <= 1; lerp += Time.deltaTime / time) {
				instance._image.color = Color.Lerp(instance._clear, instance._opaque, toOpaque ? lerp : 1 - lerp);
				yield return null;
			}
		}
		instance._image.color = toOpaque ? instance._opaque : instance._clear;
		instance._image.enabled = toOpaque;
	}

	private void Awake() {
		if (instance) {
			Destroy(transform.root.gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(transform.root.gameObject);
		_image.color = _opaque;
	}
}