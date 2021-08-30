using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // создаем класс Button для получения доступа к кнопкам уровней сложности.
    private Button button;
    // создаем класс GameManager для того чтобы получить доступ к скрипту GameManager.
    private GameManager gameManager;
    // создаем переменную для выбора уровня сложности.
    public int difficulty;

    void Start()
    {
        // ищем объект Game Manager в иерархии, получаем доступ к скрипту GameManager.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // получаем доступ к Button в Easy Button, Medium Button и Hard Button через компонент Button GetComponent
        button = GetComponent<Button>();
        // при нажатии кнопки мыши на кнопку выбора уровня сложности вызывается метод SetDifficulty, который позволяет выбрать нужный уровень сложности.
        button.onClick.AddListener(SetDifficulty);
    }

    // создаем метод для выбора уровня сложности
    void SetDifficulty()
    {
        // запускам игру с нужным уровнем сложности, для этого вызываем метод выбора уровня сложности из скрипта GameManager.
        gameManager.StartGame(difficulty);
    }
}
