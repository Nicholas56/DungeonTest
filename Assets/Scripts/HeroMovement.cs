using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float speed = 10f;

    Transform target;
    int wayPointNum = -1;

    bool goBack=false;
    bool isLeader=false;

    bool inParty=false;
    
    // Update is called once per frame
    void Update()
    {
        if (inParty)
        {
            if (isLeader)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);

                //When this is close enough to its target, the target is changed to the next one
                if (Vector3.Distance(transform.position, target.position) <= 0.4f)
                {
                    //If this reaches the last target, the order of movement is reversed
                    if (wayPointNum == WayPointHolderScript.points.Length - 1)
                    {
                        goBack = true;
                    }
                    else if (wayPointNum == 0)
                    {
                        goBack = false;
                        PartyScript.resetParty = true;
                        wayPointNum = -1;
                        Debug.Log("Reset party");
                    }
                    if (goBack == false)
                    {
                        wayPointNum++;
                    }
                    else { wayPointNum--; }
                    target = WayPointHolderScript.points[wayPointNum];
                }
            }
            else
            {
                //If the target gets too far away, this moves to catch up
                if (Vector3.Distance(transform.position, target.position) >= 1.5f)
                {
                    Vector3 direction = (target.position - transform.position).normalized;
                    transform.Translate(direction * speed * Time.deltaTime);
                }
            }
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        inParty = true;
        target = newTarget.transform;
    }

    public void SetAsLeader()
    {
        inParty = true;
        isLeader = true;
        wayPointNum = 1;
        target = WayPointHolderScript.points[wayPointNum];
    }

    public void RemoveFromParty()
    {
        wayPointNum = -1;
        inParty = false;
        isLeader = false;
    }
}
