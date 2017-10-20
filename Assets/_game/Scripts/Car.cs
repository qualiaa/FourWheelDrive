using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public Wheel[] wheels;
    public Rigidbody body_;
    public AudioSource engineSource;
    public AudioClip engineStart;
    public AudioClip engineIdle;
    public AudioClip enginePower1;
    public AudioClip enginePower2;
    public AudioClip enginePower3;

	float direction; // direction of front of car from 0-360
    float wheelBase;

	Wheel[,] axles_;


	// Use this for initialization
	void Start () {
		axles_ = new Wheel[2, 2];
        wheelBase = wheels[1].transform.localPosition.x - wheels[0].transform.localPosition.x;
        engineSource.clip = engineStart;
        engineSource.Play();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        HandleAudio();
        HandlePhysics();
	}

    void HandleAudio()
    {
        if (engineSource.clip == engineStart)
        {
            if (!engineSource.isPlaying)
            {
                engineSource.loop = true;
                engineSource.clip = engineIdle;
                engineSource.Play();
            }
        }
        else
        {
            int numActiveWheels = 0;
            foreach (var w in wheels)
            {
                numActiveWheels += w.active ? 1 : 0;
            }
            AudioClip appropriateClip;
            switch (numActiveWheels)
            {
                case 0:
                    appropriateClip = engineIdle;
                    break;
                case 1:
                    appropriateClip = enginePower1;
                    break;
                case 4:
                    appropriateClip = enginePower3;
                    break;
                default:
                    appropriateClip = enginePower2;
                    break;
            }
            if (engineSource.clip != appropriateClip)
            {
                engineSource.clip = appropriateClip;
                engineSource.Play();
            }
        }
    }

    void HandlePhysics()
    {
        Vector3 topAxleDirection = (wheels[0].direction + wheels[1].direction) / 2;
        Vector3 botAxleDirection = (wheels[2].direction + wheels[3].direction) / 2;

        var totalForce = Vector3.zero;
        foreach (var w in wheels)
        {
            totalForce += w.totalForce;
        }

        Vector3 topAxlePosition = (wheels[0].transform.localPosition + wheels[1].transform.localPosition) / 2;
        Vector3 botAxlePosition = (wheels[2].transform.localPosition + wheels[3].transform.localPosition) / 2;

        if (Mathf.Abs(Vector3.Dot(topAxleDirection.normalized, botAxleDirection.normalized)) < 0.98)
        {
            Debug.Log("Top axle pos: " + topAxleDirection);
            Debug.Log("Bot axle pos: " + botAxleDirection);
            // axles not in same direction
            /*
            var forward = new Vector3(0, 0, 1); // toward front of car
            Debug.Log("Top axle force: " + topAxleDirection.ToString());

            var topAxleParity = Mathf.Sign(Vector3.Cross(forward, topAxleDirection).y);
            Debug.Log("Top axle parity: " + topAxleParity);
            var bottomAxleParity = Mathf.Sign(Vector3.Cross(forward, bottomAxleDirection).y);
            Debug.Log("Bottom axle parity: " + bottomAxleParity);
            */
            var prod = Vector3.Cross(topAxleDirection, botAxleDirection);
            var wheelParity = Mathf.Sign(prod.y);

            //Debug.Log("Wheel prod: " + prod.ToString());
            //Debug.Log("Wheel parity: " + wheelParity);

            //var totalParity = wheelParity * topAxleParity * botAxleParity;
            //Debug.Log("Total parity: " + totalParity);

            Vector3 topNorm, botNorm;
            if (wheelParity < 0)
            {
                // positive normal
                topNorm = new Vector3(topAxleDirection.z, 0, -topAxleDirection.x);
                botNorm = new Vector3(botAxleDirection.z, 0, -botAxleDirection.x);
            }
            else
            {
                topNorm = new Vector3(-topAxleDirection.z, 0, topAxleDirection.x);
                botNorm = new Vector3(-botAxleDirection.z, 0, botAxleDirection.x);
            }

            // line intersection
            float units;
            Vector3 circleCentre;
            if (topNorm.x > 0.000001) {
                units = (topAxlePosition.z - botAxlePosition.z + (topNorm.z / topNorm.x) * (botAxlePosition.x - topAxlePosition.x)) / (botNorm.z - (topNorm.z / topNorm.x) * botNorm.x);
                circleCentre = botAxlePosition + units * botNorm;
            } else {
                units = (botAxlePosition.z - topAxlePosition.z + (botNorm.z / botNorm.x) * (topAxlePosition.x - botAxlePosition.x)) / (topNorm.z - (botNorm.z / botNorm.x) * topNorm.x);
                circleCentre = topAxlePosition + units * topNorm;
            }
            //Debug.Log("Units: " + units);
            //Debug.Log("Circle centre: " + circleCentre);

            var c = 2 * Mathf.PI * circleCentre.magnitude;
            var mass = body_.mass;
            var v = body_.velocity.magnitude;
            Debug.Log(v);
            v += 10f; // hack
            var w = 180 * v / c / Mathf.PI;

            //Quaternion.Angle(transform.rotation, 

            float frameRotation = -1 * w * Mathf.Sign(totalForce.z) * wheelParity * Time.fixedDeltaTime;
            //Debug.Log("Rotation in frame: " + frame_rotation);

            transform.Rotate(0, frameRotation, 0);
        }


        body_.AddRelativeForce(totalForce);
    }
}
