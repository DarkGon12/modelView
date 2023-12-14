using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraAround : MonoBehaviour
{
	[SerializeField] private Transform _objectContainers;
	[SerializeField] private Transform _model;

	[Header("Джойстики")]
	[SerializeField] private FixedJoystick _fixJoy;
	[SerializeField] private DynamicJoystick _dynamicJoy;

	[Header("Настройки камеры")]
	[Range(0.2f, 4f)]
	[SerializeField] private float _sensitivity; 
	[Range(1,90)]
	[SerializeField] private float _limit;

	private bool _isZoom;
	private Vector3 offset, targetOffset;
	private float _horizontalPos, _verticalPos;
	private List<Transform> _gameObjectModels = new List<Transform>();

	private void Initialization()
    {
		for (int i = 0; i < _objectContainers.childCount; i++)
		{
			_gameObjectModels.Add(_objectContainers.GetChild(i).transform);
		}

		_limit = Mathf.Abs(_limit);

		offset = new Vector3(offset.x, offset.y, -3f);
		targetOffset = new Vector3(-0.9f, 1.1f, 1.48f);
    }

	void Start()
	{
		Initialization();
	}

	public void LoadModel(int index) // event in button
	{
		_model = _gameObjectModels[index];
		Zoom();
	}

	void Update()
	{
		DynamicJoy();
		FixedJoy();
	}

	private void Zoom()
    {
		offset.z = -4;
		_isZoom = true;

		_fixJoy.enabled = false;
		_dynamicJoy.enabled = false;

		StartCoroutine(Zooming());
    }

	private IEnumerator Zooming()
    {
		while (_isZoom)
		{
			if (offset.z < -2f)
			{
				targetOffset = Vector3.Lerp(targetOffset, transform.localRotation * offset + _model.position, 0.01f);
				offset.z = Mathf.Lerp(offset.z, -1.85f, 0.01f);
				yield return new WaitForSeconds(0.002f);
			}
			else
			{
				_isZoom = false;
				_fixJoy.enabled = true;
				_dynamicJoy.enabled = true;
				yield return null;
				StopAllCoroutines();
			}
		}
    }

	private void FixedJoy()
	{
		_horizontalPos = transform.localEulerAngles.y + _fixJoy.Direction.x * _sensitivity;
		_verticalPos += _fixJoy.Direction.y * _sensitivity;
		UpdatePosition();
	}

	private void DynamicJoy()
    {
		_horizontalPos = transform.localEulerAngles.y + _dynamicJoy.Direction.x * _sensitivity;
		_verticalPos += _dynamicJoy.Direction.y * _sensitivity;
		UpdatePosition();
	}

	private void UpdatePosition()
    {
		_verticalPos = Mathf.Clamp(_verticalPos, -_limit, _limit);
		transform.localEulerAngles = new Vector3(-_verticalPos, _horizontalPos, 0);

		if (!_isZoom)
			transform.position = transform.localRotation * offset + _model.position;
		else
			transform.position = targetOffset;
	}
}