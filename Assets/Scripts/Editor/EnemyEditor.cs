using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    private void OnSceneGUI()
    {
        Enemy enemy = (Enemy)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemy.transform.position, Vector3.forward, enemy.transform.right, 360, enemy.attackDistance);

        Vector3 viewAngle01 = DirectionFromAngle(enemy.transform.eulerAngles.z, -enemy.attackAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(enemy.transform.eulerAngles.z, enemy.attackAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngle01 * enemy.attackDistance);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngle02 * enemy.attackDistance);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
