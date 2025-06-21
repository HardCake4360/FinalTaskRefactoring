using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : MonoBehaviour
{
    private Quaternion lastFixedRotation;
    private Quaternion nextFixedRotation;
    public Vector3 targetRotationVector;
    public bool tXfZ;
    public float yRotation;
    public float swingRange;
    public float swingSpeed;
    public float Speed;
    bool swingDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!tXfZ)
        {
            if (targetRotationVector.z < -swingRange)
            {
                swingDir = true;
            }
            else if (targetRotationVector.z > swingRange)
            {
                swingDir = false;
            }

            if (swingDir)
            {
                targetRotationVector.z += swingSpeed;
            }
            else
            {
                targetRotationVector.z -= swingSpeed;
            }
        }
        else
        {
            if (targetRotationVector.x < -swingRange)
            {
                swingDir = true;
            }
            else if (targetRotationVector.x > swingRange)
            {
                swingDir = false;
            }

            if (swingDir)
            {
                targetRotationVector.x += swingSpeed;
            }
            else
            {
                targetRotationVector.x -= swingSpeed;
            }
        }
        

        float interpolationAlpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        gameObject.transform.rotation = Quaternion.Slerp(lastFixedRotation, nextFixedRotation, interpolationAlpha);
    }
    private void FixedUpdate()
    {
        lastFixedRotation = nextFixedRotation;
        nextFixedRotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(targetRotationVector.x,yRotation,targetRotationVector.z), Speed * Time.fixedDeltaTime);
    }
}
