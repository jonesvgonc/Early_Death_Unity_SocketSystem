using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{


    [SerializeField] private LayerMask mask;


    Mesh mesh;

    [SerializeField] private Transform pivot;

    private Vector3 origin;

    private float startAngle;

    float fov = 90f;

    private void Start()
    {
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetPivot(Transform pivot)
    {
        this.pivot = pivot;
    }

    public void SetOrigin()
    {
        this.origin = pivot.position;
    }

    public void SetDirection(Vector3 aimDirection)
    {
        startAngle = GetAngleFromVectorFloat(aimDirection) - fov / 2f; 
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private void LateUpdate()
    {
        transform.position = pivot.position;
        transform.rotation = pivot.rotation;

        int raycount = 50;
        
        float angle = 0f;

        float angleIncrease = fov / raycount;

        float viewDistance = 5f;
        origin = pivot.position;

        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];


        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= raycount; i++)
        {
            Vector3 vertex;

            RaycastHit2D hit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, mask);

            if (hit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = hit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {

                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            angle -= angleIncrease;
            vertexIndex++;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;



    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angledRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angledRad), Mathf.Sin(angledRad));
    }


}
