using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_space_lookat : MonoBehaviour
{
    GameObject camera;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation *Vector3.back, camera.transform.rotation * Vector3.down);
    }
}
