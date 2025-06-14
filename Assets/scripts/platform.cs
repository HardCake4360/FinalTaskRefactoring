using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject arrow;
    [SerializeField] private bool stepOn;
    [SerializeField] private float gap;
    [SerializeField] private float fireTerm;

    // Start is called before the first frame update
    void Start()
    {
        stepOn = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "step") return;
        stepOn = true;
        transform.position = new Vector3(transform.position.x, transform.position.y - gap, transform.position.z);
        Instantiate(arrow,start.transform.position,start.transform.rotation);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "step") return;
        stepOn = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + gap, transform.position.z);
    }
    IEnumerator TriggerTrap()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(fireTerm);
            Instantiate(arrow,start.transform.position, start.transform.rotation);
        }
    }
}