using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text _tutorialText;
    [SerializeField] private string[] _texts;
    private int _counter = 0;

    public void ChangeText()
    {
        _tutorialText.text = _texts[_counter];
        _counter++;

        if (_counter == _texts.Length)
        {
            Time.timeScale = 1.0f;
            this.gameObject.SetActive(false);
        }
    }
}
