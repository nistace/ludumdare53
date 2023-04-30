using LD53.Data;
using LD53.Data.BeltsAndMachines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils.Extensions;
using Utils.Libraries;

namespace LD53.Scenes.Game.Ui {
	public class PlacementButtonUi : MonoBehaviour {
		public enum Type {
			Belt,
			Machine
		}

		[SerializeField] protected RectTransform _visiblePartTransform;
		[SerializeField] protected Button        _button;
		[SerializeField] protected Image         _itemImage;
		[SerializeField] protected MonoBehaviour _flagComponent;

		public Type     type    { get; private set; }
		public IMachine machine { get; private set; }

		public static UnityEvent           onBeltButtonClicked    { get; } = new UnityEvent();
		public static UnityEvent<IMachine> onMachineButtonClicked { get; } = new IMachine.Event();

		private void Start() {
			_button.onClick.AddListenerOnce(HandleButtonClicked);
		}

		private void HandleButtonClicked() {
			if (type == Type.Belt) onBeltButtonClicked.Invoke();
			else onMachineButtonClicked.Invoke(machine);
		}

		public void SetUpForBelt() {
			type = Type.Belt;
			machine = null;
			_itemImage.sprite = Sprites.Of("ui.placement.belt");
		}

		public void SetUpForMachine(LevelDescriptor.AvailableMachineCount machine) {
			type = Type.Belt;
			this.machine = machine.machinePrefab;
			_itemImage.sprite = machine.machinePrefab.uiSprite;
		}

		public void SetSelected(bool selected) => _flagComponent.enabled = selected;

		public void SetVerticalOffset(float offset) {
			_visiblePartTransform.anchorMin = new Vector2(0, offset);
			_visiblePartTransform.anchorMax = new Vector2(1, 1 + offset);
			_visiblePartTransform.offsetMin = Vector2.zero;
			_visiblePartTransform.offsetMax = Vector2.zero;
		}
	}
}