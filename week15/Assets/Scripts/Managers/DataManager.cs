using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, IGameManager {
    public ManagerStatus status { get; private set; }

    private string _filename;
    private NetworkService _network;

    public void Statup(NetworkService service)
    {
        Debug.Log("Data Manager starting ...");
        _network = service;
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        status = ManagerStatus.Started;
    }

    public void SaveGameState()
    {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        gamestate.Add("inventory", Managers.Inventory.GetData());
        gamestate.Add("health", Managers.Player.health);
        gamestate.Add("maxHealth", Managers.Player.maxHealth);
        gamestate.Add("curLevel", Managers.Player.curLevel);
        gamestate.Add("maxLevel", Managers.Player.maxLevel);
        FileStream stream = File.Create(_filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }

    public void LoadGameState

}
