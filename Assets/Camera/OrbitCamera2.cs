﻿using UnityEngine;
using System.Collections;


public class OrbitCamera2 : MonoBehaviour
{


  public Transform _target;

  public float _distance = 20.0f;

  public float _zoomStep = 1.0f;


  public float _xSpeed = 1f;
  public float _ySpeed = 1f;


  private float _x = 1000.0f;
  private float _y = 0.0f;
  private Vector3 velocity = Vector3.zero;
  private Vector3 newPos = Vector3.zero;




  private Vector3 _distanceVector;

  void Start ()
  {
    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);

    Vector2 angles = this.transform.localEulerAngles;
    _x = angles.x;
    _y = angles.y;

    this.Rotate(_x, _y);

  }

  /**
   * Rotate the camera or zoom depending on the input of the player.
   */
  void LateUpdate()
  {
    if ( _target )
    {
      this.RotateControls();
      this.Zoom();
    }

    transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, 0.2f);
    transform.LookAt(_target);

  }

  /**
   * Rotate the camera when the first button of the mouse is pressed.
   *
   */
  void RotateControls()
  {
    if ( Input.GetButton("Fire1") )
    {
      _x += Input.GetAxis("Mouse X") * _xSpeed;
      _y += -Input.GetAxis("Mouse Y") * _ySpeed;




      this.Rotate(_x, _y);
    }

    if (Input.GetKey("d"))
    {
      _x -= Time.deltaTime * _xSpeed * 16f;
      this.Rotate(_x, _y);
    }

    if (Input.GetKey("a"))
    {
      _x += Time.deltaTime * _xSpeed * 16f;
      this.Rotate(_x, _y);
    }

    if (Input.GetKey("s"))
    {
      _y -= Time.deltaTime * _ySpeed * 16f;
      this.Rotate(_x, _y);
    }

    if (Input.GetKey("w"))
    {
      _y += Time.deltaTime * _ySpeed * 16f;
      this.Rotate(_x, _y);
    }


  }

  /**
   * Transform the cursor mouvement in rotation and in a new position
   * for the camera.
   */
  void Rotate( float x, float y )
  {
    //Transform angle in degree in quaternion form used by Unity for rotation.
    Quaternion rotation = Quaternion.Euler(y, x, 0.0f);


    newPos = rotation * _distanceVector + _target.position;
    
    
    //transform.rotation = rotation;
    //transform.position = newPos;
  }

  /**
   * Zoom or dezoom depending on the input of the mouse wheel.
   */
  void Zoom()
  {
    if ( Input.GetAxis("Mouse ScrollWheel") < 0.0f )
    {
      this.ZoomOut();
    }
    else if ( Input.GetAxis("Mouse ScrollWheel") > 0.0f )
    {
      this.ZoomIn();
    }

    if (Input.GetKey("e"))
    {
      this.ZoomInKey();
    }

    if (Input.GetKey("q"))
    {
      this.ZoomOutKey();
    }

  }

  /**
   * Reduce the distance from the camera to the target and
   * update the position of the camera (with the Rotate function).
   */
  void ZoomIn()
  {
    _distance -= _zoomStep;
    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
    this.Rotate(_x, _y);
  }

  void ZoomInKey()
  {
    _distance -= _zoomStep * Time.deltaTime * 3f;
    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
    this.Rotate(_x, _y);
  }

  /**
   * Increase the distance from the camera to the target and
   * update the position of the camera (with the Rotate function).
   */
  void ZoomOut()
  {
    _distance += _zoomStep;
    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
    this.Rotate(_x, _y);
  }

  void ZoomOutKey()
  {
    _distance += _zoomStep * Time.deltaTime * 3f;
    _distanceVector = new Vector3(0.0f, 0.0f, -_distance);
    this.Rotate(_x, _y);
  }

} //End class