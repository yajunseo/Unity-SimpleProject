﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;

    [SerializeField] GameObject go_prefab_FloatingText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    public void CreateFloatingText(Vector3 pos, string _text)
    {
        GameObject clone = Instantiate(go_prefab_FloatingText, pos, go_prefab_FloatingText.transform.rotation);
        clone.GetComponentInChildren<Text>().text = _text;
    }
}
