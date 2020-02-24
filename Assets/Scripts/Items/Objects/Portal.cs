using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
	// The other portal object
	[SerializeField]
	GameObject receivingPortal;

	// Determines if the player is currently inside the portal.
	bool inPortal;

	// When the player enters the portal, send them to the other one and deactivate them
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player" && !inPortal) {
			receivingPortal.GetComponent<Portal>().inPortal = true;
			other.transform.position = receivingPortal.transform.position;
		}
	}

	// When the player exits the portal, reactivate them
	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player" && inPortal)
			inPortal = false;
	}
}