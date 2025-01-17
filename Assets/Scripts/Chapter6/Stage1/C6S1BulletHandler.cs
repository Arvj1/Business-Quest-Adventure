using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C6S1BulletHandler : MonoBehaviour
{
    private float speed = 25;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
