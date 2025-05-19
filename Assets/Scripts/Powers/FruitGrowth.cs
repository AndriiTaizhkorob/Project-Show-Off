using UnityEngine;
using UnityEngine.InputSystem;

public class FruitGrowth : MonoBehaviour
{
    public InputActionReference shoot;

    [SerializeField]
    private GameObject fruitPrefab;
    [SerializeField]
    private GameObject fruitSpawn;
    [SerializeField]
    private int fruitsLimit;
    [SerializeField]
    private float fruitPower;
    [SerializeField]
    private GameObject[] fruits;


    private int x = 0;
    private GameObject currentFruit;
    void Start()
    {
        fruits = new GameObject[fruitsLimit];
    }

    void Update()
    {
        if (shoot.action.triggered && fruitPrefab != null)
        {
            
            if (fruits.Length == 5)
                Destroy(fruits[x]);

            currentFruit = Instantiate(fruitPrefab, fruitSpawn.transform.position, Quaternion.identity);
            fruits[x] = currentFruit;

            if (x >= fruitsLimit - 1)
                x = 0;
            else
                x += 1;

            currentFruit.GetComponent<Rigidbody>().linearVelocity = fruitPower * transform.forward;
        }
    }
}
