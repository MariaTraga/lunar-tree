using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue System/Dialogue Container")]
public class DialogueContainerObject : ScriptableObject
{
    public List<string> lines;
    public ActorObject actor;
}
