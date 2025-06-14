using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerReacter : MonoBehaviour
{
    public creeper crp;
    public TriggerState enter;
    public TriggerState exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        crp.changeTriggerState(enter);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        crp.changeTriggerState(exit);
    }
}