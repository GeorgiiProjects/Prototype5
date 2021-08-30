using UnityEngine;

public class Target : MonoBehaviour
{
    // создаем класс Rigidbody, для последующего доступа к нему в префабах Bad 1, Good 1, Good 2, Good 3.
    private Rigidbody targetRb;
    // оставляем ссылку на класс GameManager для того чтобы его можно было использовать в этом скрипте.
    private GameManager gameManager;
    // минимальная скорость движения префабов Bad 1, Good 1, Good 2, Good 3.
    private float minSpeed = 12;
    // максимальная скорость движения префабов Bad 1, Good 1, Good 2, Good 3.
    private float maxSpeed = 16;
    // сила вращения префабов Bad 1, Good 1, Good 2, Good 3.
    private float maxTorque = 10;
    // Спавн префабов Bad 1, Good 1, Good 2, Good 3 по оси х.
    private float xRange = 4;
    // Спавн префабов Bad 1, Good 1, Good 2, Good 3 по оси y.
    private float ySpawnPos = -2;
    // создаем переменную чтобы определить сколько очков будет давать или отнимать определенный объект/префаб в игре (выставляем значения в префабах).
    public int pointValue;
    // создаем класс ParticleSystem explosionParticle, для того чтобы поместить в инспекторе в него префаб Explosion при уничтожении объекта.
    public ParticleSystem explosionParticle;

    void Start()
    {
        // получаем доступ к классу Rigidbody, чтобы можно было использовать различные силы на объектах/префабах Good и Bad.
        targetRb = GetComponent<Rigidbody>();
        // получаем доступ к Game Manager в иерархии, через класс GameObject.Find и строку "Game Manager", получаем доступ к скрипту GameManager.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // добавляем к Rigidbody объектов/префабов Good и Bad случайную скорость движения между 12 и 16.
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        // добавляем к Rigidbody объектов/префабов Good и Bad силу вращения по осям x,y и z от -10 до 10.
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        // объекты/префабы Good и Bad будут спавниться в координатах по x от -4 до 4, по оси y -2.
        transform.position = RandomSpawnPos();
    }

    // метод нажатия левой кнопки мыши.
    private void OnMouseDown()
    {
        // если игра активна то (из класса/скрипта gameManager вызываем публичное поле isGameActive).
        if (gameManager.isGameActive)
        {
            // уничтожается объект/префаб (Bad 1, Good 1, Good 2, Good 3) на экране.
            Destroy(gameObject);
            // создаем копии эффекта при уничтожении объекта, используя его координаты, поворот оставляем по умолчанию.
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            // после уничтожения объекта/префаба дается или отнимается определенное кол-во очков.
            gameManager.UpdateScore(pointValue);
        }     
    }

    // метод соприкосновения коллайдера игровых объектов с коллайдером сенсор.
    private void OnTriggerEnter(Collider other)
    {
        // уничтожаются все оставшиеся игровые объекты.
        Destroy(gameObject);
        // если объект/префаб не содержит тэг Bad.
        if (!gameObject.CompareTag("Bad"))
        {
            // отображается надпись GameOver при соприкосновении коллайдера префабов Good 1, Good 2, Good 3 с коллайдером объекта сенсор.
            gameManager.GameOver();
        }
    }

    // создаем метод RandomForce(), для того чтобы использовать в нем скорость движения префабов между 12 и 16.
    Vector3 RandomForce()
    {
        // используем Vector3 без new так как вычисляем (умножаем) случайное значение скорости, используем полученные вычисления в игре.
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // создаем метод RandomTorque() для того чтобы использовать в нем силу вращения от -10 до 10 по осям x,y и z.
    float RandomTorque()
    {
        // Используем в игре рандомные вращения по осям x,y,z.
        return Random.Range(-maxTorque, maxTorque);
    }
    // создаем метод RandomSpawnPos(), для того чтобы объекты Good появлялись в рандомных координатах по оси x от -4 до 4, по оси y -2. 
    Vector3 RandomSpawnPos()
    {
        // используем new Vector3 так как используем переменные без умножения, полученные значения используем в игре.
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
