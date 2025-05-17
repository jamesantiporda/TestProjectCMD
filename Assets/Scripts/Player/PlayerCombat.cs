using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerGameplayInput playerGameplayInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerGameplayInput.AttackInput)
        {
            Debug.Log("Attack!");
        }
    }
}
