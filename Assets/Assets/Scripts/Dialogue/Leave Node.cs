using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class LeaveNode : BaseNode {

    [Output] public int exit;
    public override string ReturnString()
    {
        return "Leave";
    }
}