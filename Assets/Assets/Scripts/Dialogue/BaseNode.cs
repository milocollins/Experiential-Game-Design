using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BaseNode : Node {

	public virtual string ReturnString()
    {
        return null;
    }
    public virtual bool GetBool()
    {
        return true;
    }
    public virtual void UnlockInformation()
    {
        return;
    }
}