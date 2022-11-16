using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilScript : MonoBehaviour
{
    public bool isWet;
    public Material materialDry;
    public Material materialWet;

    private MeshRenderer thisMeshRenderer;

    void Awake() { 
        thisMeshRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update material
        thisMeshRenderer.material = isWet ? materialWet : materialDry;
    }
}
