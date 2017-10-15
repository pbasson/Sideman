using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationVariable : MonoBehaviour {

    public struct Names
    {
        public static string start = "Start: ";
        public static string end = "End: ";
        public static string wall = "Wall: ";
        public static string ground = "Ground: ";
        public static string backGround = "Background: ";
        public static string ceiling = "Ceiling: ";
        public static string spawn = "Spawn: ";
        public static float startVal = -5f;
        public static float endVal = 5f;
        public static float bgVal = 6f;
    }
    
    public struct BoolSet
    {
        public static bool collect = false;
        public static bool platform = true;
    }


}
