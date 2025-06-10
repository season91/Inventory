using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTagAttribute : Attribute
{
    // [SerializeField] 같은것도 다 어트리뷰트임
    
    public int Order {get; }

    public MyTagAttribute(int order)
    {
        Order = order;
    }
}
