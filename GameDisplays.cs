using UnityEngine;

public class GameDisplays : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < Display.displays.Length; i++)
            Display.displays[i].Activate();
    }
}