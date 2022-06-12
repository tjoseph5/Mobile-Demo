using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShatter : MonoBehaviour
{

    GameObject playerBall; //The player Ball
    Renderer targetRenderer; //gameObject's renderer

    public enum TargetPointTypes { pointTen, pointTwenty, pointFifty, pointHundred, pointThousand }; //list of different types of targets. Each variable has their own unique score variable, scales, and material color
    public TargetPointTypes targetPointType = TargetPointTypes.pointTen;
    public enum TargetDurabilityTypes { fragile, normal, sturdy };
    public TargetDurabilityTypes targetDurabilityType;

    int score; //score the player recieves when the target is hit
    float zScale;
    float durableTypeBreakForce;
    TargetManager targetManager;

    [SerializeField] TargetSettings targetSettings;

    void Awake()
    {
        targetRenderer = gameObject.GetComponent<Renderer>();
        playerBall = GameObject.FindGameObjectWithTag("Player");
        targetManager = GameObject.Find("Target Manager").GetComponent<TargetManager>();

        switch (targetDurabilityType)
        {
            case TargetDurabilityTypes.fragile:
                zScale = targetSettings.fragileZScale;
                durableTypeBreakForce = targetSettings.fragileShatteredBF;
                break;

            case TargetDurabilityTypes.normal:
                zScale = targetSettings.normalZScale;
                durableTypeBreakForce = targetSettings.normalShatteredBF;
                break;

            case TargetDurabilityTypes.sturdy:
                zScale = targetSettings.sturdyZScale;
                durableTypeBreakForce = targetSettings.sturdyShatteredBF;
                break;
        }
    }

    void Start()
    {
        //This switch enum defines each list variable's score amount, size, color, and name
        switch (targetPointType)
        {
            case TargetPointTypes.pointTen:
                gameObject.transform.localScale = new Vector3(targetSettings.tenPointScale.x, targetSettings.tenPointScale.y, zScale);
                gameObject.name = targetSettings.tenPointName;
                score = targetSettings.tenPointScore;

                switch (targetDurabilityType)
                {
                    case TargetDurabilityTypes.fragile:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.tenPointMaterial[0];
                        break;

                    case TargetDurabilityTypes.normal:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.tenPointMaterial[1];
                        break;

                    case TargetDurabilityTypes.sturdy:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.tenPointMaterial[2];
                        break;
                }
                break;

            case TargetPointTypes.pointTwenty:
                gameObject.transform.localScale = new Vector3(targetSettings.twentyPointScale.x, targetSettings.twentyPointScale.y, zScale);
                gameObject.name = targetSettings.twentyPointName;
                score = targetSettings.twentyPointScore;

                switch (targetDurabilityType)
                {
                    case TargetDurabilityTypes.fragile:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.twentyPointMaterial[0];
                        break;

                    case TargetDurabilityTypes.normal:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.twentyPointMaterial[1];
                        break;

                    case TargetDurabilityTypes.sturdy:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.twentyPointMaterial[2];
                        break;
                }
                break;

            case TargetPointTypes.pointFifty:
                gameObject.transform.localScale = new Vector3(targetSettings.fiftyPointScale.x, targetSettings.fiftyPointScale.y, zScale);
                gameObject.name = targetSettings.fiftyPointName;
                score = targetSettings.fiftyPointScore;

                switch (targetDurabilityType)
                {
                    case TargetDurabilityTypes.fragile:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.fiftyPointMaterial[0];
                        break;

                    case TargetDurabilityTypes.normal:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.fiftyPointMaterial[1];
                        break;

                    case TargetDurabilityTypes.sturdy:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.fiftyPointMaterial[2];
                        break;
                }
                break;

            case TargetPointTypes.pointHundred:
                gameObject.transform.localScale = new Vector3(targetSettings.hundredPointScale.x, targetSettings.hundredPointScale.y, zScale);
                gameObject.name = targetSettings.hundredPointName;
                score = targetSettings.hundredPointScore;

                switch (targetDurabilityType)
                {
                    case TargetDurabilityTypes.fragile:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.hundredPointMaterial[0];
                        break;

                    case TargetDurabilityTypes.normal:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.hundredPointMaterial[1];
                        break;

                    case TargetDurabilityTypes.sturdy:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.hundredPointMaterial[2];
                        break;
                }
                break;

            case TargetPointTypes.pointThousand:
                gameObject.transform.localScale = new Vector3(targetSettings.thousandPointScale.x, targetSettings.thousandPointScale.y, zScale);
                gameObject.name = targetSettings.thousandPointName;
                score = targetSettings.thousandPointScore;

                switch (targetDurabilityType)
                {
                    case TargetDurabilityTypes.fragile:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.thousandPointMaterial[0];
                        break;

                    case TargetDurabilityTypes.normal:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.thousandPointMaterial[1];
                        break;

                    case TargetDurabilityTypes.sturdy:
                        gameObject.GetComponent<MeshRenderer>().material = targetSettings.thousandPointMaterial[2];
                        break;
                }
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerBall)
        {
            ShatterFunction();
        }
    }

    void ShatterFunction()
    {
        Destroy(gameObject);
        targetManager.levelScore += score;

        switch (targetPointType) //Instantiates different shattered target types depending on their variable
        {
            case TargetPointTypes.pointTen:

                GameObject tenShattered = Instantiate(targetSettings.shatteredTargetPrefab, this.transform.position, this.transform.rotation);
                tenShattered.transform.localScale = new Vector3(targetSettings.tenPointScale.x, targetSettings.tenPointScale.y, zScale);

                foreach (Renderer mat in tenShattered.GetComponentsInChildren<Renderer>())
                {
                    mat.material = gameObject.GetComponent<Renderer>().material;
                }

                foreach (Rigidbody rb in tenShattered.GetComponentsInChildren<Rigidbody>())
                {
                    Vector3 force = (rb.transform.position - transform.position).normalized * durableTypeBreakForce;
                    rb.AddForce(force);
                }

                targetManager.ten += 1;
                break;

            case TargetPointTypes.pointTwenty:

                GameObject twentyShattered = Instantiate(targetSettings.shatteredTargetPrefab, this.transform.position, this.transform.rotation);
                twentyShattered.transform.localScale = new Vector3(targetSettings.twentyPointScale.x, targetSettings.twentyPointScale.y, zScale);

                foreach (Renderer mat in twentyShattered.GetComponentsInChildren<Renderer>())
                {
                    mat.material = gameObject.GetComponent<Renderer>().material;
                }

                foreach (Rigidbody rb in twentyShattered.GetComponentsInChildren<Rigidbody>())
                {
                    Vector3 force = (rb.transform.position - transform.position).normalized * durableTypeBreakForce;
                    rb.AddForce(force);
                }

                targetManager.twenty += 1;
                break;

            case TargetPointTypes.pointFifty:

                GameObject fiftyShattered = Instantiate(targetSettings.shatteredTargetPrefab, this.transform.position, this.transform.rotation);
                fiftyShattered.transform.localScale = new Vector3(targetSettings.fiftyPointScale.x, targetSettings.fiftyPointScale.y, zScale);

                foreach (Renderer mat in fiftyShattered.GetComponentsInChildren<Renderer>())
                {
                    mat.material = gameObject.GetComponent<Renderer>().material;
                }

                foreach (Rigidbody rb in fiftyShattered.GetComponentsInChildren<Rigidbody>())
                {
                    Vector3 force = (rb.transform.position - transform.position).normalized * durableTypeBreakForce;
                    rb.AddForce(force);
                }

                targetManager.fifty += 1;
                break;

            case TargetPointTypes.pointHundred:

                GameObject hundredShattered = Instantiate(targetSettings.shatteredTargetPrefab, this.transform.position, this.transform.rotation);
                hundredShattered.transform.localScale = new Vector3(targetSettings.hundredPointScale.x, targetSettings.hundredPointScale.y, zScale);

                foreach (Renderer mat in hundredShattered.GetComponentsInChildren<Renderer>())
                {
                    mat.material = gameObject.GetComponent<Renderer>().material;
                }

                foreach (Rigidbody rb in hundredShattered.GetComponentsInChildren<Rigidbody>())
                {
                    Vector3 force = (rb.transform.position - transform.position).normalized * durableTypeBreakForce;
                    rb.AddForce(force);
                }

                targetManager.hundred += 1;
                break;

            case TargetPointTypes.pointThousand:

                GameObject thousandShattered = Instantiate(targetSettings.shatteredTargetPrefab, this.transform.position, this.transform.rotation);
                thousandShattered.transform.localScale = new Vector3(targetSettings.thousandPointScale.x, targetSettings.thousandPointScale.y, zScale);

                foreach (Renderer mat in thousandShattered.GetComponentsInChildren<Renderer>())
                {
                    mat.material = gameObject.GetComponent<Renderer>().material;
                }

                foreach (Rigidbody rb in thousandShattered.GetComponentsInChildren<Rigidbody>())
                {
                    Vector3 force = (rb.transform.position - transform.position).normalized * durableTypeBreakForce;
                    rb.AddForce(force);
                }

                targetManager.thousand += 1;
                break;
        }

        ParticleSystem particle = Instantiate(targetSettings.shatteredParticleEffect, playerBall.transform.position, playerBall.transform.rotation);
    }
}
