using UnityEngine;
using System.Collections;

public class PlayerPaddle : MonoBehaviour
{
    private Vector3 originalScale;
    private Coroutine resizeCoroutine;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void Enlarge(float factor, float duration)
    {
        if (resizeCoroutine != null) StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ChangeSize(originalScale * factor, duration));
    }

    public void Shrink(float factor, float duration)
    {
        if (resizeCoroutine != null) StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ChangeSize(originalScale / factor, duration));
    }

    IEnumerator ChangeSize(Vector3 newSize, float duration)
    {
        transform.localScale = newSize;
        yield return new WaitForSeconds(duration);
        transform.localScale = originalScale;
    }
}
