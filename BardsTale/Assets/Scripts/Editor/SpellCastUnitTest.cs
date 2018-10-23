using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

///<summary> This Class allows us to cast different spells from the Editor without
///having to input button combinations.</summary>
[CustomEditor(typeof(SpellCasting))]
public class SpellCastUnitTest : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(GUILayout.Button("Cast Light Spell")) {
            SpellCasting.instance.CastLight();
        }

        if (GUILayout.Button("Cast Firebolt Spell")) {
            SpellCasting.instance.CastFirebolt();
        }

        if (GUILayout.Button("Cast Heal Spell")) {
            SpellCasting.instance.CastHeal();
        }
    }
}
