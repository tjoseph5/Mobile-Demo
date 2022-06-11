using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shattered Targets")]
public class TargetSettings : ScriptableObject
{
    public float size;

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
