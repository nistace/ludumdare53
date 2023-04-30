using UnityEngine;

namespace LD53.Inputs {
	public static class GameInput {
		public static Controls controls { get; } = new Controls();

		private static MachineInputControl mMachine { get; set; }

		public static MachineInputControl machine {
			get {
				if (!mMachine) {
					mMachine = new GameObject("MachineInputControl").AddComponent<MachineInputControl>();
					Object.DontDestroyOnLoad(machine);
				}
				return mMachine;
			}
		}
	}
}