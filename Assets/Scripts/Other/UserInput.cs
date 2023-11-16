using System.Collections;
using UnityEngine;

public static class UserInput
{
	public static bool GrappleKeyDown => Input.GetMouseButtonDown(1);
	public static bool GrappleKeyUp => Input.GetMouseButtonUp(1);
	public static bool GrapplePullPressed => Input.GetMouseButton(1);
}
