using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject Planet;

    float speed = 4;
    bool OnGround = false;

    float gravity = 100;
    float distanceToGround;
    Vector3 Groundnormal;
    private Rigidbody rb;
    RaycastHit hit = new RaycastHit();
    [SerializeField]
    GameObject Atmosphere;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Physics.IgnoreCollision(Atmosphere.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(0, 0, z);

        // Local Rotation
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 150 * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -150 * Time.deltaTime, 0);
        }

        // GroundControl

        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            // ROTATION
            Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
            transform.rotation = toRotation;
        }
    }
}