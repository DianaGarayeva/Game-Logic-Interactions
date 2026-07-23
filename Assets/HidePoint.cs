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
            _enemy.Hide(); 
        }
    }
}
