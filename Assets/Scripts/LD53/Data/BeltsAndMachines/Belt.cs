using LD53.Data.Common;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LD53.Data.BeltsAndMachines {
	[CreateAssetMenu]
	public class Belt : ScriptableObject {
		[SerializeField] protected Direction    _direction;
		[SerializeField] protected Sprite       _sprite;
		[SerializeField] protected AnimatedTile _tile;

		public Direction direction => _direction;
		public Sprite    sprite    => _sprite;

		public TileBase GetTile() => _tile;
	}
}