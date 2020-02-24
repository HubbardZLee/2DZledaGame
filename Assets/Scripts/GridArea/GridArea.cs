using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tiled2Unity;

/*
 * This is the base class for every Grid area in the game.
 * A grid area could consist of a town, regular game area with enemies,
 * dungeon rooms with keys and puzzles, shops, special item areas, boss battles,
 * and anything else that one could think of.
 */
public class GridArea : MonoBehaviour
{
	// Reference to the Grid Map, only used if each screen is it's own gameObject
	[ReadOnly][SerializeField]
	private TiledMap map;
	public TiledMap Map { get { return map; } }

	// Index to determine place among entire map. 
	[ReadOnly][SerializeField]
	protected int mapIndex;
	public int MapIndex { get { return mapIndex; } }

	// Has this grid space been visited yet. 
	[SerializeField]
	protected bool hasBeenVisited;
	public bool HasBeenVisited { get { return hasBeenVisited; } }

	// Boolean to determine if this particular gridArea is a save state location and will save the entire games state if so
	[SerializeField]
	protected bool isSaveScreen;
	public bool IsSaveScreen { get { return isSaveScreen; } }

	// Boolean to determine if this gridArea will serve as a respawn point for when the player dies
	[SerializeField]
	protected bool isRespawnScreen;
	public bool IsRespawnScreen { get { return isRespawnScreen; } }

	// Run custom Initialization code
	protected virtual void Start () {
		/*
		map = GetComponentInParent<TiledMap> ();

		// Add a trigger Box Collider 2D to this map and set it's width and height to the same as the map.
		if (GetComponent<BoxCollider2D>() == null)
			gameObject.AddComponent<BoxCollider2D> ();

		BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D> ();
		boxCollider2D.size = new Vector2 ((map.TileWidth - 1.25F) * map.NumTilesWide, (map.TileHeight - 1.25F) * map.NumTilesHigh) * map.ExportScale;
		boxCollider2D.transform.localPosition =  new Vector3((map.TileWidth * map.NumTilesWide) / 2.0F, -(map.TileHeight * map.NumTilesHigh) / 2.0F, 0.0F) * map.ExportScale;
		boxCollider2D.isTrigger = true;
		*/

		hasBeenVisited = false;
		isSaveScreen = false;
		isRespawnScreen = false;
	}

	// What to do when something enters this space. Can be overridden
	protected virtual void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player")
			EnterGridArea ();
	}

	// What to do when something exits this space. Can be overridden
	protected virtual void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "Player")
			ExitGridArea ();
	}
		
	// Called when the player has entered this grid area. 
	protected virtual void EnterGridArea () {
		if (!hasBeenVisited)
			hasBeenVisited = true;
	}

	protected virtual void ExitGridArea () { }
}