using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneData sceneData;

    private void Awake()
    {
        sceneData.Initialize();
        sceneData.CameraController.Initialize();
        sceneData.InputHandler.Initialize();
    }
}
