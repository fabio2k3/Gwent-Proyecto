using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public string name;

    public string type;

    public int attack;

    public bool invocated;

    public int rowInvocated;
    public int colInvocated;

    public int climateInvocated;

    public Vector3 originalPosition;
    public void AssignAttack(string newName, string newType, int newAttack)
    {
        name = newName;
        type = newType;

        if(type == "Unit")
        {
            attack = newAttack;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

