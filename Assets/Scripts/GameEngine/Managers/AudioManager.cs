using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * This component class should be placed on any object that is guaranteed to exist the duration of the game.
 * Preferably an empty object designated to hold all audio assests.
 */
public class AudioManager : MonoBehaviour, IObserver
{
	// Source that plays the main music of the game : Read-Only
	private AudioSource mainAudioSource;
	public AudioSource MainAudioSource { get { return mainAudioSource; } }

	// Source that plays any kind of background noises or music that are non-essential : Read-Only
	private AudioSource mapAreaAudioSource;
	public AudioSource MapAreaAudioSource { get { return mapAreaAudioSource; } }

	// Source that plays enemy related noises : Read-Only
	private AudioSource playerAudioSource;
	public AudioSource PlayerAudioSource { get { return playerAudioSource; } }

	// Source that plays enemy related noises : Read-Only
	private AudioSource enemyAudioSource;
	public AudioSource EnemyAudioSource { get { return enemyAudioSource; } }

	// Source that plays enemy related noises : Read-Only
	private AudioSource npcAudioSource;
	public AudioSource NPCAudioSource { get { return npcAudioSource; } }

	// Source that plays the noise from item pickups : Read-Only
	private AudioSource itemPickupSource;
	public AudioSource ItemPickupSource { get { return itemPickupSource; } }

	// Source that plays usable item related noises : Read-Only
	private AudioSource itemUsedSource;
	public AudioSource ItemUsedSource { get { return itemUsedSource; } }


	// The main background Music. SET FOR DELETION IN FINAL WEEK OF GAME DEVELOPMENT
	[ReadOnly][SerializeField]
	AudioClip themeMusic;

	// A list to hold all the game's audio sources. Makes pausing and stopping much easier.
	List<AudioSource> AudioList;

	// Used for initialization code
	void Start ()
	{
		// Create the list of audio sources
		AudioList = new List<AudioSource> ();

		// Create and add all of the audio source components to the audioManager gameObject
		mainAudioSource = gameObject.AddComponent<AudioSource> ();
		mapAreaAudioSource = gameObject.AddComponent<AudioSource> ();
		playerAudioSource = gameObject.AddComponent<AudioSource> ();
		enemyAudioSource = gameObject.AddComponent<AudioSource> ();
		npcAudioSource = gameObject.AddComponent<AudioSource> ();
		itemPickupSource = gameObject.AddComponent<AudioSource> ();
		itemUsedSource = gameObject.AddComponent<AudioSource> ();

		AudioList.Add (mainAudioSource);
		AudioList.Add (mapAreaAudioSource);
		AudioList.Add (playerAudioSource);
		AudioList.Add (enemyAudioSource);
		AudioList.Add (npcAudioSource);
		AudioList.Add (itemPickupSource);
		AudioList.Add (itemUsedSource);


		themeMusic = Resources.Load ("Audio/Theme") as AudioClip;
		mainAudioSource.clip = themeMusic;
		mainAudioSource.Play ();
		mainAudioSource.loop = true;
	}

	// Handles the code for receiving all notifications from the game manager
	public void OnNotify (Notification notification)
	{
		// Just random nonsense as ideas. Delete when ready to code.
		switch (notification)
		{
		case Notification.GAME_PAUSED:
		case Notification.INVENTORY_OPENED:
		case Notification.MAP_OPENED:
			// Stop the beat!
			foreach (AudioSource audio in AudioList)
				audio.Pause ();
			break;
		case Notification.GAME_UNPAUSED:
		case Notification.INVENTORY_CLOSED:
		case Notification.MAP_CLOSED:
			// Bring that shit back.
			foreach (AudioSource audio in AudioList)
				audio.UnPause ();
			break;
		case Notification.PLAYER_HAS_DIED:
			mainAudioSource.clip = Resources.Load ("Audio/Scream") as AudioClip;
			mainAudioSource.loop = false;
			mainAudioSource.Play ();
			break;
		case Notification.ITEM_PICKED_UP:
		case Notification.UNABLE_TO_PICK_UP_ITEM:
			ItemPickupSource.Play ();
			break;
		case Notification.USE_ITEM_1:
		case Notification.USE_ITEM_2:
		case Notification.USE_ITEM_3:
		case Notification.USE_ITEM_4:
			ItemUsedSource.clip = Resources.Load ("Audio/Pew-Pew") as AudioClip;
			ItemUsedSource.Play ();
			break;
		}
	}  // End OnNotify
}