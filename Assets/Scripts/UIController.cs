using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text _countText;
    public Text _remainingCountText;
    public Text _winText;
    public Text _levelText;

    // Start is called before the first frame update
    void Start()
    {
        if (_countText == null) throw new ArgumentNullException(nameof(_countText));
        if (_winText == null) throw new ArgumentNullException(nameof(_winText));
        if (_remainingCountText == null) throw new ArgumentNullException(nameof(_remainingCountText));
        if (_levelText == null) throw new ArgumentNullException(nameof(_levelText));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string CountText
    {
        get { return _countText.text; }
        set { _countText.text = value; }
    }

    public string RemainingCountText
    {
        get { return _remainingCountText.text; }
        set { _remainingCountText.text = value; }
    }

    public string WinText
    {
        get { return _winText.text; }
        set { _winText.text = value; }
    }

    public string LevelText
    {
        get { return _levelText.text; }
        set { _levelText.text = value; }
    }
}
