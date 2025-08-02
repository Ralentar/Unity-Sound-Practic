using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private FootStepsSounds _stepsSounds;
    [SerializeField] private float _stepDistance = 0.5f;

    private float _coveredDistance = 0f;


    private void Update()
    {
        Rotate();
        Move();
    }
    private void Rotate()
    {
        float rotation = Input.GetAxis("Horizontal");
        transform.Rotate(_rotationSpeed * rotation * Time.deltaTime * Vector3.up);
    }

    private void Move()
    {
        float direction = Input.GetAxis("Vertical");

        if (direction == 0)
        {
            _coveredDistance = 0f;
            _stepsSounds.Pause();
            return;
        }

        float distance = _moveSpeed * direction * Time.deltaTime;
        transform.Translate(distance * Vector3.forward);
        _coveredDistance += Mathf.Abs(distance);

        if (_coveredDistance >= _stepDistance)
            _coveredDistance -= _stepDistance;
        
        _stepsSounds.Play();
    }
}