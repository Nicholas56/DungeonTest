using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAIScript : MonoBehaviour
{
    public enum actionState { Exploring, Fighting, Guarding, Escaping}
    public actionState state;

    public float checkRadius = 10f;
    public float checkDelay=2f;
    float timer=-1f;

    LayerMask checkFor;
    Transform enemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            SituationCheck();
            timer = Time.time + checkDelay;
        }
    }

    void SituationCheck()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius,checkFor);
        foreach(Collider col in hitColliders)
        {
            if (enemy == null)
            {
                enemy = col.transform;
            }
            else
            {
                if (Vector3.Distance(transform.position, enemy.position) > Vector3.Distance(transform.position, col.transform.position))
                {
                    enemy = col.transform;
                }
            }
        }
        if (enemy == null)
        {
            state = actionState.Exploring;
        }
        else { state = actionState.Fighting; }
    }
}
