using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ExitNode : BaseNode {

    [Input] public int entry;
    public override string ReturnString()
    {
        return "Exit";
    }
}