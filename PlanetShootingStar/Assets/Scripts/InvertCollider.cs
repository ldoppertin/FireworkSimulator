
using UnityEngine;
using System.Linq;
using System.Collections;

public class InvertCollider : MonoBehaviour
{
    public bool removeExistingColliders = true;

    public void CreateInvertedMeshCollider()
    {
        if (removeExistingColliders)
            RemoveExistingColliders();

        InvertMesh();

        gameObject.AddComponent<MeshCollider>();
        Debug.Log("done");
    }

    private void RemoveExistingColliders()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        DestroyImmediate(collider);
    }

    private void InvertMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

    private void Start()
    {
        CreateInvertedMeshCollider();
    }


}