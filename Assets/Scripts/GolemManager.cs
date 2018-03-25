﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GolemManager : Photon.PunBehaviour {

    private const int HP = 10000, MP = 0, PAP = 100, MAP = 10, PDP = 30, MDP = 5;

    private CharacterAbility characterAbility;
    private AudioListener audioListener;
    private Camera otherCamera;

    // Use this for initialization
    void Start()
    {
        characterAbility = GetComponent<CharacterAbility>();
        characterAbility.Init(HP, MP, PAP, MAP, PDP, MDP);


        if (photonView.isMine)
        {
            if(PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
            {
                this.photonView.RPC("SetTeam", PhotonTargets.All, CharacterAbility.Team.blue);                
            }
            else
            {
                this.photonView.RPC("SetTeam", PhotonTargets.All, CharacterAbility.Team.red);            
            }
        }
        else
        {
            audioListener = GetComponentInChildren<AudioListener>();
            otherCamera = GetComponentInChildren<Camera>();
            audioListener.enabled = false;
            otherCamera.enabled = false;
            //this.tag = "Player";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (characterAbility.GetHP() <= 0)
        {
            Debug.Log("Grunt is dead");
        }
    }

    [PunRPC]
    private void SetTeam(CharacterAbility.Team team)
    {
        GetComponent<CharacterAbility>().SetTeam(team);
    }
}
