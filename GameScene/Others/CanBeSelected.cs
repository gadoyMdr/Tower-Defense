using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OutlineSpecs
{
    public float outlineWidth;
    public Color outlineColor;

    public static OutlineSpecs None { get => new OutlineSpecs(0, new Color(0, 0, 0, 0)); }
    public static OutlineSpecs GreenThick { get => new OutlineSpecs(10f, Color.green); }
    public static OutlineSpecs RedThick { get => new OutlineSpecs(10f, Color.red); }
    public static OutlineSpecs OrangeThin { get => new OutlineSpecs(5f, new Color(1, 0.5f, 0f, 1f)); }
    public static OutlineSpecs OrangeTick { get => new OutlineSpecs(10f, new Color(1, 0.5f, 0f, 1f)); }

    public OutlineSpecs(float outlineWidth, Color outlineColor)
    {
        this.outlineWidth = outlineWidth;
        this.outlineColor = outlineColor;
    }
}

[RequireComponent(typeof(Outline))]
public class CanBeSelected : MonoBehaviour
{
    public Action<bool> SelectedAction;

    [HideInInspector]
    public Outline outline;

    private OutlineSpecs OutlineSpecs;
    public OutlineSpecs outlineSpecs
    {
        get => OutlineSpecs;
        set
        {
            OutlineSpecs = value;
            outline.OutlineWidth = value.outlineWidth;
            outline.OutlineColor = value.outlineColor;
        }
    }

    private bool IsSelected;
    public bool isSelected
    {
        get => IsSelected;
        set
        {
            IsSelected = value;

            if(value)
                outlineSpecs = OutlineSpecs.OrangeTick;
            else
                outlineSpecs = OutlineSpecs.None;

            SelectedAction?.Invoke(value);
        }
    }


    private bool IsHovered;
    public bool isHovered
    {
        get => IsHovered;
        set
        {
            IsHovered = value;
            if(value)
                outlineSpecs = OutlineSpecs.OrangeThin;
            else
                outlineSpecs = OutlineSpecs.None;
        }
    }

    public void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        outlineSpecs = OutlineSpecs.None;
    }

    
}
