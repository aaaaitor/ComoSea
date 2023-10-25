using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This class contain all the player's attributes and manages the way they change
/// while the game executes.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    //Player base stats
    PlayerScriptableObject playerData;
    [HideInInspector]
    public float actualHealth;
    [HideInInspector]
    public float actualMaxHealth;
    [HideInInspector]
    public float actualRecover;
    [HideInInspector]
    public float actualSpeed;
    [HideInInspector]
    public float actualDamage;
    [HideInInspector]
    public float actualProjectileSpeed;
    [HideInInspector]
    public float actualMagnetism;

    //List of player's weapons in case there are more than 1
    public List<GameObject> weapons;

    //TODO: BETA EXPERIENCE SYSTEM! NOT YET IMPLEMENTED
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceLimit;
    

    //Class to asign the level and experience limit to the player
    [System.Serializable]
    public class LevelRange
    {
        public int initialLevel;
        public int finalLevel;
        public int experienceLimitIncrease;
    }

    //List of the levels the player can achieve by gaining experience.
    public List<LevelRange> levelRanges;

    //I-Frames. Controls the way the player receives damage so he doesn't receive
    //more than 1 hit in a short period of time.
    [Header("I-Frames")]
    public float invincibleDuration;
    float actualInvincibility;
    bool invincible;

    void Awake()
    {
        //Set's games time scale to 1 (default)
        Time.timeScale = 1f;

        //Selected player data and destroy the instance
        playerData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        //Set the variable values for the game start
        actualHealth = playerData.MaxHealth;
        actualMaxHealth = playerData.MaxHealth;
        actualRecover = playerData.HealthRecover;
        actualSpeed = playerData.MovementSpeed;
        actualDamage = playerData.Damage;
        actualProjectileSpeed = playerData.ProjectileSpeed;
        actualMagnetism = playerData.Magnetism;
        invincible = false;

        //Assigns the initial weapon
        SpawnWeapon(playerData.InitialWeapon);
    }

    void Start()
    {
        //Start experience range in the first level
        experienceLimit = levelRanges[0].experienceLimitIncrease;
    }

    void Update()
    {
        //If the player has been hit he remains invincible some time
        if (actualInvincibility > 0)
        {
            actualInvincibility -= Time.deltaTime;
        }
        else if (invincible && actualInvincibility <= 0)
        {
            invincible = false;
        }

        Recover();
    }

    /// <summary>
    /// Adds a certain amount of experience to the player.
    /// </summary>
    /// <param name="xp">Experience amount to be increased.</param>
    public void IncreaseExperience(int xp)
    {
        experience += xp;
        LevelUpChecker();
    }

    /// <summary>
    /// Checks the current experience to determine if the player is able to
    /// increase a level or not. If the player increases his experience level
    /// the experience limit is also increased to mantain a progression curve.
    /// </summary>
    public void LevelUpChecker()
    {
        if (experience >= experienceLimit)
        {
            level++;
            experience -= experienceLimit;
            int experienceLimitIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.initialLevel && level <= range.finalLevel)
                {
                    experienceLimitIncrease = range.experienceLimitIncrease;
                    break;
                }
            }
            experienceLimit += experienceLimitIncrease;

            //TODO: BETA! NOT YET FINISHED!
            //ManageUpgradeStat();
        }
    }

    /// <summary>
    /// BETA! NOT YET FINISHED!
    /// Let's the player increase his level in certain attributes to upgrade them.
    /// </summary>
    public void ManageUpgradeStat()
    {
        //Set's game's time scale to 0 (stopped) while the player upgrades his stat.
        //  Time.timeScale = 0f;
        //  LevelManager.instance.LevelUp();
    }

    /// <summary>
    /// Manages the damage dealt to the player.
    /// </summary>
    /// <param name="dmg">Amount of damage dealt.</param>
    public void TakeDamage(float dmg)
    {
        //Checks that the player is not invincible
        if (!invincible)
        {
            //Damages player by decreasing health
            actualHealth -= dmg;

            actualInvincibility = invincibleDuration;
            invincible = true;

            //Checks if the player's health is less than 0 and kills him
            if (actualHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        // Mata al jugador
        //  LevelManager.instance.GameOver();
        //  gameObject.SetActive(false);
    }

    /// <summary>
    /// Rertores player's health in the case the player interacts with something that heals the player.
    /// </summary>
    /// <param name="recoveredHealth">Health amount to be recovered.</param>
    public void RestoreHealth(float recoveredHealth)
    {
        //Checks the max health so the player doesn't exceed it
        if (actualHealth < actualMaxHealth)
        {
            actualHealth += recoveredHealth;

            if (actualHealth > actualMaxHealth)
            {
                actualHealth = actualMaxHealth;
            }
        }
    }

    /// <summary>
    /// Heal the player by small amounts perodically.
    /// </summary>
    void Recover()
    {
        if (actualHealth < actualMaxHealth)
        {
            actualHealth += actualRecover * Time.deltaTime;

            //Checks the max health so the player doesn't exceed it
            if (actualHealth > actualMaxHealth)
            {
                actualHealth = actualMaxHealth;
            }
        }
    }

    /// <summary>
    /// Spawns a weapon the player can use.
    /// </summary>
    /// <param name="weapon">Weapon to be spawned.</param>
    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        weapons.Add(spawnedWeapon);
    }
}
