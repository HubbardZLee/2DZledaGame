using UnityEngine;
using System.Collections;

/*
 * Interface to be inherited by any class needing to receive messages from the GameManager.
 * Only one function but MUST be implemented by the derived class. Make it public to work properly.
 * It will receive a message made as one of the user created enum messages.
 * Easiest way to use is create a single switch statement that branches on notification.
 * Only have to handle messages that pertain to the inheriting class.
 */
public interface IObserver 
{
	// Receives a parameter of type enum Notification. Do with as you please
	void OnNotify (Notification notification);
}
