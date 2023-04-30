using LD53.Data.BeltsAndMachines.MachineProps;
using UnityEngine;

namespace LD53.Data.BeltsAndMachines {
	public class BaseMachine : MonoBehaviour {
		[SerializeField] protected SpriteRenderer        _renderer;
		[SerializeField] protected MachineInput          _input;
		[SerializeField] protected MachineDirectionLight _directionLight;
		[SerializeField] protected MachineLightIndicator _lightIndicator;

		public MachineInput          input          => _input ? _input : _input = GetComponentInChildren<MachineInput>();
		public MachineDirectionLight directionLight => _directionLight ? _directionLight : _directionLight = GetComponentInChildren<MachineDirectionLight>();
		public MachineLightIndicator lightIndicator => _lightIndicator ? _lightIndicator : _lightIndicator = GetComponentInChildren<MachineLightIndicator>();

		private void Reset() {
			_renderer = GetComponent<SpriteRenderer>();
			_input = GetComponentInChildren<MachineInput>();
			_directionLight = GetComponentInChildren<MachineDirectionLight>();
			_lightIndicator = GetComponentInChildren<MachineLightIndicator>();
		}

		public void SetLayer(int layer) {
			var sortingOrder = 20 + layer * 10;
			_renderer.sortingOrder = sortingOrder++;
			if (input) _input.SetLayer(ref sortingOrder);
			if (directionLight) _directionLight.SetLayer(ref sortingOrder);
			if (lightIndicator) _lightIndicator.SetLayer(ref sortingOrder);
		}
	}
}