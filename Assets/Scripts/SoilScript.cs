using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilScript : MonoBehaviour
{
    public bool isWet;
    public float timeToDry = 120;
    public Material materialDry;
    public Material materialWet;

    private MeshRenderer thisMeshRenderer;
    private float dryCooldown = 0;

    void Awake() { 
        thisMeshRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dryCooldown = timeToDry;
    }

    // Update is called once per frame
    void Update()
    {
        //Update material
        thisMeshRenderer.material = isWet ? materialWet : materialDry;

        // Dry soil
        if(isWet) {
            dryCooldown -= Time.deltaTime;
            if(dryCooldown <= 0) {
                isWet = false;
            } 
        }
    }

    public void Water() {
        isWet = true;
        dryCooldown = timeToDry;
    }
}
