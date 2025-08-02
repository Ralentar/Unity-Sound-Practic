using System.Linq;
using UnityEngine;

public class FootStepsSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SurfaceStepsSound[] _stepsSound;
    [SerializeField] private Transform _checkPoint;

    private SurfaceType _currentSurface;
    private bool _isPlayStepsSound = false;

    private void Awake()
    {
        _audioSource.clip = _stepsSound[0].Clip;
        _currentSurface = SurfaceType.Grass;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(_checkPoint.position, Vector3.down, out RaycastHit hitInfo) == false)
            return;

        if (hitInfo.transform.TryGetComponent(out Surface surface) == false)
            return;

        if (surface.Type == _currentSurface)
            return;

        SetSurfaceSteps(surface.Type);
    }

    public void Play()
    {
        if (_isPlayStepsSound)
            return;

        _isPlayStepsSound = true;
        _audioSource.Play();
    }

    public void Pause()
    {
        _audioSource.Pause();
        _isPlayStepsSound = false;
    }

    private void SetSurfaceSteps(SurfaceType type)
    {
        _currentSurface = type;
        _audioSource.clip = _stepsSound.First(sound => sound.Type == _currentSurface).Clip;
        _isPlayStepsSound = false;
    }
}