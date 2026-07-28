using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public void HitBarrier()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(BarrierRoutine());
    }

    IEnumerator BarrierRoutine()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
