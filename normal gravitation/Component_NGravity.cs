// Copyright (c) 2015, Felix Kate All rights reserved.

/*<Summary>
Script Component for orientating the gravity of a Rigidbody to the normal of the collision below it.
Uses the up axis for the custom gravity which means it has to be modified for objects that don't have their gravity on the y axis.
Can be used for example to move around a sphere (planet gravity like in Mario Galaxy).
*/

using UnityEngine;
using System.Collections;

public class Component_NGravity : MonoBehaviour {

	public float GravityScale = 1;

	//Update
	void Update () {
		//Return if there is no rigidbody attatched (since it's needed here
		if(GetComponent<Rigidbody>() == null)return;

		//Store Information about the Raycast hit
		RaycastHit hitInfo = new RaycastHit();

		//Apply gravity
		GetComponent<Rigidbody>().AddForce(transform.up * -GravityScale);

		//Do the Raycast
		if(!Physics.Raycast (transform.position, -transform.up , out hitInfo, 10))return;

		//If difference between up Vector and Normal is greater than 0.1% rotate object to normal direction
		if((hitInfo.normal - transform.up).sqrMagnitude > 0.001f && (hitInfo.normal - transform.up).sqrMagnitude < 0.75f){
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Vector3.Cross (hitInfo.normal, -transform.right), hitInfo.normal), Time.deltaTime * 10);
		}

	}

}
