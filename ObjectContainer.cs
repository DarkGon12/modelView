using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectContainer : MonoBehaviour
{
    [SerializeField] private GameObjectContain _container;
    [SerializeField] private List<GameObject> _viewObjects = new List<GameObject>();
    [SerializeField] private CameraAround _cameraUI, _cameraView;

    private Transform _gameObjectsContainer;
    
    private void InitialText()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = _container.GetObjectInfo(i).name;
        }
    }

    private void Start()
    {
        InitialText();
        _gameObjectsContainer = _container.transform;

        for (int i = 0; i < _gameObjectsContainer.childCount; i++)
        {
            _viewObjects.Add(_gameObjectsContainer.GetChild(i).gameObject);
        }
    }

    private void DisableObjects() // Выключает все остальные модели
    {
        for(int i = 0; i<_gameObjectsContainer.childCount; i++)
        {
            _viewObjects[i].SetActive(false);
        }
    }

    public void SelectObject(int index)
    {
        _viewObjects[index].SetActive(true);
        _cameraUI.LoadModel(index);
        _cameraView.LoadModel(index);
    }
}