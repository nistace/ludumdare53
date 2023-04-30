using LD53.Data;
using LD53.Scenes.Game.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.GameStates;
using Utils.Libraries;

namespace LD53.Scenes.Game {
	public class GameSceneManager : MonoBehaviour {
		[FormerlySerializedAs("_data")] [SerializeField] protected GameSceneSetup         _sceneSetup;
		[SerializeField] protected LibraryBuildData _libraryBuildData;
		[SerializeField] protected GameSequence     _sequence;

		public void Start() {
			_libraryBuildData.Build();
			GameSceneSetup.instance = _sceneSetup;
			GameProgressKeeper.sequence = _sequence;
			GameProgressKeeper.levelIndex = 0;
			GameState.ChangeState(LoadLevelGameState.state);
		}
	}
}