using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private GameObject heldObject;
    public float radius = 5f;

    public float distance = 2f;

    public float height = 1f;

    public string uniqueIdentifier;

    private void Update()
    {
        var t = transform;
        var pressedF = Input.GetKeyDown(KeyCode.F);
        if (heldObject)
        {
            var rigidBody = heldObject.GetComponent<Rigidbody>();
            var moveTo = t.position + distance * t.forward + height * t.up;
            var difference = moveTo - heldObject.transform.position;
            rigidBody.AddForce(difference * 2000);
            rigidBody.velocity = difference * 10;
            heldObject.transform.rotation = t.rotation;

            if (pressedF)
            {
                ;
                rigidBody.constraints = RigidbodyConstraints.None;
                rigidBody.drag = 1f;
                rigidBody.useGravity = true;
                heldObject = null;
            }


        }
        else
        {
            if (pressedF)
            {
                var hits = Physics.SphereCastAll(t.position + t.forward, radius, t.forward, radius);
                var hitIndex = Array.FindIndex(hits, hit => hit.transform.tag == "Pickuppable");

                if (hitIndex != -1)
                {
                    var hitObject = hits[hitIndex].transform.gameObject;
                    heldObject = hitObject;
                    var rigidBody = heldObject.GetComponent<Rigidbody>();
                    rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                    rigidBody.drag = 25f;
                    rigidBody.useGravity = false;
                }
            }
        }

    }


}
