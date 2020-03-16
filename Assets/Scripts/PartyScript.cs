using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyScript : MonoBehaviour
{
    public GameObject[] currentParty = new GameObject[4];

    public GameObject[] heroRoster;

    bool setParty;
    public static bool resetParty = false;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeParty();
    }

    // Update is called once per frame
    void Update()
    {
        if (setParty)
        {
            if (resetParty)
            {
                for (int i = 0; i < currentParty.Length; i++)
                {
                    currentParty[i].GetComponent<HeroMovement>().RemoveFromParty();
                }
                RandomizeParty();
                resetParty = false;
                setParty = false;
            }
        }
        else
        {
            ChooseLeader(currentParty[0]);
            for (int i = 0; i < 3; i++)
            {
                ChooseLeader(currentParty[i + 1], currentParty[i]);
            }
            setParty = true;
        }
    }

    void RandomizeParty()
    {
        List<int> randNums = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int randNum = Random.Range(0, heroRoster.Length);
            if (randNums.Contains(randNum)) { i--; } else { randNums.Add(randNum); }
        }
        for (int i = 0; i < randNums.Count; i++)
        {
            currentParty[i] = heroRoster[randNums[i]];
        }
    }
    //The function is called to make one of the party members the leader
    public void ChooseLeader(GameObject choice)
    {
        choice.GetComponent<HeroMovement>().SetAsLeader();
    }
    public void ChooseLeader(GameObject member, GameObject choice)
    {
        member.GetComponent<HeroMovement>().SetTarget(choice);
    }
}
