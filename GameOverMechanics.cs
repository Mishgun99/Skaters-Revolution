using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMechanics : MonoBehaviour
{
public MSSceneControllerFree me;
public string sceneName;
public RespawnMechanics player;
public WheelJump player2;
public bool AbortedMission;


private GameSavingDataContainer dataToSave = new GameSavingDataContainer();
private GameSavingDataContainer dataToLoad = new GameSavingDataContainer();
public MyGameSaveManager myGameSaveManager;  

    // Start is called before the first frame update

    public void SaveGameData() //загрузка данных
    {
	myGameSaveManager.LoadSettings(out dataToLoad);
	dataToSave = dataToLoad;
	dataToSave.money = player2.money;
	myGameSaveManager.SaveSettings(dataToSave);
    }
    void Awake()
	{
	if (AbortedMission == false && player2.money > 0)
		player2.money = player2.money - 100 - (player2.money / 10);
	if (player2.money < 0)
		player2.money = 0;

	//будет также падение влияния, но его механику ещё предстоит расписать
	SaveGameData();
	}
    public void Respawn()
	{
SceneManager.LoadScene (sceneName);		
	}
    public void AbortMission()
	{
player.checkpointed = false;	
SceneManager.LoadScene (sceneName);
	}
    public void RespawnBack()
	{
player.checkpointed = false;
player.n = 0;	
SceneManager.LoadScene (sceneName);
	}
    public void ToMainMenu()
	{
SceneManager.LoadScene("MainMenu");
	}
}
