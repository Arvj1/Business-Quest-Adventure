using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CH1
{
    public class Drag : MonoBehaviour
    {
        public bool isDragging = false;
        public Vector3 dragPosition = Vector3.zero;
        public Vector3 offset = Vector3.zero;
        public string Current_Tag = "na";
        public TMP_Text CorrectAnswer;
        public CanvasGroup CorrectAnswerCanvasGroup;

        public void DoAnimation()
        {
            CorrectAnswerCanvasGroup.DOFade(1, 1f).OnComplete(AnimationComplete);
        }
        public void AnimationComplete()
        {
            CorrectAnswerCanvasGroup.DOFade(0, 1f);
        }
        public void StartDrag(Vector3 hello)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(hello);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - worldPosition;
                offset.z = 0; // Ensure dragging remains on the same 2D plane
            }
        }
        private void Start()
        {
            dragPosition = transform.position;
        }
        public void EndDrag(Vector3 hello)
        {
            transform.position = dragPosition;
            isDragging = false;
            if (Current_Tag == "na")
            {
                transform.position = dragPosition;
            }
            else
            {
                DoAnimation();
                if (Current_Tag == gameObject.tag)
                {
                    CorrectAnswer.text = "correct answer";

                }
                else
                {
                    CorrectAnswer.text = "incorrect answer";
                }
            }
        }
        public void Dragging(Vector3 hello)
        {
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(hello);
            vector3.z = 0;
            transform.position = vector3 + offset;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Current_Tag = collision.gameObject.tag;
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            Current_Tag = "na";
        }
        void Update()
        {
            // Handle Mouse Input
            if (Input.GetMouseButtonDown(0))
            {
                StartDrag(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0) && isDragging)
            {
                Dragging(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                EndDrag(Input.mousePosition);
            }

            // Handle Touch Input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    StartDrag(touch.position);
                }
                else if (touch.phase == TouchPhase.Moved && isDragging)
                {
                    Dragging(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended && isDragging)
                {
                    EndDrag(touch.position);
                }
            }
        }
    }
}
