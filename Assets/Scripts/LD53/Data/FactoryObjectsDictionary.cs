using System.Linq;
using LD53.Data.BeltsAndMachines;
using LD53.Data.Common;
using UnityEngine;

namespace LD53.Data {
	[CreateAssetMenu]
	public class FactoryObjectsDictionary : ScriptableObject {
		[SerializeField] protected Belt[]        _belts;
		[SerializeField] protected InputMachine  _inputMachinePrefab;
		[SerializeField] protected OutputMachine _outputMachinePrefab;

		public InputMachine  inputMachinePrefab  => _inputMachinePrefab;
		public OutputMachine outputMachinePrefab => _outputMachinePrefab;
		public Belt GetBelt(Direction direction) => _belts.Single(t => t.direction == direction);
	}
}