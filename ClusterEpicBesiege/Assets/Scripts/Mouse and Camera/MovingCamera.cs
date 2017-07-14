using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{

    public GameObject earth;
    private float widthArea;
    private float heightArea;
    private float scrollSpeed = 15f;
    private float xMin, xMax, zMin, zMAx;

    private Vector3 desiretPosition;



    public void SetViewArea(GameObject earth)
    {
        Mesh planeMesh = earth.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;
        widthArea = earth.transform.localScale.x * bounds.size.x;
        heightArea = earth.transform.localScale.z * bounds.size.z;
        xMin = -widthArea / 3f;
        xMax = widthArea / 3f;
        zMin = -heightArea / 1.7f;
        zMAx = heightArea / 5f;
    }


    private void Start()
    {
        SetViewArea(earth);
        desiretPosition = transform.position;
    }

    void Update()
    {
        float x = 0, y = 0, z = 0;
        float speed = scrollSpeed * Time.deltaTime;

        if (Input.mousePosition.x < widthArea) x -= speed;
        else if (Input.mousePosition.x > Screen.width - widthArea) x += speed;
        if (Input.mousePosition.y < heightArea) z -= speed;
        else if (Input.mousePosition.y > Screen.height - heightArea) z += speed;
        Vector3 move = new Vector3(x, y, z) + desiretPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.z = Mathf.Clamp(move.z, zMin, zMAx);
        desiretPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiretPosition, 0.2f);

    }


}
