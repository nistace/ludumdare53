using UnityEngine;
using Utils.Libraries;

namespace LD53.Data.BeltsAndMachines {
	public class PlacementGhost : MonoBehaviour {
		[SerializeField] protected SpriteRenderer _renderer;

		public bool visible {
			get => _renderer.enabled;
			set => _renderer.enabled = value;
		}

		public bool valid {
			set => _renderer.color = Colors.Of($"placement.{(value ? "valid" : "invalid")}");
		}

		public Vector2Int position {
			set => transform.position = (Vector2)value;
		}

		public Sprite sprite {
			get => _renderer.sprite;
			set => _renderer.sprite = value;
		}

		public void ResetColor() => _renderer.color = Color.white;
	}
}