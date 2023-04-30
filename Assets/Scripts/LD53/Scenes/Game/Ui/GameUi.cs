using UnityEngine;

namespace LD53.Scenes.Game.Ui {
	public class GameUi : MonoBehaviour {
		private static GameUi instance { get; set; }

		[SerializeField] protected PlacementButtonLineUi _placementButtonLine;

		public static PlacementButtonLineUi placementButtonLine => instance._placementButtonLine;

		private void Awake() {
			instance = this;
		}

		public void Start() {
			placementButtonLine.SetHidden();
		}
	}
}