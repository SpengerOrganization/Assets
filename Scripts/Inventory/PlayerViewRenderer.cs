using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewRenderer : MonoBehaviour
{
    
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.red;

    //This stores the GameObject’s original color
    Color m_OriginalColor;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    MeshRenderer m_Renderer;

    public bool IsHovering;

    void Start()
    {
        IsHovering = false;

        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<MeshRenderer>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Renderer.material.color;
    }

    // mouse over event
    void OnMouseOver()
    {
        IsHovering = true;

        // Change the color of the GameObject to red when the mouse is over GameObject
        m_Renderer.material.color = m_MouseOverColor;
    }

    // mouse exit event
    void OnMouseExit()
    {
        IsHovering = false;

        // Reset the color of the GameObject back to normal
        m_Renderer.material.color = m_OriginalColor;
    }

    void FixedUpdate(){
        if(IsHovering){

        }else{

        }
    }
}
