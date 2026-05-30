using UnityEngine;

public class EnemyEventHandler : MonoBehaviour
{
    public Enemy_St1 enemy;

    public void OnAttackFinished()
    {
        if (enemy != null)
        {
            enemy.Attack();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
