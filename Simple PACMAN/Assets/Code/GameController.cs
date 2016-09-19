using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game.Data;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {


	[SerializeField]
	private CellComponent _prefab;

	[SerializeField]
	private Transform _wrapper;

	private int _rowCount = 10;
	private int _columnCount = 10;
	private CellComponent[,] _field;

	private int _previousSelectedRow;
	private int _previousSelectedColumn;

	private void GenerateField(){
		if (!_prefab || !_wrapper)
			return;

		_field = new CellComponent[_rowCount, _columnCount];

		for (int i = 0; i < _rowCount; i++) {
			for (int j = 0; j < _columnCount; j++) {		
			
				var item = _prefab.GetInstance ();
				item.transform.SetParent (_wrapper, false);
				_field [i, j] = item;
				item.gameObject.name = string.Format ("{0} , {1}", i, j);
				item.Row = i;
				item.Column = j;
				item.OnItemClick += Item_OnItemClick;
			}
		}
	}

	void Item_OnItemClick (int row, int column)
	{
		Debug.LogFormat ("{0} , {1}", row, column);
		_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = false;
		_field [row, column].IsSelect = true;
		_previousSelectedRow = row;
		_previousSelectedColumn = column;
	}

	// Use this for initialization
	void Start () {
		GenerateField ();
	}

	private void LateUpdate(){
		if (Input.GetKey (KeyCode.UpArrow)) {
			_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = false;
			_previousSelectedRow--;
			_previousSelectedRow = _previousSelectedRow < 0 ? _field.GetLength(0) - 1 : _previousSelectedRow;
			_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = true;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = false;
			_previousSelectedRow++;
			_previousSelectedRow = _previousSelectedRow >= _field.GetLength(0) ? 0 : _previousSelectedRow;
			_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = true;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = false;
			_previousSelectedColumn++;
			_previousSelectedColumn = _previousSelectedColumn >= _field.GetLength(1) ? 0 : _previousSelectedColumn;
			_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = true;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = false;
			_previousSelectedColumn--;
			_previousSelectedColumn = _previousSelectedColumn < 0 ? _field.GetLength(1) - 1 : _previousSelectedColumn;
			_field [_previousSelectedRow, _previousSelectedColumn].IsSelect = true;
		}
	}
}
