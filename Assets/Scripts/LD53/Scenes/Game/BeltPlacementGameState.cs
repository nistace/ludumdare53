using System.Collections;
using LD53.Data.BeltsAndMachines;
using LD53.Data.Common;
using LD53.Inputs;
using LD53.Scenes.Game.Data;
using LD53.Scenes.Game.Ui;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils.Extensions;
using Utils.GameStates;

namespace LD53.Scenes.Game {
	public class BeltPlacementGameState : GameState {
		private enum InteractionType {
			None   = 0,
			Place  = 1,
			Remove = 2,
		}

		public static BeltPlacementGameState state { get; } = new BeltPlacementGameState();

		private BeltPlacementGameState() { }

		private bool       wasInteractingLastFrame { get; set; }
		private Vector2Int previousPosition        { get; set; }

		protected override void Enable() {
			GameInput.controls.Placement.Enable();
			GameUi.placementButtonLine.SetBeltSelected();
		}

		protected override void SetListenersEnabled(bool enabled) {
			PlacementButtonUi.onMachineButtonClicked.SetListenerActive(HandleMachineButtonClicked, enabled);
		}

		private static void HandleMachineButtonClicked(IMachine machine) {
			MachinePlacementGameState.state.Prepare(machine);
			ChangeState(MachinePlacementGameState.state);
		}

		protected override IEnumerator Continue() {
			while (enabled) {
				var overUi = EventSystem.current.IsPointerOverGameObject();
				GameSceneSetup.placementRenderer.visible = !overUi;
				if (!overUi) {
					var interactionType = GetCurrentInteraction();
					var position = PlacementHelper.GetAimWorldPosition();
					var isPlacementAllowed = GameSceneSetup.beltNetwork.IsBeltPlacementAllowedAt(position);
					GameSceneSetup.placementRenderer.position = position;
					GameSceneSetup.placementRenderer.valid = isPlacementAllowed;
					if (interactionType == InteractionType.Place && wasInteractingLastFrame && position != previousPosition) {
						var direction = Directions.GetDirection(previousPosition, position);
						GameSceneSetup.beltNetwork.SetBelt(previousPosition, direction);
						GameSceneSetup.beltNetwork.SetBelt(position, direction);
					}
					previousPosition = position;
					wasInteractingLastFrame = interactionType == InteractionType.Place;
				}
				yield return null;
			}
		}

		protected override void Disable() {
			GameSceneSetup.placementRenderer.visible = false;
			GameSceneSetup.placementRenderer.ResetColor();
			GameInput.controls.Placement.Disable();
		}

		private static InteractionType GetCurrentInteraction() {
			if (EventSystem.current.IsPointerOverGameObject()) return InteractionType.None;
			if (GameInput.controls.Placement.Remove.inProgress) return InteractionType.Remove;
			if (GameInput.controls.Placement.Interact.inProgress) return InteractionType.Place;
			return InteractionType.None;
		}
	}
}