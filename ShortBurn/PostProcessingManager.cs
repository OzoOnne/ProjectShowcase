using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public Volume volume;

    private LiftGammaGain _liftGammaGain;
    
    void Awake()
    {
        volume.profile.TryGet<LiftGammaGain>(out _liftGammaGain);
    }
    
    void Start()
    {
        ChangeToWarm();
    }

    public void ChangeToCold()
    {
        _liftGammaGain.lift.Override(new Vector3(0.75f,0.85f,1));
    }
    
    public void ChangeToWarm()
    {
        _liftGammaGain.lift.Override(new Vector3(1.25f,1.15f,1));
    }

    public void ChangeToNormal()
    {
        _liftGammaGain.lift.Override(new Vector3(1f,1f,1));
    }
}
