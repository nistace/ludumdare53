using System.Collections.Generic;
using LD53.Data.BeltsAndMachines.MachineProps;
using LD53.Data.Common;
using UnityEngine;

namespace LD53.Data.BeltsAndMachines {
	[RequireComponent(typeof(BaseMachine))]
	public class SplitterMachine : MonoBehaviour, IMachine {
		[SerializeField] protected BaseMachine           _baseMachine;
		[SerializeField] protected Direction             _defaultDirection     = Direction.Right;
		[SerializeField] protected Direction             _alternativeDirection = Direction.Down;
		[SerializeField] protected MachineInput          _input;
		[SerializeField] protected MachineDirectionLight _directionLight;
		[SerializeField] protected MachineLightIndicator _lightIndicator;

		public bool                   hasUpperHandOnBelt     => true;
		public Direction              beltDirection          => _input.pressed ? _alternativeDirection : _defaultDirection;
		public IEnumerable<Direction> allPotentialDirections => new[] { _defaultDirection, _alternativeDirection };
		public BaseMachine            baseMachine            => _baseMachine ? _baseMachine : _baseMachine = GetComponent<BaseMachine>();

		private void Start() {
			_input.onChange.AddListener(HandleInputChanged);
		}

		private void HandleInputChanged() {
			_directionLight.direction = beltDirection;
			_lightIndicator.isOn = _input.pressed;
			IMachine.onTileDirectionChanged.Invoke(this);
		}
	}
}