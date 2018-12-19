using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    float mainSpeed = 100.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    public Transform target;
    public float speed = 0.7f;

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), speed * Time.deltaTime);
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;
        //Mouse  camera angle done.  

    }

}