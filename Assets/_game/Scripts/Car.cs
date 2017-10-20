using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public Wheel[] wheels;
	public Collider body_;

	float direction; // direction of front of car from 0-360
    float wheelBase;

	Wheel[,] axles_;


	// Use this for initialization
	void Start () {
		axles_ = new Wheel[2, 2];
        wheelBase = wheels[1].transform.localPosition.x - wheels[0].transform.localPosition.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 topAxleDirection = (wheels[0].direction + wheels[1].direction) / 2;
		Vector3 botAxleDirection = (wheels[2].direction + wheels[3].direction) / 2;

        Vector3 topAxlePosition = (wheels[0].transform.localPosition + wheels[1].transform.localPosition) / 2;
        Vector3 botAxlePosition = (wheels[2].transform.localPosition + wheels[3].transform.localPosition) / 2;


        Debug.Log(topAxlePosition);


        if ((topAxleDirection.normalized - botAxleDirection.normalized).magnitude > 0.00001)
        {
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

            Debug.Log("Wheel prod: " + prod.ToString());
            Debug.Log("Wheel parity: " + wheelParity);

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
            var units = (topAxlePosition.z - botAxlePosition.z + (topNorm.z / topNorm.x) * (botAxlePosition.x - topAxlePosition.x)) / (botNorm.z - (topNorm.z / topNorm.x) * botNorm.x);
            var circleCentre = botAxlePosition + units * botNorm;

            Debug.Log(circleCentre);

            var c = 2 * Mathf.PI * circleCentre.magnitude;
            var mass = body_.attachedRigidbody.mass;
            var v = body_.attachedRigidbody.velocity.magnitude;
            var w = 180 * v / c / Mathf.PI;

            float frame_rotation = -1*w * wheelParity * Time.fixedDeltaTime;
 
            transform.Rotate(0, frame_rotation, 0);
        }
        var totalForce = Vector3.zero;
        foreach (var w in wheels)
        {
            totalForce += w.totalForce;
        }
        Debug.Log("Final Force: " + totalForce.ToString());


        body_.attachedRigidbody.AddRelativeForce(totalForce);
	}
}
