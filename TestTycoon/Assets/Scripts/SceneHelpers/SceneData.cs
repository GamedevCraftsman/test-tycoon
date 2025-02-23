using UnityEngine;

public class SceneData : MonoBehaviour
{
    static public SceneData Instance;

    [SerializeField] private CameraController cameraController;
    [SerializeField] private InputHandler inputHandler;

    #region Public Values
    public CameraController CameraController { get { return cameraController; } }
    public InputHandler InputHandler { get { return inputHandler; } }
    #endregion
    
   public void Initialize()
   {
        Instance = this;
   }
}
