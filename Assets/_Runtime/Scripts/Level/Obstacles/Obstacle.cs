using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private DecorationSpawner[] _decorationSpawners;

    private List<ObstacleDecoration> _obstacleDecorations = new List<ObstacleDecoration>();

    private ObstacleDecoration FindDecorationForCollider(Collider collider)
    {
        float minDistX = Mathf.Infinity;
        ObstacleDecoration minDistDecoration = null;

        foreach (var decoration in _obstacleDecorations)
        {
            float decorationPosX = decoration.transform.position.x;
            float colliderPosX = collider.bounds.center.x;
            float distX = Mathf.Abs(decorationPosX - colliderPosX);
            if (distX < minDistX)
            {
                minDistX = distX;
                minDistDecoration = decoration;
            }
        }

        return minDistDecoration;
    }

    public void SpawnDecorations()
    {
        foreach (var decorationSpawner in _decorationSpawners)
        {
            decorationSpawner.SpawnDecorations();
            var obstacleDecoration = decorationSpawner.CurrentDecoration.GetComponent<ObstacleDecoration>();
            if (obstacleDecoration != null)
            {
                _obstacleDecorations.Add(obstacleDecoration);
            }
        }
    }

    public void PlayCollisionFeedback(Collider collider)
    {
        ObstacleDecoration decorationHit = FindDecorationForCollider(collider);
        decorationHit?.PlayCollisionFeedback();
    }
}
