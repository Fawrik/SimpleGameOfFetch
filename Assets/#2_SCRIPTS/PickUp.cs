using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ball", menuName = "ScriptableObjs/Ball")]
public class PickUp : ScriptableObject
{
	public string ID;
	public Sprite sprite;
	public ThoughtTrain thought;

}
