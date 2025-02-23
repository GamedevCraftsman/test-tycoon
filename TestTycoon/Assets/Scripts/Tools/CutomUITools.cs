using System.Collections;
using UnityEngine;

namespace CustomTools
{
    public abstract class CutomUITools : MonoBehaviour
    {
        public void AppearPanel(CanvasGroup canvasGroup, float speed)
        {
            StopActiveCoroutines();
            StartCoroutine(ShowPanel(canvasGroup, speed));
        }

        public void DisapearPanel(CanvasGroup canvasGroup, float speed)
        {
            StartCoroutine(HidePanel(canvasGroup, speed));
        }

        private IEnumerator ShowPanel(CanvasGroup canvasGroup, float speed)
        {
            while (true)
            {
                if (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += speed * Time.deltaTime;
                }
                else
                {
                    break;
                }

                yield return null;
            }
        }

        private IEnumerator HidePanel(CanvasGroup canvasGroup, float speed)
        {
            while (true)
            {
                if (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= speed * Time.deltaTime;
                }
                else
                {
                    ContinueCoroutines();
                    break;
                }

                yield return null;
            }
        }

        private void StopActiveCoroutines()
        {
            SceneData.Instance.CameraController.StopCheckDrag();
            SceneData.Instance.InputHandler.StopCheckClicks();
        }

        private void ContinueCoroutines()
        {
            SceneData.Instance.CameraController.StartCheckDrag();
            SceneData.Instance.InputHandler.StartCheckClicks();
        }
    }
}