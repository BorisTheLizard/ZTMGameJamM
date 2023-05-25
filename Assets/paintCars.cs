using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintCars : MonoBehaviour
{
	[SerializeField] Material[] carPaint;
	[SerializeField] GameObject[] carPuck;

	private void Start()
	{
		int childCount = transform.childCount;
		carPuck = new GameObject[childCount];
		for (int i = 0; i < childCount; i++)
		{
			carPuck[i] = transform.GetChild(i).gameObject;
			//carPuck[i].GetComponentInChildren<MeshRenderer>().materials[1] = carPaint[Random.Range(0,carPaint.Length)];

			Material[] currentMaterials = carPuck[i].GetComponentInChildren<MeshRenderer>().materials;
			currentMaterials[0] = carPaint[Random.Range(0, carPaint.Length)];
			carPuck[i].transform.GetComponentInChildren<MeshRenderer>().materials = currentMaterials;

			//meshRenderer.materials = currentMaterials;
		}
	}
}
