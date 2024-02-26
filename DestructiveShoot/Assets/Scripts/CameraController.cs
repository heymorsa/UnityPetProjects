using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float joystickRotationSpeed = 5f;
    public float enemyFollowRotationSpeed = 2f;

    private List<Transform> enemies;
    private Transform currentTarget;

    void UpdateEnemyList()
    {
        enemies = new List<Transform>(GameObject.FindGameObjectsWithTag("Enemy").Select(enemy => enemy.transform));
    }

    private void Update()
    {
        float horizontalRotation = joystick.Horizontal;
        float verticalRotation = joystick.Vertical;

        RotateCamera(horizontalRotation, verticalRotation);

        UpdateEnemyList();

        if (enemies == null)
        {
            Debug.LogError("Enemies list is null.");
        }
        else
        {
            UpdateCameraTarget();
        }
    }

    

    void UpdateCameraTarget()
    {
        if (enemies.Count == 0)
        {
            currentTarget = null;
            return;
        }
        
        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Transform enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;
        
        if (currentTarget != null)
        {
            Vector3 targetDirection = currentTarget.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyFollowRotationSpeed * Time.deltaTime);
        }
    }

    void RotateCamera(float horizontal, float vertical)
    {
        transform.Rotate(Vector3.up, horizontal * joystickRotationSpeed * Time.deltaTime);
        
        float newRotationX = transform.localEulerAngles.x - vertical * joystickRotationSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0);
    }
    
}