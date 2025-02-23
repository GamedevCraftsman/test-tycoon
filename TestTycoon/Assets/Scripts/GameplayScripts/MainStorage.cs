using UnityEngine;
using CustomTools;

public class MainStorage : CutomUITools, IClickableObject
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float changeStateSpeed;
    
    public void Click()
    {
        AppearPanel(canvasGroup, changeStateSpeed);
        Debug.Log("Click");
    }
    
    #region UI Functions
    public void HideStoragePanel()
    {
        DisapearPanel(canvasGroup, changeStateSpeed);
        Debug.Log("Hide");
    }
    #endregion
}
