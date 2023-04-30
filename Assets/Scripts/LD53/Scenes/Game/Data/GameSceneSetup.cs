using System;
using LD53.Data;
using LD53.Data.BeltsAndMachines;
using UnityEngine;

namespace LD53.Scenes.Game.Data {
	[Serializable]
	public class GameSceneSetup {
		public static GameSceneSetup instance          { get; set; }
		public static Camera         camera            => instance._camera;
		public static LevelBuilder   levelBuilder      => instance._levelBuilder;
		public static BeltNetwork    beltNetwork       => levelBuilder.beltNetwork;
		public static PlacementGhost placementRenderer => instance._placementGhost;

		[SerializeField] protected Camera         _camera;
		[SerializeField] protected LevelBuilder   _levelBuilder;
		[SerializeField] protected PlacementGhost _placementGhost;
	}
}