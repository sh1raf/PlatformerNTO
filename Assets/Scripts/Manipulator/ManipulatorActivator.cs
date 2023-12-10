using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ManipulatorActivator : ActionObject
{
    [SerializeField] private Manipulator manipulator;
    [SerializeField] private Transform cam;

    [Inject] private PlayerPCInput _input;

    private CameraController _camera;

    private bool _canAction = true;

    private void OnEnable()
    {
        _camera = Camera.main.GetComponent<CameraController>();

        manipulator.ActionsEnd += ActionsEnd;
        manipulator.OnStartPoint += CanAction;
    }

    private void OnDisable()
    {
        manipulator.ActionsEnd -= ActionsEnd;
    }

    private void CanAction()
    {
        _canAction = true;
    }

    private void ActionsEnd()
    {
        _input.EnableInput();
        _camera.GoPlayer();
    }

    public override void Action()
    {
        if(_canAction)
        {
            _camera.GoStatic(cam.position);
            _input.DisableInput();

            _canAction = false;
        }
    }
}
