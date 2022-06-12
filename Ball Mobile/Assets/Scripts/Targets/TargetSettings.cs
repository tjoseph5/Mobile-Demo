using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shattered Targets")]
public class TargetSettings : ScriptableObject
{
    [Header("Shattered Target Prefab")]
    public GameObject shatteredTargetPrefab;
    public ParticleSystem shatteredParticleEffect; //color of particle effect is based off of the durability type's color

    [Header("Shattered Durability Break Force")]
    public float fragileShatteredBF;
    public float normalShatteredBF;
    public float sturdyShatteredBF;

    [Header("Durability Z Sclaes")]
    public float fragileZScale;
    public float normalZScale;
    public float sturdyZScale;

    [Header("Ten Point Target Settings")]
    public string tenPointName;
    public string tenPointShatteredName;
    public int tenPointScore;
    public Vector3 tenPointScale;
    public Material[] tenPointMaterial;

    [Header("Twenty Point Target Settings")]
    public string twentyPointName;
    public string twentyPointShatteredName;
    public int twentyPointScore;
    public Vector3 twentyPointScale;
    public Material[] twentyPointMaterial;

    [Header("Fifty Point Target Settings")]
    public string fiftyPointName;
    public string fiftyPointShatteredName;
    public int fiftyPointScore;
    public Vector3 fiftyPointScale;
    public Material[] fiftyPointMaterial;

    [Header("Hundred Point Target Settings")]
    public string hundredPointName;
    public string hundredPointShatteredName;
    public int hundredPointScore;
    public Vector3 hundredPointScale;
    public Material[] hundredPointMaterial;

    [Header("Thousand Point Target Settings")]
    public string thousandPointName;
    public string thousandPointShatteredName;
    public int thousandPointScore;
    public Vector3 thousandPointScale;
    public Material[] thousandPointMaterial;

    /*
     * This will be the main script that'll be used to hold the data for each target and as well as their shattered versions
     * Doing this will greatly improve data management within the game
     * 
     * This scriptable object must contain the following data:
     *  - Target Material
     *  - Target Size
     *  - Target Score
     *  - Target Name
     *  - Target's Shattered version (size, material, and name will depend on the target itself)
     *  
     *  Use the improvements made in Drunken Kaiju's destruction system into this game and build upon it
     *  
     *  Use switch case enums to specify each target's settings
     */
}
