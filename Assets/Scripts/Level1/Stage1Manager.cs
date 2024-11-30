 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage1Manager : MonoBehaviour
{
    [SerializeField] RectTransform startTransform, endTransform;
    [SerializeField] GameObject productsListPrefab;

    private void Start()
    {
        Invoke("CreateList", 2f);
    }


    public void CreateList()
    {
        GameObject instantiatedProduct = Instantiate(productsListPrefab, startTransform.position, Quaternion.identity, startTransform.parent);

        // Get the RectTransform component of the instantiated object
        RectTransform productRect = instantiatedProduct.GetComponent<RectTransform>();

        // Set the initial position to the startTransform's position
        productRect.position = startTransform.position;

        // Animate the productRect to the endTransform's position
        productRect.DOMove(endTransform.position, 1f) // 1f is the duration of the animation
                    .SetEase(Ease.InOutSine); // Ease in-out effect
    }

}
