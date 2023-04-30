using System.Collections;
using LD53.Data.BeltsAndMachines;
using LD53.Inputs;
using LD53.Scenes.Game.Data;
using LD53.Scenes.Game.Ui;
using UnityEngine.EventSystems;
using Utils.Extensions;
using Utils.GameStates;

namespace LD53.Scenes.Game {
	public class MachinePlacementGameState : GameState {
		private enum InteractionType {
			None   = 0,
			Place  = 1,
			Remove = 2,
		}

		public static MachinePlacementGameState state { get; } = new MachinePlacementGameState();

		private IMachine machinePrefab { get; set; }

		private MachinePlacementGameState() { }

		public void Prepare(IMachine selectedMachine) {
			machinePrefab = selectedMachine;
		}

		protected override void Enable() {
			GameInput.controls.Placement.Enable();
			SetSelectedMachine(machinePrefab);
		}

		private void SetSelectedMachine(IMachine machine) {
			machinePrefab = machine;
			GameSceneSetup.placementRenderer.sprite = machinePrefab.uiSprite;
			GameUi.placementButtonLine.SetMachineSelected(machinePrefab);
		}

		private static void HandleBeltButtonClicked() => ChangeState(BeltPlacementGameState.state);

		protected override IEnumerator Continue() {
			while (enabled) {
				var overUi = EventSystem.current.IsPointerOverGameObject();
				GameSceneSetup.placementRenderer.enabled = !overUi;
				if (!overUi) {
					var interactionType = GetCurrentInteraction();
					var position = PlacementHelper.GetAimWorldPosition();
					var isPlacementAllowed = GameSceneSetup.beltNetwork.IsMachinePlacementAllowedAt(position);
					GameSceneSetup.placementRenderer.position = position;
					GameSceneSetup.placementRenderer.valid = isPlacementAllowed;
					if (interactionType == InteractionType.Place) {
						GameSceneSetup.beltNetwork.SetMachine<IMachine>(position, machinePrefab);
					}
				}
				yield return null;
			}
		}

		protected override void SetListenersEnabled(bool enabled) {
			PlacementButtonUi.onBeltButtonClicked.SetListenerActive(HandleBeltButtonClicked, enabled);
			PlacementButtonUi.onMachineButtonClicked.SetListenerActive(SetSelectedMachine, enabled);
		}

		private static InteractionType GetCurrentInteraction() {
			if (EventSystem.current.IsPointerOverGameObject()) return InteractionType.None;
			if (GameInput.controls.Placement.Remove.inProgress) return InteractionType.Remove;
			if (GameInput.controls.Placement.Interact.WasReleasedThisFrame()) return InteractionType.Place;
			return InteractionType.None;
		}

		protected override void Disable() {
			GameSceneSetup.placementRenderer.enabled = false;
			GameSceneSetup.placementRenderer.ResetColor();
			GameInput.controls.Placement.Disable();
		}
	}
}