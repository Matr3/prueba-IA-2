using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
   public static Vector2 GetHalfDimensionsInWorldUnits()
   {
	   float width, heigth;
	   
	   Camera cam = Camera.main;
	   float ratio = cam.pixelWidth / (float)cam.pixelHeight;
	   heigth = cam.orthographicSize * 2;
	   width = heigth * ratio;
	   return new Vector2(width, heigth) /2f;
   }
}
