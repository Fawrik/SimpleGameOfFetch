using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
	public static TutorialScript Instance;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}


	public bool isActive = false;
	public bool isPressed = false;
	public Animator anim;
	

    
    void Update()
    {
		if (isPressed == false && isActive)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				anim.SetBool("visible", false);
				isPressed = false;
			}
		}
    }

	public void SetTutorialActive()
	{
		isActive = true;
		anim.SetBool("visible", true);
	}
}
