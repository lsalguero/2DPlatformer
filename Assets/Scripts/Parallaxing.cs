using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;  // Array of all the back and fore grounds to be paralaxed
    private float[] parallaxScales;  // Proportions of camera's movement to move the backgrounds by
    public float smoothing;  // How smooth the paralax will be. Must be more than 0

    private Transform cam;  // Reference to the main camera transform
    private Vector3 previousCamPos;  // Stores position of cam in previous frame

    // Awake is called before Start() and after the game objects are rendered
    void Awake() {
        // Set up camera reference
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        // The previous frame had the current frame's cam position
        previousCamPos = cam.position;

        // Assinging corresponding parallax scales
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        // For each background
        for (int i = 0; i < backgrounds.Length; i++) {
            // Parallax is opposite of cam movement because the prev frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // Set a target x pos which is current pos plus parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Create target pos which is background's current pos with it's target x pos
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Fade between current pos and target pos using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Set previousCamPos to cam's pos at end of frame
        previousCamPos = cam.position;
    }
}
