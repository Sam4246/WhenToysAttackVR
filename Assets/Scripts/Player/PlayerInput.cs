using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float speed = 5f;
    public PlayerHealth playerHealth;

    [SerializeField]
    private Transform rig;

    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private Vector2 axis = Vector2.zero;

    private PlayerShooting gunBarrelEnd;
    private bool shootGun;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        gunBarrelEnd = GetComponentInChildren<PlayerShooting>();
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        if (controller.GetPress(2))
        {
            playerHealth.RestartLevel();
        }

        if (playerHealth.isPlayerDead())
            return;

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (controller.GetTouch(touchpad))
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

            if (rig != null)
            {
                rig.position += (transform.right * axis.x + transform.forward * axis.y) * speed * Time.deltaTime;
                rig.position = new Vector3(rig.position.x, 0, rig.position.z);
            }
        }

        if (controller.GetHairTriggerDown())
            shootGun = true;

        if (controller.GetHairTriggerUp())
            shootGun = false;

        if (shootGun)
        {
            Debug.Log("Pew Pew Pew");
            gunBarrelEnd.Shoot();
        }
    }
}
