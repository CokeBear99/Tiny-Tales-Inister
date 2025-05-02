using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        // 새 게임 데이터 초기화 (필요하다면)
        PlayerPrefs.SetInt("IsNewGame", 1);

        // Game Scene 로드
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadGame()
    {
        // 저장된 게임 데이터 확인 (여러 키를 확인)
        bool hasSaveData = BayatGames.SaveGameFree.SaveGame.Exists("PLAYER_POSITION") ||
                            BayatGames.SaveGameFree.SaveGame.Exists("PLAYER_STATS") ||
                            BayatGames.SaveGameFree.SaveGame.Exists("MY_INVENTORY");

        if (hasSaveData)
        {
            PlayerPrefs.SetInt("IsNewGame", 0); // 로드 게임 표시

            // Game Scene 로드
            SceneManager.LoadScene("Game Scene");
            Debug.Log("저장된 게임을 불러옵니다.");
        }
        else
        {
            Debug.Log("저장된 게임이 없습니다!");
            // 여기에 UI로 알림 메시지 표시 코드 추가
        }
    }
}