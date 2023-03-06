using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class rotatePage : MonoBehaviour
{
    [SerializeField] float speed = 5;  //
    [SerializeField] List<Transform> pages;//
    [SerializeField] int index = -1;//
    [SerializeField] GameObject buttonBack;
    [SerializeField] GameObject buttonForward;
    [SerializeField] bool rotate = false;


    public void RotateNext()
    {
        if (rotate) { return; }
        index++;
        pages[index].SetAsLastSibling();
        ButtonForwardActions();
        float angle = 180; //in order to rotate the page forward, you need to set the rotation by 180 degrees around the y axis
        StartCoroutine(Rotate(angle, true));

    }

    private void ButtonForwardActions() 
    {
        if (buttonBack.activeInHierarchy == false) //every time we turn the page forward, the back button should be activated
        {
            buttonBack.SetActive(true);
        }
        if (index == pages.Count - 1) //if the page is last then we turn off the forward button
        {
            buttonForward.SetActive(false);
        }
    }

    private void ButtonBackActions()
    {
        if (buttonForward.activeInHierarchy == false) //every time we turn the page back, the forward button should be activated
        {
            buttonForward.SetActive(true);
        }
        if (index - 1 == -1) //if the page is first then we turn off the back button
        {
            buttonBack.SetActive(false);
        }
    }

    public void RotateBack()
    {
        if (rotate) { return; }
        float angle = 0; //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
        pages[index].SetAsLastSibling();
        ButtonBackActions();
        StartCoroutine(Rotate(angle, false));

    }


    IEnumerator Rotate(float angle, bool next)
    {
        float time=0;
        while (true)
        {
            rotate = true;
            Quaternion targetRotatuion = Quaternion.Euler(0, angle, 0); 
            time += Time.deltaTime * speed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotatuion, time); //smoothly turn the page
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotatuion); //calculate the angle between the given angle of rotation and the current angle of rotation
            if (angle1<0.1f) //if the angle is about 0 stop the rotation
            {
                if (next == false)
                {
                    index--;
                }
                rotate = false;
                break;
            }
            yield return null;
        }

    }

 
}
