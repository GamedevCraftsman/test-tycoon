using System.Collections;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Coroutine _checkClicks;

    public void Initialize()
    {
        StartCheckClicks();
    }

    private IEnumerator CheckClicks()
    {
        while (true)
        {
            Click();
            yield return null;
        }
    }

    private void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMainCamera())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                CheckClickableObject(ray);
            }
        }
    }

    private bool IsMainCamera()
    {
        return Camera.main;
    }

    private void CheckClickableObject(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CallClickAction(hit);
        }
    }

    private void CallClickAction(RaycastHit hit)
    {
        IClickableObject clickable = hit.collider.GetComponent<IClickableObject>();
        if (clickable != null)
        {
            clickable.Click();
        }
    }

    public void StopCheckClicks()
    {
        StopCoroutine(_checkClicks);
    }

    public void StartCheckClicks()
    {
        _checkClicks = StartCoroutine(CheckClicks());
    }
}