using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public LinkedList<EnemyController> enemies;

    public LinkedListNode<EnemyController> selectedEnemy;

    private void Start()
    {
        enemies = new LinkedList<EnemyController>(FindObjectsOfType<EnemyController>());
        if (enemies.Count > 0)
        {
            selectedEnemy = enemies.First;
        }
    }
    public EnemyController GetNextEnemy()
    {
        this.selectedEnemy.Value.SetAsSelected(false);
        if (enemies.Last == selectedEnemy)
        {
            this.selectedEnemy= enemies.First;
        }
        else {
            this.selectedEnemy= this.selectedEnemy.Next;
        }
        this.selectedEnemy.Value.SetAsSelected(true);
        return this.selectedEnemy.Value;
    }
}
