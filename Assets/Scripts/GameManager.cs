using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private ShapeManager[] shapes;
    [SerializeField] GameObject endpannel;

    public static GameManager instance;


    private int currentShapeIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        // Disable all ShapeManagers first
        foreach (var shape in shapes)
        {
            shape.gameObject.SetActive(false);
        }

        // Start with the first shape
        if (shapes.Length > 0)
        {
            ActivateShape(currentShapeIndex);
        }
    }

    private void ActivateShape(int index)
    {
        if (index < shapes.Length)
        {
            shapes[index].gameObject.SetActive(true);
            shapes[index].ShapeCompleted += HandleShapeCompleted;
        }
    }

    private void HandleShapeCompleted()
    {
        // Unsubscribe to avoid memory leak
        shapes[currentShapeIndex].ShapeCompleted -= HandleShapeCompleted;
        shapes[currentShapeIndex].gameObject.SetActive(false);

        currentShapeIndex++;
        if (currentShapeIndex < shapes.Length)
        {
            ActivateShape(currentShapeIndex);
        }
        // Optional: else, all shapes completed
        else
        {
            endpannel.SetActive(true);
            Debug.Log("all shapes completed");
        }
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitgame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
