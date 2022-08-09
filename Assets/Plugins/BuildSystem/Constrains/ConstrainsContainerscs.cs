using System;
using UnityEngine;

[Serializable]
public class ConstrainsAndOperatorEdge
{
    public Gates combineWithPreviousBy;
    public EdgeConstrainsEnum constrain;
}

[Serializable]
public class ConstrainsAndOperatorCenter
{
    public Gates combineWithPreviousBy;
    public CenterConstrainsEnum constrain;
}