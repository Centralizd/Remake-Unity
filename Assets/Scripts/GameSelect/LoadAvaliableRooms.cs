﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.UI;

public class LoadAvaliableRooms : MonoBehaviour
{

    public GameRoomButton scrollItemTemplate;
    private Api api = null;
    private List<GameRoom> gameRooms = null;

    // Start is called before the first frame update
    async void Start()
    {
        LoadGameRooms();
    }

    public async void LoadGameRooms()
    {
        gameObject.AddComponent<Api>();
        api = gameObject.GetComponent<Api>();
        List<GameRoom> roomResponse = await api.GetOpenRooms();
        
        // Destroy all existing rooms.
        GameRoomButton[] existingButtons = FindObjectsOfType<GameRoomButton>();
        foreach (GameRoomButton gameRoomButton in existingButtons)
        {
            Destroy(gameRoomButton.gameObject);
        }

        string rooms = "";
        foreach(GameRoom room in roomResponse)
        {
            // Create a new templated item
            GameRoomButton scrollItem = (GameRoomButton)Instantiate(scrollItemTemplate);
            scrollItem.gameObject.SetActive(true);
            scrollItem.room = room;
            
            // Set the text
            Text text = scrollItem.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = "[ Title: " + room.description + ", Seed: " + room.seed + ", Players: " + room.player_count + ", Anonymous: " + room.anonimity + ", Created By: " + room.creator_id + "]";
            }
            else
            {
                Debug.Log("No Text.");
            }

            Debug.Log("[ descrip: " + room.description + ", seed: " + room.seed + "]");

            // Set the button's parent to the scroll item template.
            scrollItem.transform.SetParent(scrollItemTemplate.transform.parent, false);
            
            // rooms += "[ descrip: " + room.description + ", seed: " + room.seed + "]";
        }
        // gameObject.GetComponent<Text>().text = rooms;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
