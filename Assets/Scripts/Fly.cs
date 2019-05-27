using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public List<Transform> traincars = new List<Transform>();
    public float movementSpeed;
    public float MRD;

    public float lerpRate = 0.2f;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50000))
            {
                // Rotate
                Vector3 direction_first = hit.point - traincars[0].position;
                traincars[0].rotation = Quaternion.Lerp(traincars[0].rotation, Quaternion.RotateTowards(traincars[0].rotation, Quaternion.LookRotation(direction_first, Vector3.up), MRD), lerpRate);

                // Move
                traincars[0].position += traincars[0].forward * movementSpeed;

                for (int i = 1; i < traincars.Count; i++)
                {
                    // Rotate
                    Vector3 direction = traincars[i - 1].GetChild(0).position - traincars[i].position;
                    traincars[i].rotation = Quaternion.Lerp(traincars[i].rotation, Quaternion.RotateTowards(traincars[i].rotation, Quaternion.LookRotation(direction, Vector3.up), MRD), lerpRate);

                    // Move
                    traincars[i].position = traincars[i - 1].GetChild(0).position;
                }
            }
        }
    }
}
