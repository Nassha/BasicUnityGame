using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat : MonoBehaviour
{
    public Animator animator;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
	{
	   Swipe();
	}
        if (Input.GetButtonDown("Fire2"))
	{
	   Swipe1();
	}
        if (Input.GetButtonDown("Fire3"))
	{
	   Swipe2();
	}
    }

    void Swipe()
    {
	animator.SetTrigger("swipe");
    }
    
    void Swipe1()
    {
	animator.SetTrigger("swipe1");
    }
    void Swipe2()
    {
	animator.SetTrigger("swipe2");
    }
    
}
