using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] FootholdSpawn platformSpawner;
    [SerializeField] GameController gameController;

    [SerializeField] Transform playerObject;
    [SerializeField] float xSensitivity = 10.0f;
    [SerializeField] float moveTime = 1.0f;
    [SerializeField] float minPositionY = 0.55f;
    private float gravity = -9.81f;
    private int incrementalscore = 0;

    private RaycastHit hit;

    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameController.GameStart();

                StartCoroutine("MoveLoop");
                StartCoroutine("DecreaseMoveTime");
                break;
            }

            yield return null;
        }
    }

    private IEnumerator MoveLoop()
    {
        while (true)
        {
            incrementalscore++;

            float current = (incrementalscore - 1) * platformSpawner.ZLength;
            float next = incrementalscore * platformSpawner.ZLength;

            yield return StartCoroutine(MoveYZ(current, next));

            if (hit.transform != null)
            {
                gameController.IncreasingScore();
            }
            else
            {
                gameController.GameOver();
                break;
            }
        }
    }

    private void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f);

        if (Input.GetMouseButton(0))
        {
            MoveX();
        }
    }

    private void MoveX()
    {
        float x = 0.0f;
        Vector3 position = transform.position;

        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                x = (touch.position.x / Screen.width) - 0.5f;
            }
        }
        else
        {
            x = (Input.mousePosition.x / Screen.width) - 0.5f;
        }

        x = Mathf.Clamp(x, -0.5f, 0.5f);
        position.x = Mathf.Lerp(position.x, x * xSensitivity, xSensitivity * Time.deltaTime);
        transform.position = position;
    }

    private IEnumerator MoveYZ(float start, float end)
    {
        float current = 0;
        float percent = 0;
        float v0 = -gravity;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            float y = minPositionY + (v0 * percent) + (gravity * percent * percent);
            playerObject.position = new Vector3(playerObject.position.x, y, playerObject.position.z);

            float z = Mathf.Lerp(start, end, percent);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);

            yield return null;
        }
    }

    private IEnumerator DecreaseMoveTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            moveTime -= 0.02f;

            if (moveTime <= 0.2f)
            {
                break;
            }
        }
    }
}
