using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTextureRoll : MonoBehaviour
{
    [SerializeField] private Rigidbody RB;
    [SerializeField] private Vector2 speedMult;
    [SerializeField] private Material diceMaterial;
    [SerializeField] private string rollShaderProperty = "g";

    [SerializeField] private CharacterControler CC;
    [SerializeField] private float smoothSpeed = 1;
    [SerializeField] private float rollStep = 1;

    private int rollPropertyID = 0;
    private Vector2 roll;
    private Vector2 lastRoll;
    
    private Vector2 inputSmooth = Vector2.one;
    [SerializeField] private Transform soundParent;

private void Start()
    {
        rollPropertyID = Shader.PropertyToID(rollShaderProperty);
    }

    private void Update() {
        inputSmooth = Vector2.Lerp(inputSmooth, CC.input, Time.deltaTime * smoothSpeed);
        roll += Time.deltaTime * inputSmooth.magnitude * speedMult;
            
        diceMaterial.SetVector(rollPropertyID, roll);

        if(roll.magnitude - lastRoll.magnitude > rollStep)
        {
            lastRoll = roll;
            AudioManager.PlayStereoSound(ESound.DiceRollStep, transform.position,soundParent);
        }
    }

}
