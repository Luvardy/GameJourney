using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teachMenu : StateMachineBehaviour
{
    public GameObject TeachMainMenu;

    public void TeachMenu()
    {
        if (TeachMainMenu != null)
        {
            Animator animator = TeachMainMenu.GetComponent<Animator>();
            if (animator != null)
            {
                bool MenuisOpen = animator.GetBool("show");
                animator.SetBool("show", MenuisOpen);
            }
        }

    }
}
