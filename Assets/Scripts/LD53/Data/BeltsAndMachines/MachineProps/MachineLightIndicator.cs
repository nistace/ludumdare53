using UnityEngine;
using Utils.Libraries;

namespace LD53.Data.BeltsAndMachines.MachineProps {
	public class MachineLightIndicator : MonoBehaviour {
		[SerializeField] protected SpriteRenderer _renderer;

		public bool isOn {
			set => _renderer.color = Colors.Of("machine.indicator." + (value ? "on" : "off"));
		}

		public void SetLayer(ref int sortingOrder) => _renderer.sortingOrder = sortingOrder++;
	}
}