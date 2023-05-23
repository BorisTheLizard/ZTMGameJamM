using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform[] childTransforms;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        childTransforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
        }
        SetLineRendererPositions();
    }

    private void SetLineRendererPositions()
    {
        lineRenderer.positionCount = 0;
        for (int i = 0; i < childTransforms.Length; i++)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(i, childTransforms[i].position);
        }
    }
}