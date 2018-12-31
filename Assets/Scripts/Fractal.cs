using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{

    public Mesh mesh;
    public Material material;
    public int maxDepth;
    public int depth;
    public float childScale;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        Fractal selfRef = this;
        if (depth < maxDepth) {
            StartCoroutine(CreateChildren());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize(Fractal parent, Vector3 direction)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        transform.parent = parent.transform;
        childScale = parent.childScale;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = direction * (0.5f + 0.5f * childScale);
    }

    private IEnumerator CreateChildren()
    {
        yield return new WaitForSeconds(1.0f);
        new GameObject("Fractal Child").
            AddComponent<Fractal>().Initialize(this, Vector3.up);
        yield return new WaitForSeconds(1.5f);
        new GameObject("Fractal Child").
            AddComponent<Fractal>().Initialize(this, Vector3.right);
        yield return new WaitForSeconds(2.0f);
        new GameObject("Fractal Child").
            AddComponent<Fractal>().Initialize(this, Vector3.left);
    }
}
