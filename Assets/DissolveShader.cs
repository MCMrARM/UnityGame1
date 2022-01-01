using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveShader : MonoBehaviour
{
    private float progress = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var mat in GetComponent<SkinnedMeshRenderer>().sharedMaterials)
            mat.SetFloat("Dissolve_Progress", progress);
        progress += Time.deltaTime;
        if (progress >= 2)
            progress = 0;
    }
}
