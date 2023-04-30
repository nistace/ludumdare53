using System.Collections.Generic;
using LD53.Data.BeltsAndMachines.MachineProps;
using LD53.Data.Common;
using UnityEngine;

namespace LD53.Data.BeltsAndMachines {
	[RequireComponent(typeof(BaseMachine))]
	public class InputMachine : MonoBehaviour, IMachine {
		[SerializeField] protected BaseMachine           _baseMachine;
		[SerializeField] protected Direction             _direction;
		[SerializeField] protected MachineDirectionLight _light;

		public Direction              beltDirection          => _direction;
		public IEnumerable<Direction> allPotentialDirections => new[] { _direction };
		public bool                   hasUpperHandOnBelt     => true;
		public BaseMachine            baseMachine            => _baseMachine ? _baseMachine : _baseMachine = GetComponent<BaseMachine>();

		public Color color {
			get => _light.color;
			set => _light.color = value;
		}

		public void SetLayer(int layer) { }
	}
}