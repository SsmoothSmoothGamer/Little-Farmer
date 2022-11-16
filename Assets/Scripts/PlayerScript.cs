using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed = 10;
    public int itemIndex = 0;
    public float itemOffset = 2;

    private Rigidbody thisRigidbody;
    private GameObject holdingObject;

    void Awake() {
        thisRigidbody = GetComponent<Rigidbody>();
    }
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() 
    {
        if(holdingObject != null) {
            var newPosition = transform.position + new Vector3(0, itemOffset, 0);
            holdingObject.transform.position = newPosition;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        // Create input vector
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        Vector2 movementVector = new Vector2(inputX, inputY);

        // Get forward
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        Quaternion forward = Quaternion.Euler(0, eulerY, 0);

       // Create vector
        Vector3 walkVector = new Vector3(movementVector.x, 0, movementVector.y);
        walkVector = forward * walkVector;
        walkVector *= movementSpeed;

        // Apply input to character
        thisRigidbody.AddForce(walkVector, ForceMode.Force);
    }

    // Verifica se está colidindo com um objetos
    void OnTriggerEnter(Collider other) {
        GameObject otherObject = other.gameObject;

        // Sensor
        if(otherObject.CompareTag("Sensor")) {
            var sensorScript = otherObject.GetComponent<SensorScript>();
            var index = sensorScript.itemIndex;
            UpdateIndex(index);
        }

        // Soil
        if(otherObject.CompareTag("Soil")){
            var soilScript = otherObject.GetComponent<SoilScript>();
            if(itemIndex == 3) {
                soilScript.Water();
            }
        }
    }

    private void UpdateIndex(int index) {
        this.itemIndex = index;

        // Destroy previous object
        if(holdingObject != null) {
            Destroy(holdingObject);
            holdingObject = null;
        }

    // Create new object
        GameObject newObjectPrefab = GameManager.Instance.itemObjects[index];
        if(newObjectPrefab != null) {
            var position = transform.position;
            var rotation = newObjectPrefab.transform.rotation;
            holdingObject = Instantiate(newObjectPrefab, position, rotation);
        }
    }

}
