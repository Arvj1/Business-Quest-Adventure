using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class C6S1BulletHandler : MonoBehaviour
{
    public UnityEvent<string> OnHitOption;
    private float speed = 25;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            OnHitOption.Invoke(collision.gameObject.GetComponent<C6S1OptionTxt>().optionTxt.text);
        }
    }
}
