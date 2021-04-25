using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI DisplayText;

    private float _ticker =  0f;
    private float _lifetime = 5f;
    private float _floatSpeed = 1f;
    private float _wiggleSpeed = 4f;
    private float _wiggleMagnitude = .04f;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Initialise(string text, Vector3 position, float lifetime)
    {
        _lifetime = lifetime;
        DisplayText.text = text;
        transform.position = position;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        _ticker += Time.deltaTime;

        if (_ticker > _lifetime)
        {
            _ticker = _lifetime;
            Destroy(gameObject);
        }

        DisplayText.alpha = Mathf.Lerp(1, 0, _ticker / _lifetime);
    }

    private void FixedUpdate()
    {
        var offset = Mathf.Cos(_ticker * _wiggleSpeed) * _wiggleMagnitude;
        transform.position += Vector3.up * _floatSpeed * Time.deltaTime + new Vector3(offset, 0, 0);
    }
}
