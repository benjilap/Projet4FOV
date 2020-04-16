using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfViewSecurity))]
public class FieldOfViewSecurityEditor : Editor
{

    void OnSceneGUI()
    {
        FieldOfViewSecurity fow = (FieldOfViewSecurity)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.distVue);
        Vector3 angleVueA = fow.DirFromAngle(-fow.angleVue / 2, false);
        Vector3 angleVueB = fow.DirFromAngle(fow.angleVue / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + angleVueA * fow.distVue);
        Handles.DrawLine(fow.transform.position, fow.transform.position + angleVueB * fow.distVue);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
}