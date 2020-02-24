using UnityEngine;
using System.Collections;

public abstract class ContainerItem : Item
{
	[SerializeField]
	protected int count;
	public int Count { get { return count; } }

	[SerializeField]
	protected int capacity;
	public int Capacity { get { return capacity; } }

	public void InitializeContainer (int capacity, int count) {
		this.capacity = capacity;
		this.count = count;
	}

	public void IncreaseSize (int number = 1) {
		capacity += number;
	}

	public void Add (int number = 1) {
		count += number;

		if (IsFull ())
			MakeFull ();
	}

	public void Remove (int number = 1) {
		count -= number;

		if (IsEmpty ())
			MakeEmpty ();
	}
		
	public void MakeFull () {
		count = Capacity;
	}

	public void MakeEmpty () {
		count = 0;
	}

	public bool IsFull () {
		return Count >= Capacity;
	}

	public bool IsEmpty () {
		return Count <= 0;
	}
}