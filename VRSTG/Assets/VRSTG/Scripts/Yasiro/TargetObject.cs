using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private static List<TargetObject> ms_targets = new List<TargetObject>();
    public static List<TargetObject> targets { get { return ms_targets; } }

    public static TargetObject GetNearTarget(Vector3 pos)
    {
        if (ms_targets.Count == 0) return null;
       
        TargetObject obj = null;
        float nearDist = -1f;
        foreach(TargetObject target in ms_targets)
        {
            if(obj == null)
            {
                obj = target;
                nearDist = Vector3.Distance(pos, target.transform.position);
            }
            else
            {
                float dist = Vector3.Distance(pos, target.transform.position);
                if (dist < nearDist)
                {
                    obj = target;
                    nearDist = dist;
                }
            }
        }

        return obj;
    }

    private void Start()
    {
        ms_targets.Add(this);
    }

    private void OnDestroy()
    {
        ms_targets.Remove(this);
    }
}
