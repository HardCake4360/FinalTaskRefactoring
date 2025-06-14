using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingWall : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public float speed;
    public float holdTime;
    public Rigidbody rb;
    public bool moveFlag;
    public bool stop;
    Vector3 StoE;
    Vector3 EtoS;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        this.transform.position = start.transform.position;

        StoE = (end.transform.position - start.transform.position).normalized;
        EtoS = (start.transform.position - end.transform.position).normalized;

        moveFlag = false;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(stop == false)
        {
            if (moveFlag == true)
            {
                rb.MovePosition(transform.position + StoE * speed * Time.deltaTime);
            }
            if (moveFlag == false)
            {
                rb.MovePosition(transform.position + EtoS * speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger");
        if (other.tag == "Move")
        {
            StartCoroutine(stay());
            if (moveFlag == false)
            {
                moveFlag = true;
                //Debug.Log("F->T");
            }
            else
            {
                moveFlag = false;
                //Debug.Log("T->F");
            }
        }
    }

    IEnumerator stay()
    {
        stop = true;
        yield return new WaitForSeconds(holdTime);
        stop = false;
    }
}