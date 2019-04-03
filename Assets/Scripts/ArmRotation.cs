using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 90;

	// Update is called once per frame
	void Update () {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;  // Pos of player - mouse pos
        difference.Normalize();  // Normalizing the vector. Meaning sum of vector will = 1

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;  // Find angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
	}
}
