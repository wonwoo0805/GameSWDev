using UnityEngine;

public class Chest : Box
{
    //임시코드
    private int[] item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public override void Drop()
    {
        if(itemTable != null)
        {
            ItemData[] droppedItems = new ItemData[dropCount];

            for(int i = 0; i < dropCount; i++)
            {
                droppedItems[i] = itemTable.GetRandomItem();

                if(droppedItems[i].itemPrefab != null)
                {
                    Vector3 spawnPos = transform.position + Random.insideUnitSphere * 0.5f;
                    Instantiate(droppedItems[i].itemPrefab,spawnPos,Quaternion.identity);

                }
                Debug.Log($"{droppedItems[i].name} 드랍");
            }
        }

        

    }
}
