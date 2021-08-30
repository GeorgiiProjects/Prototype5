using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // создаем класс лист для того чтобы поместить в инспекторе в него префабы Good и Bad.
    public List<GameObject> targets;
    // создаем класс TextMeshProUGUI для того чтобы поместить в инспекторе в него объект Score Text.
    public TextMeshProUGUI scoreText;
    // создаем класс TextMeshProUGUI для того чтобы поместить в инспекторе в него объект GameOver Text.
    public TextMeshProUGUI gameOverText;
    // создаем класс Button для того чтобы поместить в инспекторе в него кнопку Restart Button.
    public Button restartButton;
    // создаем класс GameObject titleScreen, для того чтобы поместить в инспекторе в него объект Title Screen.
    public GameObject titleScreen;
    // создаем переменную, чтобы поместить в нее счет в игре.
    private int score;
    // создаем переменную для того чтобы объекты/префабы спавнились через 1 секунду.
    private float spawnRate = 1.0f;
    // создаем переменную для того чтобы определить окончена игра или нет.
    public bool isGameActive;

    // создаем метод StartGame, чтобы игра могла начаться, передаем параметр difficulty, чтобы выбрать уровень сложности при старте игры.
    // делаем метод публичным так как понадобится его вызывать в скрипте DifficultyButton для выбора уровня сложности.
    public void StartGame(int difficulty)
    {
        // игра активна при старте, создаем данное поле первым в методе так как корутина ни всегда понимает очередность запуска в методе.
        isGameActive = true;
        // счет при старте игры 0, создаем данное поле до корутины в методе так как корутина ни всегда понимает очередность запуска в методе.
        score = 0;
        // быстроту спавна объектов 1 делим на уровень сложности 1,2,3 в зависимости от выбора уровня сложности, скорость спавна объектов будет меняться.
        spawnRate /= difficulty;
        // запускаем курутину, таймер спавна объектов 1 секунда и идет спавн.
        StartCoroutine(SpawnTarget());
        // счет при старте игры 0 будет обновляться.
        UpdateScore(0);
        // Пока игра работает титульный экран отключен.
        titleScreen.gameObject.SetActive(false);
    }

    // создаем курутину/интерфейс SpawnTarget(), для того чтобы контролировать скорость спавна объектов.
    IEnumerator SpawnTarget()
    {
        // пока эти условия соблюдаются в цикле while и игра не окончена
        while (isGameActive)
        {
            // скорость спавна каждую 1 секунду.
            yield return new WaitForSeconds (spawnRate);
            // выбираем рандомный объект из 4 для появления на экране, используем targets.Count так как они берутся из листа.
            int index = Random.Range(0, targets.Count);
            // создаем копии префабов good и bad через массив для появления на экране.
            Instantiate(targets[index]);    
        }
    }

    // создаем метод для обновления счета в игре при уничтожении префабов Good и Bad, используем параметр scoreToAdd  
    // для того чтобы счет прибавлялся или убавлялся, а не просто показывал начальный счет 0.
    // делаем метод публичным так как понадобится его вызывать в скрипте Target для обновления счета.
    public void UpdateScore(int scoreToAdd)
    {
        // счет во время игры обновляется на 5, 10, 15 при уничтожении префабов Good и Bad.
        score += scoreToAdd;
        // инициализируем текст, который будет появляться на экране в игре.
        scoreText.text = "Score: " + score;
    }

    // создаем метод GameOver() для отображения Game Over по окончанию игры.
    // делаем метод публичным так как понадобится его вызывать в скрипте Target.
    public void GameOver()
    {
        // Restart отображается по окончанию игры, так как кнопка содержащая текст Restart становится активной.
        restartButton.gameObject.SetActive(true);
        // Game Over отображается по окончанию игры, так как объект содержащий текст GameOver Text становится активным.
        gameOverText.gameObject.SetActive(true);
        // игра окончена.
        isGameActive = false;
    }
    
    // Создаем метод RestartGame() для перезапуска игры по нажатию кнопки Restart в игре.
    private void RestartGame()
    {
        // Перезагружаем текущий уровень, для этого используем класс SceneManager, который понимает какая сцена используется GetActiveScene(), 
        // исходя из ее имени .name
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }  
}
