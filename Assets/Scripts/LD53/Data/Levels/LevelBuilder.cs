using System;
using LD53.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

public class LevelBuilder : MonoBehaviour {
	[SerializeField] protected Camera            _camera;
	[SerializeField] protected BeltNetwork       _beltNetwork;
	[SerializeField] protected EnvironmentSlice  _topWallSlice;
	[SerializeField] protected EnvironmentSlice  _leftWallSlice;
	[SerializeField] protected EnvironmentSlice  _rightWallSlice;
	[SerializeField] protected EnvironmentSlice  _downWallSlice;
	[SerializeField] protected EnvironmentCorner _topLeftCorner;
	[SerializeField] protected EnvironmentCorner _topRightCorner;
	[SerializeField] protected EnvironmentCorner _bottomLeftCorner;
	[SerializeField] protected EnvironmentCorner _bottomRightCorner;
	[SerializeField] protected TileBase          _floorTile;
	[SerializeField] protected Tilemap           _tilemap;
	public                     BeltNetwork       beltNetwork => _beltNetwork;

	public void Build(LevelDescriptor levelDescriptor) {
		BuildStaticLevel(levelDescriptor);
		_beltNetwork.Setup(levelDescriptor);
		SetUpCamera(levelDescriptor);
	}

	private void SetUpCamera(LevelDescriptor levelDescriptor) {
		_camera.transform.position = ((Vector3)Vector2.Lerp(levelDescriptor.gridMin, levelDescriptor.gridMax, .5f)).With(z: -10);
	}

	private void BuildStaticLevel(LevelDescriptor levelDescriptor) {
		_tilemap.ClearAllTiles();
		for (var x = levelDescriptor.gridMin.x; x <= levelDescriptor.gridMax.x; ++x) {
			for (var y = levelDescriptor.gridMin.y; y <= levelDescriptor.gridMax.y; ++y) {
				_tilemap.SetTile(new Vector3Int(x, y, 0), _floorTile);
			}
			for (var y = 0; y < _topWallSlice.count; ++y) {
				_tilemap.SetTile(new Vector3Int(x, levelDescriptor.gridMax.y + y + 1, 0), _topWallSlice[y]);
			}
			for (var y = 0; y < _downWallSlice.count; ++y) {
				_tilemap.SetTile(new Vector3Int(x, levelDescriptor.gridMin.y - y - 1, 0), _downWallSlice[y]);
			}
		}

		for (var y = levelDescriptor.gridMin.y; y <= levelDescriptor.gridMax.y; ++y) {
			for (var x = 0; x < _leftWallSlice.count; ++x) {
				_tilemap.SetTile(new Vector3Int(levelDescriptor.gridMin.x - x - 1, y, 0), _leftWallSlice[x]);
			}
			for (var x = 0; x < _rightWallSlice.count; ++x) {
				_tilemap.SetTile(new Vector3Int(levelDescriptor.gridMax.x + x + 1, y, 0), _rightWallSlice[x]);
			}
		}

		_topLeftCorner.Paint(_tilemap, new Vector2Int(levelDescriptor.gridMin.x - _topLeftCorner.width, levelDescriptor.gridMax.y + 1 + _topLeftCorner.offsetY));
		_topRightCorner.Paint(_tilemap, new Vector2Int(levelDescriptor.gridMax.x + 1, levelDescriptor.gridMax.y + 1 + _topRightCorner.offsetY));
		_bottomLeftCorner.Paint(_tilemap, new Vector2Int(levelDescriptor.gridMin.x - _bottomLeftCorner.width, levelDescriptor.gridMin.y + _bottomLeftCorner.offsetY));
		_bottomRightCorner.Paint(_tilemap, new Vector2Int(levelDescriptor.gridMax.x + 1, levelDescriptor.gridMin.y + _bottomRightCorner.offsetY));
	}

	[Serializable] protected class EnvironmentSlice {
		[SerializeField] protected TileBase[] _tiles;

		public TileBase this[int index] => _tiles[index];
		public int count => _tiles.Length;
	}

	[Serializable] protected class EnvironmentCorner {
		[SerializeField] protected EnvironmentSlice[] _slices;
		[SerializeField] protected int                _offsetY;

		public TileBase this[int x, int y] => _slices[x][y];
		public int height  => width == 0 ? 0 : _slices[0].count;
		public int offsetY => _offsetY;
		public int width   => _slices.Length;

		public void Paint(Tilemap tilemap, Vector2Int origin) {
			for (var x = 0; x < width; ++x)
			for (var y = 0; y < height; ++y) {
				tilemap.SetTile(new Vector3Int(origin.x + x, origin.y + y, 0), this[x, y]);
			}
		}
	}
}