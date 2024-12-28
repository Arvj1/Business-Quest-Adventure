using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="chapter3_Stage1_Question",menuName ="chapter3/stage1")]
public class Chap3_Stage1_SO : ScriptableObject
{
   [TextArea] public string Question;
   [TextArea] public string explenation;
    public bool answer;
}
