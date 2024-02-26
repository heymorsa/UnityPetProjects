using System;
using Shapes;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pyramid : IShape
{
    public event Action<IShape> Destroyed;
    private GameObject cubePrefab;

    public Pyramid(GameObject cubePrefab)
    {
        this.cubePrefab = cubePrefab;
    }

    public void SpawnAtPosition(Vector3 position)
    {
        GameObject basePyramid = Object.Instantiate(cubePrefab);
        basePyramid.transform.position = position;
        
        CheckForDestroy checkForDestroy = basePyramid.GetComponent<CheckForDestroy>();
        checkForDestroy.Destroyed += () => Destroyed?.Invoke(this);
        
        Mesh cubeMesh = cubePrefab.GetComponent<MeshFilter>().sharedMesh;
        
        Mesh pyramidMesh = new Mesh();
        
        Vector3[] vertices = new Vector3[5];
        vertices[0] = cubeMesh.vertices[0];
        vertices[1] = cubeMesh.vertices[2];
        vertices[2] = cubeMesh.vertices[4];
        vertices[3] = cubeMesh.vertices[6];
        vertices[4] = Vector3.zero;

        pyramidMesh.vertices = vertices;
        
        int[] triangles = new int[]
        {
            0, 1, 4,
            1, 2, 4,
            2, 3, 4,
            3, 0, 4, 
            0, 2, 1, 
            2, 0, 3 
        };

        pyramidMesh.triangles = triangles;
        pyramidMesh.RecalculateNormals();
        
        Vector2[] uv = new Vector2[pyramidMesh.vertices.Length];

        for (int index = 0; index < uv.Length; index++)
        {
            uv[index] = Vector2.zero;
        }

        pyramidMesh.uv = uv;

        MeshFilter meshFilter = basePyramid.GetComponent<MeshFilter>();
        meshFilter.mesh = pyramidMesh;

        MeshCollider meshCollider = basePyramid.AddComponent<MeshCollider>();
        meshCollider.convex = true;

        Rigidbody rigidbody = basePyramid.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        
        MeshRenderer meshRenderer = basePyramid.GetComponent<MeshRenderer>();
        meshRenderer.material = cubePrefab.GetComponent<MeshRenderer>().sharedMaterial;
        
        basePyramid.transform.Rotate(Vector3.back, 90f);
        
        Fracture fracture = basePyramid.GetComponent<Fracture>();
    }


    public void SetColor(Color color)
    {
        cubePrefab.GetComponent<Renderer>().sharedMaterial.color = color;
    }
}