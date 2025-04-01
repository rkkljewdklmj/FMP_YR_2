using UnityEngine;


    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public int startingMoney = 100;

       private MoneySystem moneySystem; // Reference to the MoneySystem

    private int currentMoney;
        //public Transform mygameManager;

        private void Awake()
        {
            // Singleton Pattern to access GameManager easily
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            currentMoney = startingMoney;

        }

            public bool AddMoney(int amount)
            {
                    moneySystem.SubtractMoney(amount);
                    return false;
            }


        public bool SpendMoney(int amount)
        {
            if (currentMoney >= amount)
            {
                currentMoney -= amount;
                UpdateMoneyUI();
                return true;
            }
            return false;
        }

        private void UpdateMoneyUI()
        {
            Debug.Log("Money: " + currentMoney);
            // Add code here to update a UI Text element

        }

        public int GetMoney()
        {

            moneySystem.GetCurrentMoney();


        return currentMoney;
        }
    }
