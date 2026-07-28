using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField]
    private int _barrelType;
    [SerializeField]
    private float _speed=5f;
    [SerializeField]
    private Transform _pointA;
    [SerializeField]
    private Transform _pointB;

    private Transform _target;

    [SerializeField]
    private float _fadeDuration = 1f;
    private Material _mat; 
    private void Start()
    {
        _mat = GetComponent<Renderer>().material; 

        _target = _pointB;
    }
    void Update()
    {
        switch (_barrelType) 
        {
            case 1:
                CalculateMovment();
                break;
        }
        
    }

    private void CalculateMovment()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, _target.position)<0.01f)
        {
            if (_target == _pointA)
            {
                _target = _pointB;
            }else if(_target == _pointB)
            {
                _target = _pointA;
            }
        }
    }
    
    public void HitBarrier()
    {
        StartCoroutine(FadeoutRoutine());
    }

    IEnumerator FadeinRoutine()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

        Color color = _mat.color;
        float time = 0;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, time / _fadeDuration);
            _mat.color = color;

            yield return null;
        }

        color.a = 1;
        _mat.color = color;
    }


    IEnumerator FadeoutRoutine()
    {
        Color color = _mat.color;
        float time = 0;
        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, time / _fadeDuration);
            _mat.color = color;

            yield return null;
        }

        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(FadeinRoutine());

    }
}
