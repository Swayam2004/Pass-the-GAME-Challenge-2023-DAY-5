using UnityEngine;

public class Zone : MonoBehaviour
{
    private const float BORDER_POINTS_PER_UNIT = 5;

    [SerializeField] private LineRenderer border;
    [SerializeField] private LineRenderer progressBorder;
    [SerializeField] private float radius;
    [SerializeField] private float rotateSpeedIdle = 5f;
    [SerializeField] private float rotateSpeedActive = 20f;
    [SerializeField] private GameObject centerSprite;
    [SerializeField] private AudioSource _zoneAudioSource;

    private float angle = 0f;
    private int planetCount = 0;

    void Start()
    {
        _zoneAudioSource.volume = 0f;
        _zoneAudioSource.Stop();

        RenderProgressBorder();
        RenderBorder(true);
        CircleCollider2D insideTrigger = gameObject.GetComponent<CircleCollider2D>();
        insideTrigger.radius = radius - 1;
        GameLogic.Instance.OnWinTimerProgress += GameLogic_OnWinTimerProgress;
        SetProgress(0f);
        centerSprite.SetActive(false);
    }

    private void GameLogic_OnWinTimerProgress(object sender, GameLogic.WinTimerProgressEventArgs e)
    {
        progressBorder.material.mainTextureOffset = new Vector2(e.progressNormalized, 0);

        _zoneAudioSource.volume = 1f;
        _zoneAudioSource.Play();

        SetProgress(e.progressNormalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Planet.LAYER)
        {
            if (collision.gameObject.GetComponent<Linkable>().isPlanet)
            {
                planetCount++;
                collision.gameObject.GetComponent<Planet>().SetSaved(true);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Planet.LAYER)
        {
            if (collision.gameObject.GetComponent<Linkable>().isPlanet)
            {
                planetCount--;
                collision.gameObject.GetComponent<Planet>().SetSaved(false);

            }
        }
    }

    private void OnDrawGizmos()
    {
        RenderBorder(false);
    }

    private void RenderBorder(bool scaled)
    {
        border.positionCount = GetNumBorderPoints(radius);
        Vector3[] points = GetBorderPoints(transform.position, radius);
        border.SetPositions(points);
        border.loop = true;
        if (scaled)
        {
            float drawnLength = 0f;
            for (int i = 0; i < border.positionCount; i++)
            {
                drawnLength += Vector3.Distance(border.GetPosition(i), border.GetPosition((i + 1) % border.positionCount));
            }
            float roundedLength = Mathf.Round(drawnLength);
            //border.textureScale = new Vector2(roundedLength / drawnLength, 1);
        }
    }

    private void RenderProgressBorder()
    {
        progressBorder.positionCount = GetNumBorderPoints(radius);
        Vector3[] points = GetBorderPoints(transform.position, radius);
        progressBorder.SetPositions(points);
    }

    private void SetProgress(float progress)
    {
        bool active = progress > 0f;
        if (progressBorder.gameObject.activeSelf != active)
        {
            progressBorder.gameObject.SetActive(active);
            centerSprite.SetActive(active);
        }
        float centerCircleRadius = progress * radius * 2;
        centerSprite.transform.localScale = new Vector3(centerCircleRadius, centerCircleRadius, 0);

        progressBorder.material.mainTextureOffset = new Vector2(-progress, 0);

        _zoneAudioSource.pitch = progress;
    }

    private void Update()
    {
        float rotateSpeed = planetCount > 0 ? rotateSpeedActive : rotateSpeedIdle;
        angle += rotateSpeed * Time.deltaTime;
        border.material.mainTextureOffset = new Vector2(angle / (2 * Mathf.PI), 0);
        RenderBorder(true);
    }

    public float GetRadius()
    {
        return radius;
    }

    public int GetPlanetCount()
    {
        return planetCount;
    }

    public static float GetBorderLength(float radius)
    {
        return 2 * Mathf.PI * radius;
    }

    public static int GetNumBorderPoints(float radius)
    {
        float borderLength = GetBorderLength(radius);
        return Mathf.CeilToInt(borderLength * BORDER_POINTS_PER_UNIT);
    }

    public static Vector3[] GetBorderPoints(Vector3 center, float radius, float angle = 0f)
    {
        int numBorderPoints = Zone.GetNumBorderPoints(radius);
        Vector3[] points = new Vector3[numBorderPoints];
        for (int i = 0; i < numBorderPoints; i++)
        {
            float a = angle + i * Mathf.PI * 2 / numBorderPoints;
            points[i] = center + new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0) * radius;
        }
        return points;
    }
}
