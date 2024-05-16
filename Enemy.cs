using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playermanager;
    CharacterStats myStats;

    void Start ()
    {
        playermanager = playermanager.instance;
        myStats = GetComponent<ChrarcterStats>();
    }
    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playermanager.player.GetComponent<CharacterCombat>();
        if(playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}
