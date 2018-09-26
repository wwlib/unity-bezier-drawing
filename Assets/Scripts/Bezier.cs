using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
[RequireComponent(typeof(LineRenderer))]
public class Bezier : MonoBehaviour
{
    public Transform[] controlPoints;
    public LineRenderer lineRenderer;
    public Button generateMeshButton;
    public Material material;

    private int curveCount = 0;
    private int layerOrder = 0;
    private int SEGMENT_COUNT = 50;

    private MeshRenderer meshRenderer;
    private MeshFilter filter;


    void Start()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.sortingLayerID = layerOrder;
        curveCount = (int)controlPoints.Length / 3;

        // Button btn1 = button.GetComponent<Button>();
        // btn1.onClick.AddListener(TaskOnClick);

        generateMeshButton.onClick.AddListener(TaskOnClick);
    }

    void Update()
    {

        DrawCurve();
        triangulateLine();
    }

    void TaskOnClick()
    {
        Debug.Log("Generating mesh!");
        triangulateLine();
    }

    void DrawCurve()
    {
        for (int j = 0; j <curveCount; j++)
        {
            for (int i = 1; i <= SEGMENT_COUNT; i++)
            {
                float t = i / (float)SEGMENT_COUNT;
                int nodeIndex = j * 3;
                Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints [nodeIndex].position, controlPoints [nodeIndex + 1].position, controlPoints [nodeIndex + 2].position, controlPoints [nodeIndex + 3].position);
                lineRenderer.SetVertexCount(((j * SEGMENT_COUNT) + i));
                lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
            }

        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    //// Mesh

    void triangulateLine () {
        // Create Vector2 vertices
        // Vector2[] testVertices2D = new Vector2[] {
        //     new Vector2(0,0),
        //     new Vector2(0,50),
        //     new Vector2(50,50),
        //     new Vector2(50,100),
        //     new Vector2(0,100),
        //     new Vector2(0,150),
        //     new Vector2(150,150),
        //     new Vector2(150,100),
        //     new Vector2(100,100),
        //     new Vector2(100,50),
        //     new Vector2(150,50),
        //     new Vector2(150,0),
        // };
        // for (int i=0; i<testVertices2D.Length; i++) {
        //     testVertices2D[i].Set( testVertices2D[i].x / 100,  testVertices2D[i].y / 100);
        //     // Logger.Log("testVert: (" + testVertices2D[i].x + ", " + testVertices2D[i].y + ")");
        // }

        int pointCount = lineRenderer.positionCount - 1; // skip last point which is the same as the first
        Vector2[] lineVertices2D = new Vector2[pointCount];
        Vector2 firstPoint = lineRenderer.GetPosition(0);
        // Logger.Log("firstPoint: (" + firstPoint.x + ", " + firstPoint.y + ")");
        for (int i=0; i<pointCount; i++) {
            Vector2 position = lineRenderer.GetPosition(i);
            // position.x -= firstPoint.x;
            // position.y -= firstPoint.y;
            // position.x /= 10;
            // position.y /= 10;
            lineVertices2D[i].Set(position.x, position.y);
            Logger.Log("lineVert: (" + lineVertices2D[i].x + ", " + lineVertices2D[i].y + ")");
        }

        Vector2[] vertices2D = lineVertices2D;
        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i=0; i<vertices.Length; i++) {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        Vector2[] uv = new Vector2[vertices.Length];
        for (int i=0; i<vertices.Length; i++) {
            Vector2 vertex = vertices[i];
            float x = (vertex.x + 5.12f) / 10.24f;
            float y = 1.0f - (vertex.y - 3.84f) / -07.68f;
            Logger.Log("uv: (" + x + ", " + y + ")");
            uv[i] = new Vector2(x, y);
        }

        msh.uv = uv;

        // Set up game object with mesh;
        if (!meshRenderer) {
          meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
          meshRenderer.material = material;
        }
        if (!filter) {
          filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        }

        filter.mesh = msh;
    }

}
