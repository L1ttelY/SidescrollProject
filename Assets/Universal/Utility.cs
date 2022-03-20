using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {
	public static RaycastHit2D[] raycastBuffer = new RaycastHit2D[100];
	public static Collider2D[] colliderBuffer = new Collider2D[100];

	public static ContactFilter2D GetFilterByLayerName(string name){
		ContactFilter2D filter=new ContactFilter2D();
		filter.useLayerMask=true;
		string[] layerMask = new string[1];
		layerMask[0]=name;
		filter.layerMask=LayerMask.GetMask(layerMask);
		return filter;
	}

	public static float Cross(Vector2 a,Vector2 b) {
		return a.x*b.y-a.y*b.x;
	}

	public static Vector2 Product(Vector2 a,Vector2 b) {
		return new Vector2(a.x*b.x-a.y*b.y,a.x*b.y+a.y*b.x);
	}

	public static Vector2 EliminateOnDirection(Vector2 original, Vector2 direction){
		if(Vector2.Dot(original,direction)<=0) return original;
		direction.Normalize();
		original-=direction*Vector2.Dot(original,direction);
		return original;
	}
}
