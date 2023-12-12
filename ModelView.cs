using System.Collections.Generic;
using UnityEngine;

public class ModelView : MonoBehaviour
{
    [SerializeField] private FixedJoystick _fixJoy;
    [SerializeField] private DynamicJoystick _dynamicJoy;
    [SerializeField] private Transform _objectContainers;
    
    [SerializeField] private List<GameObject> _gameObjectModels = new List<GameObject>();

    [SerializeField] private GameObject _model;


    private void Start()
    {
        for (int i = 0; i < _objectContainers.childCount; i++)
        {
            _gameObjectModels.Add(_objectContainers.GetChild(i).gameObject);
        }
    }

    public void LoadModel() // event in button
    {
        for (int i = 0; i < _gameObjectModels.Count; i++)
        {
            if (_gameObjectModels[i].activeSelf)
            {
                _model = _gameObjectModels[i];
                _model.transform.rotation = Quaternion.Euler(0,0,0);
            }
        }
    }

    private void Update()
    {
        if (_model == null)
            LoadModel();

        RotateFixedJoy();
        RotateDynamicJoy();
    }

    private void RotateFixedJoy()
    {
        _model.transform.Rotate(new Vector2(_fixJoy.Direction.y, _fixJoy.Direction.x));
    }

    private void RotateDynamicJoy()
    {
        _model.transform.Rotate(new Vector2(_dynamicJoy.Direction.y, _dynamicJoy.Direction.x));
    }
}