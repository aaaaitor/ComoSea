using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

/// <summary>
/// This class contains all the information about a character object.
/// This includes attributes, weapons, special variables the player uses to interact with the game world
/// and all the GETTERS and SETTERS.
/// </summary>
[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObject/Character")]
public class PlayerScriptableObject : ScriptableObject
{
    

    //Player's default intial attributes. Attributes the player ALWAYS has when starting the game.
    [SerializeField]
    GameObject initialWeapon;
    public GameObject InitialWeapon { get => initialWeapon; private set => initialWeapon = value; }

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float healthRecover;
    public float HealthRecover { get => healthRecover; private set => healthRecover = value; }

    [SerializeField]
    float movementSpeed;
    public float MovementSpeed { get => movementSpeed; private set => movementSpeed = value; }

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float jumpForce;
    public float JumpForce { get => jumpForce; private set => jumpForce = value; }


    //Optional attributes. Attributes the player only uses in certain situations.
    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField]
    float magnetism;
    public float Magnetism { get => magnetism; private set => magnetism = value; }
}
