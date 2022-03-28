using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public class PlayerStats : RealmObject
{
    [PrimaryKey]
    public string Username { get; set; }

    public RealmInteger<int> Score { get; set; }

    public PlayerStats() {} // ???
    
    public PlayerStats(string username, int score) {
        this.Username = username;
        this.Score = score;
    }
}
