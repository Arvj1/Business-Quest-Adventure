using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CH1
{
    public class Stage1Manager : MonoBehaviour
    {
        public static Stage1Manager Instance;

        [HideInInspector] public GameObject activeCharacterObject;
        public CanvasGroup correctObj, incorrectObj;
        public bool canClick = true;
        public UnityEvent OnGameOver;

        public Camera mainCamera;        // Reference to the camera
        public float zoomDuration = 1f;  // Duration of zoom effect
        public float targetZoom = 3f;    // Target orthographic size when zoomed in

        private Vector3 initialPosition;
        private float initialZoom;

        private int correctAnswerCount = 0;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            // Store the initial position and zoom level
            initialPosition = mainCamera.transform.position;
            initialZoom = mainCamera.orthographicSize;
        }

        public void CheckAnswer(GameObject button)
        {
            if (activeCharacterObject.tag == button.tag)
            {
                FadeIn(correctObj, true);
                correctAnswerCount++;

                if (correctAnswerCount == 3)
                {
                    OnGameOver.Invoke();
                }
            }
            else
            {
                FadeIn(incorrectObj, false);
            }
        }

        // Smoothly zooms in on a target position
        public void ZoomIn(Transform target)
        {
            StopAllCoroutines();  // Stop any ongoing zoom
            StartCoroutine(SmoothZoom(target.position, targetZoom, zoomDuration));
        }

        // Smoothly returns to the initial position and zoom
        public void ZoomOut()
        {
            StopAllCoroutines();  // Stop any ongoing zoom
            StartCoroutine(SmoothZoom(initialPosition, initialZoom, zoomDuration));
        }

        private IEnumerator SmoothZoom(Vector3 targetPosition, float targetSize, float duration)
        {
            Vector3 startPosition = mainCamera.transform.position;
            float startSize = mainCamera.orthographicSize;
            float elapsed = 0f;

            // Perform the zoom smoothly over time
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                // Smooth step for smoother easing effect
                t = Mathf.SmoothStep(0, 1, t);

                // Interpolate camera position and zoom level
                mainCamera.transform.position = Vector3.Lerp(startPosition, new Vector3(targetPosition.x, targetPosition.y, startPosition.z), t);
                mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);

                yield return null;
            }

            // Set the final position and size to make sure they are exact
            mainCamera.transform.position = new Vector3(targetPosition.x, targetPosition.y, startPosition.z);
            mainCamera.orthographicSize = targetSize;
        }

        // Fade In the CanvasGroup
        public void FadeIn(CanvasGroup cg, bool shouldZoomOut)
        {
            cg.DOFade(1f, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                FadeOut(cg, shouldZoomOut);
            });
        }

        // Fade Out the CanvasGroup
        public void FadeOut(CanvasGroup cg, bool shouldZoomOut)
        {
            cg.DOFade(0f, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                if (shouldZoomOut)
                {
                    SpriteRenderer sr = activeCharacterObject.GetComponent<SpriteRenderer>();
                    Color color = sr.color;
                    color.a = 0.5f;
                    sr.color = color;

                    cg.transform.parent.gameObject.SetActive(false);
                    ZoomOut();
                }
            });
        }
    }

}