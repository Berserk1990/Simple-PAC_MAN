using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CellComponent : MonoBehaviour {

	public event Action<int, int> OnItemClick;

	private void OnItemClickHandler(){
		if (OnItemClick != null)
			OnItemClick (Row, Column);
	}

	public int Row {
		set;
		get;
	}
	public int Column {
		set;
		get;
	}

	[SerializeField]
	private Image _image;

	[SerializeField]
	private Color _selectColor;

	private Color _normalColor;
	private float _delay = .25f;


	private bool _isSelect;
	public bool IsSelect{
		set{ 
			_isSelect = value;
			CancelInvoke ();
			if (value)
				InvokeRepeating ("SwichColor", 0, _delay);
			else
				_image.color = _normalColor;
		}
		get{ 
			return _isSelect;
		}
	}

	public void Selected(){
		IsSelect = !IsSelect;
	}

	private void Start(){
		if (_image)
			_normalColor = _image.color;
	}

	private void SwichColor(){
		if (!_image)
			return;

		_image.color = _image.color == _normalColor ? _selectColor : _normalColor;
	}

	public void OnSelected(){
		OnItemClickHandler ();
	}
}
