using System.Collections;
using UnityEngine;

public class PlayerSightRange : MonoBehaviour
{
    SpriteRenderer playerSightRadius;
    Coroutine pulseCoroutine;
    Vector3 originalScale;

    private void Awake()
    {
        playerSightRadius = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        EnemyEvents.Instance.SpottedPlayer += EnablePlayerSightRadius;
        EnemyEvents.Instance.LostPlayer += DisablePlayerSightRadius;
    }

    private void OnDisable()
    {
        EnemyEvents.Instance.SpottedPlayer -= EnablePlayerSightRadius;
        EnemyEvents.Instance.LostPlayer -= DisablePlayerSightRadius;
    }

    private void EnablePlayerSightRadius()
    {
        playerSightRadius.enabled = true;
        if (pulseCoroutine == null) pulseCoroutine = StartCoroutine(Pulse());
    }

    public void DisablePlayerSightRadius()
    {
        playerSightRadius.enabled = false;
        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
            pulseCoroutine = null;
        }
        transform.localScale = originalScale;
    }

    private IEnumerator Pulse()
    {
        float pulseDuration = 0.4f;
        float maxScaleMultiplier = 1.15f;
        float minAlpha = 0.4f;

        Vector3 baseScale = transform.localScale;
        Color baseColor = playerSightRadius.color;

        float t = 0f;

        while (true)
        {
            t += Time.deltaTime;

            float normalized = Mathf.PingPong(t / pulseDuration, 1f);

            float scaleMultiplier = Mathf.Lerp(1f, maxScaleMultiplier, normalized);
            transform.localScale = baseScale * scaleMultiplier;

            float alpha = Mathf.Lerp(0.5f, minAlpha, normalized);
            playerSightRadius.color = new Color(
                baseColor.r,
                baseColor.g,
                baseColor.b,
                alpha
            );

            yield return null;
        }
    }
}
