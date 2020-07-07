using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Up to 2:10:00 (part 12, Indicator[LineRenderer]
public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdLaunced;
    private float _timeStationary;

    [SerializeField] private float _launchPower = 100;


    private void Awake() {
        _initialPosition = transform.position;
    }

    private void Update() {

        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);


        if (_birdLaunced &&
            GetComponent<Rigidbody2D>().velocity.magnitude < 0.1) {
            _timeStationary += Time.deltaTime;
        }

        if (transform.position.y > 10 ||
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x < -20 ||
            _timeStationary > 2) {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }


        if (Input.GetMouseButton(0)) {
            Vector3 lookVector = _initialPosition - transform.position;
            transform.right = new Vector3(lookVector.x, lookVector.y);
        } else if (GetComponent<Rigidbody2D>().velocity.magnitude > 0.5) {
            Vector3 lookVector = GetComponent<Rigidbody2D>().velocity;
            transform.right = new Vector3(lookVector.x, lookVector.y);
        }

    }

    private void OnMouseDown() {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;

    }

    private void OnMouseUp() {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdLaunced = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag() {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
