using System.Collections;
using LD53.Scenes.Game.Data;
using LD53.Scenes.Game.Ui;
using Utils.Coroutines;
using Utils.GameStates;

namespace LD53.Scenes.Game {
	public class LoadLevelGameState : GameState {
		public static LoadLevelGameState state { get; } = new LoadLevelGameState();
		private LoadLevelGameState() { }

		protected override void Enable() { }
		protected override void SetListenersEnabled(bool enabled) { }
		protected override void Disable() { }

		protected override IEnumerator Continue() {
			yield return CoroutineRunner.Run(BlackScreen.Fade(true, 0));
			GameSceneSetup.levelBuilder.Build(GameProgressKeeper.GetCurrentLevel());
			yield return CoroutineRunner.Run(BlackScreen.Fade(false, .5f));
			yield return CoroutineRunner.Run(GameUi.placementButtonLine.Show(GameProgressKeeper.GetCurrentLevel(), 2));
			if (GameProgressKeeper.GetCurrentLevel().allowBeltPlacement) {
				ChangeState(BeltPlacementGameState.state);
			}
			else {
				MachinePlacementGameState.state.Prepare(GameProgressKeeper.GetCurrentLevel().availableMachineCounts[0].machinePrefab);
				ChangeState(MachinePlacementGameState.state);
			}
		}
	}
}