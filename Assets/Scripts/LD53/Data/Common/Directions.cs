using System.Collections.Generic;
using UnityEngine;

namespace LD53.Data.Common {
	public static class Directions {
		public static Dictionary<Direction, Vector2Int> v2Ints { get; } = new Dictionary<Direction, Vector2Int> {
			{ Direction.Up, Vector2Int.up }, { Direction.Down, Vector2Int.down }, { Direction.Left, Vector2Int.left }, { Direction.Right, Vector2Int.right },
		};

		public static Direction GetDirection(Vector2Int from, Vector2Int to) {
			var dX = to.x - from.x;
			var dY = to.y - from.y;
			if (Mathf.Abs(dY) > Mathf.Abs(dX)) return dY < 0 ? Direction.Down : Direction.Up;
			return dX < 0 ? Direction.Left : Direction.Right;
		}
	}
}