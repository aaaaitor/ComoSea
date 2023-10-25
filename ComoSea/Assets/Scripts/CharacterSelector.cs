using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the selection of the character that is going to be played and
/// it passes the character data to the game scene.
/// </summary>
public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public PlayerScriptableObject playerData;

    void Awake()
    {
        // Saves selected player's instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// GETTER used to receive selected player's data.
    /// </summary>
    /// <returns>Selected player's data.</returns>
    public static PlayerScriptableObject GetData()
    {
        return instance.playerData;
    }

    /// <summary>
    /// Saves selected player's data.
    /// </summary>
    /// <param name="player">Player to be selected.</param>
    public void SelectCharacter(PlayerScriptableObject player)
    {
        playerData = player;
    }

    /// <summary>
    /// Destroys player's game object and instance to set the player data to a default state.
    /// </summary>
    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
