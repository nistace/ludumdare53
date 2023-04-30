using LD53.Inputs;
using UnityEngine;
using UnityEngine.Events;
using Utils.Libraries;

namespace LD53.Data.BeltsAndMachines.MachineProps {
	public class MachineInput : MonoBehaviour {
		[SerializeField] protected SpriteRenderer _inputRenderer;
		[SerializeField] protected bool           _initiallyEnabled;
		[SerializeField] protected char           _input;

		public bool wasPressed { get; set; }
		public bool pressed    => enabled && GameInput.machine.IsDown(_input);

		public UnityEvent onChange { get; } = new UnityEvent();

		private void Start() {
			SetUp(_initiallyEnabled ? _input : null);
		}

		public void SetUp(char? input) {
			enabled = input.HasValue;
			_inputRenderer.enabled = input.HasValue;
			if (input.HasValue) {
				_input = input.Value;
				_inputRenderer.sprite = Sprites.Of($"machine.input.{_input}");
			}
		}

		private void Update() {
			if (wasPressed != pressed) onChange.Invoke();
			wasPressed = pressed;
		}

		public void SetLayer(ref int sortingOrder) => _inputRenderer.sortingOrder = sortingOrder++;
	}
}