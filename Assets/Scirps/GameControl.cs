using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameControl : MonoBehaviour
{
    private int currentExp;
    [SerializeField] int currentLevel = 1;
    [SerializeField] private int expToLevelUp = 10;
    [SerializeField] private Player player;
    [SerializeField] private Image energyBar;
    [SerializeField] private TextMeshProUGUI currentLvText;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gamePause;
    void Start()
    {
        currentExp = 0;
        updateEnergyBar();
        UpdateBLvText();
        Debug.Log("ssss");
        MainMenu();
        
    }

    
    void Update()
    {
        
    }


    public void addExp()
    {
        currentExp += 1;
        updateEnergyBar();
        if (currentExp >= expToLevelUp)
        {
           
            if (player != null)
            {
                player.heal(20);
                
                Debug.Log($"Level up! Current Level: {currentLevel + 1}");
            }
            else
            {
                Debug.LogError("Player reference not assigned! Drag Player GameObject to the 'Player' field in Inspector.");
            }
            
            currentExp = 0;
            updateEnergyBar();
            currentLevel++;
            UpdateBLvText();
            expToLevelUp = Mathf.RoundToInt(expToLevelUp * 1.25f);
        }
    }
    private void updateEnergyBar()
    {
        float fillAmount = Mathf.Clamp01((float)currentExp / (float)expToLevelUp);
        energyBar.fillAmount = fillAmount;
    }
    private void UpdateBLvText()
    {
        if (currentLvText != null)
        {
            currentLvText.text = "Lv: " + currentLevel.ToString();
        }
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOver.SetActive(false);
        gamePause.SetActive(false);
        Time.timeScale = 0f;
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        mainMenu.SetActive(false);
        gamePause.SetActive(false);
        Time.timeScale = 0f;
    }
    public void GamePause()
    {
        gamePause.SetActive(true);
        mainMenu.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 0f;
    }
    public void StartGame ()
    {
        gamePause.SetActive(false);
        mainMenu.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ResumeGame()
    {
        gamePause.SetActive(false);
        mainMenu.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
    }
}
