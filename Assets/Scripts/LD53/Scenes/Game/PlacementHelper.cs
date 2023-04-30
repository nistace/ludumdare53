using LD53.Inputs;
using LD53.Scenes.Game.Data;
using UnityEngine;

namespace LD53.Scenes.Game {
	public static class PlacementHelper {
		public static Vector2Int GetAimWorldPosition() {
			var worldPoint = GameSceneSetup.camera.ScreenToWorldPoint(GameInput.controls.Placement.Aim.ReadValue<Vector2>());
			return new Vector2Int(Mathf.FloorToInt(worldPoint.x + .5f), Mathf.FloorToInt(worldPoint.y + .5f));
		}
	}
}