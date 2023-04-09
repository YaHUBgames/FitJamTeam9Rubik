using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showCollectibles : MonoBehaviour
{	
	public TextMeshProUGUI text;
	GameInfo info;
    // Start is called before the first frame update
    void Start()
    {		
		info = GameObject.Find("GameInfo").GetComponent<GameInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = info.chips.ToString();
    }
}
