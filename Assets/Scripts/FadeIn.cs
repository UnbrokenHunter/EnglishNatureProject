using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private Vector3 scale;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float delta;
    [SerializeField] private float timeMultipler = 1;
    private WaitForSeconds wait;
    private float time = 0;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        scale = transform.localScale;
        transform.localScale = Vector3.zero;
        wait = new WaitForSeconds(delta);

        StartCoroutine(nameof(Scale));
    }

    public IEnumerator Scale()
    {
        while (transform.localScale != scale)
        {
            transform.localScale = curve.Evaluate(time * timeMultipler) * scale;
            time += Time.deltaTime;
            yield return wait;
        }
    }

}
