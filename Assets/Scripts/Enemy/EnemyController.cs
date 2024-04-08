using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float _minMovingDuration;
    [SerializeField]
    private float _maxMovingDuration;

    [SerializeField] private ParticleSystem _deathParticlesPrefab;

    private float _delayBetweenMovements;
    private SpriteRenderer _enemySprite;
    private float _minPointX;
    private float _maxPointX;
    private Sequence _moveSequence;


    public void Initialize(float minPointX, float maxPointX, float delayBetweenMovements)
    {
        _enemySprite = GetComponent<SpriteRenderer>();
        var offsetX = _enemySprite.bounds.size.x / 2;

        _minPointX = minPointX + offsetX;
        _maxPointX = maxPointX - offsetX;
        _delayBetweenMovements = delayBetweenMovements;

        Move();
    }

    public void Destroy()
    {

        Instantiate(_deathParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Move()
    {
        var randomMovementDuration = GetRandomMovementDuration();
        var nextPosition = GetNextRandomPositionX();

        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(nextPosition, randomMovementDuration));
        _moveSequence.AppendInterval(_delayBetweenMovements);
        _moveSequence.OnComplete(Move);
    }

    private float GetNextRandomPositionX()
    {
        return Random.Range(_minPointX, _maxPointX);
    }

    private float GetRandomMovementDuration()
    {
        return Random.Range(_minMovingDuration, _maxMovingDuration);
    }

    private void OnDestroy()
    {
        _moveSequence.Kill();
    }
}