using LD53.Data;

namespace LD53.Scenes.Game.Data {
	public static class GameProgressKeeper {
		public static GameSequence sequence   { get; set; }
		public static int          levelIndex { get; set; }

		public static LevelDescriptor GetCurrentLevel() => sequence[levelIndex];
	}
}