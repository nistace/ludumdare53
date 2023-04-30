using UnityEngine;
using Utils.Extensions;

namespace LD53.Data {
	[CreateAssetMenu]
	public class GameSequence : ScriptableObject {
		[SerializeField] protected LevelDescriptor[] _levels;

		public LevelDescriptor this[int level] => _levels.GetSafe(level);
	}
}