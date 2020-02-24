using UnityEngine;
using System.Collections;

/* 
 * Interface to be inherited by all objects wishing to be able to send messages to others.
 * Object must create its own list of IObserver to send notifications to.
 * Objects can both send and receive messages. Allows delegation of tasks.
 * Object can receive a message then turn around and notify all of its followers of said message.
 * Ex: A Manager that gives a message to a few Assistant Managers who turn around and deliver that
 * 	   message to all the workers in their particular department.
 * All three functions MUST be coded by a derived class. All must be declared as public to work properly.
 */
public interface IMessanger 
{
	// Simply adds an IObserver object to the list of objects to send messages to.
	void addObserver (IObserver observer);

	// Removes an IObserver object from the list of objects to send messages to.
	void removeObserver (IObserver observer);

	// Iterates through a list of IObserver objects sending a particular message to each.
	void notify (Notification notification);
}

/* Enum that holds all possible messages to send/receive
 * Can simply add new items whenever needed so feel free to add
 * Try to keep it organized by type of message if possible
 */
public enum Notification
{
	// Unique event
	PLAYER_HAS_DIED,

	// Deals with picking up items
	ITEM_PICKED_UP, UNABLE_TO_PICK_UP_ITEM,

	INVENTORY_UPDATE, INVENTORY_SWAP,

	// Player property related messages
	HEALTH_CHANGED,
	GAINED_CONTAINER,
	BOMBS_CHANGED,
	ARROWS_CHANGED,
	MATCHES_CHANGED,
	KEYS_CHANGED,
	MONEY_CHANGED,

	// Game state related messages
	GAME_PAUSED, GAME_UNPAUSED,
	MAP_OPENED, MAP_CLOSED,
	INVENTORY_OPENED, INVENTORY_CLOSED,

	// Camera state related messages
	ENTERED_NEW_AREA,

	// Item state related messages
	USE_ITEM_1, USE_ITEM_2, USE_ITEM_3, USE_ITEM_4
}