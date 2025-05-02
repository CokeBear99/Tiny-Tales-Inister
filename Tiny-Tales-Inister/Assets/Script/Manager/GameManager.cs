using BayatGames.SaveGameFree;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{

    [Header("Settings")]
    [SerializeField] private Player player;

    public Player Player => player;

    // 저장 키
    private readonly string PLAYER_POSITION_KEY = "PLAYER_POSITION";
    private readonly string PLAYER_STATS_KEY = "PLAYER_STATS";
    private readonly string QUEST_KEY_DATA = "MY_QUESTS";


    protected override void Awake()
    {
        base.Awake();

        SetupGame();
    }

    private void SetupGame()
    {
        // 약간의 지연을 주어 다른 매니저들이 초기화될 시간을 줌
        StartCoroutine(SetupGameDelayed());
    }

    private IEnumerator SetupGameDelayed()
    {
        // 한 프레임 기다림
        yield return null;

        // 새 게임인지 로드 게임인지 확인
        int isNewGame = PlayerPrefs.GetInt("IsNewGame", 1);

        if (isNewGame == 0)
        {
            // 저장된 게임 데이터 로드
            LoadAllGame();
        }
        else
        {
            // 새 게임 초기화
            ResetGame();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            player.RespawnPlayer();
    }



    private void ResetGame()
    {
        try
        {
            // 플레이어 스탯 초기화
            if (player != null && player.Stats != null)
            {
                player.Stats.ResetPlayer();
            }

            // 플레이어 초기 위치 설정
            if (player != null)
            {
                player.transform.position = new Vector3(0, 0, 0); // 시작 위치로 변경
            }

            // 인벤토리 초기화
            if (Inventory.Instance != null && Inventory_UI.Instance != null)
            {
                for (int i = 0; i < Inventory.Instance.InventorySize; i++)
                {
                    Inventory.Instance.Inventory_Items[i] = null;
                    Inventory_UI.Instance.DrawItem(null, i);
                }
            }

            // 퀘스트 초기화
            if (QuestManager.Instance != null && QuestManager.Instance.Quests != null)
            {
                foreach (Quest quest in QuestManager.Instance.Quests)
                {
                    if (quest != null)
                    {
                        quest.ResetQuest();
                    }
                }

                // 퀘스트 패널 비우기
                QuestManager.Instance.ClearNPCQuestPanel();
            }

            // 골드 초기화
            if (GoldManager.Instance != null)
            {
                GoldManager.Instance.SpendGolds((GoldManager.Instance.Golds));
            }

            // 카메라 위치 초기화
            if (Camera.main != null && player != null)
            {
                Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
            }

            // 저장 데이터 삭제
            DeleteSaveData();

            // 게임 시간 초기화 
            Time.timeScale = 1f;

            Debug.Log("게임 상태가 초기화되었습니다.");
        }
        catch (Exception e)
        {
            Debug.LogError("게임 초기화 중 오류 발생: " + e.Message);
        }
    }


    public void AddPlayerExp(float expAmount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(expAmount);
    }

    // 게임 데이터 저장 함수
    public void SaveAllGame()
    {
        player.Stats.SavePlayerStats();   // PlayerStats에 구현된 저장 함수 호출
        QuestManager.Instance.SaveQuests(); // QuestManager에 구현된 저장 함수 호출
        Inventory.Instance.SaveInventory(); // Inventory에 구현된 저장 함수 호출
        GoldManager.Instance.SaveGold(); // 추가
        SavePlayerPosition();

        Debug.Log("게임 데이터가 저장되었습니다.");
    }

    // 게임 데이터 불러오기 함수
    public void LoadAllGame()
    {
        player.Stats.LoadPlayerStats();   // PlayerStats에 구현된 로드 함수 호출
        QuestManager.Instance.LoadQuests(); // QuestManager에 구현된 로드 함수 호출
        Inventory.Instance.LoadInventory(); // Inventory에 구현된 로드 함수 호출
        GoldManager.Instance.LoadGold(); // 추가
        LoadPlayerPosition();

        Debug.Log("게임 데이터를 불러왔습니다.");
    }


    // 플레이어 위치 저장
    private void SavePlayerPosition()
    {
        Vector3 position = player.transform.position;
        string positionData = $"{position.x}|{position.y}|{position.z}";
        SaveGame.Save(PLAYER_POSITION_KEY, positionData);
    }

    // 플레이어 위치 로드
    private void LoadPlayerPosition()
    {
        if (SaveGame.Exists(PLAYER_POSITION_KEY))
        {
            try
            {
                string positionData = SaveGame.Load<string>(PLAYER_POSITION_KEY);
                string[] positions = positionData.Split('|');

                if (positions.Length >= 3)
                {
                    float x = float.Parse(positions[0]);
                    float y = float.Parse(positions[1]);
                    float z = float.Parse(positions[2]);

                    player.transform.position = new Vector3(x, y, z);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("플레이어 위치 로드 중 오류 발생: " + e.Message);
            }
        }
    }

    // 저장 데이터 삭제 함수
    private void DeleteSaveData()
    {
        // 필요한 경우 기존 저장 데이터 삭제
        if (BayatGames.SaveGameFree.SaveGame.Exists(PLAYER_POSITION_KEY))
            BayatGames.SaveGameFree.SaveGame.Delete(PLAYER_POSITION_KEY);

        if (BayatGames.SaveGameFree.SaveGame.Exists(PLAYER_STATS_KEY))
            BayatGames.SaveGameFree.SaveGame.Delete(PLAYER_STATS_KEY);

        if (BayatGames.SaveGameFree.SaveGame.Exists(QUEST_KEY_DATA))
            BayatGames.SaveGameFree.SaveGame.Delete(QUEST_KEY_DATA);

        if (BayatGames.SaveGameFree.SaveGame.Exists("GOLD_KEY"))
            BayatGames.SaveGameFree.SaveGame.Delete("GOLD_KEY");

        if (BayatGames.SaveGameFree.SaveGame.Exists("MY_INVENTORY"))
            BayatGames.SaveGameFree.SaveGame.Delete("MY_INVENTORY");
    }
}


