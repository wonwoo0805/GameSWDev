using UnityEngine;

public abstract class Box : MonoBehaviour
{

    public ItemTable itemTable;
    protected int dropCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public abstract void Drop();
}
