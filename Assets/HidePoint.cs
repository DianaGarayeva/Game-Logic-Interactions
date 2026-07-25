using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            EnemyAI _enemy = other.GetComponent<EnemyAI>();

            _enemy.GetComponent<Collider>().enabled = false; 
            _enemy.Hide(); 
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    other.GetComponent<Collider>().enabled = true;
    //}
}
