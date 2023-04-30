using System.Collections.Generic;
using LD53.Data.BeltsAndMachines;
using LD53.Data.Common;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;
using Utils.Types;

namespace LD53.Data {
	public class BeltNetwork : MonoBehaviour {
		[SerializeField] protected Tilemap                  _beltTilemap;
		[SerializeField] protected Transform                _machinesContainer;
		[SerializeField] protected Vector2Int               _minCoordinates;
		[SerializeField] protected Vector2Int               _maxCoordinates;
		[SerializeField] protected FactoryObjectsDictionary _factoryObjectsDictionary;

		private Dictionary<Vector2Int, Belt> belts    { get; } = new Dictionary<Vector2Int, Belt>();
		private Map<Vector2Int, IMachine>    machines { get; } = new Map<Vector2Int, IMachine>();

		private void Start() {
			IMachine.onTileDirectionChanged.AddListenerOnce(HandleTileChanged);
		}

		private void HandleTileChanged(IMachine machine) {
			if (!machines.ContainsRight(machine)) return;
			_beltTilemap.SetTile((Vector3Int)machines.LeftOf(machine), _factoryObjectsDictionary.GetBelt(machine.beltDirection).GetTile());
		}

		public void Setup(LevelDescriptor levelDescriptor) {
			Clear();
			foreach (var initialBelt in levelDescriptor.initialBelts) SetBelt(initialBelt.position, initialBelt.direction);
			foreach (var input in levelDescriptor.inputs) SetMachine(input.position, _factoryObjectsDictionary.inputMachinePrefab).color = input.color;
			foreach (var output in levelDescriptor.outputs) SetMachine(output.position, _factoryObjectsDictionary.outputMachinePrefab).color = output.color;
			_minCoordinates = levelDescriptor.gridMin;
			_maxCoordinates = levelDescriptor.gridMax;
		}

		public void Clear() {
			_beltTilemap.ClearAllTiles();
			belts.Clear();
			_machinesContainer.ClearChildren();
			machines.Clear();
		}

		private bool IsPositionInBounds(Vector2Int position) {
			if (position.x < _minCoordinates.x) return false;
			if (position.y < _minCoordinates.y) return false;
			if (position.x > _maxCoordinates.x) return false;
			if (position.y > _maxCoordinates.y) return false;
			return true;
		}

		public bool IsBeltPlacementAllowedAt(Vector2Int position) {
			if (machines.ContainsLeft(position) && machines.RightOf(position).hasUpperHandOnBelt) return false;
			return IsPositionInBounds(position);
		}

		public bool IsMachinePlacementAllowedAt(Vector2Int position) {
			if (machines.ContainsLeft(position)) return false;
			return IsPositionInBounds(position);
		}

		public void RemoveFirstAt(Vector2Int position) {
			if (machines.ContainsLeft(position)) {
				machines.RemoveLeft(position);
			}
			else if (belts.ContainsKey(position)) {
				_beltTilemap.SetTile((Vector3Int)position, null);
				belts.Remove(position);
			}
		}

		public E SetMachine<E>(Vector2Int position, E machinePrefab) where E : IMachine {
			var machineInstance = Instantiate(machinePrefab.gameObject, (Vector2)position, Quaternion.identity, _machinesContainer).GetComponent<E>();
			machines.Set(position, machineInstance);
			machineInstance.baseMachine.SetLayer(_maxCoordinates.y - position.y);
			if (machineInstance.hasUpperHandOnBelt) SetBelt(position, machineInstance.beltDirection);
			return machineInstance;
		}

		public void SetBelt(Vector2Int position, Direction beltDirection) {
			belts.Set(position, _factoryObjectsDictionary.GetBelt(beltDirection));
			RefreshBelt(position);
		}

		private void RefreshBelt(Vector2Int position) {
			if (!belts.ContainsKey(position)) return;
			_beltTilemap.SetTile((Vector3Int)position, _factoryObjectsDictionary.GetBelt(belts[position].direction).GetTile());
		}

#if UNITY_EDITOR
		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_minCoordinates - Vector2.one * .5f, new Vector3(_minCoordinates.x - .5f, _maxCoordinates.y + .5f, 0));
			Gizmos.DrawLine(_minCoordinates - Vector2.one * .5f, new Vector3(_maxCoordinates.x + .5f, _minCoordinates.y - .5f, 0));
			Gizmos.DrawLine(_maxCoordinates + Vector2.one * .5f, new Vector3(_minCoordinates.x - .5f, _maxCoordinates.y + .5f, 0));
			Gizmos.DrawLine(_maxCoordinates + Vector2.one * .5f, new Vector3(_maxCoordinates.x + .5f, _minCoordinates.y - .5f, 0));

			Gizmos.color = Color.cyan;
			foreach (var belt in belts) {
				Gizmos.DrawLine((Vector2)belt.Key, (Vector2)belt.Key + Directions.v2Ints[belt.Value.direction]);
			}

			foreach (var machine in machines) {
				foreach (var potentialDirection in machine.Value.allPotentialDirections) {
					Gizmos.DrawLine((Vector2)machine.Key, (Vector2)machine.Key + Directions.v2Ints[potentialDirection]);
				}
			}
		}
#endif
	}
}