using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Thought Train", menuName = "ScriptableObjs/Thought")]
public class ThoughtTrain : ScriptableObject
{
	[Header("Properties")]

	public string ID;
	public int index;

	public bool isRecycleable;
	[ShowIf("isRecycleable", true)]
	public bool isThgoughtThrough = false;

	[Header("Text Content")]
	public List<Thought> thoughts = new List<Thought>();
	
}




[System.Serializable]
public class ThoughtLine
{
	public int order;

	[TextArea(4, 8)]
	public string thoughtLineText;
}

[System.Serializable]
public class Thought
{
	public List<ThoughtLine> thoughtLines = new List<ThoughtLine>();
}
