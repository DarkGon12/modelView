using System.Collections.Generic;
using UnityEngine;

public class GameObjectContain : MonoBehaviour
{
    [SerializeField] private List<ObjectInfo> _objectInfo = new List<ObjectInfo>();

    private void OnEnable()  // OnEnable - испульзовался этот метод на случай если будет расширение кода до добавления кнопок и моделей в реал-тайме
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            ObjectInfo info = new();
            Transform childTransform = transform.GetChild(i);

            info.name = childTransform.gameObject.name;
            info.model = childTransform.gameObject;
            info.index = i;

            _objectInfo.Add(info);
        }
    }

    public ObjectInfo GetObjectInfo(int index)
    {
        for (int i = 0; i < _objectInfo.Count; i++)
        {
            if (_objectInfo[i].index == index)
                return _objectInfo[i];
        }
        return null;
    }
}

[System.Serializable]
public class ObjectInfo
{
    public string name;
    public int index;
    public GameObject model;
    public float rotateSpeed;
}