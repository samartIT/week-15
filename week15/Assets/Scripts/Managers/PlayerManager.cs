﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager {

	public ManagerStatus status {
		get;
		private set;
	}

	public int health {
		get;
		private set;
	}

	public int maxHealth {
		get;
		private set;
	}

    private NetworkService _network;
	public void Startup(NetworkService service) {
		Debug.Log ("Player Manager starting ...");
        _network = service;
        //health = 50;
        //maxHealth = 100;
        UpdateData(50, 100);
		status = ManagerStatus.Started;
	}
    public void UpdateData(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }

	public void ChangeHealth(int value){
		health += value;
		if (health > maxHealth)
			health = maxHealth;
		else if (health < 0)
			health = 0;

        if (health == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }

        //Debug.Log ("Health = " + health + "/" + maxHealth);
        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
	}		

    public void Respawn()
    {
        UpdateData(50, 100);
    }
}
