using System;
using System.Collections.Generic;
using LD53.Data.BeltsAndMachines;
using LD53.Data.Common;
using UnityEngine;

namespace LD53.Data {
	[CreateAssetMenu]
	public class LevelDescriptor : ScriptableObject {
		[SerializeField] protected Vector2Int              _gridMin = new Vector2Int(-10, -10);
		[SerializeField] protected Vector2Int              _gridMax = new Vector2Int(10, 10);
		[SerializeField] protected InitialBelt[]           _initialBelts;
		[SerializeField] protected bool                    _allowBeltPlacement = true;
		[SerializeField] protected AvailableMachineCount[] _availableMachineCounts;
		[SerializeField] protected InputOrOutput[]         _inputs;
		[SerializeField] protected InputOrOutput[]         _outputs;

		public Vector2Int                           gridMin                => _gridMin;
		public Vector2Int                           gridMax                => _gridMax;
		public IReadOnlyList<InitialBelt>           initialBelts           => _initialBelts;
		public bool                                 allowBeltPlacement     => _allowBeltPlacement;
		public IReadOnlyList<AvailableMachineCount> availableMachineCounts => _availableMachineCounts;
		public IReadOnlyList<InputOrOutput>         inputs                 => _inputs;
		public IReadOnlyList<InputOrOutput>         outputs                => _outputs;

		[Serializable]
		public class InitialBelt {
			[SerializeField] protected Direction  _direction;
			[SerializeField] protected Vector2Int _position;

			public Direction  direction => _direction;
			public Vector2Int position  => _position;
		}

		[Serializable]
		public class InputOrOutput {
			[SerializeField] protected Color      _color;
			[SerializeField] protected Vector2Int _position;

			public Color      color    => _color;
			public Vector2Int position => _position;
		}

		[Serializable]
		public class AvailableMachineCount {
			[SerializeField] protected GameObject _machinePrefab;
			[SerializeField] protected int        _count = 1;

			public IMachine machinePrefab => _machinePrefab.GetComponent<IMachine>();
			public int      count         => _count;
		}
	}
}