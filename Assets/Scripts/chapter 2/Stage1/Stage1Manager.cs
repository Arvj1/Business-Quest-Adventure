 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Stage1Manager : MonoBehaviour
{
    [SerializeField] RectTransform startTransform, endTransform;
    [SerializeField] GameObject[] products;
    [SerializeField] CanvasGroup correctCG, wrongCG;
    private int count = 0,index=0;
    public UnityEvent OnGameOver;


    private void Start()
    {
        StartCoroutine(CreateList(products[0]));
    }


    public IEnumerator CreateList(GameObject product)
    {
        yield return new WaitForSeconds(2);
        product.SetActive(true);
        // Get the RectTransform component of the instantiated object
        RectTransform productRect = product.GetComponent<RectTransform>();

        // Set the initial position to the startTransform's position
        productRect.position = startTransform.position;

        // Animate the productRect to the endTransform's position
        productRect.DOMove(endTransform.position, 1f) // 1f is the duration of the animation
                    .SetEase(Ease.InOutSine); // Ease in-out effect
    }

    public void CheckCorrectAnswer(GameObject dragged, GameObject dropLocation) {
        Debug.Log($"{dragged.tag} {dropLocation.tag}");
        if (dragged.tag == dropLocation.tag) { 
            Destroy(dragged.gameObject);
            count++;
            correctCG.DOFade(1, 0.5f).OnComplete(() =>
            {
                correctCG.DOFade(0, 1f);
            });
        }
        else
        {
            wrongCG.DOFade(1, 0.5f).OnComplete(() =>
            {
                wrongCG.DOFade(0, 1f);
            });
        }

        if (count == 6) {
            count = 0;
            index++;
            if (index == products.Length) { 
                OnGameOver.Invoke();
            }
            else
                StartCoroutine(CreateList(products[index]));
        }
    }

}
