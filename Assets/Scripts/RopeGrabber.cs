using System.Collections.Generic;
using UnityEngine;

public class RopeGrabber : MonoBehaviour
{
    public List<Rope> contactRopes = new();

    public bool GrabbingRope => contactRopes.Count > 0;
}
