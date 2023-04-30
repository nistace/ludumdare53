using LD53.Data.Common;
using UnityEngine;
using Utils.Libraries;

namespace LD53.Data.BeltsAndMachines.MachineProps {
	public class MachineDirectionLight : MonoBehaviour {
		[SerializeField] protected SpriteRenderer _renderer;

		public Color color {
			get => _renderer.color;
			set => _renderer.color = value;
		}

		public Direction direction {
			set => _renderer.sprite = Sprites.Of($"machine.direction.{value}");
		}

		public void SetLayer(ref int sortingOrder) => _renderer.sortingOrder = sortingOrder++;
	}
}