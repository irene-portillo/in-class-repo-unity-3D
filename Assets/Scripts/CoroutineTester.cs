using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CoroutineTester : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private AnimationCurve curve;
    private float velocity = 0;
    private float velocityScale = 5;
    private Coroutine coroutine;
    
    IEnumerator MyCoroutine()
    {
        float t = 0;
        while (t <= time)
        {
            t += Time.deltaTime;

            float eval = curve.Evaluate(t / time);
            velocity += eval * velocityScale * Time.deltaTime;
            
            yield return null;
        }

        coroutine = null;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(MyCoroutine());
        }

        Vector3 xz = transform.position;
        xz.y = 0;
        Vector3 tangentialVelocity = Vector3.Cross(Vector3.up, xz);
        transform.position += velocity * Time.deltaTime * tangentialVelocity;
        
        // de-acceleration
        velocity -= Time.deltaTime * 2;
        if (velocity < 0)
            velocity = 0;
    }
}
